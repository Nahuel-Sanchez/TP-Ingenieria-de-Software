using BE_08YS;
using BE_08YS.Metricas;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Interfaces_Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS
{
    public class SqlMetricasRepository_68SA : Connection_08YS, IMetricasRepository_68SA
    {
        #region Constructor

        public SqlMetricasRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        #endregion

        #region PN1: movimiento del día, ocupación prevista e ingresos por día

        public MovimientoHoy_68SA GetMovimientoHoy()
        {
            const string query = @"
                DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
                SELECT
                    (SELECT COUNT(*) FROM Reservas WHERE FechaIngreso = @Hoy AND Estado IN (0, 1, 2)) AS Llegadas,
                    (SELECT COUNT(*) FROM Reservas WHERE FechaIngreso = @Hoy AND Estado = 0) AS LlegadasPendientes,
                    (SELECT COUNT(*) FROM Reservas WHERE FechaEgreso = @Hoy AND Estado IN (1, 2)) AS Salidas,
                    (SELECT COUNT(*) FROM Reservas WHERE FechaEgreso <= @Hoy AND Estado = 1) AS SalidasPendientes,
                    (SELECT COUNT(*) FROM Reservas WHERE Estado = 1) AS EstadiasEnCurso,
                    (SELECT ISNULL(SUM(CantidadAdultos + CantidadNinos), 0) FROM Reservas WHERE Estado = 1) AS HuespedesAlojados,
                    (SELECT ISNULL(AVG(TarifaNoche), 0) FROM Reservas WHERE Estado = 1) AS TarifaPromedio";

            DataRow row = GetDataTable(query).Rows[0];
            return new MovimientoHoy_68SA
            {
                Llegadas = Convert.ToInt32(row["Llegadas"]),
                LlegadasPendientes = Convert.ToInt32(row["LlegadasPendientes"]),
                Salidas = Convert.ToInt32(row["Salidas"]),
                SalidasPendientes = Convert.ToInt32(row["SalidasPendientes"]),
                EstadiasEnCurso = Convert.ToInt32(row["EstadiasEnCurso"]),
                HuespedesAlojados = Convert.ToInt32(row["HuespedesAlojados"]),
                TarifaPromedio = Convert.ToDecimal(row["TarifaPromedio"])
            };
        }

        /// <summary>Para cada noche desde hoy: habitaciones con una estadía en curso o una reserva confirmada.</summary>
        public List<OcupacionDia_68SA> GetOcupacionPrevista(int dias)
        {
            const string query = @"
                DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
                DECLARE @Total INT = (SELECT COUNT(*) FROM Habitaciones WHERE Estado <> 4);
                ;WITH Dias AS (
                    SELECT 0 AS n UNION ALL SELECT n + 1 FROM Dias WHERE n + 1 < @Dias
                )
                SELECT DATEADD(DAY, d.n, @Hoy) AS Fecha,
                       (SELECT COUNT(DISTINCT r.HabitacionID) FROM Reservas r
                        WHERE r.Estado IN (0, 1)
                          AND r.FechaIngreso <= DATEADD(DAY, d.n, @Hoy)
                          AND r.FechaEgreso  >  DATEADD(DAY, d.n, @Hoy)) AS Ocupadas,
                       @Total AS Total
                FROM Dias d
                ORDER BY d.n
                OPTION (MAXRECURSION 400);";

            DataTable dt = GetDataTable(query, new[] { Param("@Dias", Math.Max(1, Math.Min(dias, 365))) });
            var lista = new List<OcupacionDia_68SA>();
            foreach (DataRow row in dt.Rows)
                lista.Add(new OcupacionDia_68SA
                {
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Ocupadas = Convert.ToInt32(row["Ocupadas"]),
                    Total = Convert.ToInt32(row["Total"])
                });
            return lista;
        }

        /// <summary>Cobros por día (tabla Pagos) en el período; sin fechas, toda la historia.</summary>
        public List<PuntoSerie_68SA> GetIngresosPorDia(DateTime? desde, DateTime? hasta)
        {
            var parametros = ParametrosPeriodo(desde, hasta);
            string query = $@"
                SELECT CAST(FechaPago AS DATE) AS Fecha, SUM(Monto) AS Valor
                FROM Pagos
                WHERE {FiltroPeriodo("FechaPago", desde, hasta)}
                GROUP BY CAST(FechaPago AS DATE)
                ORDER BY Fecha";
            return Serie(GetDataTable(query, parametros));
        }

        #endregion

        #region PN2: servicios adicionales

        public ResumenServicios_68SA GetResumenServicios(DateTime? desde, DateTime? hasta)
        {
            string filtroCargos = FiltroPeriodo("c.FechaHora", desde, hasta);
            string filtroCambios = FiltroPeriodo("hc.FechaCambio", desde, hasta);
            // Estadías que se superponen con el período (si no hay período, todas las que tuvieron check-in)
            string filtroEstadias = "r.CheckIn IS NOT NULL AND r.Estado IN (1, 2)"
                + (desde.HasValue ? " AND r.FechaEgreso >= @Desde" : "")
                + (hasta.HasValue ? " AND r.FechaIngreso <= @Hasta" : "");

            string query = $@"
                SELECT
                    (SELECT ISNULL(SUM(c.Monto), 0) FROM CargosAdicionales c WHERE c.Tipo = 0 AND {filtroCargos}) AS MontoRoomService,
                    (SELECT ISNULL(SUM(c.Monto), 0) FROM CargosAdicionales c WHERE c.Tipo = 1 AND {filtroCargos}) AS MontoUpgrades,
                    (SELECT COUNT(*) FROM CargosAdicionales c INNER JOIN PedidosRoomService p ON p.CargoID = c.CargoID WHERE {filtroCargos}) AS Pedidos,
                    (SELECT COUNT(*) FROM CargosAdicionales c INNER JOIN PedidosRoomService p ON p.CargoID = c.CargoID
                     WHERE p.Estado = 0 AND {filtroCargos}) AS PedidosPendientes,
                    (SELECT COUNT(*) FROM HistorialCambiosHabitacion hc WHERE hc.Motivo = 0 AND {filtroCambios}) AS Upgrades,
                    (SELECT COUNT(*) FROM HistorialCambiosHabitacion hc WHERE hc.Motivo = 1 AND {filtroCambios}) AS Reubicaciones,
                    (SELECT AVG(CAST(DATEDIFF(SECOND, c.FechaHora, p.FechaHoraEntrega) AS FLOAT)) / 60.0
                     FROM CargosAdicionales c INNER JOIN PedidosRoomService p ON p.CargoID = c.CargoID
                     WHERE p.Estado = 1 AND {filtroCargos}) AS MinutosEntrega,
                    (SELECT COUNT(*) FROM Reservas r WHERE {filtroEstadias}) AS EstadiasPeriodo,
                    (SELECT COUNT(*) FROM Reservas r WHERE {filtroEstadias}
                       AND EXISTS (SELECT 1 FROM CargosAdicionales c WHERE c.ReservaID = r.ReservaID AND {filtroCargos})) AS EstadiasConConsumo";

            DataRow row = GetDataTable(query, ParametrosPeriodo(desde, hasta)).Rows[0];
            return new ResumenServicios_68SA
            {
                MontoRoomService = Convert.ToDecimal(row["MontoRoomService"]),
                MontoUpgrades = Convert.ToDecimal(row["MontoUpgrades"]),
                Pedidos = Convert.ToInt32(row["Pedidos"]),
                PedidosPendientes = Convert.ToInt32(row["PedidosPendientes"]),
                Upgrades = Convert.ToInt32(row["Upgrades"]),
                Reubicaciones = Convert.ToInt32(row["Reubicaciones"]),
                MinutosEntregaPromedio = row["MinutosEntrega"] == DBNull.Value ? (double?)null : Convert.ToDouble(row["MinutosEntrega"]),
                EstadiasPeriodo = Convert.ToInt32(row["EstadiasPeriodo"]),
                EstadiasConConsumo = Convert.ToInt32(row["EstadiasConConsumo"])
            };
        }

        /// <summary>Por día: Valor = room service, Valor2 = upgrades.</summary>
        public List<PuntoSerie_68SA> GetServiciosPorDia(DateTime? desde, DateTime? hasta)
        {
            string query = $@"
                SELECT CAST(c.FechaHora AS DATE) AS Fecha,
                       SUM(CASE WHEN c.Tipo = 0 THEN c.Monto ELSE 0 END) AS Valor,
                       SUM(CASE WHEN c.Tipo = 1 THEN c.Monto ELSE 0 END) AS Valor2
                FROM CargosAdicionales c
                WHERE {FiltroPeriodo("c.FechaHora", desde, hasta)}
                GROUP BY CAST(c.FechaHora AS DATE)
                ORDER BY Fecha";
            return Serie(GetDataTable(query, ParametrosPeriodo(desde, hasta)));
        }

        /// <summary>Cantidad de pedidos de room service según la hora en que se pidieron (24 valores).</summary>
        public int[] GetPedidosPorHora(DateTime? desde, DateTime? hasta)
        {
            string query = $@"
                SELECT DATEPART(HOUR, c.FechaHora) AS Hora, COUNT(*) AS Cantidad
                FROM CargosAdicionales c
                INNER JOIN PedidosRoomService p ON p.CargoID = c.CargoID
                WHERE {FiltroPeriodo("c.FechaHora", desde, hasta)}
                GROUP BY DATEPART(HOUR, c.FechaHora)";

            var horas = new int[24];
            foreach (DataRow row in GetDataTable(query, ParametrosPeriodo(desde, hasta)).Rows)
            {
                int hora = Convert.ToInt32(row["Hora"]);
                if (hora >= 0 && hora < 24) horas[hora] = Convert.ToInt32(row["Cantidad"]);
            }
            return horas;
        }

        public List<ProductoConsumido_68SA> GetTopProductos(DateTime? desde, DateTime? hasta, int cantidad)
        {
            var parametros = new List<IDbDataParameter>(ParametrosPeriodo(desde, hasta)) { Param("@Top", Math.Max(1, cantidad)) };
            string query = $@"
                SELECT TOP (@Top) i.Nombre, SUM(d.Cantidad) AS Cantidad, SUM(d.Cantidad * d.PrecioUnitario) AS Monto
                FROM CargosAdicionales c
                INNER JOIN PedidosRoomService p       ON p.CargoID = c.CargoID
                INNER JOIN DetallePedidoRoomService d ON d.PedidoID = p.PedidoID
                INNER JOIN ItemsRoomService i         ON i.ItemID = d.ItemID
                WHERE {FiltroPeriodo("c.FechaHora", desde, hasta)}
                GROUP BY i.Nombre
                ORDER BY SUM(d.Cantidad) DESC, i.Nombre";

            var lista = new List<ProductoConsumido_68SA>();
            foreach (DataRow row in GetDataTable(query, parametros.ToArray()).Rows)
                lista.Add(new ProductoConsumido_68SA
                {
                    Nombre = row["Nombre"].ToString(),
                    Cantidad = Convert.ToInt32(row["Cantidad"]),
                    Monto = Convert.ToDecimal(row["Monto"])
                });
            return lista;
        }

        /// <summary>Upgrades del período agrupados por el tipo de la habitación nueva: Valor = monto cobrado.</summary>
        public List<ValorEtiqueta_68SA> GetUpgradesPorTipo(DateTime? desde, DateTime? hasta)
        {
            string query = $@"
                SELECT t.Nombre AS Etiqueta, COUNT(*) AS Cantidad, ISNULL(SUM(c.Monto), 0) AS Valor
                FROM HistorialCambiosHabitacion hc
                INNER JOIN Habitaciones h     ON h.HabitacionID = hc.HabitacionNuevaID
                INNER JOIN TiposHabitacion t  ON t.TipoHabitacionID = h.TipoHabitacionID
                LEFT  JOIN CargosAdicionales c ON c.CargoID = hc.CargoID
                WHERE hc.Motivo = 0 AND {FiltroPeriodo("hc.FechaCambio", desde, hasta)}
                GROUP BY t.Nombre
                ORDER BY Valor DESC, t.Nombre";
            return ValoresEtiqueta(GetDataTable(query, ParametrosPeriodo(desde, hasta)));
        }

        /// <summary>Pedidos entregados por tiempo de entrega: hasta 15, 30, 45, 60 minutos y más de una hora.</summary>
        public int[] GetTiemposEntrega(DateTime? desde, DateTime? hasta)
        {
            string query = $@"
                SELECT ISNULL(SUM(CASE WHEN x.Minutos <= 15 THEN 1 ELSE 0 END), 0) AS B0,
                       ISNULL(SUM(CASE WHEN x.Minutos > 15 AND x.Minutos <= 30 THEN 1 ELSE 0 END), 0) AS B1,
                       ISNULL(SUM(CASE WHEN x.Minutos > 30 AND x.Minutos <= 45 THEN 1 ELSE 0 END), 0) AS B2,
                       ISNULL(SUM(CASE WHEN x.Minutos > 45 AND x.Minutos <= 60 THEN 1 ELSE 0 END), 0) AS B3,
                       ISNULL(SUM(CASE WHEN x.Minutos > 60 THEN 1 ELSE 0 END), 0) AS B4
                FROM (SELECT DATEDIFF(MINUTE, c.FechaHora, p.FechaHoraEntrega) AS Minutos
                      FROM CargosAdicionales c
                      INNER JOIN PedidosRoomService p ON p.CargoID = c.CargoID
                      WHERE p.Estado = 1 AND {FiltroPeriodo("c.FechaHora", desde, hasta)}) x";

            DataRow row = GetDataTable(query, ParametrosPeriodo(desde, hasta)).Rows[0];
            var tramos = new int[5];
            for (int i = 0; i < tramos.Length; i++) tramos[i] = Convert.ToInt32(row["B" + i]);
            return tramos;
        }

        /// <summary>Pedidos de room service sin entregar (sin filtro de período: es la cola de ahora).</summary>
        public List<PedidoPendiente_68SA> GetPedidosPendientes()
        {
            const string query = @"
                SELECT p.PedidoID, h.NroHabitacion, c.Monto, c.FechaHora,
                       ISNULL((SELECT SUM(d.Cantidad) FROM DetallePedidoRoomService d WHERE d.PedidoID = p.PedidoID), 0) AS Unidades
                FROM PedidosRoomService p
                INNER JOIN CargosAdicionales c ON c.CargoID = p.CargoID
                INNER JOIN Reservas r          ON r.ReservaID = c.ReservaID
                INNER JOIN Habitaciones h      ON h.HabitacionID = r.HabitacionID
                WHERE p.Estado = 0
                ORDER BY c.FechaHora";

            var lista = new List<PedidoPendiente_68SA>();
            foreach (DataRow row in GetDataTable(query).Rows)
                lista.Add(new PedidoPendiente_68SA
                {
                    PedidoID = Convert.ToInt32(row["PedidoID"]),
                    NroHabitacion = row["NroHabitacion"].ToString(),
                    Monto = Convert.ToDecimal(row["Monto"]),
                    FechaHora = Convert.ToDateTime(row["FechaHora"]),
                    Unidades = Convert.ToInt32(row["Unidades"])
                });
            return lista;
        }

        /// <summary>Últimos cambios de habitación del período (upgrades y reubicaciones), del más nuevo al más viejo.</summary>
        public List<CambioHabitacionItem_68SA> GetCambiosHabitacion(DateTime? desde, DateTime? hasta, int cantidad)
        {
            var parametros = new List<IDbDataParameter>(ParametrosPeriodo(desde, hasta)) { Param("@Top", Math.Max(1, cantidad)) };
            string query = $@"
                SELECT TOP (@Top) hc.FechaCambio, hc.ReservaID, hc.Motivo,
                       ha.NroHabitacion AS Anterior, hn.NroHabitacion AS Nueva, t.Nombre AS TipoNuevo,
                       ISNULL(c.Monto, 0) AS Monto
                FROM HistorialCambiosHabitacion hc
                INNER JOIN Habitaciones ha     ON ha.HabitacionID = hc.HabitacionAnteriorID
                INNER JOIN Habitaciones hn     ON hn.HabitacionID = hc.HabitacionNuevaID
                INNER JOIN TiposHabitacion t   ON t.TipoHabitacionID = hn.TipoHabitacionID
                LEFT  JOIN CargosAdicionales c ON c.CargoID = hc.CargoID
                WHERE {FiltroPeriodo("hc.FechaCambio", desde, hasta)}
                ORDER BY hc.FechaCambio DESC, hc.CambioID DESC";

            var lista = new List<CambioHabitacionItem_68SA>();
            foreach (DataRow row in GetDataTable(query, parametros.ToArray()).Rows)
                lista.Add(new CambioHabitacionItem_68SA
                {
                    FechaCambio = Convert.ToDateTime(row["FechaCambio"]),
                    ReservaID = Convert.ToInt32(row["ReservaID"]),
                    Motivo = Convert.ToInt32(row["Motivo"]),
                    HabitacionAnterior = row["Anterior"].ToString(),
                    HabitacionNueva = row["Nueva"].ToString(),
                    TipoNuevo = row["TipoNuevo"].ToString(),
                    Monto = Convert.ToDecimal(row["Monto"])
                });
            return lista;
        }

        #endregion

        #region Indicadores ampliados del dashboard

        // Inicio y fin (inclusive) del período como variables de la consulta. Sin "desde" se toma la primera
        // reserva registrada y sin "hasta", el día de hoy (histórico total).
        private static string DeclararPeriodo(DateTime? desde, DateTime? hasta)
        {
            string inicio = desde.HasValue ? "@Desde"
                : "(SELECT ISNULL(MIN(FechaIngreso), CAST(GETDATE() AS DATE)) FROM Reservas WHERE Estado <> 3)";
            string fin = hasta.HasValue ? "@Hasta" : "CAST(GETDATE() AS DATE)";
            return $@"
                DECLARE @PerDesde DATE = {inicio};
                DECLARE @PerHasta DATE = {fin};
                IF @PerDesde > @PerHasta SET @PerDesde = @PerHasta;
                DECLARE @PerFin DATE = DATEADD(DAY, 1, @PerHasta);";
        }

        public IndicadoresPeriodo_68SA GetIndicadoresPeriodo(DateTime? desde, DateTime? hasta)
        {
            // Reservas (no anuladas) que empiezan en el período: base del perfil de las reservas
            const string reservasDelPeriodo = "FROM Reservas r WHERE r.Estado <> 3 AND r.FechaIngreso >= @PerDesde AND r.FechaIngreso < @PerFin";

            string query = DeclararPeriodo(desde, hasta) + $@"
                DECLARE @Hab INT = (SELECT COUNT(*) FROM Habitaciones WHERE Estado <> 4);
                ;WITH Solapadas AS (
                    -- Noches de cada reserva que caen dentro del período
                    SELECT r.TarifaNoche,
                           DATEDIFF(DAY, CASE WHEN r.FechaIngreso > @PerDesde THEN r.FechaIngreso ELSE @PerDesde END,
                                         CASE WHEN r.FechaEgreso  < @PerFin   THEN r.FechaEgreso  ELSE @PerFin   END) AS Noches
                    FROM Reservas r
                    WHERE (r.Estado IN (1, 2) OR (r.Estado = 0 AND r.FechaEgreso > CAST(GETDATE() AS DATE))) AND r.FechaIngreso < @PerFin AND r.FechaEgreso > @PerDesde
                )
                SELECT
                    ISNULL((SELECT SUM(Noches) FROM Solapadas), 0) AS NochesVendidas,
                    ISNULL((SELECT SUM(Noches * TarifaNoche) FROM Solapadas), 0) AS IngresoAlojamiento,
                    DATEDIFF(DAY, @PerDesde, @PerFin) AS Dias,
                    @Hab AS Habitaciones,
                    (SELECT COUNT(*) {reservasDelPeriodo}) AS Reservas,
                    (SELECT AVG(CAST(CASE WHEN DATEDIFF(DAY, r.FechaRegistro, r.FechaIngreso) < 0 THEN 0
                                          ELSE DATEDIFF(DAY, r.FechaRegistro, r.FechaIngreso) END AS FLOAT)) {reservasDelPeriodo}) AS Anticipacion,
                    (SELECT AVG(CAST(DATEDIFF(DAY, r.FechaIngreso, r.FechaEgreso) AS FLOAT)) {reservasDelPeriodo}) AS Estadia,
                    (SELECT AVG(CAST(r.CantidadAdultos + r.CantidadNinos AS FLOAT)) {reservasDelPeriodo}) AS Personas,
                    (SELECT SUM(CASE WHEN r.CantidadNinos > 0 THEN 1.0 ELSE 0 END) * 100 / NULLIF(COUNT(*), 0) {reservasDelPeriodo}) AS ConNinos";

            DataRow row = GetDataTable(query, ParametrosPeriodo(desde, hasta)).Rows[0];
            double? Nulable(string columna) => row[columna] == DBNull.Value ? (double?)null : Convert.ToDouble(row[columna]);
            return new IndicadoresPeriodo_68SA
            {
                NochesVendidas = Convert.ToInt32(row["NochesVendidas"]),
                IngresoAlojamiento = Convert.ToDecimal(row["IngresoAlojamiento"]),
                Dias = Convert.ToInt32(row["Dias"]),
                HabitacionesOperativas = Convert.ToInt32(row["Habitaciones"]),
                Reservas = Convert.ToInt32(row["Reservas"]),
                AnticipacionPromedioDias = Nulable("Anticipacion"),
                EstadiaPromedioNoches = Nulable("Estadia"),
                PersonasPorReserva = Nulable("Personas"),
                PorcentajeConNinos = Nulable("ConNinos")
            };
        }

        /// <summary>Cobros del período por medio de pago: Etiqueta = número del enum MetodoPago.</summary>
        public List<ValorEtiqueta_68SA> GetPagosPorMetodo(DateTime? desde, DateTime? hasta)
        {
            string query = $@"
                SELECT CAST(MetodoPago AS NVARCHAR(10)) AS Etiqueta, COUNT(*) AS Cantidad, SUM(Monto) AS Valor
                FROM Pagos
                WHERE {FiltroPeriodo("FechaPago", desde, hasta)}
                GROUP BY MetodoPago
                ORDER BY MetodoPago";
            return ValoresEtiqueta(GetDataTable(query, ParametrosPeriodo(desde, hasta)));
        }

        public int[] GetLlegadasPorDiaSemana(DateTime? desde, DateTime? hasta)
        {
            // 01/01/1900 fue lunes: el resto de dividir por 7 da 0 = lunes ... 6 = domingo, sin depender de DATEFIRST
            string query = $@"
                SELECT x.Dia, COUNT(*) AS Cantidad
                FROM (SELECT DATEDIFF(DAY, CAST('19000101' AS DATE), r.FechaIngreso) % 7 AS Dia
                      FROM Reservas r
                      WHERE r.Estado <> 3 AND {FiltroPeriodo("r.FechaIngreso", desde, hasta)}) x
                GROUP BY x.Dia";

            var dias = new int[7];
            foreach (DataRow row in GetDataTable(query, ParametrosPeriodo(desde, hasta)).Rows)
            {
                int dia = Convert.ToInt32(row["Dia"]);
                if (dia >= 0 && dia < 7) dias[dia] = Convert.ToInt32(row["Cantidad"]);
            }
            return dias;
        }

        public List<PuntoSerie_68SA> GetMovimientoProximosDias(int dias)
        {
            const string query = @"
                DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
                ;WITH Dias AS (
                    SELECT 0 AS n UNION ALL SELECT n + 1 FROM Dias WHERE n + 1 < @Dias
                )
                SELECT DATEADD(DAY, d.n, @Hoy) AS Fecha,
                       (SELECT COUNT(*) FROM Reservas r WHERE r.FechaIngreso = DATEADD(DAY, d.n, @Hoy) AND r.Estado IN (0, 1, 2)) AS Valor,
                       (SELECT COUNT(*) FROM Reservas r WHERE r.FechaEgreso  = DATEADD(DAY, d.n, @Hoy) AND r.Estado IN (0, 1, 2)) AS Valor2
                FROM Dias d
                ORDER BY d.n
                OPTION (MAXRECURSION 400);";
            return Serie(GetDataTable(query, new[] { Param("@Dias", Math.Max(1, Math.Min(dias, 60))) }));
        }

        public List<OcupacionDia_68SA> GetOcupacionMensual(int meses)
        {
            const string query = @"
                DECLARE @Inicio DATE = DATEADD(MONTH, 1 - @Meses, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1));
                DECLARE @Hab INT = (SELECT COUNT(*) FROM Habitaciones WHERE Estado <> 4);
                ;WITH Meses AS (
                    SELECT 0 AS n UNION ALL SELECT n + 1 FROM Meses WHERE n + 1 < @Meses
                ),
                Rango AS (
                    SELECT DATEADD(MONTH, n, @Inicio) AS Desde, DATEADD(MONTH, n + 1, @Inicio) AS Hasta FROM Meses
                )
                -- Unión de cada mes con las reservas que lo tocan y suma agrupada por mes. Así las columnas
                -- del mes no son una referencia externa dentro del SUM (SQL Server no lo permite).
                SELECT m.Desde AS Fecha,
                       ISNULL(SUM(DATEDIFF(DAY, CASE WHEN r.FechaIngreso > m.Desde THEN r.FechaIngreso ELSE m.Desde END,
                                                CASE WHEN r.FechaEgreso  < m.Hasta THEN r.FechaEgreso  ELSE m.Hasta END)), 0) AS Ocupadas,
                       @Hab * DATEDIFF(DAY, m.Desde, m.Hasta) AS Total
                FROM Rango m
                LEFT JOIN Reservas r
                       ON (r.Estado IN (1, 2) OR (r.Estado = 0 AND r.FechaEgreso > CAST(GETDATE() AS DATE)))
                      AND r.FechaIngreso < m.Hasta AND r.FechaEgreso > m.Desde
                GROUP BY m.Desde, m.Hasta
                ORDER BY m.Desde
                OPTION (MAXRECURSION 400);";

            var lista = new List<OcupacionDia_68SA>();
            foreach (DataRow row in GetDataTable(query, new[] { Param("@Meses", Math.Max(1, Math.Min(meses, 36))) }).Rows)
                lista.Add(new OcupacionDia_68SA
                {
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Ocupadas = Convert.ToInt32(row["Ocupadas"]),
                    Total = Convert.ToInt32(row["Total"])
                });
            return lista;
        }

        /// <summary>Habitaciones con más noches ocupadas en los últimos días: Valor = noches, Cantidad = estadías.</summary>
        public List<ValorEtiqueta_68SA> GetHabitacionesMasOcupadas(int dias, int cantidad)
        {
            const string query = @"
                DECLARE @Fin DATE = DATEADD(DAY, 1, CAST(GETDATE() AS DATE));
                DECLARE @Ini DATE = DATEADD(DAY, -@Dias, @Fin);
                SELECT TOP (@Top) h.NroHabitacion + N' · ' + t.Nombre AS Etiqueta, COUNT(*) AS Cantidad,
                       SUM(DATEDIFF(DAY, CASE WHEN r.FechaIngreso > @Ini THEN r.FechaIngreso ELSE @Ini END,
                                         CASE WHEN r.FechaEgreso  < @Fin THEN r.FechaEgreso  ELSE @Fin END)) AS Valor
                FROM Reservas r
                INNER JOIN Habitaciones h    ON h.HabitacionID = r.HabitacionID
                INNER JOIN TiposHabitacion t ON t.TipoHabitacionID = h.TipoHabitacionID
                WHERE r.Estado IN (1, 2) AND r.FechaIngreso < @Fin AND r.FechaEgreso > @Ini
                GROUP BY h.NroHabitacion, t.Nombre
                ORDER BY Valor DESC, h.NroHabitacion";
            return ValoresEtiqueta(GetDataTable(query, new[]
            {
                Param("@Dias", Math.Max(1, Math.Min(dias, 730))),
                Param("@Top", Math.Max(1, cantidad))
            }));
        }

        /// <summary>
        /// Huéspedes alojados (titulares y acompañantes) por rango de edad al ingresar:
        /// Etiqueta = 0 (menores), 1 (18-24), 2 (25-34), 3 (35-49), 4 (50-64), 5 (65 o más).
        /// </summary>
        public List<ValorEtiqueta_68SA> GetRangoEtario(DateTime? desde, DateTime? hasta)
        {
            string filtro = "r.Estado <> 3 AND " + FiltroPeriodo("r.FechaIngreso", desde, hasta);
            string query = $@"
                SELECT CAST(x.Rango AS NVARCHAR(2)) AS Etiqueta, COUNT(*) AS Cantidad, COUNT(*) AS Valor
                FROM (
                    SELECT CASE WHEN e.Edad < 18 THEN 0 WHEN e.Edad < 25 THEN 1 WHEN e.Edad < 35 THEN 2
                                WHEN e.Edad < 50 THEN 3 WHEN e.Edad < 65 THEN 4 ELSE 5 END AS Rango
                    FROM (
                        SELECT hu.HuespedID,
                               (CONVERT(INT, CONVERT(CHAR(8), MAX(p.FechaIngreso), 112))
                              - CONVERT(INT, CONVERT(CHAR(8), hu.FechaNacimiento, 112))) / 10000 AS Edad
                        FROM (SELECT r.HuespedTitularID AS HuespedID, r.FechaIngreso FROM Reservas r WHERE {filtro}
                              UNION ALL
                              SELECT a.HuespedID, r.FechaIngreso FROM Acompanantes a
                              INNER JOIN Reservas r ON r.ReservaID = a.ReservaID WHERE {filtro}) p
                        INNER JOIN Huespedes hu ON hu.HuespedID = p.HuespedID
                        GROUP BY hu.HuespedID, hu.FechaNacimiento
                    ) e
                ) x
                GROUP BY x.Rango
                ORDER BY x.Rango";
            return ValoresEtiqueta(GetDataTable(query, ParametrosPeriodo(desde, hasta)));
        }

        /// <summary>Reservas por composición del grupo: Etiqueta = 0 (solo), 1 (pareja), 2 (con niños), 3 (grupo de adultos).</summary>
        public List<ValorEtiqueta_68SA> GetComposicionGrupos(DateTime? desde, DateTime? hasta)
        {
            string query = $@"
                SELECT CAST(x.Grupo AS NVARCHAR(2)) AS Etiqueta, COUNT(*) AS Cantidad, COUNT(*) AS Valor
                FROM (SELECT CASE WHEN r.CantidadNinos > 0 THEN 2
                                  WHEN r.CantidadAdultos <= 1 THEN 0
                                  WHEN r.CantidadAdultos = 2 THEN 1
                                  ELSE 3 END AS Grupo
                      FROM Reservas r
                      WHERE r.Estado <> 3 AND {FiltroPeriodo("r.FechaIngreso", desde, hasta)}) x
                GROUP BY x.Grupo
                ORDER BY x.Grupo";
            return ValoresEtiqueta(GetDataTable(query, ParametrosPeriodo(desde, hasta)));
        }

        /// <summary>Titulares que más gastaron en las reservas del período (alojamiento + servicios).</summary>
        public List<HuespedDestacado_68SA> GetTopHuespedes(DateTime? desde, DateTime? hasta, int cantidad)
        {
            var parametros = new List<IDbDataParameter>(ParametrosPeriodo(desde, hasta)) { Param("@Top", Math.Max(1, cantidad)) };
            string query = $@"
                SELECT TOP (@Top) hu.Apellido + N', ' + hu.Nombre AS Nombre,
                       COUNT(*) AS Estadias,
                       SUM(DATEDIFF(DAY, r.FechaIngreso, r.FechaEgreso)) AS Noches,
                       SUM(r.MontoTotal) AS MontoAlojamiento,
                       ISNULL(SUM(cg.Monto), 0) AS MontoServicios
                FROM Reservas r
                INNER JOIN Huespedes hu ON hu.HuespedID = r.HuespedTitularID
                OUTER APPLY (SELECT SUM(c.Monto) AS Monto FROM CargosAdicionales c WHERE c.ReservaID = r.ReservaID) cg
                WHERE r.Estado <> 3 AND {FiltroPeriodo("r.FechaIngreso", desde, hasta)}
                GROUP BY hu.HuespedID, hu.Apellido, hu.Nombre
                ORDER BY SUM(r.MontoTotal) + ISNULL(SUM(cg.Monto), 0) DESC, hu.Apellido";

            var lista = new List<HuespedDestacado_68SA>();
            foreach (DataRow row in GetDataTable(query, parametros.ToArray()).Rows)
                lista.Add(new HuespedDestacado_68SA
                {
                    Nombre = row["Nombre"].ToString(),
                    Estadias = Convert.ToInt32(row["Estadias"]),
                    Noches = Convert.ToInt32(row["Noches"]),
                    MontoAlojamiento = Convert.ToDecimal(row["MontoAlojamiento"]),
                    MontoServicios = Convert.ToDecimal(row["MontoServicios"])
                });
            return lista;
        }

        private static List<ValorEtiqueta_68SA> ValoresEtiqueta(DataTable dt)
        {
            var lista = new List<ValorEtiqueta_68SA>();
            foreach (DataRow row in dt.Rows)
                lista.Add(new ValorEtiqueta_68SA
                {
                    Etiqueta = row["Etiqueta"].ToString(),
                    Valor = row["Valor"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Valor"]),
                    Cantidad = row["Cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(row["Cantidad"])
                });
            return lista;
        }

        #endregion

        #region Helpers de período

        // Condición sobre una columna DATETIME: desde las 00:00 de "desde" hasta el final del día "hasta"
        private static string FiltroPeriodo(string columna, DateTime? desde, DateTime? hasta)
        {
            string filtro = "1=1";
            if (desde.HasValue) filtro += $" AND {columna} >= @Desde";
            if (hasta.HasValue) filtro += $" AND {columna} < @HastaExcl";
            return filtro;
        }

        // Cada consulta necesita sus propias instancias de parámetro
        private IDbDataParameter[] ParametrosPeriodo(DateTime? desde, DateTime? hasta)
        {
            var lista = new List<IDbDataParameter>();
            if (desde.HasValue) lista.Add(Param("@Desde", desde.Value.Date));
            if (hasta.HasValue)
            {
                lista.Add(Param("@Hasta", hasta.Value.Date));
                lista.Add(Param("@HastaExcl", hasta.Value.Date.AddDays(1)));
            }
            return lista.ToArray();
        }

        private static List<PuntoSerie_68SA> Serie(DataTable dt)
        {
            var lista = new List<PuntoSerie_68SA>();
            foreach (DataRow row in dt.Rows)
                lista.Add(new PuntoSerie_68SA
                {
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Valor = row["Valor"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Valor"]),
                    Valor2 = dt.Columns.Contains("Valor2") && row["Valor2"] != DBNull.Value ? Convert.ToDecimal(row["Valor2"]) : 0
                });
            return lista;
        }

        #endregion

        #region Consultas

        public ResumenOcupacion_68SA GetResumenOcupacion()
        {
            string query = @"
                ;WITH HabitacionesAgg AS (
                    SELECT
                        COUNT(*) AS Total,
                        ISNULL(SUM(CASE WHEN h.Estado = 0 AND rHoy.ReservaID IS NULL THEN 1 ELSE 0 END), 0) AS Disponibles,
                        ISNULL(SUM(CASE WHEN h.Estado = 0 AND rHoy.ReservaID IS NOT NULL THEN 1 ELSE 0 END), 0) AS Reservadas,
                        ISNULL(SUM(CASE WHEN h.Estado = 2 THEN 1 ELSE 0 END), 0) AS Ocupadas,
                        ISNULL(SUM(CASE WHEN h.Estado = 3 THEN 1 ELSE 0 END), 0) AS EnLimpieza,
                        ISNULL(SUM(CASE WHEN h.Estado = 4 THEN 1 ELSE 0 END), 0) AS FueraDeServicio
                    FROM Habitaciones h
                    LEFT JOIN Reservas rHoy ON rHoy.HabitacionID = h.HabitacionID AND rHoy.Estado = 0
                                             AND rHoy.FechaIngreso <= CAST(GETDATE() AS DATE)
                                             AND rHoy.FechaEgreso > CAST(GETDATE() AS DATE)
                ),
                EstadiaAgg AS (
                    SELECT ISNULL(AVG(CAST(DATEDIFF(MINUTE, CheckIn, CheckOut) AS FLOAT)), 0) / 60.0 AS DuracionPromedioHoras
                    FROM Reservas WHERE Estado = 2 -- Finalizada
                )
                SELECT ha.Total, ha.Disponibles, ha.Reservadas, ha.Ocupadas, ha.EnLimpieza, ha.FueraDeServicio, ea.DuracionPromedioHoras
                FROM HabitacionesAgg ha CROSS JOIN EstadiaAgg ea";

            DataTable dt = GetDataTable(query);
            var row = dt.Rows[0];

            var resumen = new ResumenOcupacion_68SA
            {
                Total = Convert.ToInt32(row["Total"]),
                Disponibles = Convert.ToInt32(row["Disponibles"]),
                Reservadas = Convert.ToInt32(row["Reservadas"]),
                Ocupadas = Convert.ToInt32(row["Ocupadas"]),
                EnLimpieza = Convert.ToInt32(row["EnLimpieza"]),
                FueraDeServicio = Convert.ToInt32(row["FueraDeServicio"]),
                DuracionPromedioEstadiaHoras = Convert.ToDouble(row["DuracionPromedioHoras"])
            };

            resumen.TasaOcupacionPorcentaje = resumen.Total > 0
                ? Math.Round((decimal)resumen.Ocupadas / resumen.Total * 100, 2)
                : 0;

            return resumen;
        }

        public List<OcupacionPorTipoItem_68SA> GetOcupacionPorTipo()
        {
            string query = @"
                SELECT t.Nombre AS TipoHabitacion_68SA, SUM(CASE WHEN h.Estado = 2 THEN 1 ELSE 0 END) AS CantidadOcupadas
                FROM Habitaciones h
                INNER JOIN TiposHabitacion t ON h.TipoHabitacionID = t.TipoHabitacionID
                GROUP BY t.Nombre
                ORDER BY t.Nombre";

            DataTable dt = GetDataTable(query);
            var lista = new List<OcupacionPorTipoItem_68SA>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new OcupacionPorTipoItem_68SA
                {
                    TipoHabitacion = row["TipoHabitacion_68SA"].ToString(),
                    CantidadOcupadas = Convert.ToInt32(row["CantidadOcupadas"])
                });
            }
            return lista;
        }

        public ResumenIngresos_68SA GetResumenIngresos(DateTime? desde, DateTime? hasta)
        {
            var parametros = new List<IDbDataParameter>();
            string filtroPagos = "1=1";
            string filtroReservas = "1=1";

            if (desde.HasValue)
            {
                filtroPagos += " AND FechaPago >= @Desde";
                filtroReservas += " AND r.FechaIngreso >= @Desde";
                parametros.Add(Param("@Desde", desde.Value.Date));
            }
            if (hasta.HasValue)
            {
                DateTime hastaInclusive = hasta.Value.Date.AddDays(1).AddTicks(-1);
                filtroPagos += " AND FechaPago <= @HastaInclusive";
                filtroReservas += " AND r.FechaIngreso <= @Hasta";
                parametros.Add(Param("@HastaInclusive", hastaInclusive));
                parametros.Add(Param("@Hasta", hasta.Value.Date));
            }

            string query = $@"
                ;WITH PagosAgg AS (
                    SELECT ISNULL(SUM(Monto), 0) AS IngresosPeriodo, COUNT(DISTINCT ReservaID) AS CantidadPagos
                    FROM Pagos WHERE {filtroPagos}
                ),
                ReservasAgg AS (
                    SELECT
                        COUNT(*) AS TotalReservasPeriodo,
                        ISNULL(SUM(CASE WHEN Estado = 3 THEN 1 ELSE 0 END), 0) AS ReservasAnuladas,
                        ISNULL(SUM(CASE WHEN Estado <> 3 THEN MontoOriginal ELSE 0 END), 0) AS MontoOrigenReserva,
                        ISNULL(SUM(CASE WHEN Estado <> 3 THEN MontoTotal - MontoOriginal ELSE 0 END), 0) AS MontoOrigenRenovacion
                    FROM Reservas r WHERE {filtroReservas}
                ),
                AnuladasAgg AS (
                    SELECT ISNULL(SUM(p.Monto), 0) AS MontoAnulado
                    FROM Pagos p
                    INNER JOIN Reservas r ON r.ReservaID = p.ReservaID
                    WHERE r.Estado = 3 AND {filtroReservas}
                ),
                -- PN2: cargos adicionales de las reservas del período (0 = room service, 1 = upgrade)
                CargosAgg AS (
                    SELECT ISNULL(SUM(CASE WHEN c.Tipo = 0 THEN c.Monto ELSE 0 END), 0) AS MontoOrigenRoomService,
                           ISNULL(SUM(CASE WHEN c.Tipo = 1 THEN c.Monto ELSE 0 END), 0) AS MontoOrigenUpgrade
                    FROM CargosAdicionales c
                    INNER JOIN Reservas r ON r.ReservaID = c.ReservaID
                    WHERE r.Estado <> 3 AND {filtroReservas}
                )
                SELECT pa.IngresosPeriodo, pa.CantidadPagos,
                       ra.TotalReservasPeriodo, ra.ReservasAnuladas, ra.MontoOrigenReserva, ra.MontoOrigenRenovacion,
                       aa.MontoAnulado, ca.MontoOrigenRoomService, ca.MontoOrigenUpgrade
                FROM PagosAgg pa CROSS JOIN ReservasAgg ra CROSS JOIN AnuladasAgg aa CROSS JOIN CargosAgg ca";

            DataTable dt = GetDataTable(query, parametros.ToArray());
            var row = dt.Rows[0];

            decimal ingresos = Convert.ToDecimal(row["IngresosPeriodo"]);
            int cantidadPagos = Convert.ToInt32(row["CantidadPagos"]);
            int totalReservasPeriodo = Convert.ToInt32(row["TotalReservasPeriodo"]);
            int reservasAnuladas = Convert.ToInt32(row["ReservasAnuladas"]);

            var resumen = new ResumenIngresos_68SA
            {
                IngresosPeriodo = ingresos,
                TicketPromedio = cantidadPagos > 0 ? Math.Round(ingresos / cantidadPagos, 2) : 0,
                PorcentajeReservasAnuladas = totalReservasPeriodo > 0
                    ? Math.Round((decimal)reservasAnuladas / totalReservasPeriodo * 100, 2) : 0,
                MontoAnuladoPeriodo = Convert.ToDecimal(row["MontoAnulado"])
            };

            resumen.OrigenDelGasto.Add(new OrigenGastoItem_68SA { Origen = "Reserva", Monto = Convert.ToDecimal(row["MontoOrigenReserva"]) });
            resumen.OrigenDelGasto.Add(new OrigenGastoItem_68SA { Origen = "Renovación", Monto = Convert.ToDecimal(row["MontoOrigenRenovacion"]) });
            resumen.OrigenDelGasto.Add(new OrigenGastoItem_68SA { Origen = "Servicio a la Habitación", Monto = Convert.ToDecimal(row["MontoOrigenRoomService"]) });
            resumen.OrigenDelGasto.Add(new OrigenGastoItem_68SA { Origen = "Cambio de Habitación", Monto = Convert.ToDecimal(row["MontoOrigenUpgrade"]) });

            return resumen;
        }

        public ResumenHuespedes_68SA GetResumenHuespedes(DateTime? desde, DateTime? hasta)
        {
            string filtroReservas = "1=1";
            string filtroPagos = "1=1";
            string condicionRecurrente = "0";

            if (desde.HasValue)
            {
                filtroReservas += " AND r.FechaIngreso >= @Desde";
                filtroPagos += " AND FechaPago >= @Desde";
                condicionRecurrente = @"CASE WHEN EXISTS (
                SELECT 1 FROM Reservas r2
                WHERE r2.HuespedTitularID = hu.HuespedID AND r2.FechaIngreso < @Desde
                ) THEN 1 ELSE 0 END";
            }
            if (hasta.HasValue)
            {
                filtroReservas += " AND r.FechaIngreso <= @Hasta";
                filtroPagos += " AND FechaPago <= @HastaInclusive";
            }

            // Cada llamada a GetDataTable necesita SUS PROPIAS instancias de parámetro — no se puede
            // reusar el mismo SqlParameter en dos comandos distintos.
            List<IDbDataParameter> ConstruirParametros()
            {
                var lista = new List<IDbDataParameter>();
                if (desde.HasValue)
                    lista.Add(Param("@Desde", desde.Value.Date));
                if (hasta.HasValue)
                {
                    DateTime hastaInclusive = hasta.Value.Date.AddDays(1).AddTicks(-1);
                    lista.Add(Param("@Hasta", hasta.Value.Date));
                    lista.Add(Param("@HastaInclusive", hastaInclusive));
                }
                return lista;
            }

            string queryResumen = $@"
                ;WITH HuespedesPeriodo AS (
                    SELECT hu.HuespedID, {condicionRecurrente} AS EsRecurrente
                    FROM Huespedes hu
                    INNER JOIN Reservas r ON r.HuespedTitularID = hu.HuespedID
                    WHERE {filtroReservas} AND r.Estado <> 3
                    GROUP BY hu.HuespedID
                ),
                IngresosAgg AS (
                    SELECT ISNULL(SUM(Monto), 0) AS IngresosPeriodo
                    FROM Pagos WHERE {filtroPagos}
                )
                SELECT
                    (SELECT COUNT(*) FROM HuespedesPeriodo) AS Total,
                    (SELECT ISNULL(SUM(EsRecurrente), 0) FROM HuespedesPeriodo) AS Recurrentes,
                    ia.IngresosPeriodo
                FROM IngresosAgg ia";

            DataTable dtResumen = GetDataTable(queryResumen, ConstruirParametros().ToArray());
            var rowResumen = dtResumen.Rows[0];

            int total = Convert.ToInt32(rowResumen["Total"]);
            int recurrentes = Convert.ToInt32(rowResumen["Recurrentes"]);
            decimal ingresosPeriodo = Convert.ToDecimal(rowResumen["IngresosPeriodo"]);

            var resumen = new ResumenHuespedes_68SA
            {
                TotalAtendidos = total,
                Nuevos = total - recurrentes,
                Recurrentes = recurrentes,
                TasaRecurrenciaPorcentaje = total > 0 ? Math.Round((decimal)recurrentes / total * 100, 2) : 0,
                GastoPromedioPorHuesped = total > 0 ? Math.Round(ingresosPeriodo / total, 2) : 0
            };

            string queryNacionalidad = $@"
                SELECT hu.Nacionalidad, COUNT(DISTINCT hu.HuespedID) AS Cantidad
                FROM Huespedes hu
                INNER JOIN Reservas r ON r.HuespedTitularID = hu.HuespedID
                WHERE {filtroReservas} AND r.Estado <> 3
                GROUP BY hu.Nacionalidad
                ORDER BY Cantidad DESC";

            DataTable dtNac = GetDataTable(queryNacionalidad, ConstruirParametros().ToArray());
            foreach (DataRow row in dtNac.Rows)
            {
                resumen.PorNacionalidad.Add(new NacionalidadItem_68SA
                {
                    Nacionalidad = row["Nacionalidad"].ToString(),
                    Cantidad = Convert.ToInt32(row["Cantidad"])
                });
            }

            return resumen;
        }

        #endregion
    }
}
