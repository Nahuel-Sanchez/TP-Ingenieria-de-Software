using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Repositories_Interfaces;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlReservaRepository_68SA : Connection_08YS, IReservaRepository_68SA
    {
        private const string BaseSelect = @"
            SELECT r.ReservaID, r.HuespedTitularID, r.HabitacionID, r.FechaIngreso, r.FechaEgreso,
                   r.CheckIn, r.CheckOut, r.Estado, r.CantidadAdultos, r.CantidadNinos, r.TarifaNoche, r.MontoTotal,
                   hab.NroHabitacion,
                   hu.Documento AS DocumentoTitular, hu.Nombre AS NombreTitular, hu.Apellido AS ApellidoTitular
            FROM Reservas r
            INNER JOIN Habitaciones hab ON r.HabitacionID = hab.HabitacionID
            INNER JOIN Huespedes hu ON r.HuespedTitularID = hu.HuespedID";

        public SqlReservaRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

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

        public int ProcesarNoShows()
        {
            var salida = ParamOutput("@CantidadCanceladas");
            ExecuteNonQuery("sp_ProcesarNoShows", new[] { salida }, storedProcedure: true);
            return Convert.ToInt32(salida.Value);
        }
    }
}