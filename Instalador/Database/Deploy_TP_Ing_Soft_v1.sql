/* ============================================================================
   HORIZON HOTEL & RESORT - BASE DE DATOS TP_Ing_Soft
   Deploy_TP_Ing_Soft_v1.sql  ·  INSTALACIÓN DESDE CERO (versión 1 - primer cuatrimestre)
   ----------------------------------------------------------------------------
   Crea la base y todo lo del sistema: usuarios, perfiles (roles, familias y
   permisos), bitácora de eventos, dígitos verificadores, backup/restore e idioma.
   No incluye nada de negocio: eso lo agrega Update_TP_Ing_Soft_v2.sql (Reinstalar).

   - Se puede ejecutar más de una vez: si la base ya tiene el sistema instalado
     no toca nada e informa DEPLOY_OMITIDO (nunca borra ni pisa datos).
   - Todo va en una sola transacción: si algo falla no queda nada a medias.
   - Los datos iniciales (usuarios de prueba, roles, familias, permisos y sus
     dígitos verificadores) son los de la versión 1 entregada, sin cambios, así
     la verificación de integridad da consistente en el primer ingreso.
   - Ejecutar con:  sqlcmd -S <instancia> -E -C -I -b -f 65001 -i Deploy_TP_Ing_Soft_v1.sql
     (en SSMS: menú Consulta > Modo SQLCMD).
   Requiere SQL Server 2016 SP1 o superior (incluye LocalDB y Express).
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
USE [master];
GO
IF DB_ID(N'TP_Ing_Soft') IS NULL
BEGIN
    CREATE DATABASE [TP_Ing_Soft];
    PRINT N'Base de datos TP_Ing_Soft creada.';
END
ELSE
    PRINT N'La base de datos TP_Ing_Soft ya existía.';
GO
USE [TP_Ing_Soft];
GO
IF OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL
BEGIN
    PRINT N'DEPLOY_OMITIDO: la base TP_Ing_Soft ya tiene el sistema instalado; no se modificó nada.';
    SET NOEXEC ON;
END
GO
BEGIN TRANSACTION;
GO

/* ----------------------------------------------------------------------------
   1. TIPOS
   ---------------------------------------------------------------------------- */
CREATE TYPE [dbo].[IntList] AS TABLE(
	[ID] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO

/* ----------------------------------------------------------------------------
   2. TABLAS
   ---------------------------------------------------------------------------- */
CREATE TABLE [dbo].[Bitacora](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[Modulo] [int] NOT NULL,
	[Evento] [int] NOT NULL,
	[Criticidad] [int] NOT NULL,
	[TargetUsername] [nvarchar](50) NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[DV](
	[Tabla] [nvarchar](100) NOT NULL,
	[DVH] [nvarchar](max) NOT NULL,
	[DVV] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Tabla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[FamiliaIntegrada](
	[FamiliaPadreID] [int] NOT NULL,
	[FamiliaID] [int] NOT NULL,
 CONSTRAINT [PK_FamiliaIntegrada] PRIMARY KEY CLUSTERED 
(
	[FamiliaID] ASC,
	[FamiliaPadreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[FamiliaPermiso](
	[FamiliaID] [int] NOT NULL,
	[PermisoID] [int] NOT NULL,
 CONSTRAINT [PK_FamiliaPermiso] PRIMARY KEY CLUSTERED 
(
	[FamiliaID] ASC,
	[PermisoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Familias](
	[FamiliaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FamiliaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Permisos](
	[PermisoID] [int] IDENTITY(0,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](200) NULL,
 CONSTRAINT [PK__Permisos__96E0C703D1806354] PRIMARY KEY CLUSTERED 
(
	[PermisoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Roles](
	[RolID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[RolFamilia](
	[RolID] [int] NOT NULL,
	[FamiliaID] [int] NOT NULL,
 CONSTRAINT [PK_RolFamilia] PRIMARY KEY CLUSTERED 
(
	[RolID] ASC,
	[FamiliaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[RolPermiso](
	[RolID] [int] NOT NULL,
	[PermisoID] [int] NOT NULL,
 CONSTRAINT [PK_RolPermiso] PRIMARY KEY CLUSTERED 
(
	[RolID] ASC,
	[PermisoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Users](
	[DNI] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[RolID] [int] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Hash] [nvarchar](255) NOT NULL,
	[Salt] [nvarchar](255) NOT NULL,
	[Bloqueado] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Idioma] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/* ----------------------------------------------------------------------------
   3. DATOS INICIALES (versión 1)
   ---------------------------------------------------------------------------- */
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'FamiliaIntegrada', N'5IosYTXNpmSR1HPORAmeNQ==', N'1iPTQXhgIkDeNjdii7cfOQ==')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'FamiliaPermiso', N'SS5amWewvtVrB+MWBQq1noI5AuM+NBqZ4sdK5bopfQdwVoAq8CSQgRzPvFEjJYy4wiicupDMV7Ukamh5Dsco6w==', N'XM45C3pJvHb1T7S2eKv4ypXhfqkxYR+ePSwB/xCBqARPgl0Tgd0AaTdiwFn/NYJi2nsyhjUFTQ/Ot/Gi5a7VZA==')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Familias', N'4ABMalsu8ryZjyVlUyC4n/7xIHifXgW/mLnna0tFJkl7dgbn3+5atdMIjv4qTV7do6Vlfc6VH5nhhUf0VogxYl5AZtKRL9dv9nFhf6CjFUe4MZmkAbNZ0c7bt2ldGpaI', N'RDErtZaGeAgChlJ1ad8tf0N55F0q+Tshe/tuQJaeG0qvXZHKBuz7Ukg7lc00xXsgCCBF7K1Kf+d6rY0OyPxpNAEcOIS2jKZhxLZCQ0KMIlIOJutaw9SxQ0gnq6ZfheRL')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Permisos', N'mPUuVSIeVqmG6oJN7Op0bOPwZGoIgrH8PGJDBqy0GzS4K5+IkFx1PEvflFrTZi48xg5veCvIVvdwCmsInpbza8wA1kBr1hT8Qvv/rVXGQGMauMZdOA+xg1ycw8zZYHdK1Wf8PO3rTstylI14YVRePL0/Utd0RR2V6+KgU+6xqRHA0sgScC8Gay/N+gUadXdqzyCIVkf9wkiBKUmLDOtPwJgfoCMMWQQnWs87eqkdUnkayoCrjKct+6hjOqpgSIfO+BiAvMy8a9orcYFtlNzBhC3dukZmMQfLh1efgxPaYVUWj+TXIj93tYGwr5WS8MUpcjBcMlSDgVRlrA13+P3/XK2oQfaoSRer5TAoPJgje/eUuih/tFDIZ6YLqpo7egenPw+SHTcZ3hU2GMS+yzAFxxB38DSlUJ1X8vX8hiFvTBW++MAG8/LvWmLCBviuOuAdlKRIkxHSV7vjkLixAHkRzBdcAtWEn+/FscWwv6f3/WT1MhRT/M2MVgOnsmb1mvqJEaqQkhn+cWuNUkRdhCEWoYpU6QOYq78hTi1g1wXemueZhogBpfQZ/+S2rMz+iUwQeYts1YokmT9hfhjlhedQn4fdiCiBNxsJVmKO/MzfrIpTwmPKCdMwjjao5IoKVBtYO0U5TlTlMBqKvVtFik2YjNyhgtfdnpKCxHIZTKulk74bDiGpfW/EW/hllo079G25g0a053/LQNma84qSd4W1a+6qD59HYF8PE6Mtj2KPGpa3jVlnCkC4Hjv1c5d8z3+bX9U0lyOYeZLwVmZbvM+XZS3q8TFEQKaWBOH+jrdOQjGYw3YZ1OyDHyCX22lYEz7cDLaa56VEquYWvAlb0LL31qpXTiib2eK85fTUgGR1BZIIYNj1pFDU03ceZ6FdfCd9PzXyOxsTz6Oz/bYHaC9BS42VeapJlW2vtgo5wB/RQ/VtORkRJ3NNeWm/5MYyoU+nlw+AARyP2auG2UV35HXA1+NI/HmVjDKUPydQCoB0ZwQ1su8XQkrgYWgUk/guJejjmyD7I5JiU2yp4DmprRQfNYUpfbiECR69e87iGQJQ/PfFNPnxLNcWrE6F1Zy4qjQlgpHsm1RZLj1gqIdxoIi1KnIlgvYQAf48chYO7qcf+lH/6Zr8VGQsp+oahPghfZEK8Mt6QNSySZQE98Id9sltydfgHkIGzScVA0heQh7DUTm6eR5/wTZ9AsyF0qdoITIhTldhOrdF263wmblHZz6JX7mbp6n6ZHFayyASWMlF2X0D1r0+Qpz0UHFe+d66UNOTIgVUZWJ/3fv+pcQrmbFmBqeMDhPt2sxEqpLRTvKrqJQeVCJ0AZAwA5sp/f2Z2HawSPW9dFDVuD1iARhFbgsDk+t6EvMuxqGfRxANPoI4VJRtUtUDyOJEtq+VgEdFkuUZHh9y0CmEQeMm/yHFQ1TqEpXmhdz3EdvOi8Q7J2vuRhofq3FkfX0/5s0LYQuDsqtVQPAsaBPBsa8uKt66UsZuqChIeOCYKTOZztLYI45EU9nrH0IS2NLQ0KIqyiopWqvnPCVatkE+7wdxuseAO6LZY1jDL2EEZhHxtD5RTDjUurdquXUqDaJW4dAl5WWaVQwSrYKqWconHDlQ/gMH4wszRPFXGlMp0iTuFUbY7oSq31qX2GjBukA528xPpVsQT0k49wBO86N43Z5HStmmlW/rLA==', N'5IT8HGGlienDeB5hQgWaJhcGQT6YwNH5guW6YKfTsw14yK8IY1FID+FaYUP0pxlximgvhNFpaLJo3qIgpRSHB8sq2aH+EpoDr/uBtuI1a3ZrIlvYTzi4eH5OoXAQ/AMpKLMEgZ823z0+lKiSKBG+X6YdvI1fvK0i+z4O/nVdhZiq9ZpiCk31llGt1eNNPsX1D4e1D44XZ+nX4CQc4bp+XIpfI4JaVVnGzs/F2CqHZMR7oaNC4ICdPhUbFZjWHusL5dEI+2fvaeKlQImUQpHye1TPqHXpMaKEVQmzvrwigP2J90WXAyba4VTbD0wf+ajrLXrzr9ljFeD6SCLVH9Zg7vx5xbZrWDwB4MTCZHje8R9Jw/ADlufmqltbBDPMIRTGZ8i001dx8/7m0RSzwd1tm7ntC317dXEd/EJwVULgmz65UpXgY2S9CHtXrdu5FK8DnKMEED9rPUeqf+pJMRjaNBA1FGsbwnJpP0iqrgKCtTCG18hwnZaqa0qG9HBsTdaZG4UvHvPYiUq8U9kGR/hBGHElynfHWbdouyfSWoRO7BWON74jrp2zPtSxswaBM85u44ebMX67DhS3mSsHi9gDD7eAU3wlsWAugCceV7JivrfbjUKAA8yQJXOGvPGCIoezIZDpTgXbeGuuDg9xvTOet1VBuoLYGwXDUmIN95VNvqrU+Tz6tkXsLGNCYBpaStxIMSwu6QEh27F+OyPnAQ5royrNLGBslQbJZZTrwTP2C9FnY+Tty6MpQU3zIDXqe8ID7Y9HXhwCDLMa6Rm4iLBIWM2+hGMKbqdIijFrNZusKVz3sXn7fVh7ARYSTvaUjQZ1WLhLos9IETitRUIRKazY1riFgvBAUcpXp5Rju4CYgJezLOhYTSCvF1/PczgI9vEELEXNGsXTJk4r0lx6iJYjBCaWXlKw2CeGvzvksl0BN7hJpBLttYfF/N9KW1efNDEx6F3OlB0Yd9Xi4kpdJQBFQtKv2BLQiWA6UPYWQCcPgDcP8b8i9SCng7ovwSObs2WnR/OE00Y8v7uaZECChUTwoFfd3b2HP/XHeUOb4dH6YZicVTMJeZYS8ZjyYWdHHUK6oEPo0vnnAjQoqRFsl6Af0fEbfLhAuo/+S9tKfYN3FNiuozwHlvTr575UNgkVXLIpRGQihKaCWWxkJ29pxHGoBrMc6iviZvDPAVnHnFZNlXoeR2Ki+x/89HwSSqn8PKSw7dHsJk7zEbe3ZSBrCQK3gvSgmuIsgAPzEATBzT5p4b58ie1aNUcvo3wX33+voKUIkRyo+k7ZQCVgw+DaBsM4sWR3VuN9JCICOwPzPpq1HDPVusvVc/CaNTw6Sb346+HJgvRWyIprf8j/Adn7Ckogrsa5E5koDTjWt2UbMYKzYEnLdzMaGzDe/VXx2vpjI11FHRJFZTsY8bP5Znea2wq+KRXKvwjo+ylQtIlDXjA6H0un1YFojKuEP17FEvrKHImi/qruprvDXQYOISfzzeRlkW24hiv/p0CC78uRB89njNggGiudgWkl9jvAIb4aNymHqmrjBNYLZzh12gFVz2B7Q0qQhTwHxxKzS4jVMjsrcZTl3iukKPin6jIzDrtyCDhTJRv67tnyTdN0rMBo59VXnXw8pGzS9+cg/7Jp0LHvhUWZcwiRmDhnuoJ4DSH79k4tEWavSpDACtTJMoNTN9A5lQ==')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Roles', N'cougKoLpSK6LuvvoaHF+6jC+rAkkNWqKSU+b9oQY4X2PQA96UPHqY4y7rHZKrYi5', N'5Dr+L382+27pTaJHJ66RNV3B6YGQQr1Dr5qKRN7Nvx72AkHD0RXVNDkGvS3TG/VD')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'RolFamilia', N'Hxu5RBQWxRMG5LA7KXk6Hg==', N'XNlj1JClqbLf8peex7Z6hQ==')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'RolPermiso', N'Pv4XRPS4tMAqr1UC5WWL8g==', N'/GNeCqLnGl4iYDs3/2uKEw==')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Users', N'u9Z54JP9QF/uKMyD2hPtxKTY2Q7Kvkj5Vc0r6jswHlquXPgm7kdQaEWIn0Fr8IPnO1Udg/l/CAoPIr1r8Pmul4ig7++1cu4GfY6InGLPKIh0RqbWuQukqE4rlBI1AzjCdbrUNDuDtMyuiBNXaXf3mpbSM+ZdKX/ekjGvoC6wXvlMnQX9GIjysNJqu69oWT4flNNL2zmBzSUCfkl4li6R+D8bRUh2zax4LWZnQe0PeejHUuLis5C5B5K2T3DNMwXyFFcUmzSe3ig2Sh4y3WkLkA2r7Flafj7xGCfIBAITVmamTsoiiINKw7lMqYX4hWnNGzk6O+B68ZrDYj+t1zvjJQH+Q6+LhIpEeH5zt5LYlYPyI5mcXVmmO+7GUGBVjoB1Ovkbd7YTrxOIenpfODh+SLH6EobxesJmlrKk8R00nB+QskW1H5i8UI+AajRNVrV6VtQ3CzucrCsdTTddeLvpkmhsksvd6lQCWn7Z1J+jcBQXiNGJWc51MyUe5oFmVrnRe8pd3kBh4u4GhFbxMvyRCVMjh5H96S+dQcC6M0w0B1Rh8a9Yt+JA4Vgbd12vabusn2Zj352/0+v3t1etrDc/WeLrw3y/EDpdkb6VDBcWmijIMKU4YBu7WwrnXeaC3tQvjiuchgzy4eMUidIeSPhUR5bPbaE3LvE+Bfurd+nbnSUImXM2L8Hv5qGB3Nn1jqyH', N'nVxWDsoPvYEdgbzccx/rnBYQjOgbt2d3B/O4BGbMi9DCL6A6BN+ksmHZ2Jy6hyysqXNbiRFaiF6EB0wkfxpTm2MNF5foKHeq6A+akRV6qgZC9+6Xfno/l9y6iRgUAJtO1X1adcT/63FWs50CtSgZXwB+5N1ovvTi5DMq5a1runM5H6K8PolmljJ/llY5D+vR8aoGx88DOTKtpH0uj4ecp1YH56xls6uCI8CgvbesBU1e23dUmn3Ws5V/GgnQ/LOfwD6aIQIABlLS6dpgMfvfAPXyjXpf4Y2mJD2mYvA1K8GCPxZp5TfFp+IvMyFM03ps86C1jFXjMmZ8SAA7J2fP47v/inhqRwteIKFzxj+BQZ0EcVr499eSixBzfrnPI3a60ThrV3Qv7gf4H/HD27pTkPN8mtsbmRuIzGb9sdJTMctfueAoeNc+gNB9nmbhpPyVw+lvTPd3CaiGHV5U97TaN9wYSUyo0XDVNqVU0lj4XfhtlePDwYgfZXLLGHfmlrd0Nh5vZ6OtiR+t+PVg+L3PzEzDK9A+xLupTXHo1jaVnF//Dbf5arhmdmvwy1VXJx3V5ckOsdUcN75e4/YzZOb5WSZe5OsIuNdqD4TWnra+ne7IEHHwtNdiYSNlsCzlu+uDOUUpyzIwMHdZQXvdK48Psr39cPX55WkzUJ+rkncPN+BxNi6JuFABEQLhzQrVWK35')
GO
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (1, 2)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (1, 3)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (1, 4)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (1, 5)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (1, 6)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (2, 11)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (2, 12)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (2, 13)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (2, 14)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (3, 7)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (3, 8)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (3, 9)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (3, 10)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (4, 1)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (4, 2)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (4, 7)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (4, 11)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (7, 17)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (7, 18)
INSERT [dbo].[FamiliaPermiso] ([FamiliaID], [PermisoID]) VALUES (7, 19)
GO
SET IDENTITY_INSERT [dbo].[Familias] ON 

INSERT [dbo].[Familias] ([FamiliaID], [Nombre]) VALUES (1, N'Gestion de usuarios')
INSERT [dbo].[Familias] ([FamiliaID], [Nombre]) VALUES (2, N'Gestion de familias')
INSERT [dbo].[Familias] ([FamiliaID], [Nombre]) VALUES (3, N'Gestion de roles')
INSERT [dbo].[Familias] ([FamiliaID], [Nombre]) VALUES (4, N'Consulta general')
INSERT [dbo].[Familias] ([FamiliaID], [Nombre]) VALUES (7, N'Gestion de respaldos')
SET IDENTITY_INSERT [dbo].[Familias] OFF
GO
SET IDENTITY_INSERT [dbo].[Permisos] ON 

INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (1, N'VerBitacora', N'Consultar eventos del sistema')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (2, N'VerUsuarios', N'Acceder a la pantalla gestion de usuarios con la lista de usuarios en el sistema')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (3, N'CrearUsuario', N'Crear usuarios nuevos dentro del sistema')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (4, N'ModificarUsuario', N'Modificar usuarios existentes')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (5, N'DesActivarUsuario', N'Activar o desactivar usarios existentes')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (6, N'DesbloquearUsuario', N'Desbloquear usuarios que se encuentren bloqueados')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (7, N'VerRoles', N'Acceder a la pantalla gestion de roles con la lista de roles en el sistema')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (8, N'CrearRoles', N'Crear roles nuevos dentro del sistema')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (9, N'ModificarRoles', N'Modificar roles existentes')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (10, N'EliminarRoles', N'Eliminar roles existentes')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (11, N'VerFamilias', N'Acceder a la pantalla gestion de familias con la lista de familias en el sistema')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (12, N'CrearFamilias', N'Crear familias nuevas dentro del sistema')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (13, N'ModificarFamilias', N'Modificar familias existentes')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (14, N'EliminarFamilias', N'Eliminar Familias existentes')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (16, N'ManejarInconsistencias', N'Permite tomar decisiones en caso de producirse una inconsistencia de base de datos en el sistema')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (17, N'VerRespaldos', N'Permite acceder al formulario Gestion de Respaldos en el menu principal')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (18, N'RealizarBackup', N'Permite realizar un backup de la base de datos actual y guardar el archivo .bak')
INSERT [dbo].[Permisos] ([PermisoID], [Nombre], [Descripcion]) VALUES (19, N'RealizarRestore', N'Permite restaurar toda la base de datos actual del sistema a una version anterior guardada en un backup seleccionando el archivo .bak')
SET IDENTITY_INSERT [dbo].[Permisos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RolID], [Nombre]) VALUES (1, N'SuperAdmin')
INSERT [dbo].[Roles] ([RolID], [Nombre]) VALUES (2, N'Supervisor')
INSERT [dbo].[Roles] ([RolID], [Nombre]) VALUES (3, N'Gestor de accesos')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[RolFamilia] ([RolID], [FamiliaID]) VALUES (1, 1)
INSERT [dbo].[RolFamilia] ([RolID], [FamiliaID]) VALUES (1, 2)
INSERT [dbo].[RolFamilia] ([RolID], [FamiliaID]) VALUES (1, 3)
INSERT [dbo].[RolFamilia] ([RolID], [FamiliaID]) VALUES (1, 7)
INSERT [dbo].[RolFamilia] ([RolID], [FamiliaID]) VALUES (2, 4)
INSERT [dbo].[RolFamilia] ([RolID], [FamiliaID]) VALUES (3, 2)
INSERT [dbo].[RolFamilia] ([RolID], [FamiliaID]) VALUES (3, 3)
GO
INSERT [dbo].[RolPermiso] ([RolID], [PermisoID]) VALUES (1, 1)
INSERT [dbo].[RolPermiso] ([RolID], [PermisoID]) VALUES (1, 16)
INSERT [dbo].[RolPermiso] ([RolID], [PermisoID]) VALUES (2, 6)
GO
INSERT [dbo].[Users] ([DNI], [Username], [RolID], [Nombre], [Apellido], [Email], [Hash], [Salt], [Bloqueado], [Activo], [Idioma]) VALUES (11111111, N'11111111Supervisor', 2, N'Supervisor', N'Tester', N'Supervisor@test.com', N'P44CmfpIK8fTMExJOI1vN72CDu0YJAO7+gNiNk0fbvY=', N'DXjrPLoTKxZtR440OXyAcQ==', 0, 1, N'es')
INSERT [dbo].[Users] ([DNI], [Username], [RolID], [Nombre], [Apellido], [Email], [Hash], [Salt], [Bloqueado], [Activo], [Idioma]) VALUES (12345678, N'Maximo', 1, N'Maximo', N'Yapura', N'Maximo@gmail.com', N'N4Rf9Gw3hwxmXiqY4tp1IFxaOezlZdSiuYkDZqKOGFQ=', N'CWu49+d6I6N7m98eZq7sUw==', 0, 1, N'es')
INSERT [dbo].[Users] ([DNI], [Username], [RolID], [Nombre], [Apellido], [Email], [Hash], [Salt], [Bloqueado], [Activo], [Idioma]) VALUES (22222222, N'22222222GestorDeAccesos', 3, N'GestorDeAccesos', N'Tester', N'GestorDeAccesos@tester.com', N'r+jN0y27Q07T5QOV8nfJ+kdNtLVrRPoTtAd405ZzCfw=', N'4AG3HFhMbpjExCYFSl6emQ==', 0, 1, N'es')
INSERT [dbo].[Users] ([DNI], [Username], [RolID], [Nombre], [Apellido], [Email], [Hash], [Salt], [Bloqueado], [Activo], [Idioma]) VALUES (46898868, N'46898868Nahuel', 1, N'Nahuel', N'Sanchez', N'nahu@gmail.com', N'P0CEZfzHWTxdjdWcJRHCkIPHz0QYTyQJoa3aimrEwgw=', N'TcjsVE5ajt6fBmNFr16KDg==', 0, 1, N'es')
GO

/* ----------------------------------------------------------------------------
   4. RESTRICCIONES (claves únicas, valores por defecto, claves foráneas y checks)
   ---------------------------------------------------------------------------- */
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Users_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bitacora] ADD  CONSTRAINT [DF_Bitacora_Fecha]  DEFAULT (getdate()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Bloqueado]  DEFAULT ((0)) FOR [Bloqueado]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Idioma]  DEFAULT ('es') FOR [Idioma]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Users] FOREIGN KEY([Username])
REFERENCES [dbo].[Users] ([Username])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Users]
GO
ALTER TABLE [dbo].[FamiliaIntegrada]  WITH CHECK ADD FOREIGN KEY([FamiliaPadreID])
REFERENCES [dbo].[Familias] ([FamiliaID])
GO
ALTER TABLE [dbo].[FamiliaIntegrada]  WITH CHECK ADD FOREIGN KEY([FamiliaID])
REFERENCES [dbo].[Familias] ([FamiliaID])
GO
ALTER TABLE [dbo].[FamiliaPermiso]  WITH CHECK ADD FOREIGN KEY([FamiliaID])
REFERENCES [dbo].[Familias] ([FamiliaID])
GO
ALTER TABLE [dbo].[FamiliaPermiso]  WITH CHECK ADD  CONSTRAINT [FK__FamiliaPe__Permi__5165187F] FOREIGN KEY([PermisoID])
REFERENCES [dbo].[Permisos] ([PermisoID])
GO
ALTER TABLE [dbo].[FamiliaPermiso] CHECK CONSTRAINT [FK__FamiliaPe__Permi__5165187F]
GO
ALTER TABLE [dbo].[RolFamilia]  WITH CHECK ADD FOREIGN KEY([FamiliaID])
REFERENCES [dbo].[Familias] ([FamiliaID])
GO
ALTER TABLE [dbo].[RolFamilia]  WITH CHECK ADD FOREIGN KEY([RolID])
REFERENCES [dbo].[Roles] ([RolID])
GO
ALTER TABLE [dbo].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK__RolPermis__Permi__5441852A] FOREIGN KEY([PermisoID])
REFERENCES [dbo].[Permisos] ([PermisoID])
GO
ALTER TABLE [dbo].[RolPermiso] CHECK CONSTRAINT [FK__RolPermis__Permi__5441852A]
GO
ALTER TABLE [dbo].[RolPermiso]  WITH CHECK ADD FOREIGN KEY([RolID])
REFERENCES [dbo].[Roles] ([RolID])
GO
ALTER TABLE [dbo].[FamiliaIntegrada]  WITH CHECK ADD  CONSTRAINT [CHK_NoAutorreferencia] CHECK  (([FamiliaID]<>[FamiliaPadreID]))
GO
ALTER TABLE [dbo].[FamiliaIntegrada] CHECK CONSTRAINT [CHK_NoAutorreferencia]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CHK_Users_Idioma] CHECK  (([Idioma]='en' OR [Idioma]='es'))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CHK_Users_Idioma]
GO

/* ----------------------------------------------------------------------------
   5. PROCEDIMIENTOS ALMACENADOS
   ---------------------------------------------------------------------------- */
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
    SELECT * FROM Users;            -- RS0
    SELECT * FROM Roles;            -- RS1
    SELECT * FROM Familias;         -- RS2
    SELECT * FROM Permisos;         -- RS3
    SELECT * FROM FamiliaPermiso;   -- RS4
    SELECT * FROM FamiliaIntegrada; -- RS5
    SELECT * FROM RolFamilia;       -- RS6
    SELECT * FROM RolPermiso;       -- RS7
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
COMMIT TRANSACTION;
GO
PRINT N'DEPLOY_OK: base TP_Ing_Soft instalada (versión 1).';
GO
SET NOEXEC OFF;
GO
