/* =====================================================================
   Permisos de negocio - Sistema de Hotelería (TP_Ing_Soft)
   Idempotente: se puede ejecutar más de una vez sin duplicar datos.
   IMPORTANTE: al terminar, iniciar sesión con un usuario SuperAdmin
   (ManejarInconsistencias) y elegir "Recalcular" en la pantalla de
   inconsistencia de DV (Permisos, Familias, FamiliaPermiso, Roles y
   RolFamilia cambian por fuera del sistema).
   ===================================================================== */
USE [TP_Ing_Soft]
GO

SET NOCOUNT ON;
BEGIN TRANSACTION;

/* ---------------------------------------------------------------------
   1. Permisos (el código los busca por Nombre = valor del enum Permisos)
   --------------------------------------------------------------------- */
DECLARE @Permisos TABLE (Nombre nvarchar(100), Descripcion nvarchar(200));
INSERT INTO @Permisos (Nombre, Descripcion) VALUES
 -- Reservas
 (N'VerReservas',                 N'Acceder a la pantalla Reservas con el listado y el calendario de reservas'),
 (N'RegistrarReserva',            N'Registrar reservas nuevas desde Recepción, el listado o el control de habitaciones'),
 (N'ModificarReserva',            N'Modificar fechas y composición de reservas pendientes'),
 (N'CancelarReserva',             N'Cancelar reservas pendientes'),
 (N'ImprimirComprobante',         N'Imprimir el comprobante de una reserva con sus pagos'),
 (N'Cobrar',                      N'Registrar pagos de reservas (al reservar y saldo pendiente en el check-out)'),
 -- Estadías
 (N'ExtenderEstadia',             N'Renovar (extender) la estadía de una reserva en curso'),
 (N'VerControlHabitaciones',      N'Acceder a la pantalla Control de habitaciones'),
 (N'CambiarEstadoHabitacion',     N'Marcar habitaciones como disponibles o ponerlas en mantenimiento'),
 (N'RegistrarCheckIn',            N'Registrar el check-in de una reserva confirmada'),
 (N'RegistrarCheckOut',           N'Registrar el check-out de una reserva en curso'),
 -- Huéspedes
 (N'VerHuespedes',                N'Acceder al Maestro de Huéspedes'),
 (N'CrearHuesped',                N'Registrar huéspedes nuevos'),
 (N'ModificarHuesped',            N'Modificar huéspedes existentes'),
 (N'EliminarHuesped',             N'Eliminar huéspedes sin reservas asociadas'),
 (N'SerializarHuespedes',         N'Guardar en un archivo XML los huéspedes de la grilla'),
 (N'DeserializarHuespedes',       N'Cargar en la grilla un archivo XML de huéspedes'),
 -- Habitaciones
 (N'VerHabitaciones',             N'Acceder a la pantalla Habitaciones'),
 (N'CrearHabitacion',             N'Crear habitaciones nuevas'),
 (N'ModificarHabitacion',         N'Modificar habitaciones existentes'),
 (N'EliminarHabitacion',          N'Eliminar habitaciones sin reservas asociadas'),
 (N'VerTiposHabitacion',          N'Acceder a la pantalla Tipos de habitación'),
 (N'CrearTipoHabitacion',         N'Crear tipos de habitación nuevos'),
 (N'ModificarTipoHabitacion',     N'Modificar tipos de habitación existentes'),
 (N'EliminarTipoHabitacion',      N'Eliminar tipos de habitación sin habitaciones asociadas'),
 (N'VerPisos',                    N'Acceder a la pantalla Pisos'),
 (N'CrearPiso',                   N'Crear pisos nuevos'),
 (N'ModificarPiso',               N'Modificar pisos existentes'),
 (N'EliminarPiso',                N'Eliminar pisos sin habitaciones asociadas'),
 -- Reportes y configuración
 (N'VerDashboard',                N'Acceder al Dashboard de métricas'),
 (N'GenerarReporteReservas',      N'Generar el reporte de reservas desde el Dashboard'),
 (N'VerConfiguracionHotel',       N'Acceder a la pantalla Configuración del hotel'),
 (N'ModificarConfiguracionHotel', N'Modificar horarios de check-in/check-out y tolerancia de no-show');

INSERT INTO [dbo].[Permisos] ([Nombre], [Descripcion])
SELECT p.Nombre, p.Descripcion
FROM @Permisos p
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Permisos] x WHERE x.Nombre = p.Nombre);

/* ---------------------------------------------------------------------
   2. Familias (sin permisos repetidos entre familias)
   --------------------------------------------------------------------- */
DECLARE @FamiliaPermiso TABLE (Familia nvarchar(100), Permiso nvarchar(100));
INSERT INTO @FamiliaPermiso (Familia, Permiso) VALUES
 (N'Gestion de reservas',     N'VerReservas'),
 (N'Gestion de reservas',     N'RegistrarReserva'),
 (N'Gestion de reservas',     N'ModificarReserva'),
 (N'Gestion de reservas',     N'CancelarReserva'),
 (N'Gestion de reservas',     N'ImprimirComprobante'),
 (N'Gestion de reservas',     N'Cobrar'),
 (N'Gestion de estadias',     N'ExtenderEstadia'),
 (N'Gestion de estadias',     N'VerControlHabitaciones'),
 (N'Gestion de estadias',     N'CambiarEstadoHabitacion'),
 (N'Gestion de estadias',     N'RegistrarCheckIn'),
 (N'Gestion de estadias',     N'RegistrarCheckOut'),
 (N'Gestion de huespedes',    N'VerHuespedes'),
 (N'Gestion de huespedes',    N'CrearHuesped'),
 (N'Gestion de huespedes',    N'ModificarHuesped'),
 (N'Gestion de huespedes',    N'EliminarHuesped'),
 (N'Gestion de huespedes',    N'SerializarHuespedes'),
 (N'Gestion de huespedes',    N'DeserializarHuespedes'),
 (N'Gestion de habitaciones', N'VerHabitaciones'),
 (N'Gestion de habitaciones', N'CrearHabitacion'),
 (N'Gestion de habitaciones', N'ModificarHabitacion'),
 (N'Gestion de habitaciones', N'EliminarHabitacion'),
 (N'Gestion de habitaciones', N'VerTiposHabitacion'),
 (N'Gestion de habitaciones', N'CrearTipoHabitacion'),
 (N'Gestion de habitaciones', N'ModificarTipoHabitacion'),
 (N'Gestion de habitaciones', N'EliminarTipoHabitacion'),
 (N'Gestion de habitaciones', N'VerPisos'),
 (N'Gestion de habitaciones', N'CrearPiso'),
 (N'Gestion de habitaciones', N'ModificarPiso'),
 (N'Gestion de habitaciones', N'EliminarPiso'),
 (N'Reportes',                N'VerDashboard'),
 (N'Reportes',                N'GenerarReporteReservas'),
 (N'Configuracion del hotel', N'VerConfiguracionHotel'),
 (N'Configuracion del hotel', N'ModificarConfiguracionHotel');

INSERT INTO [dbo].[Familias] ([Nombre])
SELECT DISTINCT fp.Familia
FROM @FamiliaPermiso fp
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Familias] f WHERE f.Nombre = fp.Familia);

INSERT INTO [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID])
SELECT f.FamiliaID, p.PermisoID
FROM @FamiliaPermiso fp
JOIN [dbo].[Familias] f ON f.Nombre = fp.Familia
JOIN [dbo].[Permisos] p ON p.Nombre = fp.Permiso
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[FamiliaPermiso] x
                  WHERE x.FamiliaID = f.FamiliaID AND x.PermisoID = p.PermisoID);

/* ---------------------------------------------------------------------
   3. Roles
      - SuperAdmin: todas las familias de negocio
      - Recepcionista (nuevo): reservas, estadías y huéspedes
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM [dbo].[Roles] WHERE Nombre = N'Recepcionista')
    INSERT INTO [dbo].[Roles] ([Nombre]) VALUES (N'Recepcionista');

DECLARE @RolFamilia TABLE (Rol nvarchar(100), Familia nvarchar(100));
INSERT INTO @RolFamilia (Rol, Familia) VALUES
 (N'SuperAdmin',    N'Gestion de reservas'),
 (N'SuperAdmin',    N'Gestion de estadias'),
 (N'SuperAdmin',    N'Gestion de huespedes'),
 (N'SuperAdmin',    N'Gestion de habitaciones'),
 (N'SuperAdmin',    N'Reportes'),
 (N'SuperAdmin',    N'Configuracion del hotel'),
 (N'Recepcionista', N'Gestion de reservas'),
 (N'Recepcionista', N'Gestion de estadias'),
 (N'Recepcionista', N'Gestion de huespedes');

INSERT INTO [dbo].[RolFamilia] ([RolID], [FamiliaID])
SELECT r.RolID, f.FamiliaID
FROM @RolFamilia rf
JOIN [dbo].[Roles] r    ON r.Nombre = rf.Rol
JOIN [dbo].[Familias] f ON f.Nombre = rf.Familia
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[RolFamilia] x
                  WHERE x.RolID = r.RolID AND x.FamiliaID = f.FamiliaID);

COMMIT TRANSACTION;

/* Verificación */
SELECT r.Nombre AS Rol, f.Nombre AS Familia, p.PermisoID, p.Nombre AS Permiso
FROM [dbo].[RolFamilia] rf
JOIN [dbo].[Roles] r           ON r.RolID = rf.RolID
JOIN [dbo].[Familias] f        ON f.FamiliaID = rf.FamiliaID
JOIN [dbo].[FamiliaPermiso] fp ON fp.FamiliaID = f.FamiliaID
JOIN [dbo].[Permisos] p        ON p.PermisoID = fp.PermisoID
WHERE f.Nombre IN (N'Gestion de reservas', N'Gestion de estadias', N'Gestion de huespedes',
                   N'Gestion de habitaciones', N'Reportes', N'Configuracion del hotel')
ORDER BY r.Nombre, f.Nombre, p.PermisoID;
GO
