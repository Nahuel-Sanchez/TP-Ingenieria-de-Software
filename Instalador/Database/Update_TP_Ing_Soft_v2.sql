/* ============================================================================
   HORIZON HOTEL & RESORT - BASE DE DATOS TP_Ing_Soft
   Update_TP_Ing_Soft_v2.sql  ·  REINSTALACIÓN: actualiza la versión 1 a la versión 2
   ----------------------------------------------------------------------------
   Agrega todo lo de negocio sin perder lo que ya hay en la base:
   - Tablas nuevas: Acompanantes, Bitacora_Reservas, CargosAdicionales, ConfiguracionHotel, DetallePedidoRoomService, Facturas, Habitaciones, HistorialCambiosHabitacion, Huespedes, ItemsRoomService, PedidosRoomService, Pisos, Reservas, TiposHabitacion.
   - Tipos, función, procedimientos almacenados y triggers de la versión 2
     (los del sistema se actualizan con CREATE OR ALTER).
   - Bitácora de cambios de reservas (triggers que la escriben y la protegen).
   - Seguridad: agrega por NOMBRE los permisos, familias y roles nuevos y sus
     relaciones. No toca usuarios, ni contraseñas, ni los roles y familias que el
     cliente haya modificado (solo les suma lo nuevo de la versión 2).
   - Catálogos iniciales (configuración del hotel, pisos, tipos de habitación,
     habitaciones y carta de servicio a la habitación) al crear esas tablas.
   - Dígitos verificadores: se vacía la tabla DV porque cambió el contenido de las
     tablas controladas; la aplicación los vuelve a calcular en la próxima
     operación que modifique datos (o con "Recalcular" desde Integridad).

   - Se puede ejecutar más de una vez: lo que ya existe no se vuelve a crear.
   - Todo va en una sola transacción: si algo falla, la base queda como estaba.
   - Ejecutar con:  sqlcmd -S <instancia> -E -C -I -b -f 65001 -i Update_TP_Ing_Soft_v2.sql
     (en SSMS: menú Consulta > Modo SQLCMD).
   Requiere SQL Server 2016 SP1 o superior (CREATE OR ALTER, SESSION_CONTEXT).
   ============================================================================ */
:on error exit
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;
GO
IF DB_ID(N'TP_Ing_Soft') IS NULL
BEGIN
    THROW 50001, N'No existe la base TP_Ing_Soft en esta instancia. Use la opción Instalar.', 1;
END
GO
USE [TP_Ing_Soft];
GO
IF OBJECT_ID(N'dbo.Users', N'U') IS NULL OR OBJECT_ID(N'dbo.Permisos', N'U') IS NULL
BEGIN
    THROW 50002, N'La base TP_Ing_Soft no tiene el sistema instalado (versión 1). Use la opción Instalar.', 1;
END
GO
IF OBJECT_ID(N'dbo.Pagos', N'U') IS NOT NULL AND OBJECT_ID(N'dbo.Facturas', N'U') IS NULL
BEGIN
    THROW 50003, N'La base tiene la tabla Pagos de una versión de desarrollo: ejecute antes facturas_renombrar.sql.', 1;
END
GO
IF OBJECT_ID(N'tempdb..#TablasNuevas') IS NOT NULL DROP TABLE #TablasNuevas;
CREATE TABLE #TablasNuevas (Tabla SYSNAME NOT NULL PRIMARY KEY);
GO
BEGIN TRANSACTION;
GO

/* ----------------------------------------------------------------------------
   1. TIPOS
   ---------------------------------------------------------------------------- */
IF TYPE_ID(N'dbo.IntList') IS NULL
BEGIN
    EXEC(N'CREATE TYPE [dbo].[IntList] AS TABLE(
	[ID] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)');
END
GO
IF TYPE_ID(N'dbo.ItemCantidadList') IS NULL
BEGIN
    EXEC(N'CREATE TYPE [dbo].[ItemCantidadList] AS TABLE(
	[ItemID] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (IGNORE_DUP_KEY = OFF),
	CHECK (([Cantidad]>(0)))
)');
END
GO

/* ----------------------------------------------------------------------------
   2. TABLAS NUEVAS (solo si no existen)
   ---------------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.Acompanantes', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Acompanantes](
    	[ReservaID] [int] NOT NULL,
    	[HuespedID] [int] NOT NULL,
     CONSTRAINT [PK_Acompanantes] PRIMARY KEY CLUSTERED 
    (
    	[ReservaID] ASC,
    	[HuespedID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'Acompanantes');
END
GO
IF OBJECT_ID(N'dbo.Bitacora_Reservas', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Bitacora_Reservas](
    	[BitacoraID] [int] IDENTITY(1,1) NOT NULL,
    	[ReservaID] [int] NOT NULL,
    	[HuespedTitularID] [int] NOT NULL,
    	[HabitacionID] [int] NOT NULL,
    	[FechaIngreso] [date] NOT NULL,
    	[FechaEgreso] [date] NOT NULL,
    	[CheckIn] [datetime] NULL,
    	[CheckOut] [datetime] NULL,
    	[Estado] [int] NOT NULL,
    	[CantidadAdultos] [int] NOT NULL,
    	[CantidadNinos] [int] NOT NULL,
    	[TarifaNoche] [decimal](10, 2) NOT NULL,
    	[MontoTotal] [decimal](10, 2) NOT NULL,
    	[UsuarioRegistroDNI] [int] NULL,
    	[FechaRegistro] [datetime] NOT NULL,
    	[MontoOriginal] [decimal](10, 2) NOT NULL,
    	[Operacion] [nvarchar](40) NOT NULL,
    	[CambioID] [int] NULL,
    	[Fecha] [date] NOT NULL,
    	[Hora] [time](0) NOT NULL,
    	[UsuarioDB] [nvarchar](128) NOT NULL,
    	[UsuarioApp] [nvarchar](50) NULL,
    	[Act] [bit] NOT NULL,
    	[RollbackDeID] [int] NULL,
     CONSTRAINT [PK_Bitacora_Reservas] PRIMARY KEY CLUSTERED 
    (
    	[BitacoraID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'Bitacora_Reservas');
END
GO
IF OBJECT_ID(N'dbo.CargosAdicionales', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[CargosAdicionales](
    	[CargoID] [int] IDENTITY(1,1) NOT NULL,
    	[ReservaID] [int] NOT NULL,
    	[Tipo] [int] NOT NULL,
    	[Descripcion] [nvarchar](200) NOT NULL,
    	[FechaHora] [datetime] NOT NULL,
    	[Monto] [decimal](10, 2) NOT NULL,
    	[UsuarioRegistroDNI] [int] NULL,
     CONSTRAINT [PK_CargosAdicionales] PRIMARY KEY CLUSTERED 
    (
    	[CargoID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'CargosAdicionales');
END
GO
IF OBJECT_ID(N'dbo.ConfiguracionHotel', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ConfiguracionHotel](
    	[ConfiguracionID] [int] IDENTITY(1,1) NOT NULL,
    	[HoraCheckIn] [time](7) NOT NULL,
    	[HoraCheckOut] [time](7) NOT NULL,
    	[GraciaNoShowHoras] [decimal](5, 2) NOT NULL,
     CONSTRAINT [PK_ConfiguracionHotel] PRIMARY KEY CLUSTERED 
    (
    	[ConfiguracionID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'ConfiguracionHotel');
END
GO
IF OBJECT_ID(N'dbo.DetallePedidoRoomService', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[DetallePedidoRoomService](
    	[PedidoID] [int] NOT NULL,
    	[ItemID] [int] NOT NULL,
    	[Cantidad] [int] NOT NULL,
    	[PrecioUnitario] [decimal](10, 2) NOT NULL,
     CONSTRAINT [PK_DetallePedidoRoomService] PRIMARY KEY CLUSTERED 
    (
    	[PedidoID] ASC,
    	[ItemID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'DetallePedidoRoomService');
END
GO
IF OBJECT_ID(N'dbo.Facturas', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Facturas](
    	[FacturaID] [int] IDENTITY(1,1) NOT NULL,
    	[ReservaID] [int] NOT NULL,
    	[Monto] [decimal](10, 2) NOT NULL,
    	[MetodoPago] [int] NOT NULL,
    	[FechaEmision] [datetime] NOT NULL,
     CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED 
    (
    	[FacturaID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'Facturas');
END
GO
IF OBJECT_ID(N'dbo.Habitaciones', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Habitaciones](
    	[HabitacionID] [int] IDENTITY(1,1) NOT NULL,
    	[NroHabitacion] [nvarchar](10) NOT NULL,
    	[PisoID] [int] NOT NULL,
    	[TipoHabitacionID] [int] NOT NULL,
    	[Estado] [int] NOT NULL,
     CONSTRAINT [PK_Habitaciones] PRIMARY KEY CLUSTERED 
    (
    	[HabitacionID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'Habitaciones');
END
GO
IF OBJECT_ID(N'dbo.HistorialCambiosHabitacion', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[HistorialCambiosHabitacion](
    	[CambioID] [int] IDENTITY(1,1) NOT NULL,
    	[ReservaID] [int] NOT NULL,
    	[HabitacionID] [int] NOT NULL,
    	[Motivo] [int] NOT NULL,
    	[Observacion] [nvarchar](200) NULL,
    	[CargoID] [int] NULL,
    	[TarifaNoche] [decimal](10, 2) NOT NULL,
    	[NochesRestantes] [int] NOT NULL,
    	[FechaCambio] [datetime] NOT NULL,
     CONSTRAINT [PK_HistorialCambiosHabitacion] PRIMARY KEY CLUSTERED 
    (
    	[CambioID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'HistorialCambiosHabitacion');
END
GO
IF OBJECT_ID(N'dbo.Huespedes', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Huespedes](
    	[HuespedID] [int] IDENTITY(1,1) NOT NULL,
    	[Nombre] [nvarchar](100) NOT NULL,
    	[Apellido] [nvarchar](100) NOT NULL,
    	[Documento] [nvarchar](20) NOT NULL,
    	[TipoDocumento] [int] NOT NULL,
    	[Nacionalidad] [nvarchar](50) NOT NULL,
    	[FechaNacimiento] [date] NOT NULL,
    	[Email] [nvarchar](150) NULL,
    	[Telefono] [nvarchar](30) NULL,
     CONSTRAINT [PK_Huespedes] PRIMARY KEY CLUSTERED 
    (
    	[HuespedID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'Huespedes');
END
GO
IF OBJECT_ID(N'dbo.ItemsRoomService', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ItemsRoomService](
    	[ItemID] [int] IDENTITY(1,1) NOT NULL,
    	[Nombre] [nvarchar](100) NOT NULL,
    	[Descripcion] [nvarchar](200) NULL,
    	[PrecioUnitario] [decimal](10, 2) NOT NULL,
    	[Activo] [bit] NOT NULL,
     CONSTRAINT [PK_ItemsRoomService] PRIMARY KEY CLUSTERED 
    (
    	[ItemID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'ItemsRoomService');
END
GO
IF OBJECT_ID(N'dbo.PedidosRoomService', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PedidosRoomService](
    	[PedidoID] [int] IDENTITY(1,1) NOT NULL,
    	[CargoID] [int] NOT NULL,
    	[Estado] [int] NOT NULL,
    	[FechaHoraEntrega] [datetime] NULL,
     CONSTRAINT [PK_PedidosRoomService] PRIMARY KEY CLUSTERED 
    (
    	[PedidoID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'PedidosRoomService');
END
GO
IF OBJECT_ID(N'dbo.Pisos', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Pisos](
    	[PisoID] [int] IDENTITY(1,1) NOT NULL,
    	[Numero] [int] NOT NULL,
    	[Nombre] [nvarchar](50) NOT NULL,
     CONSTRAINT [PK_Pisos] PRIMARY KEY CLUSTERED 
    (
    	[PisoID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'Pisos');
END
GO
IF OBJECT_ID(N'dbo.Reservas', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Reservas](
    	[ReservaID] [int] IDENTITY(1,1) NOT NULL,
    	[HuespedTitularID] [int] NOT NULL,
    	[HabitacionID] [int] NOT NULL,
    	[FechaIngreso] [date] NOT NULL,
    	[FechaEgreso] [date] NOT NULL,
    	[CheckIn] [datetime] NULL,
    	[CheckOut] [datetime] NULL,
    	[Estado] [int] NOT NULL,
    	[CantidadAdultos] [int] NOT NULL,
    	[CantidadNinos] [int] NOT NULL,
    	[TarifaNoche] [decimal](10, 2) NOT NULL,
    	[MontoTotal] [decimal](10, 2) NOT NULL,
    	[UsuarioRegistroDNI] [int] NULL,
    	[FechaRegistro] [datetime] NOT NULL,
    	[MontoOriginal] [decimal](10, 2) NOT NULL,
     CONSTRAINT [PK_Reservas] PRIMARY KEY CLUSTERED 
    (
    	[ReservaID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'Reservas');
END
GO
IF OBJECT_ID(N'dbo.TiposHabitacion', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TiposHabitacion](
    	[TipoHabitacionID] [int] IDENTITY(1,1) NOT NULL,
    	[Nombre] [nvarchar](50) NOT NULL,
    	[Descripcion] [nvarchar](200) NULL,
    	[Capacidad] [int] NOT NULL,
    	[TarifaNoche] [decimal](10, 2) NOT NULL,
     CONSTRAINT [PK_TiposHabitacion] PRIMARY KEY CLUSTERED 
    (
    	[TipoHabitacionID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    INSERT INTO #TablasNuevas (Tabla) VALUES (N'TiposHabitacion');
END
GO

/* ----------------------------------------------------------------------------
   3. ÍNDICES Y RESTRICCIONES DE LAS TABLAS NUEVAS (solo de las que se acaban de crear)
   ---------------------------------------------------------------------------- */
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'CREATE NONCLUSTERED INDEX [IX_BitacoraReservas_Fecha] ON [dbo].[Bitacora_Reservas]
    (
    	[Fecha] ASC,
    	[ReservaID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'CREATE NONCLUSTERED INDEX [IX_BitacoraReservas_Reserva] ON [dbo].[Bitacora_Reservas]
    (
    	[ReservaID] ASC,
    	[BitacoraID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'CREATE UNIQUE NONCLUSTERED INDEX [UX_BitacoraReservas_Activo] ON [dbo].[Bitacora_Reservas]
    (
    	[ReservaID] ASC
    )
    WHERE ([Act]=(1))
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'CREATE NONCLUSTERED INDEX [IX_CargosAdicionales_Reserva] ON [dbo].[CargosAdicionales]
    (
    	[ReservaID] ASC
    )
    INCLUDE([Tipo],[Monto]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'CREATE NONCLUSTERED INDEX [IX_HistorialCambiosHabitacion_Reserva] ON [dbo].[HistorialCambiosHabitacion]
    (
    	[ReservaID] ASC,
    	[FechaCambio] ASC,
    	[CambioID] ASC
    )
    INCLUDE([HabitacionID],[Motivo],[TarifaNoche]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'CREATE UNIQUE NONCLUSTERED INDEX [UX_HistorialCambiosHabitacion_Cargo] ON [dbo].[HistorialCambiosHabitacion]
    (
    	[CargoID] ASC
    )
    WHERE ([CargoID] IS NOT NULL)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'CREATE UNIQUE NONCLUSTERED INDEX [UX_HistorialCambiosHabitacion_Inicial] ON [dbo].[HistorialCambiosHabitacion]
    (
    	[ReservaID] ASC
    )
    WHERE ([Motivo]=(2))
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Habitaciones')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Habitaciones] ADD  CONSTRAINT [UQ_Habitaciones_NroHabitacion] UNIQUE NONCLUSTERED 
    (
    	[NroHabitacion] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Huespedes')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Huespedes] ADD  CONSTRAINT [UQ_Huespedes_DocumentoCompleto] UNIQUE NONCLUSTERED 
    (
    	[TipoDocumento] ASC,
    	[Documento] ASC,
    	[Nacionalidad] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'ItemsRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[ItemsRoomService] ADD  CONSTRAINT [UQ_ItemsRoomService_Nombre] UNIQUE NONCLUSTERED 
    (
    	[Nombre] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'PedidosRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[PedidosRoomService] ADD  CONSTRAINT [UQ_PedidosRoomService_Cargo] UNIQUE NONCLUSTERED 
    (
    	[CargoID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Pisos')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Pisos] ADD  CONSTRAINT [UQ_Pisos_Numero] UNIQUE NONCLUSTERED 
    (
    	[Numero] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas] ADD  CONSTRAINT [DF_BitacoraReservas_UsuarioDB]  DEFAULT (suser_sname()) FOR [UsuarioDB]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales] ADD  CONSTRAINT [DF_CargosAdicionales_FechaHora]  DEFAULT (getdate()) FOR [FechaHora]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'ItemsRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[ItemsRoomService] ADD  CONSTRAINT [DF_ItemsRoomService_Activo]  DEFAULT ((1)) FOR [Activo]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'PedidosRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[PedidosRoomService] ADD  CONSTRAINT [DF_PedidosRoomService_Estado]  DEFAULT ((0)) FOR [Estado]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] ADD  DEFAULT (getdate()) FOR [FechaRegistro]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Acompanantes')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Acompanantes]  WITH CHECK ADD  CONSTRAINT [FK_Acompanantes_Huespedes] FOREIGN KEY([HuespedID])
    REFERENCES [dbo].[Huespedes] ([HuespedID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Acompanantes')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Acompanantes] CHECK CONSTRAINT [FK_Acompanantes_Huespedes]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Acompanantes')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Acompanantes]  WITH CHECK ADD  CONSTRAINT [FK_Acompanantes_Reservas] FOREIGN KEY([ReservaID])
    REFERENCES [dbo].[Reservas] ([ReservaID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Acompanantes')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Acompanantes] CHECK CONSTRAINT [FK_Acompanantes_Reservas]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas]  WITH CHECK ADD  CONSTRAINT [FK_BitacoraReservas_Cambio] FOREIGN KEY([CambioID])
    REFERENCES [dbo].[HistorialCambiosHabitacion] ([CambioID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas] CHECK CONSTRAINT [FK_BitacoraReservas_Cambio]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas]  WITH CHECK ADD  CONSTRAINT [FK_BitacoraReservas_Reservas] FOREIGN KEY([ReservaID])
    REFERENCES [dbo].[Reservas] ([ReservaID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas] CHECK CONSTRAINT [FK_BitacoraReservas_Reservas]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas]  WITH CHECK ADD  CONSTRAINT [FK_BitacoraReservas_Rollback] FOREIGN KEY([RollbackDeID])
    REFERENCES [dbo].[Bitacora_Reservas] ([BitacoraID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas] CHECK CONSTRAINT [FK_BitacoraReservas_Rollback]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales]  WITH CHECK ADD  CONSTRAINT [FK_CargosAdicionales_Reservas] FOREIGN KEY([ReservaID])
    REFERENCES [dbo].[Reservas] ([ReservaID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales] CHECK CONSTRAINT [FK_CargosAdicionales_Reservas]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales]  WITH CHECK ADD  CONSTRAINT [FK_CargosAdicionales_Users] FOREIGN KEY([UsuarioRegistroDNI])
    REFERENCES [dbo].[Users] ([DNI])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales] CHECK CONSTRAINT [FK_CargosAdicionales_Users]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'DetallePedidoRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[DetallePedidoRoomService]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedidoRoomService_Items] FOREIGN KEY([ItemID])
    REFERENCES [dbo].[ItemsRoomService] ([ItemID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'DetallePedidoRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[DetallePedidoRoomService] CHECK CONSTRAINT [FK_DetallePedidoRoomService_Items]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'DetallePedidoRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[DetallePedidoRoomService]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedidoRoomService_Pedidos] FOREIGN KEY([PedidoID])
    REFERENCES [dbo].[PedidosRoomService] ([PedidoID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'DetallePedidoRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[DetallePedidoRoomService] CHECK CONSTRAINT [FK_DetallePedidoRoomService_Pedidos]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Facturas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Reservas] FOREIGN KEY([ReservaID])
    REFERENCES [dbo].[Reservas] ([ReservaID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Facturas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Reservas]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Habitaciones')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Habitaciones]  WITH CHECK ADD  CONSTRAINT [FK_Habitaciones_Pisos] FOREIGN KEY([PisoID])
    REFERENCES [dbo].[Pisos] ([PisoID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Habitaciones')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Habitaciones] CHECK CONSTRAINT [FK_Habitaciones_Pisos]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Habitaciones')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Habitaciones]  WITH CHECK ADD  CONSTRAINT [FK_Habitaciones_TiposHabitacion] FOREIGN KEY([TipoHabitacionID])
    REFERENCES [dbo].[TiposHabitacion] ([TipoHabitacionID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Habitaciones')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Habitaciones] CHECK CONSTRAINT [FK_Habitaciones_TiposHabitacion]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'PedidosRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[PedidosRoomService]  WITH CHECK ADD  CONSTRAINT [FK_PedidosRoomService_Cargos] FOREIGN KEY([CargoID])
    REFERENCES [dbo].[CargosAdicionales] ([CargoID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'PedidosRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[PedidosRoomService] CHECK CONSTRAINT [FK_PedidosRoomService_Cargos]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_Habitaciones] FOREIGN KEY([HabitacionID])
    REFERENCES [dbo].[Habitaciones] ([HabitacionID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reservas_Habitaciones]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_Huespedes] FOREIGN KEY([HuespedTitularID])
    REFERENCES [dbo].[Huespedes] ([HuespedID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reservas_Huespedes]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_Users] FOREIGN KEY([UsuarioRegistroDNI])
    REFERENCES [dbo].[Users] ([DNI])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reservas_Users]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas]  WITH CHECK ADD  CONSTRAINT [CK_BitacoraReservas_Operacion] CHECK  (([Operacion]=N''Rollback'' OR [Operacion]=N''Cambio de habitación'' OR [Operacion]=N''Cambio de habitación administrativo'' OR [Operacion]=N''Cambio de habitación por huésped'' OR [Operacion]=N''Renovación'' OR [Operacion]=N''Check-out'' OR [Operacion]=N''Check-in'' OR [Operacion]=N''No-show'' OR [Operacion]=N''Cancelación'' OR [Operacion]=N''Modificación'' OR [Operacion]=N''Alta'' OR [Operacion]=N''Carga inicial''))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Bitacora_Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas] CHECK CONSTRAINT [CK_BitacoraReservas_Operacion]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales]  WITH CHECK ADD  CONSTRAINT [CK_CargosAdicionales_Monto] CHECK  (([Monto]>(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales] CHECK CONSTRAINT [CK_CargosAdicionales_Monto]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales]  WITH CHECK ADD  CONSTRAINT [CK_CargosAdicionales_Tipo] CHECK  (([Tipo]=(1) OR [Tipo]=(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'CargosAdicionales')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[CargosAdicionales] CHECK CONSTRAINT [CK_CargosAdicionales_Tipo]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'DetallePedidoRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[DetallePedidoRoomService]  WITH CHECK ADD  CONSTRAINT [CK_DetallePedidoRoomService_Cantidad] CHECK  (([Cantidad]>(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'DetallePedidoRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[DetallePedidoRoomService] CHECK CONSTRAINT [CK_DetallePedidoRoomService_Cantidad]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'DetallePedidoRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[DetallePedidoRoomService]  WITH CHECK ADD  CONSTRAINT [CK_DetallePedidoRoomService_Precio] CHECK  (([PrecioUnitario]>(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'DetallePedidoRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[DetallePedidoRoomService] CHECK CONSTRAINT [CK_DetallePedidoRoomService_Precio]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'ItemsRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[ItemsRoomService]  WITH CHECK ADD  CONSTRAINT [CK_ItemsRoomService_Precio] CHECK  (([PrecioUnitario]>(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'ItemsRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[ItemsRoomService] CHECK CONSTRAINT [CK_ItemsRoomService_Precio]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'PedidosRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[PedidosRoomService]  WITH CHECK ADD  CONSTRAINT [CK_PedidosRoomService_Entrega] CHECK  (([Estado]=(0) AND [FechaHoraEntrega] IS NULL OR [Estado]=(1) AND [FechaHoraEntrega] IS NOT NULL))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'PedidosRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[PedidosRoomService] CHECK CONSTRAINT [CK_PedidosRoomService_Entrega]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'PedidosRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[PedidosRoomService]  WITH CHECK ADD  CONSTRAINT [CK_PedidosRoomService_Estado] CHECK  (([Estado]=(1) OR [Estado]=(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'PedidosRoomService')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[PedidosRoomService] CHECK CONSTRAINT [CK_PedidosRoomService_Estado]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [CK_Reservas_Adultos] CHECK  (([CantidadAdultos]>=(1)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [CK_Reservas_Adultos]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [CK_Reservas_Fechas] CHECK  (([FechaEgreso]>[FechaIngreso]))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [CK_Reservas_Fechas]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [CK_Reservas_Monto] CHECK  (([MontoTotal]>=(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [CK_Reservas_Monto]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [CK_Reservas_Ninos] CHECK  (([CantidadNinos]>=(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [CK_Reservas_Ninos]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [CK_Reservas_Tarifa] CHECK  (([TarifaNoche]>(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Reservas')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [CK_Reservas_Tarifa]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion] ADD  CONSTRAINT [DF_HistorialCambiosHabitacion_Fecha]  DEFAULT (getdate()) FOR [FechaCambio]');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [FK_HistorialCambiosHabitacion_Reservas] FOREIGN KEY([ReservaID])
    REFERENCES [dbo].[Reservas] ([ReservaID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [FK_HistorialCambiosHabitacion_Habitaciones] FOREIGN KEY([HabitacionID])
    REFERENCES [dbo].[Habitaciones] ([HabitacionID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [FK_HistorialCambiosHabitacion_Cargos] FOREIGN KEY([CargoID])
    REFERENCES [dbo].[CargosAdicionales] ([CargoID])');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [CK_HistorialCambiosHabitacion_Motivo] CHECK  (([Motivo]=(0) OR [Motivo]=(1) OR [Motivo]=(2)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [CK_HistorialCambiosHabitacion_Noches] CHECK  (([NochesRestantes]>=(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [CK_HistorialCambiosHabitacion_Tarifa] CHECK  (([TarifaNoche]>(0)))');
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'HistorialCambiosHabitacion')
BEGIN
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [CK_HistorialCambiosHabitacion_Reglas] CHECK  (([Motivo]=(0) AND [CargoID] IS NOT NULL AND [NochesRestantes]>(0) OR ([Motivo]=(1) OR [Motivo]=(2)) AND [CargoID] IS NULL))');
END
GO

/* ----------------------------------------------------------------------------
   3b. HISTORIAL DE HABITACIONES: una base que ya tenía el diseño anterior (dos FK a Habitaciones) se convierte
   ---------------------------------------------------------------------------- */
IF COL_LENGTH(N'dbo.HistorialCambiosHabitacion', N'HabitacionAnteriorID') IS NOT NULL
BEGIN
    PRINT N'Historial de cambios de habitación: se pasa al diseño de una sola relación con Habitaciones.';

    -- 1. Restricciones e índice del diseño anterior (las dos FK a Habitaciones y las reglas entre las dos columnas)
    IF OBJECT_ID(N'dbo.CK_HistorialCambiosHabitacion_Distintas', N'C') IS NOT NULL
        ALTER TABLE dbo.HistorialCambiosHabitacion DROP CONSTRAINT CK_HistorialCambiosHabitacion_Distintas;
    IF OBJECT_ID(N'dbo.CK_HistorialCambiosHabitacion_Reglas', N'C') IS NOT NULL
        ALTER TABLE dbo.HistorialCambiosHabitacion DROP CONSTRAINT CK_HistorialCambiosHabitacion_Reglas;
    IF OBJECT_ID(N'dbo.CK_HistorialCambiosHabitacion_Motivo', N'C') IS NOT NULL
        ALTER TABLE dbo.HistorialCambiosHabitacion DROP CONSTRAINT CK_HistorialCambiosHabitacion_Motivo;
    IF OBJECT_ID(N'dbo.FK_HistorialCambiosHabitacion_HabAnterior', N'F') IS NOT NULL
        ALTER TABLE dbo.HistorialCambiosHabitacion DROP CONSTRAINT FK_HistorialCambiosHabitacion_HabAnterior;
    IF OBJECT_ID(N'dbo.FK_HistorialCambiosHabitacion_HabNueva', N'F') IS NOT NULL
        ALTER TABLE dbo.HistorialCambiosHabitacion DROP CONSTRAINT FK_HistorialCambiosHabitacion_HabNueva;
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_HistorialCambiosHabitacion_Reserva'
                                           AND object_id = OBJECT_ID(N'dbo.HistorialCambiosHabitacion'))
        DROP INDEX IX_HistorialCambiosHabitacion_Reserva ON dbo.HistorialCambiosHabitacion;

    -- 2. Columnas nuevas: la habitación que ocupa la reserva desde el cambio y la tarifa que paga desde ahí
    EXEC(N'ALTER TABLE dbo.HistorialCambiosHabitacion ADD HabitacionID INT NULL, TarifaNoche DECIMAL(10,2) NULL;');
    EXEC(N'UPDATE dbo.HistorialCambiosHabitacion
           SET HabitacionID = HabitacionNuevaID,
               TarifaNoche  = CASE WHEN Motivo = 0 THEN TarifaNueva ELSE TarifaAnterior END;');

    -- 3. Registro inicial de cada reserva que ya tenía cambios: la habitación y la tarifa con las que se
    --    reservó (las "anteriores" de su primer cambio), con la fecha de registro de la reserva
    EXEC(N'INSERT INTO dbo.HistorialCambiosHabitacion
               (ReservaID, Motivo, Observacion, CargoID, HabitacionAnteriorID, HabitacionNuevaID, TarifaAnterior,
                TarifaNueva, NochesRestantes, FechaCambio, HabitacionID, TarifaNoche)
           SELECT p.ReservaID, 2, NULL, NULL, p.HabitacionAnteriorID, p.HabitacionAnteriorID, p.TarifaAnterior,
                  p.TarifaAnterior, DATEDIFF(DAY, r.FechaIngreso, r.FechaEgreso), r.FechaRegistro,
                  p.HabitacionAnteriorID, p.TarifaAnterior
           FROM (SELECT h.ReservaID, h.HabitacionAnteriorID, h.TarifaAnterior,
                        ROW_NUMBER() OVER (PARTITION BY h.ReservaID ORDER BY h.FechaCambio, h.CambioID) AS Orden
                 FROM dbo.HistorialCambiosHabitacion h) p
           INNER JOIN dbo.Reservas r ON r.ReservaID = p.ReservaID
           WHERE p.Orden = 1;');

    -- 4. Fuera las dos columnas de habitación y las dos de tarifa
    EXEC(N'ALTER TABLE dbo.HistorialCambiosHabitacion ALTER COLUMN HabitacionID INT NOT NULL;');
    EXEC(N'ALTER TABLE dbo.HistorialCambiosHabitacion ALTER COLUMN TarifaNoche DECIMAL(10,2) NOT NULL;');
    EXEC(N'ALTER TABLE dbo.HistorialCambiosHabitacion DROP COLUMN HabitacionAnteriorID, HabitacionNuevaID, TarifaAnterior, TarifaNueva;');

    -- 5. Una sola FK a Habitaciones, reglas e índices del diseño nuevo
    EXEC(N'CREATE NONCLUSTERED INDEX [IX_HistorialCambiosHabitacion_Reserva] ON [dbo].[HistorialCambiosHabitacion]
(
	[ReservaID] ASC,
	[FechaCambio] ASC,
	[CambioID] ASC
)
INCLUDE([HabitacionID],[Motivo],[TarifaNoche]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
    EXEC(N'CREATE UNIQUE NONCLUSTERED INDEX [UX_HistorialCambiosHabitacion_Inicial] ON [dbo].[HistorialCambiosHabitacion]
(
	[ReservaID] ASC
)
WHERE ([Motivo]=(2))
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]');
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [FK_HistorialCambiosHabitacion_Habitaciones] FOREIGN KEY([HabitacionID])
REFERENCES [dbo].[Habitaciones] ([HabitacionID])');
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [CK_HistorialCambiosHabitacion_Motivo] CHECK  (([Motivo]=(0) OR [Motivo]=(1) OR [Motivo]=(2)))');
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [CK_HistorialCambiosHabitacion_Tarifa] CHECK  (([TarifaNoche]>(0)))');
    EXEC(N'ALTER TABLE [dbo].[HistorialCambiosHabitacion]  WITH CHECK ADD  CONSTRAINT [CK_HistorialCambiosHabitacion_Reglas] CHECK  (([Motivo]=(0) AND [CargoID] IS NOT NULL AND [NochesRestantes]>(0) OR ([Motivo]=(1) OR [Motivo]=(2)) AND [CargoID] IS NULL))');
END
GO

/* ----------------------------------------------------------------------------
   3c. USUARIOS: Users.RolID pasa a ser clave foránea a Roles. La versión 1 no la
       tenía: que el rol del usuario existiera solo lo controlaba la aplicación
       (y el SP que elimina un rol en uso). Se agrega si todavía no existe y si no
       hay usuarios apuntando a un rol inexistente; si los hay, avisa y no la crea.
---------------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.FK_Users_Roles', N'F') IS NULL
BEGIN
    IF EXISTS (SELECT 1 FROM dbo.Users u WHERE NOT EXISTS (SELECT 1 FROM dbo.Roles r WHERE r.RolID = u.RolID))
        PRINT N'AVISO: hay usuarios con un RolID que no existe en Roles; no se creó FK_Users_Roles.';
    ELSE
    BEGIN
        EXEC(N'ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RolID])
    REFERENCES [dbo].[Roles] ([RolID])');
        EXEC(N'ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]');
    END
END
GO

/* ----------------------------------------------------------------------------
   3d. BITÁCORA DE RESERVAS: nombres de las operaciones de cambio de habitación.
       "Upgrade" pasa a ser "Cambio de habitación por huésped" y "Cambio
       administrativo" pasa a ser "Cambio de habitación administrativo". En una base
       que ya tenía la bitácora se agranda la columna Operacion, se renombran los
       registros existentes (con el trigger de solo lectura deshabilitado mientras
       tanto) y se vuelve a crear la restricción con los nombres nuevos.
---------------------------------------------------------------------------- */
IF COL_LENGTH(N'dbo.Bitacora_Reservas', N'Operacion') < 80
   OR EXISTS (SELECT 1 FROM sys.check_constraints
              WHERE name = N'CK_BitacoraReservas_Operacion' AND definition LIKE N'%Upgrade%')
BEGIN
    IF OBJECT_ID(N'dbo.CK_BitacoraReservas_Operacion', N'C') IS NOT NULL
        ALTER TABLE dbo.Bitacora_Reservas DROP CONSTRAINT CK_BitacoraReservas_Operacion;
    ALTER TABLE dbo.Bitacora_Reservas ALTER COLUMN Operacion NVARCHAR(40) NOT NULL;

    IF OBJECT_ID(N'dbo.TR_BitacoraReservas_SoloLectura', N'TR') IS NOT NULL
        EXEC(N'DISABLE TRIGGER dbo.TR_BitacoraReservas_SoloLectura ON dbo.Bitacora_Reservas');
    EXEC(N'UPDATE dbo.Bitacora_Reservas
          SET Operacion = CASE Operacion WHEN N''Upgrade'' THEN N''Cambio de habitación por huésped''
                                         ELSE N''Cambio de habitación administrativo'' END
          WHERE Operacion IN (N''Upgrade'', N''Cambio administrativo'')');
    IF OBJECT_ID(N'dbo.TR_BitacoraReservas_SoloLectura', N'TR') IS NOT NULL
        EXEC(N'ENABLE TRIGGER dbo.TR_BitacoraReservas_SoloLectura ON dbo.Bitacora_Reservas');

    EXEC(N'ALTER TABLE [dbo].[Bitacora_Reservas]  WITH CHECK ADD  CONSTRAINT [CK_BitacoraReservas_Operacion] CHECK  (([Operacion]=N''Rollback'' OR [Operacion]=N''Cambio de habitación'' OR [Operacion]=N''Cambio de habitación administrativo'' OR [Operacion]=N''Cambio de habitación por huésped'' OR [Operacion]=N''Renovación'' OR [Operacion]=N''Check-out'' OR [Operacion]=N''Check-in'' OR [Operacion]=N''No-show'' OR [Operacion]=N''Cancelación'' OR [Operacion]=N''Modificación'' OR [Operacion]=N''Alta'' OR [Operacion]=N''Carga inicial''))');
END
GO

/* ----------------------------------------------------------------------------
   4. FUNCIONES
   ---------------------------------------------------------------------------- */
/* ============================================================================
   TARIFA VIGENTE DE UNA RESERVA
   ============================================================================ */
-- La del último registro del historial de habitaciones de la reserva (un upgrade la cambia; un cambio
-- administrativo la mantiene); si la reserva nunca cambió de habitación, la tarifa con la que se reservó.
CREATE OR ALTER FUNCTION [dbo].[fn_TarifaVigenteReserva] (@ReservaID INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Tarifa DECIMAL(10,2);

    SELECT TOP (1) @Tarifa = h.TarifaNoche
    FROM dbo.HistorialCambiosHabitacion h
    WHERE h.ReservaID = @ReservaID
    ORDER BY h.FechaCambio DESC, h.CambioID DESC;

    IF @Tarifa IS NULL
        SELECT @Tarifa = r.TarifaNoche FROM dbo.Reservas r WHERE r.ReservaID = @ReservaID;

    RETURN @Tarifa;
END
GO

/* ----------------------------------------------------------------------------
   5. PROCEDIMIENTOS ALMACENADOS (sistema y negocio)
   ---------------------------------------------------------------------------- */
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
CREATE OR ALTER PROCEDURE [dbo].[sp_AnalisisDemanda]
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
CREATE OR ALTER PROCEDURE [dbo].[sp_CancelarReserva]
    @ReservaID INT,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Reservas
    SET Estado = 3 -- Cancelada
    WHERE ReservaID = @ReservaID AND Estado = 0; -- solo se puede cancelar si sigue Confirmada

    SET @Resultado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END
GO
-- El personal de servicio confirma la entrega. @Resultado: 1 confirmado, 0 no existía o ya estaba entregado.
CREATE OR ALTER PROCEDURE [dbo].[sp_ConfirmarEntregaPedido]
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
CREATE OR ALTER PROCEDURE [dbo].[sp_CrearReserva]
    @HuespedTitularID INT,
    @HabitacionID INT,
    @FechaIngreso DATE,
    @FechaEgreso DATE,
    @CantidadAdultos INT,
    @CantidadNinos INT,
    @TarifaNoche DECIMAL(10,2),
    @MontoTotal DECIMAL(10,2),
    @UsuarioRegistroDNI INT,
    @NuevaReservaID INT OUTPUT,
    @Disponible BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        IF EXISTS (
            SELECT 1 FROM Reservas WITH (UPDLOCK, HOLDLOCK)
            WHERE HabitacionID = @HabitacionID
              AND Estado IN (0, 1)
              AND FechaIngreso < @FechaEgreso
              AND FechaEgreso > @FechaIngreso
        )
        BEGIN
            SET @Disponible = 0;
            SET @NuevaReservaID = NULL;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO Reservas (HuespedTitularID, HabitacionID, FechaIngreso, FechaEgreso, Estado,
                               CantidadAdultos, CantidadNinos, TarifaNoche, MontoTotal, MontoOriginal,
                               UsuarioRegistroDNI, FechaRegistro)
        VALUES (@HuespedTitularID, @HabitacionID, @FechaIngreso, @FechaEgreso, 0,
                @CantidadAdultos, @CantidadNinos, @TarifaNoche, @MontoTotal, @MontoTotal,
                @UsuarioRegistroDNI, GETDATE());

        SET @NuevaReservaID = CAST(SCOPE_IDENTITY() AS INT);
        SET @Disponible = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        SET @Disponible = 0;
        SET @NuevaReservaID = NULL;
        THROW;
    END CATCH
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_CreateFamilia]
    @Nombre         NVARCHAR(100),
    @Permisos       dbo.IntList READONLY,
    @SubFamilias    dbo.IntList READONLY,
    @NuevoFamiliaID INT OUTPUT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Familias (Nombre) VALUES (@Nombre);
        SET @NuevoFamiliaID = SCOPE_IDENTITY();

        INSERT INTO FamiliaPermiso (FamiliaID, PermisoID)
        SELECT @NuevoFamiliaID, ID FROM @Permisos;

        INSERT INTO FamiliaIntegrada (FamiliaID, FamiliaPadreID)
        SELECT ID, @NuevoFamiliaID FROM @SubFamilias;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK; THROW;
    END CATCH
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_CreateRol]
    @Nombre     NVARCHAR(100),
    @Familias   dbo.IntList READONLY,
    @Permisos   dbo.IntList READONLY,
    @NuevoRolID INT OUTPUT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Roles (Nombre) VALUES (@Nombre);
        SET @NuevoRolID = SCOPE_IDENTITY();

        INSERT INTO RolFamilia (RolID, FamiliaID)
        SELECT @NuevoRolID, ID FROM @Familias;

        INSERT INTO RolPermiso (RolID, PermisoID)
        SELECT @NuevoRolID, ID FROM @Permisos;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK; THROW;
    END CATCH
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_DeleteFamilia] @FamiliaID INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM RolFamilia       WHERE FamiliaID = @FamiliaID)
    OR EXISTS (SELECT 1 FROM FamiliaIntegrada WHERE FamiliaID = @FamiliaID)
        RAISERROR('La familia está siendo utilizada.', 16, 1);
    ELSE
    BEGIN
        DELETE FROM FamiliaPermiso   WHERE FamiliaID = @FamiliaID;
        DELETE FROM FamiliaIntegrada WHERE FamiliaPadreID = @FamiliaID;
        DELETE FROM Familias         WHERE FamiliaID = @FamiliaID;
    END
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_DeleteRol] @RolID INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Users WHERE RolID = @RolID)
        RAISERROR('El rol está asignado a usuarios.', 16, 1);
    ELSE
    BEGIN
        DELETE FROM RolPermiso WHERE RolID = @RolID;
        DELETE FROM RolFamilia WHERE RolID = @RolID;
        DELETE FROM Roles      WHERE RolID = @RolID;
    END
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_EjecutarBackup]
    @RutaArchivo NVARCHAR(260),
    @NombreBaseDatos NVARCHAR(100)
AS
    BACKUP DATABASE @NombreBaseDatos
    TO DISK = @RutaArchivo
    WITH FORMAT, INIT,
         NAME = 'Horizon Backup',
         SKIP, NOREWIND, NOUNLOAD, CHECKSUM, STATS = 10;
GO
-- Renovación: las noches agregadas se cobran a la tarifa VIGENTE (si hubo upgrade, la nueva).
-- Mismo contrato que la versión original del SP.
CREATE OR ALTER PROCEDURE [dbo].[sp_ExtenderEstadia]
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
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllFamilias]
AS
BEGIN
    -- RS0: todas las familias con su padre (NULL si es raíz)
    -- LEFT JOIN porque una familia raíz no tiene fila en FamiliaIntegrada
    SELECT f.FamiliaID, f.Nombre, fi.FamiliaPadreID
    FROM Familias f
    LEFT JOIN FamiliaIntegrada fi ON f.FamiliaID = fi.FamiliaID;

    -- RS1: permisos directos por familia
    SELECT fp.FamiliaID, p.PermisoID, p.Nombre AS PermisoNombre, p.Descripcion
    FROM FamiliaPermiso fp
    INNER JOIN Permisos p ON fp.PermisoID = p.PermisoID;
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllPermisos]
AS
    SELECT PermisoID, Nombre AS PermisoNombre, Descripcion FROM Permisos;
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllRolesConEstructura]
AS
BEGIN
    SELECT RolID, Nombre FROM Roles;                               -- RS0

    SELECT RolID, FamiliaID FROM RolFamilia;                       -- RS1

    SELECT rp.RolID, p.PermisoID, p.Nombre AS PermisoNombre,       -- RS2
           p.Descripcion
    FROM RolPermiso rp
    INNER JOIN Permisos p ON rp.PermisoID = p.PermisoID;

    -- RS3: familias con FamiliaPadreID — mismo formato que EnsamblarFamilias espera
    SELECT f.FamiliaID, f.Nombre, fi.FamiliaPadreID
    FROM Familias f
    LEFT JOIN FamiliaIntegrada fi ON f.FamiliaID = fi.FamiliaID;

    -- RS4: permisos de familias — mismo formato que EnsamblarFamilias espera
    SELECT fp.FamiliaID, p.PermisoID, p.Nombre AS PermisoNombre,
           p.Descripcion
    FROM FamiliaPermiso fp
    INNER JOIN Permisos p ON fp.PermisoID = p.PermisoID;
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllTablesParaDV]
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
    SELECT * FROM Facturas;                   -- RS14
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
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAncestrosFamilia]
    @FamiliaID INT
AS
BEGIN
    -- RS0: familias que contienen a @FamiliaID, directa o transitivamente
    ;WITH FamiliasAncestros AS (
        SELECT FamiliaPadreID AS FamiliaID
        FROM FamiliaIntegrada
        WHERE FamiliaID = @FamiliaID

        UNION ALL

        SELECT fi.FamiliaPadreID
        FROM FamiliaIntegrada fi
        INNER JOIN FamiliasAncestros fa ON fi.FamiliaID = fa.FamiliaID
    )
    SELECT DISTINCT FamiliaID FROM FamiliasAncestros;

    -- RS1: roles que contienen a @FamiliaID, directa o transitivamente
    -- (incluye el caso en que el rol la contiene directamente)
    ;WITH FamiliasIncluyendoSelf AS (
        SELECT @FamiliaID AS FamiliaID
        UNION ALL
        SELECT fi.FamiliaPadreID
        FROM FamiliaIntegrada fi
        INNER JOIN FamiliasIncluyendoSelf f ON fi.FamiliaID = f.FamiliaID
    )
    SELECT DISTINCT rf.RolID
    FROM RolFamilia rf
    INNER JOIN FamiliasIncluyendoSelf f ON rf.FamiliaID = f.FamiliaID;
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_GetDVTodo]
AS SELECT Tabla, DVH, DVV FROM DV ORDER BY Tabla;
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_GetHabitacionesDisponibles]
    @FechaIngreso DATE,
    @FechaEgreso DATE,
    @TipoHabitacionID INT = NULL,
    @CapacidadMinima INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT h.HabitacionID, h.NroHabitacion, h.Estado,
           p.PisoID, p.Numero, p.Nombre AS NombrePiso,
           t.TipoHabitacionID, t.Nombre AS NombreTipo, t.Descripcion, t.Capacidad, t.TarifaNoche
    FROM Habitaciones h
    INNER JOIN Pisos p ON h.PisoID = p.PisoID
    INNER JOIN TiposHabitacion t ON h.TipoHabitacionID = t.TipoHabitacionID
    WHERE h.Estado <> 4 -- 4 = FueraDeServicio (mantenimiento)
      AND NOT EXISTS (
              SELECT 1 FROM Reservas r
              WHERE r.HabitacionID = h.HabitacionID
                AND r.Estado IN (0, 1) -- Confirmada, EnCurso
                AND r.FechaIngreso < @FechaEgreso
                AND r.FechaEgreso > @FechaIngreso
          )
      AND (@TipoHabitacionID IS NULL OR h.TipoHabitacionID = @TipoHabitacionID)
      AND (@CapacidadMinima IS NULL OR t.Capacidad >= @CapacidadMinima)
    ORDER BY p.Numero, h.NroHabitacion;
END
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
CREATE OR ALTER PROCEDURE [dbo].[sp_GetHabitacionesParaCambio]
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
CREATE OR ALTER PROCEDURE [dbo].[sp_GetRolByID]
    @RolID INT
AS
BEGIN
    -- RS0: el rol
    SELECT RolID, Nombre FROM Roles WHERE RolID = @RolID;

    -- RS1: permisos directos del rol
    SELECT p.PermisoID, p.Nombre AS PermisoNombre, p.Descripcion
    FROM RolPermiso rp
    INNER JOIN Permisos p ON rp.PermisoID = p.PermisoID
    WHERE rp.RolID = @RolID;

    -- Tabla temporal: CTE una sola vez
    ;WITH FamiliasDelRol AS (
        SELECT f.FamiliaID, f.Nombre, CAST(NULL AS INT) AS FamiliaPadreID
        FROM RolFamilia rf
        INNER JOIN Familias f ON rf.FamiliaID = f.FamiliaID
        WHERE rf.RolID = @RolID

        UNION ALL

        SELECT f.FamiliaID, f.Nombre, fi.FamiliaPadreID
        FROM FamiliaIntegrada fi
        INNER JOIN Familias f         ON fi.FamiliaID      = f.FamiliaID
        INNER JOIN FamiliasDelRol fdr ON fi.FamiliaPadreID = fdr.FamiliaID
    )
    SELECT FamiliaID, Nombre, FamiliaPadreID
    INTO #FamiliasTemp
    FROM FamiliasDelRol;

    -- RS2: familias con su relación padre-hijo
    SELECT FamiliaID, Nombre, FamiliaPadreID FROM #FamiliasTemp;

    -- RS3: permisos de esas familias — reutiliza #FamiliasTemp sin repetir CTE
    SELECT fp.FamiliaID, p.PermisoID, p.Nombre AS PermisoNombre, p.Descripcion
    FROM FamiliaPermiso fp
    INNER JOIN Permisos p       ON fp.PermisoID = p.PermisoID
    INNER JOIN #FamiliasTemp ft ON fp.FamiliaID = ft.FamiliaID;

    DROP TABLE #FamiliasTemp;
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_ModificarReserva]
    @ReservaID INT,
    @FechaIngreso DATE,
    @FechaEgreso DATE,
    @CantidadAdultos INT,
    @CantidadNinos INT,
    @Disponible BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @HabitacionID INT, @TarifaNoche DECIMAL(10,2), @Estado INT;

        SELECT @HabitacionID = HabitacionID, @TarifaNoche = TarifaNoche, @Estado = Estado
        FROM Reservas WHERE ReservaID = @ReservaID;

        IF @HabitacionID IS NULL OR @Estado <> 0 -- Confirmada
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
              AND FechaIngreso < @FechaEgreso
              AND FechaEgreso > @FechaIngreso
        )
        BEGIN
            SET @Disponible = 0;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        DECLARE @NuevoMonto DECIMAL(10,2) = @TarifaNoche * DATEDIFF(DAY, @FechaIngreso, @FechaEgreso);

        UPDATE Reservas
        SET FechaIngreso = @FechaIngreso,
            FechaEgreso = @FechaEgreso,
            CantidadAdultos = @CantidadAdultos,
            CantidadNinos = @CantidadNinos,
            MontoTotal = @NuevoMonto,
            MontoOriginal = @NuevoMonto
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
CREATE OR ALTER PROCEDURE [dbo].[sp_ModifyFamilia]
    @FamiliaID   INT,
    @Nombre      NVARCHAR(100),
    @Permisos    dbo.IntList READONLY,
    @SubFamilias dbo.IntList READONLY
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Familias SET Nombre = @Nombre WHERE FamiliaID = @FamiliaID;

        DELETE FROM FamiliaPermiso   WHERE FamiliaID    = @FamiliaID;
        DELETE FROM FamiliaIntegrada WHERE FamiliaPadreID = @FamiliaID;

        INSERT INTO FamiliaPermiso (FamiliaID, PermisoID)
            SELECT @FamiliaID, ID FROM @Permisos;

        INSERT INTO FamiliaIntegrada (FamiliaID, FamiliaPadreID)
            SELECT ID, @FamiliaID FROM @SubFamilias;

        COMMIT;
    END TRY
    BEGIN CATCH ROLLBACK; THROW; END CATCH
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_ModifyRol]
    @RolID    INT,
    @Nombre   NVARCHAR(100),
    @Familias dbo.IntList READONLY,
    @Permisos dbo.IntList READONLY
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Roles SET Nombre = @Nombre WHERE RolID = @RolID;

        DELETE FROM RolFamilia WHERE RolID = @RolID;
        DELETE FROM RolPermiso  WHERE RolID = @RolID;

        INSERT INTO RolFamilia (RolID, FamiliaID)
            SELECT @RolID, ID FROM @Familias;

        INSERT INTO RolPermiso (RolID, PermisoID)
            SELECT @RolID, ID FROM @Permisos;

        COMMIT;
    END TRY
    BEGIN CATCH ROLLBACK; THROW; END CATCH
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_ProcesarNoShows]
    @CantidadCanceladas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @HoraCheckIn TIME, @GraciaHoras DECIMAL(5,2);
    SELECT @HoraCheckIn = HoraCheckIn, @GraciaHoras = GraciaNoShowHoras FROM ConfiguracionHotel;

    DECLARE @SegundosCheckIn INT =
        DATEPART(HOUR, @HoraCheckIn) * 3600 + DATEPART(MINUTE, @HoraCheckIn) * 60 + DATEPART(SECOND, @HoraCheckIn);

    UPDATE Reservas
    SET Estado = 3 -- Cancelada (no-show)
    WHERE Estado = 0 -- Confirmada
      AND FechaIngreso <= CAST(GETDATE() AS DATE)
      AND GETDATE() > DATEADD(MINUTE, CAST(@GraciaHoras * 60 AS INT),
                               DATEADD(SECOND, @SegundosCheckIn, CAST(FechaIngreso AS DATETIME)));

    SET @CantidadCanceladas = @@ROWCOUNT;
END
GO
-- Registra el cambio de habitación en UNA sola llamada y UNA sola transacción, validando todo con los datos
-- del momento y bajo bloqueo (la BLL ya validó permiso y datos de la solicitud):
--   historial (+ cargo si es upgrade) + nueva habitación en la reserva (dispara la bitácora)
--   + estados físicos si el huésped ya está alojado (la anterior queda En limpieza, salvo que esté Fuera de servicio).
-- Upgrade (solo en curso): @Monto = (tarifa nueva - tarifa vigente) x noches restantes; la descripción del cargo
-- se arma acá con los datos de la habitación nueva.
-- Reubicación: en curso, o Confirmada sin check-in (se cambia la habitación de toda la estadía).
-- @Resultado: 0 OK, 1 la reserva no admite el cambio, 2 habitación no disponible, 3 misma habitación,
--             4 upgrade sin noches restantes (egresa hoy), 5 upgrade a una tarifa que no es mayor,
--             6 capacidad insuficiente, 7 motivo inválido.
CREATE OR ALTER PROCEDURE [dbo].[sp_RegistrarCambioHabitacion]
    @ReservaID         INT,
    @HabitacionNuevaID INT,
    @Motivo            INT,             -- 0 = Upgrade solicitado, 1 = Cambio administrativo
    @Observacion       NVARCHAR(200),
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

        -- Validación 1: en curso -> upgrade o reubicación; Confirmada (sin check-in) -> solo reubicación del hotel
        IF @Estado IS NULL OR NOT (@Estado = 1 OR (@Estado = 0 AND @Motivo = 1))
        BEGIN SET @Resultado = 1; ROLLBACK TRANSACTION; RETURN; END

        -- Validación 2: tiene que ser otra habitación
        IF @HabitacionAnterior = @HabitacionNuevaID
        BEGIN SET @Resultado = 3; ROLLBACK TRANSACTION; RETURN; END

        -- Validación 3: el upgrade necesita al menos una noche por delante
        DECLARE @Desde DATE = CASE WHEN @FechaIngreso > @Hoy THEN @FechaIngreso ELSE @Hoy END;
        DECLARE @NochesRestantes INT = DATEDIFF(DAY, @Desde, @FechaEgreso);
        IF @NochesRestantes < 0 SET @NochesRestantes = 0;
        IF @Motivo = 0 AND @NochesRestantes = 0
        BEGIN SET @Resultado = 4; ROLLBACK TRANSACTION; RETURN; END

        DECLARE @TarifaVigente DECIMAL(10,2) = dbo.fn_TarifaVigenteReserva(@ReservaID);
        DECLARE @TarifaNueva DECIMAL(10,2), @Capacidad INT, @NroNueva NVARCHAR(10), @TipoNuevo NVARCHAR(50);
        SELECT @TarifaNueva = t.TarifaNoche, @Capacidad = t.Capacidad, @NroNueva = h.NroHabitacion, @TipoNuevo = t.Nombre
        FROM dbo.Habitaciones h
        INNER JOIN dbo.TiposHabitacion t ON t.TipoHabitacionID = h.TipoHabitacionID
        WHERE h.HabitacionID = @HabitacionNuevaID;

        -- Validación 4: capacidad para todos los huéspedes de la estadía
        IF @Capacidad IS NULL OR @Capacidad < @Huespedes
        BEGIN SET @Resultado = 6; ROLLBACK TRANSACTION; RETURN; END

        -- Validación 5: el upgrade es solo a una tarifa mayor a la vigente
        IF @Motivo = 0 AND @TarifaNueva <= @TarifaVigente
        BEGIN SET @Resultado = 5; ROLLBACK TRANSACTION; RETURN; END

        -- Validación 6: la habitación nueva tiene que estar libre hasta el egreso (bajo bloqueo)
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

            -- "Upgrade a hab. 305 (Suite): 3 noches x $12.500,00 de diferencia" (máximo 200 caracteres)
            DECLARE @DescripcionCargo NVARCHAR(MAX) =
                N'Upgrade a hab. ' + @NroNueva + N' (' + @TipoNuevo + N'): '
                + CASE WHEN @NochesRestantes = 1 THEN N'1 noche' ELSE CAST(@NochesRestantes AS NVARCHAR(10)) + N' noches' END
                + N' x $' + FORMAT(@TarifaNueva - @TarifaVigente, 'N2', 'es-AR') + N' de diferencia';
            IF LEN(@DescripcionCargo) > 200
                SET @DescripcionCargo = LEFT(@DescripcionCargo, 199) + NCHAR(8230);   -- "…"

            INSERT INTO dbo.CargosAdicionales (ReservaID, Tipo, Descripcion, FechaHora, Monto, UsuarioRegistroDNI)
            VALUES (@ReservaID, 1, @DescripcionCargo, GETDATE(), @Monto, @UsuarioDNI);
            SET @CargoID = CAST(SCOPE_IDENTITY() AS INT);
        END

        -- Historial de habitaciones de la reserva: un registro por cada habitación que ocupa, con la tarifa
        -- que paga desde ese momento. El primer cambio deja también el registro de la habitación con la
        -- que se reservó (Motivo 2 = asignación inicial), así la habitación anterior de cada cambio es la
        -- del registro previo y la tabla tiene una sola relación con Habitaciones.
        IF NOT EXISTS (SELECT 1 FROM dbo.HistorialCambiosHabitacion WHERE ReservaID = @ReservaID)
            INSERT INTO dbo.HistorialCambiosHabitacion
                (ReservaID, HabitacionID, Motivo, Observacion, CargoID, TarifaNoche, NochesRestantes, FechaCambio)
            SELECT r.ReservaID, @HabitacionAnterior, 2, NULL, NULL, @TarifaVigente,
                   DATEDIFF(DAY, r.FechaIngreso, r.FechaEgreso), r.FechaRegistro
            FROM dbo.Reservas r
            WHERE r.ReservaID = @ReservaID;

        -- Upgrade: desde ahora paga la tarifa nueva. Cambio administrativo: sigue pagando la misma.
        INSERT INTO dbo.HistorialCambiosHabitacion
            (ReservaID, HabitacionID, Motivo, Observacion, CargoID, TarifaNoche, NochesRestantes, FechaCambio)
        VALUES (@ReservaID, @HabitacionNuevaID, @Motivo, @Observacion, @CargoID,
                CASE WHEN @Motivo = 0 THEN @TarifaNueva ELSE @TarifaVigente END, @NochesRestantes, GETDATE());
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
CREATE OR ALTER PROCEDURE [dbo].[sp_RegistrarCheckIn]
    @ReservaID INT,
    @FechaHora DATETIME,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @HabitacionID INT;

    -- Obtenemos el ID de la habitación asegurando que la reserva esté Confirmada (0)
    SELECT @HabitacionID = HabitacionID 
    FROM Reservas 
    WHERE ReservaID = @ReservaID AND Estado = 0;

    -- Si no existe la reserva o ya se le hizo Check-In, salimos inmediatamente
    IF @HabitacionID IS NULL
    BEGIN
        SET @Resultado = 0;
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY
        -- 1. Actualizamos la reserva a "EnCurso" (1)
        UPDATE Reservas 
        SET CheckIn = @FechaHora, Estado = 1 
        WHERE ReservaID = @ReservaID;

        -- 2. Sincronizamos el estado físico de la habitación a "Ocupada" (2)
        UPDATE Habitaciones 
        SET Estado = 2 
        WHERE HabitacionID = @HabitacionID;

        SET @Resultado = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
    END CATCH
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_RegistrarCheckOut]
    @ReservaID INT,
    @FechaHora DATETIME,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @HabitacionID INT;

    -- Obtenemos la habitación asegurando que la reserva esté activa ("EnCurso" = 1)
    SELECT @HabitacionID = HabitacionID 
    FROM Reservas 
    WHERE ReservaID = @ReservaID AND Estado = 1;

    -- Si la reserva no existe o no estaba en curso, abortamos de forma segura
    IF @HabitacionID IS NULL
    BEGIN
        SET @Resultado = 0;
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY
        -- 1. Finalizamos la reserva (Estado 2 = Finalizada)
        UPDATE Reservas 
        SET CheckOut = @FechaHora, Estado = 2 
        WHERE ReservaID = @ReservaID;

        -- 2. Cambiamos el estado físico de la habitación a "EnLimpieza" (3)
        UPDATE Habitaciones 
        SET Estado = 3 
        WHERE HabitacionID = @HabitacionID;

        SET @Resultado = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si algo falla, deshacemos todo para evitar inconsistencias
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
    END CATCH
END
GO
-- Registra el pedido y su cargo en UNA sola llamada y UNA sola transacción.
-- Las reglas que dependen del estado de la base se validan acá, con los datos del momento y bajo bloqueo,
-- para que nada cambie entre la validación y el alta (la BLL ya validó permiso, ítems y cantidades del pedido).
-- La descripción del cargo se arma con los nombres de la carta y el monto con los precios vigentes.
-- @Resultado: 0 OK, 1 reserva no en curso, 2 pedido vacío, con cantidades inválidas o con ítems inexistentes / dados de baja.
CREATE OR ALTER PROCEDURE [dbo].[sp_RegistrarPedidoRoomService]
    @ReservaID   INT,
    @Items       dbo.ItemCantidadList READONLY,
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

        -- Validación 1: la estadía tiene que estar En Curso (UPDLOCK: no se puede cerrar mientras se registra el pedido)
        IF NOT EXISTS (SELECT 1 FROM dbo.Reservas WITH (UPDLOCK, HOLDLOCK) WHERE ReservaID = @ReservaID AND Estado = 1)
        BEGIN SET @Resultado = 1; ROLLBACK TRANSACTION; RETURN; END

        -- Validación 2: al menos un ítem, cantidades positivas y todos activos en la carta
        -- (HOLDLOCK: no se pueden dar de baja mientras se registra el pedido)
        IF NOT EXISTS (SELECT 1 FROM @Items)
           OR EXISTS (SELECT 1 FROM @Items WHERE Cantidad <= 0)
           OR EXISTS (SELECT 1 FROM @Items i
                      LEFT JOIN dbo.ItemsRoomService it WITH (HOLDLOCK) ON it.ItemID = i.ItemID AND it.Activo = 1
                      WHERE it.ItemID IS NULL)
        BEGIN SET @Resultado = 2; ROLLBACK TRANSACTION; RETURN; END

        -- Monto con los precios vigentes y descripción "Room service: 2 x Café, 1 x Tostado" (máximo 200 caracteres)
        DECLARE @Monto DECIMAL(10,2) =
            (SELECT SUM(i.Cantidad * it.PrecioUnitario)
             FROM @Items i INNER JOIN dbo.ItemsRoomService it ON it.ItemID = i.ItemID);

        DECLARE @Descripcion NVARCHAR(MAX) = N'Room service: ' + STUFF(
            (SELECT N', ' + CAST(i.Cantidad AS NVARCHAR(10)) + N' x ' + it.Nombre
             FROM @Items i INNER JOIN dbo.ItemsRoomService it ON it.ItemID = i.ItemID
             ORDER BY it.Nombre
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, N'');
        IF LEN(@Descripcion) > 200
            SET @Descripcion = LEFT(@Descripcion, 199) + NCHAR(8230);   -- "…"

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

    -- Upgrades (la cantidad son las noches que se cobraron con la tarifa nueva).
    -- La habitación y la tarifa anteriores son las del registro previo del historial de la reserva.
    ;WITH hc AS (
        SELECT h.CambioID, h.CargoID, h.NochesRestantes, h.HabitacionID, h.TarifaNoche,
               LAG(h.HabitacionID) OVER (PARTITION BY h.ReservaID ORDER BY h.FechaCambio, h.CambioID) AS HabitacionAnteriorID,
               LAG(h.TarifaNoche)  OVER (PARTITION BY h.ReservaID ORDER BY h.FechaCambio, h.CambioID) AS TarifaAnterior
        FROM dbo.HistorialCambiosHabitacion h
    )
    INSERT INTO #Servicios
    SELECT c.CargoID, c.ReservaID, c.Tipo, c.Descripcion, c.FechaHora, c.Monto, c.UsuarioRegistroDNI,
           2, NULL, NULL, hc.NochesRestantes, NULL,
           ha.NroHabitacion, hn.NroHabitacion, tn.Nombre, hc.TarifaNoche - hc.TarifaAnterior
    FROM dbo.CargosAdicionales c
    INNER JOIN hc                                ON hc.CargoID = c.CargoID
    INNER JOIN dbo.Habitaciones ha               ON ha.HabitacionID = hc.HabitacionAnteriorID
    INNER JOIN dbo.Habitaciones hn               ON hn.HabitacionID = hc.HabitacionID
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
                    -- Habitación en el momento del servicio: la del último registro del historial hasta esa hora
                    (SELECT TOP 1 h2.NroHabitacion
                     FROM dbo.HistorialCambiosHabitacion hc2
                     INNER JOIN dbo.Habitaciones h2 ON h2.HabitacionID = hc2.HabitacionID
                     WHERE hc2.ReservaID = s.ReservaID AND hc2.FechaCambio <= s.FechaHora
                     ORDER BY hc2.FechaCambio DESC, hc2.CambioID DESC),
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
   8. ROLLBACK: recuperar un registro histórico de la bitácora como ACTIVO
   ----------------------------------------------------------------------------
   Copia la foto guardada en Bitacora_Reservas sobre la fila de Reservas (en una transacción).
   El trigger de Reservas registra el cambio como "Rollback" (nuevo registro activo, con
   RollbackDeID = el registro restaurado) y el registro que estaba activo pasa a historia.
   Además se acomoda el estado físico de las habitaciones:
   - si la reserva estaba En curso y deja de estarlo o cambia de habitación, la habitación
     que ocupaba queda En limpieza;
   - si la foto restaurada está En curso, su habitación vuelve a quedar Ocupada.
   Los cargos adicionales y los pagos no se tocan. Si la foto vuelve a otra habitación y la reserva ya
   tiene historial de habitaciones, se agrega el registro de esa habitación (sin cargo y con la misma
   tarifa vigente), así la secuencia de habitaciones de la reserva sigue completa.

   @Resultado: 0 Ok
               1 El registro no existe
               2 El registro ya es el activo
               3 El registro es igual al estado actual (no hay nada que restaurar)
               4 La habitación de la foto está tomada por otra reserva en esas fechas
               5 La habitación de la foto está fuera de servicio (no puede volver a ocuparse)
   ============================================================================ */
CREATE OR ALTER PROCEDURE [dbo].[sp_RollbackReserva]
    @BitacoraID INT,
    @Resultado  INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @ReservaID INT, @Act BIT, @HabitacionFoto INT, @EstadoFoto INT, @Ingreso DATE, @Egreso DATE;

        SELECT @ReservaID = ReservaID, @Act = Act, @HabitacionFoto = HabitacionID, @EstadoFoto = Estado,
               @Ingreso = FechaIngreso, @Egreso = FechaEgreso
        FROM dbo.Bitacora_Reservas
        WHERE BitacoraID = @BitacoraID;

        IF @ReservaID IS NULL
        BEGIN
            SET @Resultado = 1; ROLLBACK TRANSACTION; RETURN;
        END
        IF @Act = 1
        BEGIN
            SET @Resultado = 2; ROLLBACK TRANSACTION; RETURN;
        END

        DECLARE @HabitacionActual INT, @EstadoActual INT;
        SELECT @HabitacionActual = HabitacionID, @EstadoActual = Estado
        FROM dbo.Reservas WITH (UPDLOCK, HOLDLOCK)
        WHERE ReservaID = @ReservaID;

        -- Si la foto es igual a la reserva actual no hay nada que restaurar (y el trigger no registraría nada)
        IF NOT EXISTS (
            SELECT b.HuespedTitularID, b.HabitacionID, b.FechaIngreso, b.FechaEgreso, b.CheckIn, b.CheckOut, b.Estado,
                   b.CantidadAdultos, b.CantidadNinos, b.TarifaNoche, b.MontoTotal, b.UsuarioRegistroDNI,
                   b.FechaRegistro, b.MontoOriginal
            FROM dbo.Bitacora_Reservas b WHERE b.BitacoraID = @BitacoraID
            EXCEPT
            SELECT r.HuespedTitularID, r.HabitacionID, r.FechaIngreso, r.FechaEgreso, r.CheckIn, r.CheckOut, r.Estado,
                   r.CantidadAdultos, r.CantidadNinos, r.TarifaNoche, r.MontoTotal, r.UsuarioRegistroDNI,
                   r.FechaRegistro, r.MontoOriginal
            FROM dbo.Reservas r WHERE r.ReservaID = @ReservaID)
        BEGIN
            SET @Resultado = 3; ROLLBACK TRANSACTION; RETURN;
        END

        -- Una foto Confirmada o En curso vuelve a ocupar su habitación en sus fechas: no puede pisar otra reserva
        IF @EstadoFoto IN (0, 1) AND EXISTS (
            SELECT 1 FROM dbo.Reservas o WITH (UPDLOCK, HOLDLOCK)
            WHERE o.HabitacionID = @HabitacionFoto
              AND o.ReservaID <> @ReservaID
              AND o.Estado IN (0, 1)                 -- Confirmada, En curso
              AND o.FechaIngreso < @Egreso
              AND o.FechaEgreso > @Ingreso)
        BEGIN
            SET @Resultado = 4; ROLLBACK TRANSACTION; RETURN;
        END

        -- (si el huésped ya está alojado en esa misma habitación, se puede restaurar igual)
        IF @EstadoFoto = 1 AND NOT (@EstadoActual = 1 AND @HabitacionActual = @HabitacionFoto)
           AND EXISTS (SELECT 1 FROM dbo.Habitaciones WHERE HabitacionID = @HabitacionFoto AND Estado = 4)
        BEGIN
            SET @Resultado = 5; ROLLBACK TRANSACTION; RETURN;
        END

        -- Historial de habitaciones: la reserva vuelve a la habitación de la foto
        IF @HabitacionFoto <> @HabitacionActual
           AND EXISTS (SELECT 1 FROM dbo.HistorialCambiosHabitacion WHERE ReservaID = @ReservaID)
        BEGIN
            DECLARE @DesdeRollback DATE = CASE WHEN @Ingreso > CAST(GETDATE() AS DATE) THEN @Ingreso ELSE CAST(GETDATE() AS DATE) END;
            INSERT INTO dbo.HistorialCambiosHabitacion
                (ReservaID, HabitacionID, Motivo, Observacion, CargoID, TarifaNoche, NochesRestantes, FechaCambio)
            VALUES (@ReservaID, @HabitacionFoto, 1,
                    N'Rollback de la bitácora de reservas (registro ' + CAST(@BitacoraID AS NVARCHAR(10)) + N')',
                    NULL, dbo.fn_TarifaVigenteReserva(@ReservaID),
                    CASE WHEN DATEDIFF(DAY, @DesdeRollback, @Egreso) > 0 THEN DATEDIFF(DAY, @DesdeRollback, @Egreso) ELSE 0 END,
                    GETDATE());
        END

        -- El trigger de Reservas lee estos valores para registrar la operación "Rollback"
        EXEC sys.sp_set_session_context @key = N'OperacionReserva',   @value = N'Rollback';
        EXEC sys.sp_set_session_context @key = N'RollbackBitacoraID', @value = @BitacoraID;

        UPDATE r
        SET HuespedTitularID   = b.HuespedTitularID,
            HabitacionID       = b.HabitacionID,
            FechaIngreso       = b.FechaIngreso,
            FechaEgreso        = b.FechaEgreso,
            CheckIn            = b.CheckIn,
            CheckOut           = b.CheckOut,
            Estado             = b.Estado,
            CantidadAdultos    = b.CantidadAdultos,
            CantidadNinos      = b.CantidadNinos,
            TarifaNoche        = b.TarifaNoche,
            MontoTotal         = b.MontoTotal,
            UsuarioRegistroDNI = b.UsuarioRegistroDNI,
            FechaRegistro      = b.FechaRegistro,
            MontoOriginal      = b.MontoOriginal
        FROM dbo.Reservas r
        INNER JOIN dbo.Bitacora_Reservas b ON b.BitacoraID = @BitacoraID
        WHERE r.ReservaID = @ReservaID;

        -- Estado físico de las habitaciones (2 Ocupada, 3 En limpieza)
        IF @EstadoActual = 1 AND (@EstadoFoto <> 1 OR @HabitacionActual <> @HabitacionFoto)
            UPDATE dbo.Habitaciones SET Estado = 3 WHERE HabitacionID = @HabitacionActual AND Estado <> 4; -- fuera de servicio se respeta
        IF @EstadoFoto = 1
            UPDATE dbo.Habitaciones SET Estado = 2 WHERE HabitacionID = @HabitacionFoto;

        EXEC sys.sp_set_session_context @key = N'RollbackBitacoraID', @value = NULL;
        EXEC sys.sp_set_session_context @key = N'OperacionReserva',   @value = NULL;

        SET @Resultado = 0;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        EXEC sys.sp_set_session_context @key = N'RollbackBitacoraID', @value = NULL;
        THROW;
    END CATCH
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_UpsertDVTabla]
    @Tabla NVARCHAR(100),
    @DVH   NVARCHAR(MAX),
    @DVV   NVARCHAR(MAX)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM DV WHERE Tabla = @Tabla)
        UPDATE DV SET DVH = @DVH, DVV = @DVV WHERE Tabla = @Tabla;
    ELSE
        INSERT INTO DV (Tabla, DVH, DVV) VALUES (@Tabla, @DVH, @DVV);
END
GO

/* ----------------------------------------------------------------------------
   6. BITÁCORA DE CAMBIOS DE RESERVAS: triggers
   ---------------------------------------------------------------------------- */
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
    DECLARE @RollbackID INT          = CAST(SESSION_CONTEXT(N'RollbackBitacoraID') AS INT);

    -- Solo se acepta una operación informada que exista en la bitácora (si no, se deduce).
    -- "Rollback" solo vale si sp_RollbackReserva informó qué registro restauró.
    IF @Informada NOT IN (N'Alta', N'Modificación', N'Cancelación', N'No-show', N'Check-in', N'Check-out', N'Renovación', N'Rollback')
       OR (@Informada = N'Rollback' AND @RollbackID IS NULL)
        SET @Informada = NULL;

    DECLARE @Cambios TABLE
    (
        ReservaID INT PRIMARY KEY,
        Operacion NVARCHAR(40) NOT NULL,
        CambioID  INT NULL
    );

    INSERT INTO @Cambios (ReservaID, Operacion, CambioID)
    SELECT i.ReservaID,
           CASE
               WHEN d.ReservaID IS NULL THEN N'Alta'
               WHEN @Informada = N'Rollback' THEN N'Rollback'
               WHEN i.HabitacionID <> d.HabitacionID THEN
                    -- Sin el cambio informado por sp_RegistrarCambioHabitacion: se cambió por fuera del sistema
                    CASE WHEN hc.CambioID IS NULL THEN N'Cambio de habitación'
                         WHEN hc.Motivo = 0       THEN N'Cambio de habitación por huésped'
                         ELSE N'Cambio de habitación administrativo' END
               WHEN @Informada IS NOT NULL THEN @Informada
               WHEN d.Estado <> 3 AND i.Estado = 3 THEN N'Cancelación'
               WHEN d.Estado <> 1 AND i.Estado = 1 THEN N'Check-in'
               WHEN d.Estado <> 2 AND i.Estado = 2 THEN N'Check-out'
               WHEN d.Estado = 1  AND i.FechaEgreso <> d.FechaEgreso THEN N'Renovación'
               ELSE N'Modificación'
           END,
           CASE WHEN d.ReservaID IS NOT NULL AND i.HabitacionID <> d.HabitacionID
                     AND ISNULL(@Informada, N'') <> N'Rollback' THEN hc.CambioID END
    FROM inserted i
    LEFT JOIN deleted d ON d.ReservaID = i.ReservaID
    LEFT JOIN dbo.HistorialCambiosHabitacion hc ON hc.CambioID = @CambioID
                                               AND hc.ReservaID = i.ReservaID
                                               AND hc.HabitacionID = i.HabitacionID
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
         Operacion, CambioID, Fecha, Hora, UsuarioDB, UsuarioApp, Act, RollbackDeID)
    SELECT i.ReservaID, i.HuespedTitularID, i.HabitacionID, i.FechaIngreso, i.FechaEgreso, i.CheckIn, i.CheckOut, i.Estado,
           i.CantidadAdultos, i.CantidadNinos, i.TarifaNoche, i.MontoTotal, i.UsuarioRegistroDNI, i.FechaRegistro, i.MontoOriginal,
           c.Operacion, c.CambioID, CAST(@Ahora AS DATE), CAST(DATEADD(MILLISECOND, -DATEPART(MILLISECOND, @Ahora), @Ahora) AS TIME(0)), SUSER_SNAME(),
           -- En el alta, si el sistema no informó el usuario, se toma el que registró la reserva
           COALESCE(@UsuarioApp, CASE WHEN c.Operacion = N'Alta' THEN u.Username END),
           1,
           CASE WHEN c.Operacion = N'Rollback' THEN @RollbackID END
    FROM inserted i
    INNER JOIN @Cambios c ON c.ReservaID = i.ReservaID
    LEFT  JOIN dbo.Users u ON u.DNI = i.UsuarioRegistroDNI;
END
GO
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
       OR UPDATE(UsuarioDB) OR UPDATE(UsuarioApp) OR UPDATE(RollbackDeID)
        THROW 50102, N'La bitácora de reservas es de solo lectura: sus registros los escribe el trigger de Reservas.', 1;
END
GO

/* ----------------------------------------------------------------------------
   7. SEGURIDAD: permisos, familias y roles nuevos (por nombre)
   ---------------------------------------------------------------------------- */
-- Permisos nuevos (42)
INSERT INTO dbo.Permisos (Nombre, Descripcion)
SELECT n.Nombre, n.Descripcion
FROM (VALUES
    (N'VerReservas', N'Acceder a la pantalla Reservas con el listado y el calendario de reservas'),
    (N'RegistrarReserva', N'Registrar reservas nuevas desde Recepción, el listado o el control de habitaciones'),
    (N'ModificarReserva', N'Modificar fechas y composición de reservas pendientes'),
    (N'CancelarReserva', N'Cancelar reservas pendientes'),
    (N'ImprimirComprobante', N'Imprimir el comprobante de una reserva con sus pagos'),
    (N'Cobrar', N'Registrar pagos de reservas (al reservar y saldo pendiente en el check-out)'),
    (N'ExtenderEstadia', N'Renovar (extender) la estadía de una reserva en curso'),
    (N'VerControlHabitaciones', N'Acceder a la pantalla Control de habitaciones'),
    (N'CambiarEstadoHabitacion', N'Marcar habitaciones como disponibles o ponerlas en mantenimiento'),
    (N'RegistrarCheckIn', N'Registrar el check-in de una reserva confirmada'),
    (N'RegistrarCheckOut', N'Registrar el check-out de una reserva en curso'),
    (N'VerHuespedes', N'Acceder al Maestro de Huéspedes'),
    (N'CrearHuesped', N'Registrar huéspedes nuevos'),
    (N'ModificarHuesped', N'Modificar huéspedes existentes'),
    (N'EliminarHuesped', N'Eliminar huéspedes sin reservas asociadas'),
    (N'SerializarHuespedes', N'Guardar en un archivo XML los huéspedes de la grilla'),
    (N'DeserializarHuespedes', N'Cargar en la grilla un archivo XML de huéspedes'),
    (N'VerHabitaciones', N'Acceder a la pantalla Habitaciones'),
    (N'CrearHabitacion', N'Crear habitaciones nuevas'),
    (N'ModificarHabitacion', N'Modificar habitaciones existentes'),
    (N'EliminarHabitacion', N'Eliminar habitaciones sin reservas asociadas'),
    (N'VerTiposHabitacion', N'Acceder a la pantalla Tipos de habitación'),
    (N'CrearTipoHabitacion', N'Crear tipos de habitación nuevos'),
    (N'ModificarTipoHabitacion', N'Modificar tipos de habitación existentes'),
    (N'EliminarTipoHabitacion', N'Eliminar tipos de habitación sin habitaciones asociadas'),
    (N'VerPisos', N'Acceder a la pantalla Pisos'),
    (N'CrearPiso', N'Crear pisos nuevos'),
    (N'ModificarPiso', N'Modificar pisos existentes'),
    (N'EliminarPiso', N'Eliminar pisos sin habitaciones asociadas'),
    (N'VerDashboard', N'Acceder al Dashboard de métricas'),
    (N'GenerarReporteReservas', N'Generar el reporte de reservas desde el Dashboard'),
    (N'VerConfiguracionHotel', N'Acceder a la pantalla Configuración del hotel'),
    (N'ModificarConfiguracionHotel', N'Modificar horarios de check-in/check-out y tolerancia de no-show'),
    (N'RegistrarUpgradeHabitacion', N'Registrar el upgrade de habitación pedido por el huésped (genera un cargo)'),
    (N'ReubicarHuesped', N'Cambiar de habitación a un huésped por motivos del hotel (sin cargo)'),
    (N'RegistrarRoomService', N'Cargar pedidos de servicio a la habitación a la cuenta del huésped'),
    (N'GestionarItemsRoomService', N'Administrar el catálogo de ítems de servicio a la habitación'),
    (N'ConfirmarEntregaRoomService', N'Ver los pedidos pendientes y confirmar su entrega'),
    (N'VerBitacoraReservas', N'Consultar la bitácora de cambios de reservas'),
    (N'GenerarReporteServicios', N'Generar el reporte de servicios consumidos por huésped'),
    (N'GenerarAnalisisDemanda', N'Generar el análisis predictivo de ocupación y demanda'),
    (N'RollbackBitacoraReservas', N'Restaurar un registro histórico de la bitácora de reservas como activo (rollback)')
) AS n (Nombre, Descripcion)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos p WHERE p.Nombre = n.Nombre);

-- Familias nuevas (9)
INSERT INTO dbo.Familias (Nombre)
SELECT n.Nombre
FROM (VALUES
    (N'Configuracion del hotel'),
    (N'Gestion de estadias'),
    (N'Gestion de habitaciones'),
    (N'Gestion de huespedes'),
    (N'Gestion de reservas'),
    (N'Reportes'),
    (N'Auditoria de habitaciones'),
    (N'Cargos adicionales'),
    (N'Servicio a la habitacion')
) AS n (Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Familias f WHERE f.Nombre = n.Nombre);

-- Roles nuevos (2)
INSERT INTO dbo.Roles (Nombre)
SELECT n.Nombre
FROM (VALUES
    (N'Recepcionista'),
    (N'Personal de servicio')
) AS n (Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Roles r WHERE r.Nombre = n.Nombre);

-- Permisos de las familias (42)
INSERT INTO dbo.FamiliaPermiso (FamiliaID, PermisoID)
SELECT DISTINCT f.FamiliaID, p.PermisoID
FROM (VALUES
    (N'Configuracion del hotel', N'VerConfiguracionHotel'),
    (N'Configuracion del hotel', N'ModificarConfiguracionHotel'),
    (N'Gestion de estadias', N'ExtenderEstadia'),
    (N'Gestion de estadias', N'VerControlHabitaciones'),
    (N'Gestion de estadias', N'CambiarEstadoHabitacion'),
    (N'Gestion de estadias', N'RegistrarCheckIn'),
    (N'Gestion de estadias', N'RegistrarCheckOut'),
    (N'Gestion de estadias', N'ReubicarHuesped'),
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
    (N'Gestion de huespedes', N'VerHuespedes'),
    (N'Gestion de huespedes', N'CrearHuesped'),
    (N'Gestion de huespedes', N'ModificarHuesped'),
    (N'Gestion de huespedes', N'EliminarHuesped'),
    (N'Gestion de huespedes', N'SerializarHuespedes'),
    (N'Gestion de huespedes', N'DeserializarHuespedes'),
    (N'Gestion de reservas', N'VerReservas'),
    (N'Gestion de reservas', N'RegistrarReserva'),
    (N'Gestion de reservas', N'ModificarReserva'),
    (N'Gestion de reservas', N'CancelarReserva'),
    (N'Gestion de reservas', N'ImprimirComprobante'),
    (N'Gestion de reservas', N'Cobrar'),
    (N'Reportes', N'VerDashboard'),
    (N'Reportes', N'GenerarReporteReservas'),
    (N'Reportes', N'GenerarReporteServicios'),
    (N'Reportes', N'GenerarAnalisisDemanda'),
    (N'Auditoria de habitaciones', N'VerBitacoraReservas'),
    (N'Auditoria de habitaciones', N'RollbackBitacoraReservas'),
    (N'Cargos adicionales', N'RegistrarUpgradeHabitacion'),
    (N'Cargos adicionales', N'RegistrarRoomService'),
    (N'Cargos adicionales', N'GestionarItemsRoomService'),
    (N'Servicio a la habitacion', N'ConfirmarEntregaRoomService')
) AS n (Familia, Permiso)
INNER JOIN dbo.Familias f ON f.Nombre = n.Familia
INNER JOIN dbo.Permisos p ON p.Nombre = n.Permiso
WHERE NOT EXISTS (SELECT 1 FROM dbo.FamiliaPermiso x WHERE x.FamiliaID = f.FamiliaID AND x.PermisoID = p.PermisoID);

-- Familias de los roles (16)
INSERT INTO dbo.RolFamilia (RolID, FamiliaID)
SELECT DISTINCT r.RolID, f.FamiliaID
FROM (VALUES
    (N'SuperAdmin', N'Configuracion del hotel'),
    (N'SuperAdmin', N'Gestion de estadias'),
    (N'SuperAdmin', N'Gestion de habitaciones'),
    (N'SuperAdmin', N'Gestion de huespedes'),
    (N'SuperAdmin', N'Gestion de reservas'),
    (N'SuperAdmin', N'Reportes'),
    (N'SuperAdmin', N'Auditoria de habitaciones'),
    (N'SuperAdmin', N'Cargos adicionales'),
    (N'SuperAdmin', N'Servicio a la habitacion'),
    (N'Supervisor', N'Auditoria de habitaciones'),
    (N'Recepcionista', N'Gestion de estadias'),
    (N'Recepcionista', N'Gestion de huespedes'),
    (N'Recepcionista', N'Gestion de reservas'),
    (N'Recepcionista', N'Cargos adicionales'),
    (N'Recepcionista', N'Servicio a la habitacion'),
    (N'Personal de servicio', N'Servicio a la habitacion')
) AS n (Rol, Familia)
INNER JOIN dbo.Roles r    ON r.Nombre = n.Rol
INNER JOIN dbo.Familias f ON f.Nombre = n.Familia
WHERE NOT EXISTS (SELECT 1 FROM dbo.RolFamilia x WHERE x.RolID = r.RolID AND x.FamiliaID = f.FamiliaID);
GO

/* ----------------------------------------------------------------------------
   8. CATÁLOGOS INICIALES (solo en las tablas que se acaban de crear)
   ---------------------------------------------------------------------------- */
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'ConfiguracionHotel') AND NOT EXISTS (SELECT 1 FROM dbo.ConfiguracionHotel)
BEGIN
    SET IDENTITY_INSERT dbo.ConfiguracionHotel ON;
    INSERT INTO dbo.ConfiguracionHotel ([ConfiguracionID], [HoraCheckIn], [HoraCheckOut], [GraciaNoShowHoras]) VALUES
        (1, CAST(N'14:00:00' AS Time), CAST(N'10:00:00' AS Time), CAST(3.00 AS Decimal(5, 2)));
    SET IDENTITY_INSERT dbo.ConfiguracionHotel OFF;
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Pisos') AND NOT EXISTS (SELECT 1 FROM dbo.Pisos)
BEGIN
    SET IDENTITY_INSERT dbo.Pisos ON;
    INSERT INTO dbo.Pisos ([PisoID], [Numero], [Nombre]) VALUES
        (1, 1, N'Primer Piso'),
        (2, 2, N'Segundo Piso'),
        (3, 3, N'Tercer Piso'),
        (4, 4, N'Cuarto Piso');
    SET IDENTITY_INSERT dbo.Pisos OFF;
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'TiposHabitacion') AND NOT EXISTS (SELECT 1 FROM dbo.TiposHabitacion)
BEGIN
    SET IDENTITY_INSERT dbo.TiposHabitacion ON;
    INSERT INTO dbo.TiposHabitacion ([TipoHabitacionID], [Nombre], [Descripcion], [Capacidad], [TarifaNoche]) VALUES
        (1, N'Matrimonial Plus', N'Cama matrimonial con vista', 2, CAST(45000.00 AS Decimal(10, 2))),
        (2, N'Premium', N'Habitación superior con balcón', 2, CAST(60000.00 AS Decimal(10, 2))),
        (3, N'Jacuzzi Gold', N'Con jacuzzi privado', 2, CAST(85000.00 AS Decimal(10, 2))),
        (4, N'Jacuzzi Dubai', N'Suite temática con jacuzzi', 2, CAST(95000.00 AS Decimal(10, 2))),
        (5, N'Matrimonial 2 Plazas', N'Dos camas matrimoniales', 4, CAST(70000.00 AS Decimal(10, 2))),
        (6, N'Jacuzzi', N'Habitación con jacuzzi estándar', 2, CAST(75000.00 AS Decimal(10, 2)));
    SET IDENTITY_INSERT dbo.TiposHabitacion OFF;
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'Habitaciones') AND NOT EXISTS (SELECT 1 FROM dbo.Habitaciones)
BEGIN
    SET IDENTITY_INSERT dbo.Habitaciones ON;
    INSERT INTO dbo.Habitaciones ([HabitacionID], [NroHabitacion], [PisoID], [TipoHabitacionID], [Estado]) VALUES
        (1, N'101', 1, 1, 0),
        (2, N'102', 1, 1, 0),
        (3, N'103', 1, 2, 0),
        (4, N'104', 1, 1, 0),
        (5, N'105', 1, 2, 0),
        (6, N'201', 2, 3, 0),
        (7, N'202', 2, 4, 0),
        (8, N'203', 2, 5, 0),
        (9, N'204', 2, 5, 0),
        (10, N'205', 2, 6, 0),
        (11, N'301', 3, 1, 0),
        (12, N'302', 3, 2, 0),
        (13, N'303', 3, 1, 0),
        (14, N'304', 3, 6, 0),
        (15, N'305', 3, 3, 0),
        (16, N'401', 4, 4, 0),
        (17, N'402', 4, 5, 0),
        (18, N'403', 4, 3, 0),
        (19, N'404', 4, 6, 0);
    SET IDENTITY_INSERT dbo.Habitaciones OFF;
END
GO
IF EXISTS (SELECT 1 FROM #TablasNuevas WHERE Tabla = N'ItemsRoomService') AND NOT EXISTS (SELECT 1 FROM dbo.ItemsRoomService)
BEGIN
    SET IDENTITY_INSERT dbo.ItemsRoomService ON;
    INSERT INTO dbo.ItemsRoomService ([ItemID], [Nombre], [Descripcion], [PrecioUnitario], [Activo]) VALUES
        (1, N'Desayuno continental', N'Café o té, jugo de naranja, medialunas, tostadas, manteca y mermelada', CAST(14000.00 AS Decimal(10, 2)), 1),
        (2, N'Desayuno americano', N'Huevos revueltos, panceta, tostadas, fruta de estación, café y jugo', CAST(19500.00 AS Decimal(10, 2)), 1),
        (3, N'Club sándwich', N'Pollo, jamón, panceta, lechuga, tomate y huevo, con papas fritas', CAST(16500.00 AS Decimal(10, 2)), 1),
        (4, N'Hamburguesa completa', N'Medallón de carne, cheddar, panceta, huevo y papas fritas', CAST(18000.00 AS Decimal(10, 2)), 1),
        (5, N'Ensalada César', N'Lechuga, pollo grillado, croutons, parmesano y aderezo César', CAST(14500.00 AS Decimal(10, 2)), 1),
        (6, N'Tabla de fiambres', N'Selección de fiambres y quesos regionales para dos personas', CAST(22000.00 AS Decimal(10, 2)), 1),
        (7, N'Agua mineral 500 ml', N'Con o sin gas (frigobar)', CAST(3000.00 AS Decimal(10, 2)), 1),
        (8, N'Gaseosa 500 ml', N'Línea cola, lima-limón o naranja (frigobar)', CAST(3800.00 AS Decimal(10, 2)), 1),
        (9, N'Cerveza 473 ml', N'Rubia o negra (frigobar)', CAST(5500.00 AS Decimal(10, 2)), 1),
        (10, N'Botella de vino Malbec', N'Vino tinto de Mendoza, 750 ml', CAST(28000.00 AS Decimal(10, 2)), 1),
        (11, N'Snack de frigobar', N'Maní, papas o barra de cereal', CAST(4200.00 AS Decimal(10, 2)), 1),
        (12, N'Café o té en habitación', N'Servicio de cafetería para dos personas', CAST(6000.00 AS Decimal(10, 2)), 1);
    SET IDENTITY_INSERT dbo.ItemsRoomService OFF;
END
GO

/* ----------------------------------------------------------------------------
   9. BITÁCORA DE RESERVAS: registro inicial de las reservas que ya existan
   ---------------------------------------------------------------------------- */
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

/* ----------------------------------------------------------------------------
   10. DÍGITOS VERIFICADORES: se recalculan desde la aplicación
   ---------------------------------------------------------------------------- */
DELETE FROM dbo.DV;
GO
COMMIT TRANSACTION;
GO
DROP TABLE #TablasNuevas;
GO
PRINT N'UPDATE_OK: base TP_Ing_Soft actualizada a la versión 2.';
GO
