using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_08YS
{
    public static class ReservaMapper_68SA
    {
        public static List<Reserva_68SA> FromDataTable(DataTable dt)
        {
            var reservas = new List<Reserva_68SA>();
            foreach (DataRow row in dt.Rows)
                reservas.Add(FromDataRow(row));
            return reservas;
        }

        public static Reserva_68SA FromDataRow(DataRow row)
        {
            var reserva = new Reserva_68SA
            {
                Id = Convert.ToInt32(row["ReservaID"]),
                FechaIngreso = Convert.ToDateTime(row["FechaIngreso"]),
                FechaEgreso = Convert.ToDateTime(row["FechaEgreso"]),
                CheckIn = row["CheckIn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["CheckIn"]),
                CheckOut = row["CheckOut"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["CheckOut"]),
                Estado = (EstadoReserva)Convert.ToInt32(row["Estado"]),
                CantidadAdultos = Convert.ToInt32(row["CantidadAdultos"]),
                CantidadNinos = Convert.ToInt32(row["CantidadNinos"]),
                TarifaNoche = Convert.ToDecimal(row["TarifaNoche"]),
                MontoTotal = Convert.ToDecimal(row["MontoTotal"]),

                Habitacion = new Habitacion_68SA { Id = Convert.ToInt32(row["HabitacionID"]) },
                Titular = new Huesped_68SA { Id = Convert.ToInt32(row["HuespedTitularID"]) }
            };

            reserva.MontoOriginal = row.Table.Columns.Contains("MontoOriginal") && row["MontoOriginal"] != DBNull.Value
                                        ? Convert.ToDecimal(row["MontoOriginal"])
                                        : reserva.MontoTotal;

            if (row.Table.Columns.Contains("NroHabitacion") && row["NroHabitacion"] != DBNull.Value)
                reserva.Habitacion.NroHabitacion = row["NroHabitacion"].ToString();

            if (row.Table.Columns.Contains("DocumentoTitular") && row["DocumentoTitular"] != DBNull.Value)
                reserva.Titular.Documento = row["DocumentoTitular"].ToString();

            if (row.Table.Columns.Contains("NombreTitular") && row["NombreTitular"] != DBNull.Value)
                reserva.Titular.Nombre = row["NombreTitular"].ToString();

            if (row.Table.Columns.Contains("ApellidoTitular") && row["ApellidoTitular"] != DBNull.Value)
                reserva.Titular.Apellido = row["ApellidoTitular"].ToString();

            if (row.Table.Columns.Contains("UsuarioRegistroDNI") && row["UsuarioRegistroDNI"] != DBNull.Value)
                reserva.UsuarioRegistroDni = Convert.ToInt32(row["UsuarioRegistroDNI"]);

            if (row.Table.Columns.Contains("FechaRegistro") && row["FechaRegistro"] != DBNull.Value)
                reserva.FechaRegistro = Convert.ToDateTime(row["FechaRegistro"]);

            if (row.Table.Columns.Contains("NombreUsuarioRegistro") && row["NombreUsuarioRegistro"] != DBNull.Value)
                reserva.UsuarioRegistroNombre = $"{row["NombreUsuarioRegistro"]} {row["ApellidoUsuarioRegistro"]}";

            return reserva;
        }
    }
}
