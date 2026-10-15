/* ============================================================================
   BITÁCORA DE CAMBIOS DE RESERVAS                                  (reemplaza a la
   Horizon Hotel & Resort (TP_Ing_Soft)                    bitácora de cambios de habitación)
   ----------------------------------------------------------------------------
   Patrón de bitácora de cambios de la cátedra aplicado a la tabla Reservas:
   - Reservas sigue siendo la tabla con la información ACTIVA.
   - Bitacora_Reservas guarda una FOTO COMPLETA de la reserva (todas sus columnas) cada vez
     que se da de alta o que cambia cualquier dato: modificación de fechas o huéspedes,
     cancelación, no-show, check-in, check-out, renovación, upgrade o reubicación, y
     también los cambios hechos directamente en la base.
   - Cada registro dice qué operación fue, la fecha, la hora, el usuario del sistema
     (y el login de SQL Server) y Act = 1 si es el estado vigente de la reserva.
     El índice filtrado garantiza un solo registro activo por reserva.
   - La escribe SOLO el trigger de Reservas. No se borra ningún registro y nada se
     modifica a mano (Act solo cambia cuando el trigger agrega el registro nuevo).

   Cómo sabe el trigger qué operación fue y quién la hizo:
   - El sistema informa por SESSION_CONTEXT el usuario ('UsuarioApp') en cada operación
     y, cuando hace falta, la operación ('OperacionReserva', por ejemplo 'No-show').
   - Si no se informa la operación, el trigger la deduce comparando la fila anterior
     con la nueva (Estado, CheckIn/CheckOut, HabitacionID, fechas...).
   - Un cambio de habitación hecho por sp_RegistrarCambioHabitacion informa el CambioID,
     así se sabe si fue Upgrade o Cambio administrativo y se enlaza con su historial.

   Qué hace este script (se puede ejecutar más de una vez):
   1. Quita la bitácora anterior (Bitacora_CambiosHabitacion y sus triggers). El detalle
      de los upgrades y reubicaciones se conserva en HistorialCambiosHabitacion.
   2. Crea Bitacora_Reservas, su trigger en Reservas y los triggers que la protegen.
   3. Carga un registro inicial ("Carga inicial", activo) por cada reserva existente y
      actualiza sp_ReporteServiciosConsumidos, que usaba la bitácora anterior.
   4. Renombra el permiso de la pantalla (VerBitacoraCambiosHabitacion -> VerBitacoraReservas).
   5. Actualiza sp_GetAllTablesParaDV (dígitos verificadores) con la tabla nueva.

   Requiere SQL Server 2016 SP1 o superior (CREATE OR ALTER, SESSION_CONTEXT).
   AL TERMINAR: entrar con un SuperAdmin y RECALCULAR los dígitos verificadores.
   ============================================================================ */
USE [TP_Ing_Soft];
GO
SET NOCOUNT ON;
GO

/* ============================================================================
   1. QUITAR LA BITÁCORA ANTERIOR (solo registraba cambios de habitación)
   ============================================================================ */
IF OBJECT_ID(N'dbo.TR_Reservas_BitacoraHabitacion_Alta', N'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Reservas_BitacoraHabitacion_Alta;
GO
IF OBJECT_ID(N'dbo.TR_Reservas_BitacoraHabitacion_Cambio', N'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Reservas_BitacoraHabitacion_Cambio;
GO
-- Sus triggers de protección (NoBorrar / SoloLectura) se eliminan junto con la tabla
IF OBJECT_ID(N'dbo.Bitacora_CambiosHabitacion', N'U') IS NOT NULL
    DROP TABLE dbo.Bitacora_CambiosHabitacion;
GO
DELETE FROM dbo.DV WHERE Tabla = N'Bitacora_CambiosHabitacion';
GO

/* ============================================================================
   2. TABLA Bitacora_Reservas
   ============================================================================ */
IF OBJECT_ID(N'dbo.Bitacora_Reservas', N'U') IS NULL
CREATE TABLE dbo.Bitacora_Reservas
(
    BitacoraID         INT IDENTITY(1,1) NOT NULL,

    -- Foto de la reserva: las mismas columnas que dbo.Reservas
    ReservaID          INT               NOT NULL,
    HuespedTitularID   INT               NOT NULL,
    HabitacionID       INT               NOT NULL,
    FechaIngreso       DATE              NOT NULL,
    FechaEgreso        DATE              NOT NULL,
    CheckIn            DATETIME          NULL,
    CheckOut           DATETIME          NULL,
    Estado             INT               NOT NULL,   -- 0 Confirmada, 1 En curso, 2 Finalizada, 3 Cancelada
    CantidadAdultos    INT               NOT NULL,
    CantidadNinos      INT               NOT NULL,
    TarifaNoche        DECIMAL(10,2)     NOT NULL,
    MontoTotal         DECIMAL(10,2)     NOT NULL,
    UsuarioRegistroDNI INT               NULL,
    FechaRegistro      DATETIME          NOT NULL,
    MontoOriginal      DECIMAL(10,2)     NOT NULL,

    -- Datos del cambio
    Operacion          NVARCHAR(30)      NOT NULL,
    CambioID           INT               NULL,       -- upgrade / reubicación que lo originó (HistorialCambiosHabitacion)
    Fecha              DATE              NOT NULL,
    Hora               TIME(0)           NOT NULL,
    UsuarioDB          NVARCHAR(128)     NOT NULL CONSTRAINT DF_BitacoraReservas_UsuarioDB DEFAULT (SUSER_SNAME()),
    UsuarioApp         NVARCHAR(50)      NULL,       -- usuario del sistema (NULL = cambio hecho fuera del sistema)
    Act                BIT               NOT NULL,

    CONSTRAINT PK_Bitacora_Reservas PRIMARY KEY CLUSTERED (BitacoraID),
    CONSTRAINT FK_BitacoraReservas_Reservas FOREIGN KEY (ReservaID) REFERENCES dbo.Reservas (ReservaID),
    CONSTRAINT FK_BitacoraReservas_Cambio   FOREIGN KEY (CambioID)  REFERENCES dbo.HistorialCambiosHabitacion (CambioID),
    CONSTRAINT CK_BitacoraReservas_Operacion CHECK (Operacion IN (
        N'Carga inicial', N'Alta', N'Modificación', N'Cancelación', N'No-show', N'Check-in', N'Check-out',
        N'Renovación', N'Upgrade', N'Cambio administrativo', N'Cambio de habitación'))
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_BitacoraReservas_Activo')
    CREATE UNIQUE NONCLUSTERED INDEX UX_BitacoraReservas_Activo
        ON dbo.Bitacora_Reservas (ReservaID) WHERE Act = 1;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_BitacoraReservas_Reserva')
    CREATE NONCLUSTERED INDEX IX_BitacoraReservas_Reserva
        ON dbo.Bitacora_Reservas (ReservaID, BitacoraID);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_BitacoraReservas_Fecha')
    CREATE NONCLUSTERED INDEX IX_BitacoraReservas_Fecha
        ON dbo.Bitacora_Reservas (Fecha, ReservaID);
GO

/* ============================================================================
   3. TRIGGER DE Reservas: cada alta o cambio deja un registro nuevo y activo
   ============================================================================ */
CREATE OR ALTER TRIGGER dbo.TR_Reservas_Bitacora
ON dbo.Reservas
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM inserted) RETURN;

    DECLARE @Ahora      DATETIME     = GETDATE();
    DECLARE @UsuarioApp NVARCHAR(50) = CAST(SESSION_CONTEXT(N'UsuarioApp') AS NVARCHAR(50));
    DECLARE @CambioID   INT          = CAST(SESSION_CONTEXT(N'CambioHabitacionID') AS INT);
    DECLARE @Informada  NVARCHAR(30) = CAST(SESSION_CONTEXT(N'OperacionReserva') AS NVARCHAR(30));

    -- Solo se acepta una operación informada que exista en la bitácora (si no, se deduce)
    IF @Informada NOT IN (N'Alta', N'Modificación', N'Cancelación', N'No-show', N'Check-in', N'Check-out', N'Renovación')
        SET @Informada = NULL;

    DECLARE @Cambios TABLE
    (
        ReservaID INT PRIMARY KEY,
        Operacion NVARCHAR(30) NOT NULL,
        CambioID  INT NULL
    );

    INSERT INTO @Cambios (ReservaID, Operacion, CambioID)
    SELECT i.ReservaID,
           CASE
               WHEN d.ReservaID IS NULL THEN N'Alta'
               WHEN i.HabitacionID <> d.HabitacionID THEN
                    CASE WHEN hc.CambioID IS NULL THEN N'Cambio de habitación'
                         WHEN hc.Motivo = 0       THEN N'Upgrade'
                         ELSE N'Cambio administrativo' END
               WHEN @Informada IS NOT NULL THEN @Informada
               WHEN d.Estado <> 3 AND i.Estado = 3 THEN N'Cancelación'
               WHEN d.Estado <> 1 AND i.Estado = 1 THEN N'Check-in'
               WHEN d.Estado <> 2 AND i.Estado = 2 THEN N'Check-out'
               WHEN d.Estado = 1  AND i.FechaEgreso <> d.FechaEgreso THEN N'Renovación'
               ELSE N'Modificación'
           END,
           CASE WHEN d.ReservaID IS NOT NULL AND i.HabitacionID <> d.HabitacionID THEN hc.CambioID END
    FROM inserted i
    LEFT JOIN deleted d ON d.ReservaID = i.ReservaID
    LEFT JOIN dbo.HistorialCambiosHabitacion hc ON hc.CambioID = @CambioID
                                               AND hc.ReservaID = i.ReservaID
                                               AND hc.HabitacionNuevaID = i.HabitacionID
    -- Un UPDATE que no cambia ningún valor no deja registro
    WHERE d.ReservaID IS NULL
       OR EXISTS (SELECT i.HuespedTitularID, i.HabitacionID, i.FechaIngreso, i.FechaEgreso, i.CheckIn, i.CheckOut,
                         i.Estado, i.CantidadAdultos, i.CantidadNinos, i.TarifaNoche, i.MontoTotal,
                         i.UsuarioRegistroDNI, i.FechaRegistro, i.MontoOriginal
                  EXCEPT
                  SELECT d.HuespedTitularID, d.HabitacionID, d.FechaIngreso, d.FechaEgreso, d.CheckIn, d.CheckOut,
                         d.Estado, d.CantidadAdultos, d.CantidadNinos, d.TarifaNoche, d.MontoTotal,
                         d.UsuarioRegistroDNI, d.FechaRegistro, d.MontoOriginal);

    IF NOT EXISTS (SELECT 1 FROM @Cambios) RETURN;

    -- El registro que estaba vigente pasa a ser historia
    UPDATE b SET Act = 0
    FROM dbo.Bitacora_Reservas b
    INNER JOIN @Cambios c ON c.ReservaID = b.ReservaID
    WHERE b.Act = 1;

    INSERT INTO dbo.Bitacora_Reservas
        (ReservaID, HuespedTitularID, HabitacionID, FechaIngreso, FechaEgreso, CheckIn, CheckOut, Estado,
         CantidadAdultos, CantidadNinos, TarifaNoche, MontoTotal, UsuarioRegistroDNI, FechaRegistro, MontoOriginal,
         Operacion, CambioID, Fecha, Hora, UsuarioDB, UsuarioApp, Act)
    SELECT i.ReservaID, i.HuespedTitularID, i.HabitacionID, i.FechaIngreso, i.FechaEgreso, i.CheckIn, i.CheckOut, i.Estado,
           i.CantidadAdultos, i.CantidadNinos, i.TarifaNoche, i.MontoTotal, i.UsuarioRegistroDNI, i.FechaRegistro, i.MontoOriginal,
           c.Operacion, c.CambioID, CAST(@Ahora AS DATE), CAST(DATEADD(MILLISECOND, -DATEPART(MILLISECOND, @Ahora), @Ahora) AS TIME(0)), SUSER_SNAME(),
           -- En el alta, si el sistema no informó el usuario, se toma el que registró la reserva
           COALESCE(@UsuarioApp, CASE WHEN c.Operacion = N'Alta' THEN u.Username END),
           1
    FROM inserted i
    INNER JOIN @Cambios c ON c.ReservaID = i.ReservaID
    LEFT  JOIN dbo.Users u ON u.DNI = i.UsuarioRegistroDNI;
END
GO

/* ============================================================================
   4. PROTECCIÓN DE LA BITÁCORA
   ============================================================================ */
-- No se elimina ningún registro
CREATE OR ALTER TRIGGER dbo.TR_BitacoraReservas_NoBorrar
ON dbo.Bitacora_Reservas
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    THROW 50101, N'La bitácora de reservas no admite bajas: sus registros los escribe el trigger de Reservas.', 1;
END
GO

-- Ningún registro se modifica a mano. La única actualización permitida es la del trigger de Reservas
-- al pasar el registro anterior a Act = 0: tiene que venir de ese trigger y tocar solo Act.
CREATE OR ALTER TRIGGER dbo.TR_BitacoraReservas_SoloLectura
ON dbo.Bitacora_Reservas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL(OBJECT_ID(N'dbo.TR_Reservas_Bitacora'), 'AFTER', 'DML') = 0
       OR UPDATE(ReservaID) OR UPDATE(HuespedTitularID) OR UPDATE(HabitacionID) OR UPDATE(FechaIngreso)
       OR UPDATE(FechaEgreso) OR UPDATE(CheckIn) OR UPDATE(CheckOut) OR UPDATE(Estado)
       OR UPDATE(CantidadAdultos) OR UPDATE(CantidadNinos) OR UPDATE(TarifaNoche) OR UPDATE(MontoTotal)
       OR UPDATE(UsuarioRegistroDNI) OR UPDATE(FechaRegistro) OR UPDATE(MontoOriginal)
       OR UPDATE(Operacion) OR UPDATE(CambioID) OR UPDATE(Fecha) OR UPDATE(Hora)
       OR UPDATE(UsuarioDB) OR UPDATE(UsuarioApp)
        THROW 50102, N'La bitácora de reservas es de solo lectura: sus registros los escribe el trigger de Reservas.', 1;
END
GO

/* ============================================================================
   5. CARGA INICIAL: un registro activo con el estado actual de cada reserva existente
   ============================================================================ */
DECLARE @Ahora DATETIME = GETDATE();
INSERT INTO dbo.Bitacora_Reservas
    (ReservaID, HuespedTitularID, HabitacionID, FechaIngreso, FechaEgreso, CheckIn, CheckOut, Estado,
     CantidadAdultos, CantidadNinos, TarifaNoche, MontoTotal, UsuarioRegistroDNI, FechaRegistro, MontoOriginal,
     Operacion, CambioID, Fecha, Hora, UsuarioDB, UsuarioApp, Act)
SELECT r.ReservaID, r.HuespedTitularID, r.HabitacionID, r.FechaIngreso, r.FechaEgreso, r.CheckIn, r.CheckOut, r.Estado,
       r.CantidadAdultos, r.CantidadNinos, r.TarifaNoche, r.MontoTotal, r.UsuarioRegistroDNI, r.FechaRegistro, r.MontoOriginal,
       N'Carga inicial', NULL, CAST(@Ahora AS DATE), CAST(DATEADD(MILLISECOND, -DATEPART(MILLISECOND, @Ahora), @Ahora) AS TIME(0)), SUSER_SNAME(), NULL, 1
FROM dbo.Reservas r
WHERE NOT EXISTS (SELECT 1 FROM dbo.Bitacora_Reservas b WHERE b.ReservaID = r.ReservaID);
GO

/* ============================================================================
   5b. REPORTE DE SERVICIOS: ya no usa la bitácora vieja para saber en qué habitación
       estaba el huésped al pedir el servicio (lo toma del historial de cambios)
   ============================================================================ */
CREATE OR ALTER PROCEDURE [dbo].[sp_ReporteServiciosConsumidos]
    @Desde   DATE          = NULL,
    @Hasta   DATE          = NULL,
    @Tipo    INT           = NULL,   -- 0 Room service, 1 Upgrade
    @Estado  INT           = NULL,   -- 0 Solicitado, 1 Entregado, 2 Completado
    @Huesped NVARCHAR(100) = NULL    -- documento, nombre o apellido del titular (contiene)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @HastaExcl DATETIME = CASE WHEN @Hasta IS NULL THEN NULL ELSE DATEADD(DAY, 1, CAST(@Hasta AS DATETIME)) END;
    DECLARE @Busqueda NVARCHAR(104) = CASE WHEN NULLIF(LTRIM(RTRIM(@Huesped)), N'') IS NULL THEN NULL
                                           ELSE N'%' + LTRIM(RTRIM(@Huesped)) + N'%' END;

    CREATE TABLE #Servicios
    (
        -- COLLATE DATABASE_DEFAULT: la tabla temporal vive en tempdb y puede tener otra intercalación
        CargoID INT PRIMARY KEY, ReservaID INT, Tipo INT, Descripcion NVARCHAR(200) COLLATE DATABASE_DEFAULT,
        FechaHora DATETIME, Monto DECIMAL(10, 2), UsuarioRegistroDNI INT NULL, EstadoServicio INT, PedidoID INT NULL,
        FechaHoraEntrega DATETIME NULL, Cantidad INT NULL, Detalle NVARCHAR(MAX) COLLATE DATABASE_DEFAULT NULL,
        HabitacionAnterior NVARCHAR(10) COLLATE DATABASE_DEFAULT NULL, HabitacionNueva NVARCHAR(10) COLLATE DATABASE_DEFAULT NULL,
        TipoNuevo NVARCHAR(50) COLLATE DATABASE_DEFAULT NULL, Diferencia DECIMAL(10, 2) NULL
    );

    -- Room service
    INSERT INTO #Servicios
    SELECT c.CargoID, c.ReservaID, c.Tipo, c.Descripcion, c.FechaHora, c.Monto, c.UsuarioRegistroDNI,
           CASE WHEN p.Estado = 0 THEN 0 ELSE 1 END, p.PedidoID, p.FechaHoraEntrega,
           (SELECT SUM(d.Cantidad) FROM dbo.DetallePedidoRoomService d WHERE d.PedidoID = p.PedidoID),
           STUFF((SELECT N', ' + CAST(d.Cantidad AS NVARCHAR(5)) + N' x ' + i.Nombre
                  FROM dbo.DetallePedidoRoomService d
                  INNER JOIN dbo.ItemsRoomService i ON i.ItemID = d.ItemID
                  WHERE d.PedidoID = p.PedidoID
                  ORDER BY i.Nombre
                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, N''),
           NULL, NULL, NULL, NULL
    FROM dbo.CargosAdicionales c
    INNER JOIN dbo.PedidosRoomService p ON p.CargoID = c.CargoID
    WHERE c.Tipo = 0
      AND (@Desde IS NULL OR c.FechaHora >= @Desde)
      AND (@HastaExcl IS NULL OR c.FechaHora < @HastaExcl);

    -- Upgrades (la cantidad son las noches que se cobraron con la tarifa nueva)
    INSERT INTO #Servicios
    SELECT c.CargoID, c.ReservaID, c.Tipo, c.Descripcion, c.FechaHora, c.Monto, c.UsuarioRegistroDNI,
           2, NULL, NULL, hc.NochesRestantes, NULL,
           ha.NroHabitacion, hn.NroHabitacion, tn.Nombre, hc.TarifaNueva - hc.TarifaAnterior
    FROM dbo.CargosAdicionales c
    INNER JOIN dbo.HistorialCambiosHabitacion hc ON hc.CargoID = c.CargoID
    INNER JOIN dbo.Habitaciones ha               ON ha.HabitacionID = hc.HabitacionAnteriorID
    INNER JOIN dbo.Habitaciones hn               ON hn.HabitacionID = hc.HabitacionNuevaID
    INNER JOIN dbo.TiposHabitacion tn            ON tn.TipoHabitacionID = hn.TipoHabitacionID
    WHERE c.Tipo = 1
      AND (@Desde IS NULL OR c.FechaHora >= @Desde)
      AND (@HastaExcl IS NULL OR c.FechaHora < @HastaExcl);

    -- Filtros por tipo, estado y huésped
    DELETE s FROM #Servicios s
    INNER JOIN dbo.Reservas r   ON r.ReservaID = s.ReservaID
    INNER JOIN dbo.Huespedes hu ON hu.HuespedID = r.HuespedTitularID
    WHERE (@Tipo IS NOT NULL AND s.Tipo <> @Tipo)
       OR (@Estado IS NOT NULL AND s.EstadoServicio <> @Estado)
       OR (@Busqueda IS NOT NULL AND NOT (hu.Documento LIKE @Busqueda OR hu.Apellido LIKE @Busqueda OR hu.Nombre LIKE @Busqueda
                                          OR (hu.Nombre + N' ' + hu.Apellido) LIKE @Busqueda
                                          OR (hu.Apellido + N', ' + hu.Nombre) LIKE @Busqueda));

    -- 1) Servicios
    SELECT s.CargoID, s.ReservaID, s.Tipo, s.Descripcion, s.FechaHora, s.Monto, s.EstadoServicio, s.PedidoID,
           s.FechaHoraEntrega, ISNULL(s.Cantidad, 0) AS Cantidad, s.Detalle, s.HabitacionAnterior, s.TipoNuevo, s.Diferencia,
           r.HuespedTitularID AS HuespedID, hu.Documento, hu.Nombre, hu.Apellido,
           COALESCE(s.HabitacionNueva,
                    -- Habitación en el momento del servicio: la "anterior" del primer cambio posterior a esa hora
                    (SELECT TOP 1 ha2.NroHabitacion
                     FROM dbo.HistorialCambiosHabitacion hc2
                     INNER JOIN dbo.Habitaciones ha2 ON ha2.HabitacionID = hc2.HabitacionAnteriorID
                     WHERE hc2.ReservaID = s.ReservaID AND hc2.FechaCambio > s.FechaHora
                     ORDER BY hc2.FechaCambio, hc2.CambioID),
                    hab.NroHabitacion) AS NroHabitacion,
           u.Nombre AS NombreUsuario, u.Apellido AS ApellidoUsuario
    FROM #Servicios s
    INNER JOIN dbo.Reservas r     ON r.ReservaID = s.ReservaID
    INNER JOIN dbo.Huespedes hu   ON hu.HuespedID = r.HuespedTitularID
    INNER JOIN dbo.Habitaciones hab ON hab.HabitacionID = r.HabitacionID
    LEFT  JOIN dbo.Users u        ON u.DNI = s.UsuarioRegistroDNI
    ORDER BY hu.Apellido, hu.Nombre, r.HuespedTitularID, s.FechaHora, s.CargoID;

    -- 2) Alojamiento facturado de las reservas que ingresan en el período (sin canceladas)
    SELECT ISNULL(SUM(r.MontoTotal), 0) AS Alojamiento
    FROM dbo.Reservas r
    WHERE r.Estado <> 3
      AND (@Desde IS NULL OR r.FechaIngreso >= @Desde)
      AND (@Hasta IS NULL OR r.FechaIngreso <= @Hasta)
      -- Con filtro de huésped, tipo o estado: solo las estadías que quedaron en el reporte
      AND ((@Busqueda IS NULL AND @Tipo IS NULL AND @Estado IS NULL)
           OR EXISTS (SELECT 1 FROM #Servicios s WHERE s.ReservaID = r.ReservaID));

    -- 3) Productos de la carta más pedidos (de los pedidos que quedaron en el reporte)
    SELECT i.Nombre, SUM(d.Cantidad) AS Cantidad, SUM(d.Cantidad * d.PrecioUnitario) AS Monto
    FROM #Servicios s
    INNER JOIN dbo.DetallePedidoRoomService d ON d.PedidoID = s.PedidoID
    INNER JOIN dbo.ItemsRoomService i         ON i.ItemID = d.ItemID
    GROUP BY i.Nombre
    ORDER BY SUM(d.Cantidad) DESC, i.Nombre;

    DROP TABLE #Servicios;
END
GO

/* ============================================================================
   6. PERMISO: la pantalla pasa a ser la bitácora de reservas
   ============================================================================ */
UPDATE dbo.Permisos
SET Nombre = N'VerBitacoraReservas',
    Descripcion = N'Consultar la bitácora de cambios de reservas'
WHERE Nombre = N'VerBitacoraCambiosHabitacion';
GO

/* ============================================================================
   7. DÍGITOS VERIFICADORES: la tabla nueva reemplaza a la anterior
   (mismo orden que DvBLL_08YS.TablasMonitoreadas en la aplicación)
   ============================================================================ */
CREATE OR ALTER PROCEDURE dbo.sp_GetAllTablesParaDV
AS
BEGIN
    SELECT * FROM Users;                      -- RS0
    SELECT * FROM Roles;                      -- RS1
    SELECT * FROM Familias;                   -- RS2
    SELECT * FROM Permisos;                   -- RS3
    SELECT * FROM FamiliaPermiso;             -- RS4
    SELECT * FROM FamiliaIntegrada;           -- RS5
    SELECT * FROM RolFamilia;                 -- RS6
    SELECT * FROM RolPermiso;                 -- RS7
    SELECT * FROM Pisos;                      -- RS8
    SELECT * FROM TiposHabitacion;            -- RS9
    SELECT * FROM Habitaciones;               -- RS10
    SELECT * FROM Huespedes;                  -- RS11
    SELECT * FROM Reservas;                   -- RS12
    SELECT * FROM Acompanantes;               -- RS13
    SELECT * FROM Pagos;                      -- RS14
    SELECT * FROM ConfiguracionHotel;         -- RS15
    -- PN2
    SELECT * FROM ItemsRoomService           ORDER BY ItemID;              -- RS16
    SELECT * FROM CargosAdicionales          ORDER BY CargoID;             -- RS17
    SELECT * FROM PedidosRoomService         ORDER BY PedidoID;            -- RS18
    SELECT * FROM DetallePedidoRoomService   ORDER BY PedidoID, ItemID;    -- RS19
    SELECT * FROM HistorialCambiosHabitacion ORDER BY CambioID;            -- RS20
    -- Bitácora de cambios de reservas
    SELECT * FROM Bitacora_Reservas          ORDER BY BitacoraID;          -- RS21
END
GO

PRINT N'Bitácora de reservas lista. Entrar con un SuperAdmin y recalcular los dígitos verificadores.';
GO
