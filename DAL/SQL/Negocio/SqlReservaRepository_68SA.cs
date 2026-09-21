using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Interfaces_Repositories;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlReservaRepository_68SA : Connection_08YS, IReservaRepository_68SA
    {
        private const string BaseColumnas = @"
            r.ReservaID, r.HuespedTitularID, r.HabitacionID, r.FechaIngreso, r.FechaEgreso,
            r.CheckIn, r.CheckOut, r.Estado, r.CantidadAdultos, r.CantidadNinos, r.TarifaNoche, r.MontoTotal, r.MontoOriginal,
            r.UsuarioRegistroDNI, r.FechaRegistro,
            hab.NroHabitacion,
            hu.Documento AS DocumentoTitular, hu.Nombre AS NombreTitular, hu.Apellido AS ApellidoTitular,
            ureg.Nombre AS NombreUsuarioRegistro, ureg.Apellido AS ApellidoUsuarioRegistro";

        private const string BaseFrom = @"
            FROM Reservas r
            INNER JOIN Habitaciones hab ON r.HabitacionID = hab.HabitacionID
            INNER JOIN Huespedes hu ON r.HuespedTitularID = hu.HuespedID
            LEFT JOIN Users ureg ON r.UsuarioRegistroDNI = ureg.DNI";

        private const string BaseSelect = "SELECT " + BaseColumnas + BaseFrom;

        // Solo para el listado: trae el total pagado en la misma consulta (evita una query por fila)
        private const string ListadoSelect =
            "SELECT " + BaseColumnas + ", ISNULL(pg.MontoPagado, 0) AS MontoPagado" + BaseFrom + @"
             LEFT JOIN (SELECT ReservaID, SUM(Monto) AS MontoPagado FROM Pagos GROUP BY ReservaID) pg
             ON pg.ReservaID = r.ReservaID";

        public SqlReservaRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public List<Reserva_68SA> GetAll(ReservaFiltro_68SA filtro)
        {
            var where = new List<string>();
            var parametros = new List<IDbDataParameter>();

            #region filtros if
            if (!string.IsNullOrWhiteSpace(filtro.Huesped))
            {
                where.Add("(hu.Nombre LIKE @Huesped OR hu.Apellido LIKE @Huesped OR hu.Documento LIKE @Huesped)");
                parametros.Add(Param("@Huesped", $"%{filtro.Huesped.Trim()}%"));
            }
            if (!string.IsNullOrWhiteSpace(filtro.Habitacion))
            {
                where.Add("hab.NroHabitacion LIKE @Habitacion");
                parametros.Add(Param("@Habitacion", $"%{filtro.Habitacion.Trim()}%"));
            }
            if (!string.IsNullOrWhiteSpace(filtro.Registro))
            {
                where.Add("(ureg.Nombre LIKE @Registro OR ureg.Apellido LIKE @Registro)");
                parametros.Add(Param("@Registro", $"%{filtro.Registro.Trim()}%"));
            }
            if (filtro.Estado.HasValue)
            {
                where.Add("r.Estado = @Estado");
                parametros.Add(Param("@Estado", (int)filtro.Estado.Value));
            }
            if (filtro.FechaDesde.HasValue)
            {
                where.Add("r.FechaIngreso >= @FechaDesde");
                parametros.Add(Param("@FechaDesde", filtro.FechaDesde.Value.Date));
            }
            if (filtro.FechaHasta.HasValue)
            {
                where.Add("r.FechaIngreso <= @FechaHasta");
                parametros.Add(Param("@FechaHasta", filtro.FechaHasta.Value.Date.AddDays(1).AddTicks(-1)));
            }
            if (filtro.FechaEgresoDesde.HasValue)
            {
                where.Add("r.FechaEgreso >= @FechaEgresoDesde");
                parametros.Add(Param("@FechaEgresoDesde", filtro.FechaEgresoDesde.Value.Date));
            }
            if (filtro.FechaEgresoHasta.HasValue)
            {
                where.Add("r.FechaEgreso <= @FechaEgresoHasta");
                parametros.Add(Param("@FechaEgresoHasta", filtro.FechaEgresoHasta.Value.Date.AddDays(1).AddTicks(-1)));
            }
            if (filtro.CostoDesde.HasValue)
            {
                where.Add("r.MontoTotal >= @CostoDesde");
                parametros.Add(Param("@CostoDesde", filtro.CostoDesde.Value));
            }
            if (filtro.CostoHasta.HasValue)
            {
                where.Add("r.MontoTotal <= @CostoHasta");
                parametros.Add(Param("@CostoHasta", filtro.CostoHasta.Value));
            }
            #endregion

            string query = ListadoSelect
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY r.FechaIngreso DESC";

            DataTable dt = GetDataTable(query, parametros.ToArray());
            return ReservaMapper_68SA.FromDataTable(dt);
        }

        public int Crear(Reserva_68SA reserva)
        {
            var idOutput = ParamOutput("@NuevaReservaID");
            var disponibleOutput = ParamOutput("@Disponible");

            ExecuteNonQuery("sp_CrearReserva",
                new[]
                {
            Param("@HuespedTitularID", reserva.Titular.Id),
            Param("@HabitacionID",     reserva.Habitacion.Id),
            Param("@FechaIngreso",     reserva.FechaIngreso.Date),
            Param("@FechaEgreso",      reserva.FechaEgreso.Date),
            Param("@CantidadAdultos",  reserva.CantidadAdultos),
            Param("@CantidadNinos",    reserva.CantidadNinos),
            Param("@TarifaNoche",      reserva.TarifaNoche),
            Param("@MontoTotal",       reserva.MontoTotal),
            Param("@UsuarioRegistroDNI", (object)reserva.UsuarioRegistroDni ?? DBNull.Value),
            idOutput,
            disponibleOutput
                },
                storedProcedure: true);

            bool disponible = Convert.ToBoolean(disponibleOutput.Value);
            return disponible ? Convert.ToInt32(idOutput.Value) : -1;
        }

        public Reserva_68SA GetById(int reservaId)
        {
            DataTable dt = GetDataTable(BaseSelect + " WHERE r.ReservaID = @Id", new[] { Param("@Id", reservaId) });
            return dt.Rows.Count > 0 ? ReservaMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public Reserva_68SA GetPorDocumentoTitularYEstado(string documento, EstadoReserva estado)
        {
            string query = @"
                SELECT TOP 1 r.ReservaID, r.HuespedTitularID, r.HabitacionID, r.FechaIngreso, r.FechaEgreso,
                       r.CheckIn, r.CheckOut, r.Estado, r.CantidadAdultos, r.CantidadNinos, r.TarifaNoche, r.MontoTotal,
                       hab.NroHabitacion,
                       hu.Documento AS DocumentoTitular, hu.Nombre AS NombreTitular, hu.Apellido AS ApellidoTitular
                FROM Reservas r
                INNER JOIN Habitaciones hab ON r.HabitacionID = hab.HabitacionID
                INNER JOIN Huespedes hu ON r.HuespedTitularID = hu.HuespedID
                WHERE hu.Documento = @Documento AND r.Estado = @Estado
                ORDER BY r.FechaIngreso ASC";

            DataTable dt = GetDataTable(query, new[] { Param("@Documento", documento), Param("@Estado", (int)estado) });
            return dt.Rows.Count > 0 ? ReservaMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public Reserva_68SA GetConfirmadaHoyPorHabitacion(int habitacionId)
        {
            string query = BaseSelect + @"
                WHERE r.HabitacionID = @HabitacionID
                  AND r.Estado = 0 -- Confirmada
                  AND r.FechaIngreso <= CAST(GETDATE() AS DATE)
                  AND r.FechaEgreso > CAST(GETDATE() AS DATE)";

            DataTable dt = GetDataTable(query, new[] { Param("@HabitacionID", habitacionId) });
            return dt.Rows.Count > 0 ? ReservaMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public List<Reserva_68SA> GetEnRangoVisible(DateTime desde, DateTime hasta)
        {
            string query = BaseSelect + @"
                WHERE r.Estado <> 3 -- excluye Canceladas
                  AND r.FechaIngreso < @Hasta
                  AND r.FechaEgreso > @Desde";

            DataTable dt = GetDataTable(query, new[] { Param("@Desde", desde.Date), Param("@Hasta", hasta.Date) });
            return ReservaMapper_68SA.FromDataTable(dt);
        }

        public bool RegistrarCheckIn(int reservaId, DateTime fechaHora)
        {
            var resultado = ParamOutput("@Resultado");
            ExecuteNonQuery("sp_RegistrarCheckIn",
                new[] { Param("@ReservaID", reservaId), Param("@FechaHora", fechaHora), resultado },
                storedProcedure: true);
            return Convert.ToBoolean(resultado.Value);
        }

        public bool RegistrarCheckOut(int reservaId, DateTime fechaHora)
        {
            var resultado = ParamOutput("@Resultado");
            ExecuteNonQuery("sp_RegistrarCheckOut",
                new[] { Param("@ReservaID", reservaId), Param("@FechaHora", fechaHora), resultado },
                storedProcedure: true);
            return Convert.ToBoolean(resultado.Value);
        }

        public bool Cancelar(int reservaId)
        {
            var resultado = ParamOutput("@Resultado");
            ExecuteNonQuery("sp_CancelarReserva",
                new[] { Param("@ReservaID", reservaId), resultado },
                storedProcedure: true);
            return Convert.ToBoolean(resultado.Value);
        }

        public void AgregarAcompanante(int reservaId, int huespedId)
        {
            ExecuteNonQuery(
                "INSERT INTO Acompanantes (ReservaID, HuespedID) VALUES (@ReservaID, @HuespedID)",
                new[] { Param("@ReservaID", reservaId), Param("@HuespedID", huespedId) });
        }

        public List<Huesped_68SA> GetAcompanantes(int reservaId)
        {
            DataTable dt = GetDataTable(
                @"SELECT hu.HuespedID, hu.Nombre, hu.Apellido, hu.Documento, hu.TipoDocumento,
                         hu.Nacionalidad, hu.FechaNacimiento, hu.Email, hu.Telefono
                  FROM Huespedes hu
                  INNER JOIN Acompanantes a ON hu.HuespedID = a.HuespedID
                  WHERE a.ReservaID = @ReservaID",
                new[] { Param("@ReservaID", reservaId) });

            return HuespedMapper_68SA.FromDataTable(dt);
        }

        public Reserva_68SA GetEnCursoPorHabitacion(int habitacionId)
        {
            DataTable dt = GetDataTable(
                BaseSelect + " WHERE r.HabitacionID = @HabitacionID AND r.Estado = 1", // EnCurso
                new[] { Param("@HabitacionID", habitacionId) });

            return dt.Rows.Count > 0 ? ReservaMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public int ProcesarNoShows()
        {
            var salida = ParamOutput("@CantidadCanceladas");
            ExecuteNonQuery("sp_ProcesarNoShows", new[] { salida }, storedProcedure: true);
            return Convert.ToInt32(salida.Value);
        }

        public bool ExtenderEstadia(int reservaId, DateTime nuevaFechaEgreso)
        {
            var disponibleOutput = ParamOutput("@Disponible");
            ExecuteNonQuery("sp_ExtenderEstadia",
                new[] { Param("@ReservaID", reservaId), Param("@NuevaFechaEgreso", nuevaFechaEgreso.Date), disponibleOutput },
                storedProcedure: true);
            return Convert.ToBoolean(disponibleOutput.Value);
        }

    }
}