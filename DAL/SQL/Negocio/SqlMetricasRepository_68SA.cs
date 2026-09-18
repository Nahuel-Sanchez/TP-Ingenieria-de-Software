using BE_08YS;
using BE_08YS.Metricas;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Repositories_Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS
{
    public class SqlMetricasRepository_68SA : Connection_08YS, IMetricasRepository_68SA
    {
        public SqlMetricasRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public ResumenOcupacion_68SA GetResumenOcupacion()
        {
            string query = @"
                ;WITH HabitacionesAgg AS (
                    SELECT
                        COUNT(*) AS Total,
                        SUM(CASE WHEN h.Estado = 0 AND rHoy.ReservaID IS NULL THEN 1 ELSE 0 END) AS Disponibles,
                        SUM(CASE WHEN h.Estado = 0 AND rHoy.ReservaID IS NOT NULL THEN 1 ELSE 0 END) AS Reservadas,
                        SUM(CASE WHEN h.Estado = 2 THEN 1 ELSE 0 END) AS Ocupadas,
                        SUM(CASE WHEN h.Estado = 3 THEN 1 ELSE 0 END) AS EnLimpieza,
                        SUM(CASE WHEN h.Estado = 4 THEN 1 ELSE 0 END) AS FueraDeServicio
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
                SELECT t.Nombre AS TipoHabitacion, SUM(CASE WHEN h.Estado = 2 THEN 1 ELSE 0 END) AS CantidadOcupadas
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
                    TipoHabitacion = row["TipoHabitacion"].ToString(),
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
                        SUM(CASE WHEN Estado = 3 THEN 1 ELSE 0 END) AS ReservasAnuladas,
                        SUM(CASE WHEN Estado <> 3 THEN MontoOriginal ELSE 0 END) AS MontoOrigenReserva,
                        SUM(CASE WHEN Estado <> 3 THEN MontoTotal - MontoOriginal ELSE 0 END) AS MontoOrigenRenovacion
                    FROM Reservas r WHERE {filtroReservas}
                ),
                AnuladasAgg AS (
                    SELECT ISNULL(SUM(p.Monto), 0) AS MontoAnulado
                    FROM Pagos p
                    INNER JOIN Reservas r ON r.ReservaID = p.ReservaID
                    WHERE r.Estado = 3 AND {filtroReservas}
                )
                SELECT pa.IngresosPeriodo, pa.CantidadPagos,
                       ra.TotalReservasPeriodo, ra.ReservasAnuladas, ra.MontoOrigenReserva, ra.MontoOrigenRenovacion,
                       aa.MontoAnulado
                FROM PagosAgg pa CROSS JOIN ReservasAgg ra CROSS JOIN AnuladasAgg aa";

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
            resumen.OrigenDelGasto.Add(new OrigenGastoItem_68SA { Origen = "Servicio a la Habitación", Monto = 0 });
            resumen.OrigenDelGasto.Add(new OrigenGastoItem_68SA { Origen = "Cambio de Habitación", Monto = 0 });

            return resumen;
        }

        public ResumenHuespedes_68SA GetResumenHuespedes(DateTime desde, DateTime hasta)
        {
            DateTime hastaInclusive = hasta.Date.AddDays(1).AddTicks(-1);

            string queryResumen = @"
                ;WITH HuespedesPeriodo AS (
                    SELECT hu.HuespedID,
                           CASE WHEN EXISTS (
                               SELECT 1 FROM Reservas r2
                               WHERE r2.HuespedTitularID = hu.HuespedID AND r2.FechaIngreso < @Desde
                           ) THEN 1 ELSE 0 END AS EsRecurrente
                    FROM Huespedes hu
                    INNER JOIN Reservas r ON r.HuespedTitularID = hu.HuespedID
                    WHERE r.FechaIngreso >= @Desde AND r.FechaIngreso <= @Hasta AND r.Estado <> 3
                    GROUP BY hu.HuespedID
                ),
                IngresosAgg AS (
                    SELECT ISNULL(SUM(Monto), 0) AS IngresosPeriodo
                    FROM Pagos
                    WHERE FechaPago >= @Desde AND FechaPago <= @HastaInclusive
                )
                SELECT
                    (SELECT COUNT(*) FROM HuespedesPeriodo) AS Total,
                    (SELECT ISNULL(SUM(EsRecurrente), 0) FROM HuespedesPeriodo) AS Recurrentes,
                    ia.IngresosPeriodo
                FROM IngresosAgg ia";

            DataTable dtResumen = GetDataTable(queryResumen, new[]
            {
                Param("@Desde", desde.Date),
                Param("@Hasta", hasta.Date),
                Param("@HastaInclusive", hastaInclusive)
            });
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

            string queryNacionalidad = @"
                SELECT hu.Nacionalidad, COUNT(DISTINCT hu.HuespedID) AS Cantidad
                FROM Huespedes hu
                INNER JOIN Reservas r ON r.HuespedTitularID = hu.HuespedID
                WHERE r.FechaIngreso >= @Desde AND r.FechaIngreso <= @Hasta AND r.Estado <> 3
                GROUP BY hu.Nacionalidad
                ORDER BY Cantidad DESC";

            DataTable dtNac = GetDataTable(queryNacionalidad, new[] { Param("@Desde", desde.Date), Param("@Hasta", hasta.Date) });
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
    }
}