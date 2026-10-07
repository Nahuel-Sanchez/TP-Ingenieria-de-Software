/* ============================================================================
   PN2 — Gestión de cargos adicionales durante la estadía      (versión 2)
   Horizon Hotel & Resort (TP_Ing_Soft)
   ----------------------------------------------------------------------------
   Reglas de negocio
   - Cambio de habitación de una estadía EN CURSO, de dos tipos:
       0 = Upgrade solicitado por el huésped: solo a una tarifa MAYOR a la vigente y genera
           un cargo = noches restantes x tarifa nueva - lo ya pagado por esas noches
             = (tarifa nueva - tarifa vigente) x noches restantes.
       1 = Cambio administrativo (mantenimiento, problema físico, decisión del hotel):
           a cualquier habitación libre con capacidad suficiente, SIN cargo.
     No hay devoluciones ni downgrade cobrado/descontado.
   - Tarifa vigente de una reserva = la del último upgrade; si no hubo, la tarifa con la
     que se reservó. Las renovaciones posteriores se cobran a la tarifa vigente.
   - Servicio a la habitación: pedido con ítems del catálogo = un cargo; el personal de
     servicio confirma la entrega (hora de entrega y estado).
   - Todos los cargos se liquidan en el check-out.
   - Una reserva Confirmada (sin check-in) también puede reubicarse por el hotel (por ejemplo, si su
     habitación pasa a mantenimiento): se valida la disponibilidad de toda su estadía, sin cargo.
   - Bitácora de cambios (patrón PRODUCTOS_C de la cátedra) llenada SOLO por triggers: no se
     borra ni se modifica ningún registro (tampoco Act) desde fuera de esos triggers.

   Crea (idempotente). Si ya se ejecutó la versión 1 de este script, detecta su esquema
   viejo y vuelve a crear las tablas del PN2 (se pierden solo datos de prueba del PN2).

   Requiere SQL Server 2016 SP1 o superior (CREATE OR ALTER, SESSION_CONTEXT).
   AL TERMINAR: entrar con un SuperAdmin y RECALCULAR los dígitos verificadores.
   ============================================================================ */
USE [TP_Ing_Soft];
GO
SET NOCOUNT ON;
GO

/* ============================================================================
   0. MIGRACIÓN DESDE LA VERSIÓN 1 (solo si existe el esquema anterior)
   ============================================================================ */
IF OBJECT_ID(N'dbo.HistorialCambiosHabitacion', N'U') IS NOT NULL
   AND COL_LENGTH(N'dbo.HistorialCambiosHabitacion', N'Motivo') IS NULL
BEGIN
    PRINT N'Se detectó la versión 1 del PN2: se vuelven a crear sus tablas.';
    IF OBJECT_ID(N'dbo.TR_Reservas_BitacoraHabitacion_Alta', N'TR') IS NOT NULL   DROP TRIGGER dbo.TR_Reservas_BitacoraHabitacion_Alta;
    IF OBJECT_ID(N'dbo.TR_Reservas_BitacoraHabitacion_Cambio', N'TR') IS NOT NULL DROP TRIGGER dbo.TR_Reservas_BitacoraHabitacion_Cambio;
    IF OBJECT_ID(N'dbo.Bitacora_CambiosHabitacion', N'U') IS NOT NULL   DROP TABLE dbo.Bitacora_CambiosHabitacion;
    IF OBJECT_ID(N'dbo.HistorialCambiosHabitacion', N'U') IS NOT NULL   DROP TABLE dbo.HistorialCambiosHabitacion;
    IF OBJECT_ID(N'dbo.DetallePedidoRoomService', N'U') IS NOT NULL     DROP TABLE dbo.DetallePedidoRoomService;
    IF OBJECT_ID(N'dbo.PedidosRoomService', N'U') IS NOT NULL           DROP TABLE dbo.PedidosRoomService;
    IF OBJECT_ID(N'dbo.CargosAdicionales', N'U') IS NOT NULL            DROP TABLE dbo.CargosAdicionales;
END
GO

/* ============================================================================
   1. TIPO TABLA PARA LOS PEDIDOS (se pasa desde C# como parámetro tabla)
   ============================================================================ */
IF TYPE_ID(N'dbo.ItemCantidadList') IS NULL
    CREATE TYPE dbo.ItemCantidadList AS TABLE
    (
        ItemID   INT NOT NULL PRIMARY KEY,
        Cantidad INT NOT NULL CHECK (Cantidad > 0)
    );
GO

/* ============================================================================
   2. TABLAS
   ============================================================================ */

-- Catálogo de ítems de servicio a la habitación (baja lógica con Activo)
IF OBJECT_ID(N'dbo.ItemsRoomService', N'U') IS NULL
CREATE TABLE dbo.ItemsRoomService
(
    ItemID          INT IDENTITY(1,1) NOT NULL,
    Nombre          NVARCHAR(100)     NOT NULL,
    Descripcion     NVARCHAR(200)     NULL,
    PrecioUnitario  DECIMAL(10,2)     NOT NULL,
    Activo          BIT               NOT NULL CONSTRAINT DF_ItemsRoomService_Activo DEFAULT (1),
    CONSTRAINT PK_ItemsRoomService PRIMARY KEY CLUSTERED (ItemID),
    CONSTRAINT UQ_ItemsRoomService_Nombre UNIQUE (Nombre),
    CONSTRAINT CK_ItemsRoomService_Precio CHECK (PrecioUnitario > 0)
);
GO

-- Cuenta del huésped: cada consumo / upgrade es un cargo que se liquida en el check-out
IF OBJECT_ID(N'dbo.CargosAdicionales', N'U') IS NULL
CREATE TABLE dbo.CargosAdicionales
(
    CargoID            INT IDENTITY(1,1) NOT NULL,
    ReservaID          INT               NOT NULL,
    Tipo               INT               NOT NULL,   -- 0 = Servicio a la habitación, 1 = Upgrade de habitación
    Descripcion        NVARCHAR(200)     NOT NULL,
    FechaHora          DATETIME          NOT NULL CONSTRAINT DF_CargosAdicionales_FechaHora DEFAULT (GETDATE()),
    Monto              DECIMAL(10,2)     NOT NULL,
    UsuarioRegistroDNI INT               NULL,       -- recepcionista que cargó el consumo
    CONSTRAINT PK_CargosAdicionales PRIMARY KEY CLUSTERED (CargoID),
    CONSTRAINT FK_CargosAdicionales_Reservas FOREIGN KEY (ReservaID) REFERENCES dbo.Reservas (ReservaID),
    CONSTRAINT FK_CargosAdicionales_Users    FOREIGN KEY (UsuarioRegistroDNI) REFERENCES dbo.Users (DNI),
    CONSTRAINT CK_CargosAdicionales_Tipo  CHECK (Tipo IN (0, 1)),
    CONSTRAINT CK_CargosAdicionales_Monto CHECK (Monto > 0)
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_CargosAdicionales_Reserva')
    CREATE NONCLUSTERED INDEX IX_CargosAdicionales_Reserva ON dbo.CargosAdicionales (ReservaID) INCLUDE (Tipo, Monto);
GO

-- Pedido de room service: 1 pedido = 1 cargo (el cargo tiene la reserva, la fecha y el monto)
IF OBJECT_ID(N'dbo.PedidosRoomService', N'U') IS NULL
CREATE TABLE dbo.PedidosRoomService
(
    PedidoID         INT IDENTITY(1,1) NOT NULL,
    CargoID          INT               NOT NULL,
    Estado           INT               NOT NULL CONSTRAINT DF_PedidosRoomService_Estado DEFAULT (0), -- 0 = Pendiente, 1 = Entregado
    FechaHoraEntrega DATETIME          NULL,
    CONSTRAINT PK_PedidosRoomService PRIMARY KEY CLUSTERED (PedidoID),
    CONSTRAINT UQ_PedidosRoomService_Cargo UNIQUE (CargoID),
    CONSTRAINT FK_PedidosRoomService_Cargos FOREIGN KEY (CargoID) REFERENCES dbo.CargosAdicionales (CargoID),
    CONSTRAINT CK_PedidosRoomService_Estado CHECK (Estado IN (0, 1)),
    -- Pendiente sin hora de entrega / Entregado con hora de entrega
    CONSTRAINT CK_PedidosRoomService_Entrega CHECK ((Estado = 0 AND FechaHoraEntrega IS NULL) OR (Estado = 1 AND FechaHoraEntrega IS NOT NULL))
);
GO

-- Ítems de cada pedido (el precio se copia: si después cambia el catálogo, el pedido no cambia)
IF OBJECT_ID(N'dbo.DetallePedidoRoomService', N'U') IS NULL
CREATE TABLE dbo.DetallePedidoRoomService
(
    PedidoID       INT           NOT NULL,
    ItemID         INT           NOT NULL,
    Cantidad       INT           NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_DetallePedidoRoomService PRIMARY KEY CLUSTERED (PedidoID, ItemID),
    CONSTRAINT FK_DetallePedidoRoomService_Pedidos FOREIGN KEY (PedidoID) REFERENCES dbo.PedidosRoomService (PedidoID),
    CONSTRAINT FK_DetallePedidoRoomService_Items   FOREIGN KEY (ItemID)   REFERENCES dbo.ItemsRoomService (ItemID),
    CONSTRAINT CK_DetallePedidoRoomService_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_DetallePedidoRoomService_Precio   CHECK (PrecioUnitario > 0)
);
GO

-- Cambio de habitación (dato de negocio). Upgrade => con cargo; administrativo => sin cargo.
IF OBJECT_ID(N'dbo.HistorialCambiosHabitacion', N'U') IS NULL
CREATE TABLE dbo.HistorialCambiosHabitacion
(
    CambioID             INT IDENTITY(1,1) NOT NULL,
    ReservaID            INT               NOT NULL,
    Motivo               INT               NOT NULL,   -- 0 = Upgrade solicitado por el huésped, 1 = Cambio administrativo
    Observacion          NVARCHAR(200)     NULL,       -- por qué se reubicó al huésped (cambio administrativo)
    CargoID              INT               NULL,       -- solo el upgrade genera cargo
    HabitacionAnteriorID INT               NOT NULL,
    HabitacionNuevaID    INT               NOT NULL,
    TarifaAnterior       DECIMAL(10,2)     NOT NULL,   -- tarifa vigente de la reserva al momento del cambio
    TarifaNueva          DECIMAL(10,2)     NOT NULL,   -- tarifa del tipo de la habitación nueva
    NochesRestantes      INT               NOT NULL,
    FechaCambio          DATETIME          NOT NULL CONSTRAINT DF_HistorialCambiosHabitacion_Fecha DEFAULT (GETDATE()),
    CONSTRAINT PK_HistorialCambiosHabitacion PRIMARY KEY CLUSTERED (CambioID),
    CONSTRAINT FK_HistorialCambiosHabitacion_Reservas    FOREIGN KEY (ReservaID)            REFERENCES dbo.Reservas (ReservaID),
    CONSTRAINT FK_HistorialCambiosHabitacion_Cargos      FOREIGN KEY (CargoID)              REFERENCES dbo.CargosAdicionales (CargoID),
    CONSTRAINT FK_HistorialCambiosHabitacion_HabAnterior FOREIGN KEY (HabitacionAnteriorID) REFERENCES dbo.Habitaciones (HabitacionID),
    CONSTRAINT FK_HistorialCambiosHabitacion_HabNueva    FOREIGN KEY (HabitacionNuevaID)    REFERENCES dbo.Habitaciones (HabitacionID),
    CONSTRAINT CK_HistorialCambiosHabitacion_Motivo    CHECK (Motivo IN (0, 1)),
    CONSTRAINT CK_HistorialCambiosHabitacion_Distintas CHECK (HabitacionAnteriorID <> HabitacionNuevaID),
    CONSTRAINT CK_HistorialCambiosHabitacion_Noches    CHECK (NochesRestantes >= 0),
    -- Upgrade: tarifa mayor, al menos una noche y con su cargo. Administrativo: nunca genera cargo.
    CONSTRAINT CK_HistorialCambiosHabitacion_Reglas CHECK (
        (Motivo = 0 AND CargoID IS NOT NULL AND TarifaNueva > TarifaAnterior AND NochesRestantes > 0)
     OR (Motivo = 1 AND CargoID IS NULL))
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_HistorialCambiosHabitacion_Reserva')
    CREATE NONCLUSTERED INDEX IX_HistorialCambiosHabitacion_Reserva ON dbo.HistorialCambiosHabitacion (ReservaID, Motivo) INCLUDE (TarifaNueva);
GO
-- Un cargo pertenece a un solo upgrade; los cambios administrativos (CargoID NULL) pueden ser varios
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_HistorialCambiosHabitacion_Cargo')
    CREATE UNIQUE NONCLUSTERED INDEX UX_HistorialCambiosHabitacion_Cargo ON dbo.HistorialCambiosHabitacion (CargoID) WHERE CargoID IS NOT NULL;
GO

/* Bitácora de cambios (patrón "PRODUCTOS_C" pedido por la cátedra):
   - Reservas es la tabla con la información ACTIVA; esta tabla guarda en el tiempo cada
     asignación de habitación que tuvo cada reserva.
   - La escriben SOLO los triggers de Reservas.
   - Act = 1 marca el registro activo de cada reserva: el índice filtrado garantiza uno solo.
   - No se borra nada (trigger INSTEAD OF DELETE) y nada se modifica a mano (trigger de solo lectura):
     Act solo cambia cuando el trigger de Reservas agrega el registro nuevo. */
IF OBJECT_ID(N'dbo.Bitacora_CambiosHabitacion', N'U') IS NULL
CREATE TABLE dbo.Bitacora_CambiosHabitacion
(
    BitacoraID           INT IDENTITY(1,1) NOT NULL,
    ReservaID            INT               NOT NULL,
    HabitacionID         INT               NOT NULL,
    NroHabitacion        NVARCHAR(10)      NOT NULL,  -- foto del momento (aunque la habitación cambie después)
    TipoHabitacion       NVARCHAR(50)      NOT NULL,
    TarifaNoche          DECIMAL(10,2)     NOT NULL,
    HabitacionAnteriorID INT               NULL,
    CambioID             INT               NULL,      -- cambio que lo originó (NULL = alta o cambio hecho por fuera del sistema)
    MontoCargo           DECIMAL(10,2)     NOT NULL CONSTRAINT DF_BitacoraCambiosHabitacion_Monto DEFAULT (0),
    Operacion            NVARCHAR(25)      NOT NULL,  -- Alta | Upgrade | Cambio administrativo | Cambio en BD
    Fecha                DATE              NOT NULL,
    Hora                 TIME(0)           NOT NULL,
    UsuarioDB            NVARCHAR(128)     NOT NULL CONSTRAINT DF_BitacoraCambiosHabitacion_UsuarioDB DEFAULT (SUSER_SNAME()),
    UsuarioApp           NVARCHAR(50)      NULL,      -- usuario del sistema (lo informa el SP por SESSION_CONTEXT)
    Act                  BIT               NOT NULL,
    CONSTRAINT PK_Bitacora_CambiosHabitacion PRIMARY KEY CLUSTERED (BitacoraID),
    CONSTRAINT FK_BitacoraCambiosHabitacion_Reservas    FOREIGN KEY (ReservaID)            REFERENCES dbo.Reservas (ReservaID),
    CONSTRAINT FK_BitacoraCambiosHabitacion_Habitacion  FOREIGN KEY (HabitacionID)         REFERENCES dbo.Habitaciones (HabitacionID),
    CONSTRAINT FK_BitacoraCambiosHabitacion_HabAnterior FOREIGN KEY (HabitacionAnteriorID) REFERENCES dbo.Habitaciones (HabitacionID),
    CONSTRAINT FK_BitacoraCambiosHabitacion_Cambio      FOREIGN KEY (CambioID)             REFERENCES dbo.HistorialCambiosHabitacion (CambioID),
    CONSTRAINT CK_BitacoraCambiosHabitacion_Operacion   CHECK (Operacion IN (N'Alta', N'Upgrade', N'Cambio administrativo', N'Cambio en BD'))
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_BitacoraCambiosHabitacion_Activo')
    CREATE UNIQUE NONCLUSTERED INDEX UX_BitacoraCambiosHabitacion_Activo
        ON dbo.Bitacora_CambiosHabitacion (ReservaID) WHERE Act = 1;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_BitacoraCambiosHabitacion_Fecha')
    CREATE NONCLUSTERED INDEX IX_BitacoraCambiosHabitacion_Fecha
        ON dbo.Bitacora_CambiosHabitacion (Fecha, ReservaID);
GO

/* ============================================================================
   3. TARIFA VIGENTE DE UNA RESERVA
   ============================================================================ */
-- La del último upgrade; si no hubo, la tarifa con la que se reservó.
-- (Un cambio administrativo no la modifica: el huésped sigue pagando lo mismo.)
CREATE OR ALTER FUNCTION dbo.fn_TarifaVigenteReserva (@ReservaID INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Tarifa DECIMAL(10,2);

    SELECT TOP (1) @Tarifa = h.TarifaNueva
    FROM dbo.HistorialCambiosHabitacion h
    WHERE h.ReservaID = @ReservaID AND h.Motivo = 0
    ORDER BY h.CambioID DESC;

    IF @Tarifa IS NULL
        SELECT @Tarifa = r.TarifaNoche FROM dbo.Reservas r WHERE r.ReservaID = @ReservaID;

    RETURN @Tarifa;
END
GO

/* ============================================================================
   4. TRIGGERS DE LA BITÁCORA
   ============================================================================ */

-- Toda reserva nueva deja su primer registro (Alta) como activo
CREATE OR ALTER TRIGGER dbo.TR_Reservas_BitacoraHabitacion_Alta
ON dbo.Reservas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM inserted) RETURN;

    INSERT INTO dbo.Bitacora_CambiosHabitacion
        (ReservaID, HabitacionID, NroHabitacion, TipoHabitacion, TarifaNoche, HabitacionAnteriorID,
         CambioID, MontoCargo, Operacion, Fecha, Hora, UsuarioDB, UsuarioApp, Act)
    SELECT i.ReservaID, i.HabitacionID, h.NroHabitacion, t.Nombre, i.TarifaNoche, NULL,
           NULL, 0, N'Alta', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME(0)), SUSER_SNAME(),
           COALESCE(CAST(SESSION_CONTEXT(N'UsuarioApp') AS NVARCHAR(50)), u.Username), 1
    FROM inserted i
    INNER JOIN dbo.Habitaciones h    ON h.HabitacionID = i.HabitacionID
    INNER JOIN dbo.TiposHabitacion t ON t.TipoHabitacionID = h.TipoHabitacionID
    LEFT  JOIN dbo.Users u           ON u.DNI = i.UsuarioRegistroDNI;
END
GO

-- Cada vez que cambia la habitación de una reserva: el registro activo pasa a Act = 0
-- y se agrega uno nuevo con Act = 1, con el motivo y el cargo del cambio que informa el SP.
-- Si alguien cambia la habitación directo en la base, igual queda registrado ("Cambio en BD").
CREATE OR ALTER TRIGGER dbo.TR_Reservas_BitacoraHabitacion_Cambio
ON dbo.Reservas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT UPDATE(HabitacionID) RETURN;

    DECLARE @Cambios TABLE (ReservaID INT PRIMARY KEY, HabitacionAnteriorID INT NOT NULL, HabitacionNuevaID INT NOT NULL);
    INSERT INTO @Cambios (ReservaID, HabitacionAnteriorID, HabitacionNuevaID)
    SELECT i.ReservaID, d.HabitacionID, i.HabitacionID
    FROM inserted i
    INNER JOIN deleted d ON d.ReservaID = i.ReservaID
    WHERE i.HabitacionID <> d.HabitacionID;

    IF NOT EXISTS (SELECT 1 FROM @Cambios) RETURN;

    DECLARE @CambioID INT = CAST(SESSION_CONTEXT(N'CambioHabitacionID') AS INT);

    UPDATE b SET Act = 0
    FROM dbo.Bitacora_CambiosHabitacion b
    INNER JOIN @Cambios c ON c.ReservaID = b.ReservaID
    WHERE b.Act = 1;

    INSERT INTO dbo.Bitacora_CambiosHabitacion
        (ReservaID, HabitacionID, NroHabitacion, TipoHabitacion, TarifaNoche, HabitacionAnteriorID,
         CambioID, MontoCargo, Operacion, Fecha, Hora, UsuarioDB, UsuarioApp, Act)
    SELECT c.ReservaID, c.HabitacionNuevaID, h.NroHabitacion, t.Nombre, t.TarifaNoche, c.HabitacionAnteriorID,
           hc.CambioID, ISNULL(cg.Monto, 0),
           CASE WHEN hc.CambioID IS NULL THEN N'Cambio en BD'
                WHEN hc.Motivo = 0       THEN N'Upgrade'
                ELSE N'Cambio administrativo' END,
           CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME(0)), SUSER_SNAME(),
           CAST(SESSION_CONTEXT(N'UsuarioApp') AS NVARCHAR(50)), 1
    FROM @Cambios c
    INNER JOIN dbo.Habitaciones h    ON h.HabitacionID = c.HabitacionNuevaID
    INNER JOIN dbo.TiposHabitacion t ON t.TipoHabitacionID = h.TipoHabitacionID
    LEFT  JOIN dbo.HistorialCambiosHabitacion hc ON hc.CambioID = @CambioID
                                                AND hc.ReservaID = c.ReservaID
                                                AND hc.HabitacionNuevaID = c.HabitacionNuevaID
    LEFT  JOIN dbo.CargosAdicionales cg          ON cg.CargoID = hc.CargoID;
END
GO

-- A la bitácora no se le elimina ningún registro
CREATE OR ALTER TRIGGER dbo.TR_BitacoraCambiosHabitacion_NoBorrar
ON dbo.Bitacora_CambiosHabitacion
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    THROW 50101, N'La bitácora de cambios de habitación no admite bajas: sus registros los escriben los triggers de Reservas.', 1;
END
GO

-- Versión anterior (permitía modificar Act a mano): se reemplaza por la de solo lectura
IF OBJECT_ID(N'dbo.TR_BitacoraCambiosHabitacion_SoloAct', N'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_BitacoraCambiosHabitacion_SoloAct;
GO

-- Ningún registro se modifica a mano. La única actualización permitida es la que hace el trigger de
-- Reservas al pasar el registro anterior a Act = 0: tiene que venir de ese trigger y tocar solo Act.
CREATE OR ALTER TRIGGER dbo.TR_BitacoraCambiosHabitacion_SoloLectura
ON dbo.Bitacora_CambiosHabitacion
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL(OBJECT_ID(N'dbo.TR_Reservas_BitacoraHabitacion_Cambio'), 'AFTER', 'DML') = 0
       OR UPDATE(ReservaID) OR UPDATE(HabitacionID) OR UPDATE(NroHabitacion) OR UPDATE(TipoHabitacion)
       OR UPDATE(TarifaNoche) OR UPDATE(HabitacionAnteriorID) OR UPDATE(CambioID) OR UPDATE(MontoCargo)
       OR UPDATE(Operacion) OR UPDATE(Fecha) OR UPDATE(Hora) OR UPDATE(UsuarioDB) OR UPDATE(UsuarioApp)
        THROW 50102, N'La bitácora de cambios de habitación es de solo lectura: sus registros los escriben los triggers de Reservas.', 1;
END
GO

/* ============================================================================
   5. CARGA INICIAL DE LA BITÁCORA (una fila "Alta" activa por cada reserva existente)
   ============================================================================ */
INSERT INTO dbo.Bitacora_CambiosHabitacion
    (ReservaID, HabitacionID, NroHabitacion, TipoHabitacion, TarifaNoche, HabitacionAnteriorID,
     CambioID, MontoCargo, Operacion, Fecha, Hora, UsuarioDB, UsuarioApp, Act)
SELECT r.ReservaID, r.HabitacionID, h.NroHabitacion, t.Nombre, r.TarifaNoche, NULL,
       NULL, 0, N'Alta', CAST(r.FechaRegistro AS DATE), CAST(r.FechaRegistro AS TIME(0)), SUSER_SNAME(), u.Username, 1
FROM dbo.Reservas r
INNER JOIN dbo.Habitaciones h    ON h.HabitacionID = r.HabitacionID
INNER JOIN dbo.TiposHabitacion t ON t.TipoHabitacionID = h.TipoHabitacionID
LEFT  JOIN dbo.Users u           ON u.DNI = r.UsuarioRegistroDNI
WHERE NOT EXISTS (SELECT 1 FROM dbo.Bitacora_CambiosHabitacion b WHERE b.ReservaID = r.ReservaID);
GO

/* ============================================================================
   6. PROCEDIMIENTOS — CAMBIO DE HABITACIÓN
   ============================================================================ */

-- Habitaciones a las que se puede cambiar una reserva:
--   - En curso (upgrade o reubicación): disponibles AHORA (se muda ya) y libres desde hoy hasta el egreso.
--   - Confirmada, sin check-in (solo reubicación): que no estén fuera de servicio y libres durante toda
--     la estadía que falta (desde el ingreso o desde hoy, lo que sea posterior).
-- Siempre con capacidad para todos. @SoloUpgrade = 1: además, de tarifa mayor a la vigente de la reserva.
-- Mismas columnas que sp_GetHabitacionesDisponibles (se reutiliza el mapper de habitaciones).
CREATE OR ALTER PROCEDURE dbo.sp_GetHabitacionesParaCambio
    @ReservaID   INT,
    @SoloUpgrade BIT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
    DECLARE @HabitacionActual INT, @Estado INT, @FechaIngreso DATE, @FechaEgreso DATE, @Huespedes INT;
    DECLARE @TarifaVigente DECIMAL(10,2) = dbo.fn_TarifaVigenteReserva(@ReservaID);

    SELECT @HabitacionActual = r.HabitacionID, @Estado = r.Estado, @FechaIngreso = r.FechaIngreso,
           @FechaEgreso = r.FechaEgreso, @Huespedes = r.CantidadAdultos + r.CantidadNinos
    FROM dbo.Reservas r
    WHERE r.ReservaID = @ReservaID
      AND (r.Estado = 1 OR (r.Estado = 0 AND @SoloUpgrade = 0)); -- el upgrade es solo con la estadía en curso

    DECLARE @Desde DATE = CASE WHEN @FechaIngreso > @Hoy THEN @FechaIngreso ELSE @Hoy END;

    SELECT h.HabitacionID, h.NroHabitacion, h.Estado,
           p.PisoID, p.Numero, p.Nombre AS NombrePiso,
           t.TipoHabitacionID, t.Nombre AS NombreTipo, t.Descripcion, t.Capacidad, t.TarifaNoche
    FROM dbo.Habitaciones h
    INNER JOIN dbo.Pisos p           ON p.PisoID = h.PisoID
    INNER JOIN dbo.TiposHabitacion t ON t.TipoHabitacionID = h.TipoHabitacionID
    WHERE @HabitacionActual IS NOT NULL
      AND h.HabitacionID <> @HabitacionActual
      AND ((@Estado = 1 AND h.Estado = 0)                      -- en curso: se muda ahora, tiene que estar lista
        OR (@Estado = 0 AND h.Estado <> 4))                    -- sin check-in: alcanza con que no esté fuera de servicio
      AND t.Capacidad >= @Huespedes
      AND (@SoloUpgrade = 0 OR t.TarifaNoche > @TarifaVigente) -- upgrade: categoría superior
      AND NOT EXISTS (SELECT 1 FROM dbo.Reservas r
                      WHERE r.HabitacionID = h.HabitacionID
                        AND r.ReservaID <> @ReservaID
                        AND r.Estado IN (0, 1)
                        AND r.FechaIngreso < @FechaEgreso
                        AND r.FechaEgreso  > @Desde)
    ORDER BY t.TarifaNoche, p.Numero, h.NroHabitacion;
END
GO

-- Registra el cambio en una transacción y vuelve a validar todo con datos frescos:
--   historial (+ cargo si es upgrade) + nueva habitación en la reserva (dispara la bitácora)
--   + estados físicos si el huésped ya está alojado (la anterior queda En limpieza, salvo que esté Fuera de servicio).
-- Upgrade (solo en curso): @Monto = (tarifa nueva - tarifa vigente) x noches restantes.
-- Reubicación: en curso, o Confirmada sin check-in (se cambia la habitación de toda la estadía).
-- @Resultado: 0 OK, 1 la reserva no admite el cambio, 2 habitación no disponible, 3 misma habitación,
--             4 upgrade sin noches restantes (egresa hoy), 5 upgrade a una tarifa que no es mayor,
--             6 capacidad insuficiente, 7 motivo inválido.
CREATE OR ALTER PROCEDURE dbo.sp_RegistrarCambioHabitacion
    @ReservaID         INT,
    @HabitacionNuevaID INT,
    @Motivo            INT,             -- 0 = Upgrade solicitado, 1 = Cambio administrativo
    @Observacion       NVARCHAR(200),
    @DescripcionCargo  NVARCHAR(200),   -- texto del cargo (solo upgrade)
    @UsuarioDNI        INT,
    @UsuarioApp        NVARCHAR(50),
    @CambioID          INT OUTPUT,
    @Monto             DECIMAL(10,2) OUTPUT,
    @Resultado         INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @CambioID = NULL;
    SET @Monto = 0;

    IF @Motivo NOT IN (0, 1) BEGIN SET @Resultado = 7; RETURN; END

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @HabitacionAnterior INT, @Estado INT, @FechaIngreso DATE, @FechaEgreso DATE, @Huespedes INT,
                @Hoy DATE = CAST(GETDATE() AS DATE);
        SELECT @HabitacionAnterior = HabitacionID, @Estado = Estado, @FechaIngreso = FechaIngreso,
               @FechaEgreso = FechaEgreso, @Huespedes = CantidadAdultos + CantidadNinos
        FROM dbo.Reservas WITH (UPDLOCK, HOLDLOCK)
        WHERE ReservaID = @ReservaID;

        -- En curso: upgrade o reubicación. Confirmada (sin check-in): solo reubicación del hotel.
        IF @Estado IS NULL OR NOT (@Estado = 1 OR (@Estado = 0 AND @Motivo = 1))
        BEGIN SET @Resultado = 1; ROLLBACK TRANSACTION; RETURN; END

        IF @HabitacionAnterior = @HabitacionNuevaID
        BEGIN SET @Resultado = 3; ROLLBACK TRANSACTION; RETURN; END

        DECLARE @Desde DATE = CASE WHEN @FechaIngreso > @Hoy THEN @FechaIngreso ELSE @Hoy END;
        DECLARE @NochesRestantes INT = DATEDIFF(DAY, @Desde, @FechaEgreso);
        IF @NochesRestantes < 0 SET @NochesRestantes = 0;
        IF @Motivo = 0 AND @NochesRestantes = 0
        BEGIN SET @Resultado = 4; ROLLBACK TRANSACTION; RETURN; END

        DECLARE @TarifaVigente DECIMAL(10,2) = dbo.fn_TarifaVigenteReserva(@ReservaID);
        DECLARE @TarifaNueva DECIMAL(10,2), @Capacidad INT;
        SELECT @TarifaNueva = t.TarifaNoche, @Capacidad = t.Capacidad
        FROM dbo.Habitaciones h
        INNER JOIN dbo.TiposHabitacion t ON t.TipoHabitacionID = h.TipoHabitacionID
        WHERE h.HabitacionID = @HabitacionNuevaID;

        IF @Capacidad IS NULL OR @Capacidad < @Huespedes
        BEGIN SET @Resultado = 6; ROLLBACK TRANSACTION; RETURN; END

        IF @Motivo = 0 AND @TarifaNueva <= @TarifaVigente
        BEGIN SET @Resultado = 5; ROLLBACK TRANSACTION; RETURN; END

        IF NOT EXISTS (SELECT 1 FROM dbo.Habitaciones WITH (UPDLOCK, HOLDLOCK)
                       WHERE HabitacionID = @HabitacionNuevaID
                         AND ((@Estado = 1 AND Estado = 0) OR (@Estado = 0 AND Estado <> 4)))
           OR EXISTS (SELECT 1 FROM dbo.Reservas WITH (UPDLOCK, HOLDLOCK)
                      WHERE HabitacionID = @HabitacionNuevaID
                        AND ReservaID <> @ReservaID
                        AND Estado IN (0, 1)
                        AND FechaIngreso < @FechaEgreso
                        AND FechaEgreso  > @Desde)
        BEGIN SET @Resultado = 2; ROLLBACK TRANSACTION; RETURN; END

        DECLARE @CargoID INT = NULL;
        IF @Motivo = 0
        BEGIN
            SET @Monto = (@TarifaNueva - @TarifaVigente) * @NochesRestantes;
            INSERT INTO dbo.CargosAdicionales (ReservaID, Tipo, Descripcion, FechaHora, Monto, UsuarioRegistroDNI)
            VALUES (@ReservaID, 1, @DescripcionCargo, GETDATE(), @Monto, @UsuarioDNI);
            SET @CargoID = CAST(SCOPE_IDENTITY() AS INT);
        END

        INSERT INTO dbo.HistorialCambiosHabitacion
            (ReservaID, Motivo, Observacion, CargoID, HabitacionAnteriorID, HabitacionNuevaID,
             TarifaAnterior, TarifaNueva, NochesRestantes, FechaCambio)
        VALUES (@ReservaID, @Motivo, @Observacion, @CargoID, @HabitacionAnterior, @HabitacionNuevaID,
                @TarifaVigente, @TarifaNueva, @NochesRestantes, GETDATE());
        SET @CambioID = CAST(SCOPE_IDENTITY() AS INT);

        -- Datos para el trigger de la bitácora (usuario del sistema y cambio que origina el registro)
        EXEC sys.sp_set_session_context @key = N'UsuarioApp', @value = @UsuarioApp;
        EXEC sys.sp_set_session_context @key = N'CambioHabitacionID', @value = @CambioID;

        UPDATE dbo.Reservas SET HabitacionID = @HabitacionNuevaID WHERE ReservaID = @ReservaID;

        EXEC sys.sp_set_session_context @key = N'CambioHabitacionID', @value = NULL;
        EXEC sys.sp_set_session_context @key = N'UsuarioApp', @value = NULL;

        -- Solo si el huésped ya está alojado se mueven los estados físicos.
        -- La anterior: si se está sacando de servicio (mantenimiento) queda así; si no, a limpieza.
        IF @Estado = 1
        BEGIN
            UPDATE dbo.Habitaciones SET Estado = 3 WHERE HabitacionID = @HabitacionAnterior AND Estado <> 4;
            UPDATE dbo.Habitaciones SET Estado = 2 WHERE HabitacionID = @HabitacionNuevaID;
        END

        COMMIT TRANSACTION;
        SET @Resultado = 0;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        EXEC sys.sp_set_session_context @key = N'CambioHabitacionID', @value = NULL;
        EXEC sys.sp_set_session_context @key = N'UsuarioApp', @value = NULL;
        THROW;
    END CATCH
END
GO

-- "Activar" a mano un registro de la bitácora quedó descartado (la bitácora es de solo lectura)
IF OBJECT_ID(N'dbo.sp_ActivarBitacoraHabitacion', N'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ActivarBitacoraHabitacion;
GO

-- Renovación: las noches agregadas se cobran a la tarifa VIGENTE (si hubo upgrade, la nueva).
-- Mismo contrato que la versión original del SP.
CREATE OR ALTER PROCEDURE dbo.sp_ExtenderEstadia
    @ReservaID INT,
    @NuevaFechaEgreso DATE,
    @Disponible BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @HabitacionID INT, @FechaEgresoActual DATE, @Estado INT;

        SELECT @HabitacionID = HabitacionID, @FechaEgresoActual = FechaEgreso, @Estado = Estado
        FROM Reservas WHERE ReservaID = @ReservaID;

        IF @HabitacionID IS NULL OR @Estado <> 1 -- EnCurso
        BEGIN
            SET @Disponible = 0;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF EXISTS (
            SELECT 1 FROM Reservas WITH (UPDLOCK, HOLDLOCK)
            WHERE HabitacionID = @HabitacionID
              AND ReservaID <> @ReservaID
              AND Estado IN (0, 1) -- Confirmada, EnCurso
              AND FechaIngreso < @NuevaFechaEgreso
              AND FechaEgreso > @FechaEgresoActual
        )
        BEGIN
            SET @Disponible = 0;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        DECLARE @TarifaVigente DECIMAL(10,2) = dbo.fn_TarifaVigenteReserva(@ReservaID);

        UPDATE Reservas
        SET FechaEgreso = @NuevaFechaEgreso,
            MontoTotal = MontoTotal + (@TarifaVigente * DATEDIFF(DAY, @FechaEgresoActual, @NuevaFechaEgreso))
        WHERE ReservaID = @ReservaID;

        SET @Disponible = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        SET @Disponible = 0;
        THROW;
    END CATCH
END
GO

/* ============================================================================
   7. PROCEDIMIENTOS — SERVICIO A LA HABITACIÓN
   ============================================================================ */

-- Registra el pedido y su cargo en una transacción. El monto se calcula con los precios vigentes.
-- @Resultado: 0 OK, 1 reserva no en curso, 2 pedido vacío o con ítems inexistentes / dados de baja.
CREATE OR ALTER PROCEDURE dbo.sp_RegistrarPedidoRoomService
    @ReservaID   INT,
    @Items       dbo.ItemCantidadList READONLY,
    @Descripcion NVARCHAR(200),
    @UsuarioDNI  INT,
    @PedidoID    INT OUTPUT,
    @Resultado   INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @PedidoID = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM dbo.Reservas WITH (UPDLOCK, HOLDLOCK) WHERE ReservaID = @ReservaID AND Estado = 1)
        BEGIN SET @Resultado = 1; ROLLBACK TRANSACTION; RETURN; END

        IF NOT EXISTS (SELECT 1 FROM @Items)
           OR EXISTS (SELECT 1 FROM @Items i
                      LEFT JOIN dbo.ItemsRoomService it ON it.ItemID = i.ItemID AND it.Activo = 1
                      WHERE it.ItemID IS NULL)
        BEGIN SET @Resultado = 2; ROLLBACK TRANSACTION; RETURN; END

        DECLARE @Monto DECIMAL(10,2) =
            (SELECT SUM(i.Cantidad * it.PrecioUnitario)
             FROM @Items i INNER JOIN dbo.ItemsRoomService it ON it.ItemID = i.ItemID);

        INSERT INTO dbo.CargosAdicionales (ReservaID, Tipo, Descripcion, FechaHora, Monto, UsuarioRegistroDNI)
        VALUES (@ReservaID, 0, @Descripcion, GETDATE(), @Monto, @UsuarioDNI);
        DECLARE @CargoID INT = CAST(SCOPE_IDENTITY() AS INT);

        INSERT INTO dbo.PedidosRoomService (CargoID, Estado) VALUES (@CargoID, 0);
        SET @PedidoID = CAST(SCOPE_IDENTITY() AS INT);

        INSERT INTO dbo.DetallePedidoRoomService (PedidoID, ItemID, Cantidad, PrecioUnitario)
        SELECT @PedidoID, i.ItemID, i.Cantidad, it.PrecioUnitario
        FROM @Items i INNER JOIN dbo.ItemsRoomService it ON it.ItemID = i.ItemID;

        COMMIT TRANSACTION;
        SET @Resultado = 0;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- El personal de servicio confirma la entrega. @Resultado: 1 confirmado, 0 no existía o ya estaba entregado.
CREATE OR ALTER PROCEDURE dbo.sp_ConfirmarEntregaPedido
    @PedidoID  INT,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.PedidosRoomService
    SET Estado = 1, FechaHoraEntrega = GETDATE()
    WHERE PedidoID = @PedidoID AND Estado = 0;
    SET @Resultado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO

-- Limpieza de la versión 1 (SP renombrado)
IF OBJECT_ID(N'dbo.sp_GetHabitacionesParaUpgrade', N'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetHabitacionesParaUpgrade;
GO

/* ============================================================================
   8. CATÁLOGO INICIAL DE ÍTEMS
   ============================================================================ */
DECLARE @Items TABLE (Nombre NVARCHAR(100), Descripcion NVARCHAR(200), Precio DECIMAL(10,2));
INSERT INTO @Items VALUES
 (N'Desayuno continental',    N'Café o té, jugo de naranja, medialunas, tostadas, manteca y mermelada', 14000),
 (N'Desayuno americano',      N'Huevos revueltos, panceta, tostadas, fruta de estación, café y jugo',  19500),
 (N'Club sándwich',           N'Pollo, jamón, panceta, lechuga, tomate y huevo, con papas fritas',     16500),
 (N'Hamburguesa completa',    N'Medallón de carne, cheddar, panceta, huevo y papas fritas',            18000),
 (N'Ensalada César',          N'Lechuga, pollo grillado, croutons, parmesano y aderezo César',         14500),
 (N'Tabla de fiambres',       N'Selección de fiambres y quesos regionales para dos personas',          22000),
 (N'Agua mineral 500 ml',     N'Con o sin gas (frigobar)',                                              3000),
 (N'Gaseosa 500 ml',          N'Línea cola, lima-limón o naranja (frigobar)',                           3800),
 (N'Cerveza 473 ml',          N'Rubia o negra (frigobar)',                                              5500),
 (N'Botella de vino Malbec',  N'Vino tinto de Mendoza, 750 ml',                                        28000),
 (N'Snack de frigobar',       N'Maní, papas o barra de cereal',                                         4200),
 (N'Café o té en habitación', N'Servicio de cafetería para dos personas',                               6000);

INSERT INTO dbo.ItemsRoomService (Nombre, Descripcion, PrecioUnitario, Activo)
SELECT i.Nombre, i.Descripcion, i.Precio, 1
FROM @Items i
WHERE NOT EXISTS (SELECT 1 FROM dbo.ItemsRoomService x WHERE x.Nombre = i.Nombre);
GO

/* ============================================================================
   9. PERMISOS, FAMILIAS Y ROLES DEL PN2
   (el código los busca por Nombre = valor del enum Permisos)
   ============================================================================ */
BEGIN TRANSACTION;

-- Renombre de la versión 1: el permiso único de cambio pasa a ser el de upgrade
UPDATE dbo.Permisos
SET Nombre = N'RegistrarUpgradeHabitacion',
    Descripcion = N'Registrar el upgrade de habitación pedido por el huésped (genera un cargo)'
WHERE Nombre = N'RegistrarCambioHabitacion'
  AND NOT EXISTS (SELECT 1 FROM dbo.Permisos WHERE Nombre = N'RegistrarUpgradeHabitacion');

DECLARE @Permisos TABLE (Nombre NVARCHAR(100), Descripcion NVARCHAR(200));
INSERT INTO @Permisos VALUES
 (N'RegistrarUpgradeHabitacion',   N'Registrar el upgrade de habitación pedido por el huésped (genera un cargo)'),
 (N'ReubicarHuesped',              N'Cambiar de habitación a un huésped por motivos del hotel (sin cargo)'),
 (N'RegistrarRoomService',         N'Cargar pedidos de servicio a la habitación a la cuenta del huésped'),
 (N'GestionarItemsRoomService',    N'Administrar el catálogo de ítems de servicio a la habitación'),
 (N'ConfirmarEntregaRoomService',  N'Ver los pedidos pendientes y confirmar su entrega'),
 (N'VerBitacoraCambiosHabitacion', N'Consultar la bitácora de cambios de habitación');

INSERT INTO dbo.Permisos (Nombre, Descripcion)
SELECT p.Nombre, p.Descripcion FROM @Permisos p
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos x WHERE x.Nombre = p.Nombre);

DECLARE @FamiliaPermiso TABLE (Familia NVARCHAR(100), Permiso NVARCHAR(100));
INSERT INTO @FamiliaPermiso VALUES
 (N'Cargos adicionales',        N'RegistrarUpgradeHabitacion'),
 (N'Cargos adicionales',        N'RegistrarRoomService'),
 (N'Cargos adicionales',        N'GestionarItemsRoomService'),
 (N'Gestion de estadias',       N'ReubicarHuesped'),
 (N'Servicio a la habitacion',  N'ConfirmarEntregaRoomService'),
 (N'Auditoria de habitaciones', N'VerBitacoraCambiosHabitacion');

INSERT INTO dbo.Familias (Nombre)
SELECT DISTINCT fp.Familia FROM @FamiliaPermiso fp
WHERE NOT EXISTS (SELECT 1 FROM dbo.Familias f WHERE f.Nombre = fp.Familia);

INSERT INTO dbo.FamiliaPermiso (FamiliaID, PermisoID)
SELECT f.FamiliaID, p.PermisoID
FROM @FamiliaPermiso fp
JOIN dbo.Familias f ON f.Nombre = fp.Familia
JOIN dbo.Permisos p ON p.Nombre = fp.Permiso
WHERE NOT EXISTS (SELECT 1 FROM dbo.FamiliaPermiso x WHERE x.FamiliaID = f.FamiliaID AND x.PermisoID = p.PermisoID);

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Nombre = N'Personal de servicio')
    INSERT INTO dbo.Roles (Nombre) VALUES (N'Personal de servicio');

DECLARE @RolFamilia TABLE (Rol NVARCHAR(100), Familia NVARCHAR(100));
INSERT INTO @RolFamilia VALUES
 (N'SuperAdmin',           N'Cargos adicionales'),
 (N'SuperAdmin',           N'Servicio a la habitacion'),
 (N'SuperAdmin',           N'Auditoria de habitaciones'),
 (N'Recepcionista',        N'Cargos adicionales'),
 (N'Recepcionista',        N'Servicio a la habitacion'),
 (N'Personal de servicio', N'Servicio a la habitacion');

INSERT INTO dbo.RolFamilia (RolID, FamiliaID)
SELECT r.RolID, f.FamiliaID
FROM @RolFamilia rf
JOIN dbo.Roles r    ON r.Nombre = rf.Rol
JOIN dbo.Familias f ON f.Nombre = rf.Familia
WHERE NOT EXISTS (SELECT 1 FROM dbo.RolFamilia x WHERE x.RolID = r.RolID AND x.FamiliaID = f.FamiliaID);

-- El permiso de "Activar" (versiones anteriores de este script) ya no existe
DELETE fp FROM dbo.FamiliaPermiso fp JOIN dbo.Permisos p ON p.PermisoID = fp.PermisoID WHERE p.Nombre = N'ActivarCambioHabitacion';
DELETE rp FROM dbo.RolPermiso rp     JOIN dbo.Permisos p ON p.PermisoID = rp.PermisoID WHERE p.Nombre = N'ActivarCambioHabitacion';
DELETE FROM dbo.Permisos WHERE Nombre = N'ActivarCambioHabitacion';

COMMIT TRANSACTION;
GO

/* ============================================================================
   10. DÍGITOS VERIFICADORES: las tablas del PN2 pasan a estar controladas
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
    SELECT * FROM Bitacora_CambiosHabitacion ORDER BY BitacoraID;          -- RS21
END
GO

/* ============================================================================
   11. REPORTE DE SERVICIOS CONSUMIDOS POR HUÉSPED (RFN2)
   Un renglón por servicio (cargo adicional): pedido de room service o upgrade.
     - Habitación: la que tenía la reserva en el momento del consumo (sale de la bitácora de
       cambios de habitación); en un upgrade, la habitación nueva.
     - Estado: 0 Solicitado (pedido sin entregar), 1 Entregado, 2 Completado (upgrade).
   Devuelve 3 resultados: los servicios, el alojamiento facturado del período (para calcular la
   participación de los servicios) y los productos de la carta más pedidos.
   Todos los filtros son opcionales.
   ============================================================================ */
CREATE OR ALTER PROCEDURE dbo.sp_ReporteServiciosConsumidos
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
                    (SELECT TOP 1 b.NroHabitacion
                     FROM dbo.Bitacora_CambiosHabitacion b
                     WHERE b.ReservaID = s.ReservaID
                       AND DATEADD(SECOND, DATEDIFF(SECOND, CAST('00:00:00' AS TIME(0)), b.Hora), CAST(b.Fecha AS DATETIME)) <= s.FechaHora
                     ORDER BY b.Fecha DESC, b.Hora DESC, b.BitacoraID DESC),
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
   12. ANÁLISIS PREDICTIVO DE OCUPACIÓN Y DEMANDA (reporte inteligente)
   Datos crudos para el análisis; los cálculos (ocupación por noche, proyección, temporadas,
   alertas y sugerencias) se hacen en la capa de negocio.
   Devuelve 4 resultados:
     1. Tipos de habitación con la cantidad de habitaciones (totales y vendibles hoy).
     2. Reservas que tocan el período [@DesdeHistorial, @Hasta] (incluye canceladas, para la
        tasa de cancelación), con el tipo de la habitación y los datos del titular. El tipo es el
        de la habitación actual de la reserva (después de un upgrade, toda la estadía cuenta en el
        tipo nuevo): simplificación aceptada para el análisis.
     3. Cargos adicionales (room service y upgrades) de esas reservas.
     4. Productos de la carta pedidos en esas reservas.
   ============================================================================ */
CREATE OR ALTER PROCEDURE dbo.sp_AnalisisDemanda
    @DesdeHistorial DATE,
    @Hasta          DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT t.TipoHabitacionID, t.Nombre, t.TarifaNoche, t.Capacidad,
           COUNT(h.HabitacionID)                                   AS Habitaciones,
           SUM(CASE WHEN h.Estado <> 4 THEN 1 ELSE 0 END)          AS Operativas
    FROM dbo.TiposHabitacion t
    LEFT JOIN dbo.Habitaciones h ON h.TipoHabitacionID = t.TipoHabitacionID
    GROUP BY t.TipoHabitacionID, t.Nombre, t.TarifaNoche, t.Capacidad
    ORDER BY t.TarifaNoche, t.Nombre;

    SELECT r.ReservaID, r.HuespedTitularID, h.TipoHabitacionID, r.FechaRegistro, r.FechaIngreso,
           r.FechaEgreso, r.Estado, r.TarifaNoche, r.MontoTotal,
           r.CantidadAdultos + r.CantidadNinos AS Personas,
           hu.Nombre, hu.Apellido, hu.Documento
    INTO #Reservas
    FROM dbo.Reservas r
    JOIN dbo.Habitaciones h ON h.HabitacionID = r.HabitacionID
    JOIN dbo.Huespedes hu   ON hu.HuespedID = r.HuespedTitularID
    WHERE r.FechaEgreso > @DesdeHistorial
      AND r.FechaIngreso <= @Hasta;

    SELECT * FROM #Reservas ORDER BY FechaIngreso, ReservaID;

    SELECT c.ReservaID, c.Tipo, c.Monto, c.FechaHora
    FROM dbo.CargosAdicionales c
    JOIN #Reservas r ON r.ReservaID = c.ReservaID
    ORDER BY c.FechaHora;

    SELECT c.ReservaID, i.Nombre, d.Cantidad, d.Cantidad * d.PrecioUnitario AS Monto, c.FechaHora
    FROM dbo.DetallePedidoRoomService d
    JOIN dbo.PedidosRoomService p  ON p.PedidoID = d.PedidoID
    JOIN dbo.CargosAdicionales c   ON c.CargoID = p.CargoID
    JOIN dbo.ItemsRoomService i    ON i.ItemID = d.ItemID
    JOIN #Reservas r               ON r.ReservaID = c.ReservaID;

    DROP TABLE #Reservas;
END
GO

/* ============================================================================
   13. PERMISOS DE LOS REPORTES (familia "Reportes")
   - GenerarReporteServicios: reporte de servicios consumidos por huésped (PN2).
   - GenerarAnalisisDemanda: análisis predictivo de ocupación y demanda.
   - GenerarReporteReservas (PN1) también queda en la familia si existe el permiso.
   El SuperAdmin siempre tiene la familia: así ve el botón "Reportes" del menú.
   ============================================================================ */
SET XACT_ABORT ON;
BEGIN TRANSACTION;
DECLARE @PermisosReportes TABLE (Nombre NVARCHAR(100), Descripcion NVARCHAR(200));
INSERT INTO @PermisosReportes VALUES
 (N'GenerarReporteServicios', N'Generar el reporte de servicios consumidos por huésped'),
 (N'GenerarAnalisisDemanda',  N'Generar el análisis predictivo de ocupación y demanda');

INSERT INTO dbo.Permisos (Nombre, Descripcion)
SELECT p.Nombre, p.Descripcion FROM @PermisosReportes p
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos x WHERE x.Nombre = p.Nombre);

IF NOT EXISTS (SELECT 1 FROM dbo.Familias WHERE Nombre = N'Reportes')
    INSERT INTO dbo.Familias (Nombre) VALUES (N'Reportes');

INSERT INTO dbo.FamiliaPermiso (FamiliaID, PermisoID)
SELECT f.FamiliaID, p.PermisoID
FROM dbo.Familias f CROSS JOIN dbo.Permisos p
WHERE f.Nombre = N'Reportes'
  AND p.Nombre IN (N'GenerarReporteReservas', N'GenerarReporteServicios', N'GenerarAnalisisDemanda')
  AND NOT EXISTS (SELECT 1 FROM dbo.FamiliaPermiso x WHERE x.FamiliaID = f.FamiliaID AND x.PermisoID = p.PermisoID);

INSERT INTO dbo.RolFamilia (RolID, FamiliaID)
SELECT r.RolID, f.FamiliaID
FROM dbo.Roles r CROSS JOIN dbo.Familias f
WHERE r.Nombre = N'SuperAdmin' AND f.Nombre = N'Reportes'
  AND NOT EXISTS (SELECT 1 FROM dbo.RolFamilia x WHERE x.RolID = r.RolID AND x.FamiliaID = f.FamiliaID);
COMMIT TRANSACTION;
GO

PRINT N'PN2 instalado. Recordá RECALCULAR los dígitos verificadores desde la aplicación.';
GO
