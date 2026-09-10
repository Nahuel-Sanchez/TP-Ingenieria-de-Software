using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlReservaRepository_68SA : Connection_08YS, IReservaRepository_68SA
    {
        // Alias usados por ReservaMapper_08YS: NroHabitacion, DocumentoTitular, NombreTitular, ApellidoTitular
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
            return ExecuteScalar<int>(
                @"INSERT INTO Reservas (HuespedTitularID, HabitacionID, FechaIngreso, FechaEgreso, Estado,
                                         CantidadAdultos, CantidadNinos, TarifaNoche, MontoTotal)
                  VALUES (@HuespedTitularID, @HabitacionID, @FechaIngreso, @FechaEgreso, @Estado,
                          @CantidadAdultos, @CantidadNinos, @TarifaNoche, @MontoTotal);
                  SELECT CAST(SCOPE_IDENTITY() AS int);",
                new[]
                {
                    Param("@HuespedTitularID", reserva.Titular.Id),
                    Param("@HabitacionID",     reserva.Habitacion.Id),
                    Param("@FechaIngreso",     reserva.FechaIngreso.Date),
                    Param("@FechaEgreso",      reserva.FechaEgreso.Date),
                    Param("@Estado",           (int)reserva.Estado),
                    Param("@CantidadAdultos",  reserva.CantidadAdultos),
                    Param("@CantidadNinos",    reserva.CantidadNinos),
                    Param("@TarifaNoche",      reserva.TarifaNoche),
                    Param("@MontoTotal",       reserva.MontoTotal)
                });
        }

        public Reserva_68SA GetById(int reservaId)
        {
            DataTable dt = GetDataTable(
                BaseSelect + " WHERE r.ReservaID = @Id",
                new[] { Param("@Id", reservaId) });

            return dt.Rows.Count > 0 ? ReservaMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public Reserva_68SA GetConfirmadaPorDocumentoTitular(string documento)
        {
            string query = @"
                SELECT TOP 1 r.ReservaID, r.HuespedTitularID, r.HabitacionID, r.FechaIngreso, r.FechaEgreso,
                       r.CheckIn, r.CheckOut, r.Estado, r.CantidadAdultos, r.CantidadNinos, r.TarifaNoche, r.MontoTotal,
                       hab.NroHabitacion,
                       hu.Documento AS DocumentoTitular, hu.Nombre AS NombreTitular, hu.Apellido AS ApellidoTitular
                FROM Reservas r
                INNER JOIN Habitaciones hab ON r.HabitacionID = hab.HabitacionID
                INNER JOIN Huespedes hu ON r.HuespedTitularID = hu.HuespedID
                WHERE hu.Documento = @Documento AND r.Estado = 0 -- Confirmada
                ORDER BY r.FechaIngreso ASC";

            DataTable dt = GetDataTable(query, new[] { Param("@Documento", documento) });
            return dt.Rows.Count > 0 ? ReservaMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public bool ExisteSolapamiento(int habitacionId, DateTime fechaIngreso, DateTime fechaEgreso)
        {
            return ExecuteScalar<int>(
                @"SELECT COUNT(1) FROM Reservas
                  WHERE HabitacionID = @HabitacionID
                    AND Estado IN (0, 1) -- Confirmada, EnCurso
                    AND FechaIngreso < @FechaEgreso
                    AND FechaEgreso > @FechaIngreso",
                new[]
                {
                    Param("@HabitacionID", habitacionId),
                    Param("@FechaIngreso", fechaIngreso.Date),
                    Param("@FechaEgreso",  fechaEgreso.Date)
                }) > 0;
        }

        public void RegistrarCheckIn(int reservaId, DateTime fechaHora)
        {
            ExecuteNonQuery(
                "UPDATE Reservas SET CheckIn = @Fecha, Estado = 1 WHERE ReservaID = @Id", // 1 = EnCurso
                new[] { Param("@Fecha", fechaHora), Param("@Id", reservaId) });
        }

        public void RegistrarCheckOut(int reservaId, DateTime fechaHora)
        {
            ExecuteNonQuery(
                "UPDATE Reservas SET CheckOut = @Fecha, Estado = 2 WHERE ReservaID = @Id", // 2 = Finalizada
                new[] { Param("@Fecha", fechaHora), Param("@Id", reservaId) });
        }

        public void Cancelar(int reservaId)
        {
            ExecuteNonQuery(
                "UPDATE Reservas SET Estado = 3 WHERE ReservaID = @Id", // 3 = Cancelada
                new[] { Param("@Id", reservaId) });
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
    }
}
