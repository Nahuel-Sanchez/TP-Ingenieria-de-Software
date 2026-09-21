using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_08YS
{
    public static class HabitacionMapper_68SA
    {
        public static List<Habitacion_68SA> FromDataTable(DataTable dt)
        {
            var habitaciones = new List<Habitacion_68SA>();
            foreach (DataRow row in dt.Rows)
                habitaciones.Add(FromDataRow(row));
            return habitaciones;
        }

        public static Habitacion_68SA FromDataRow(DataRow row)
        {
            var habitacion = new Habitacion_68SA
            {
                Id = Convert.ToInt32(row["HabitacionID"]),
                NroHabitacion = row["NroHabitacion"].ToString(),
                Estado = (EstadoHabitacion)Convert.ToInt32(row["Estado"]),
                Piso = new Piso_68SA
                {
                    PisoId = Convert.ToInt32(row["PisoID"]),
                    Numero = Convert.ToInt32(row["Numero"]),
                    Nombre = row["NombrePiso"].ToString()
                },
                Tipo = new TipoHabitacion
                {
                    Id = Convert.ToInt32(row["TipoHabitacionID"]),
                    Nombre = row["NombreTipo"].ToString(),
                    Descripcion = row["Descripcion"] == DBNull.Value ? null : row["Descripcion"].ToString(),
                    Capacidad = Convert.ToInt32(row["Capacidad"]),
                    TarifaNoche = Convert.ToDecimal(row["TarifaNoche"])
                },
                TieneReservaHoy = row.Table.Columns.Contains("TieneReservaHoy") && Convert.ToInt32(row["TieneReservaHoy"]) == 1,
            };

            if (row.Table.Columns.Contains("CantidadReservas") && row["CantidadReservas"] != DBNull.Value)
                habitacion.CantidadReservas = Convert.ToInt32(row["CantidadReservas"]);

            if (row.Table.Columns.Contains("CantidadReservasActivas") && row["CantidadReservasActivas"] != DBNull.Value)
                habitacion.CantidadReservasActivas = Convert.ToInt32(row["CantidadReservasActivas"]);

            return habitacion;
        }
    }

    public static class PisoMapper_08YS
    {
        public static List<Piso_68SA> FromDataTable(DataTable dt)
        {
            var pisos = new List<Piso_68SA>();
            foreach (DataRow row in dt.Rows)
                pisos.Add(FromDataRow(row));
            return pisos;
        }

        public static Piso_68SA FromDataRow(DataRow row)
        {
            var piso = new Piso_68SA
            {
                PisoId = Convert.ToInt32(row["PisoID"]),
                Numero = Convert.ToInt32(row["Numero"]),
                Nombre = row["Nombre"].ToString()
            };

            if (row.Table.Columns.Contains("CantidadHabitaciones") && row["CantidadHabitaciones"] != DBNull.Value)
                piso.CantidadHabitaciones = Convert.ToInt32(row["CantidadHabitaciones"]);

            return piso;
        }
    }

    public static class TipoHabitacionMapper_08YS
    {
        public static List<TipoHabitacion> FromDataTable(DataTable dt)
        {
            var tipos = new List<TipoHabitacion>();
            foreach (DataRow row in dt.Rows)
                tipos.Add(FromDataRow(row));
            return tipos;
        }

        public static TipoHabitacion FromDataRow(DataRow row)
        {
            var tipo = new TipoHabitacion
            {
                Id = Convert.ToInt32(row["TipoHabitacionID"]),
                Nombre = row["Nombre"].ToString(),
                Descripcion = row["Descripcion"] == DBNull.Value ? null : row["Descripcion"].ToString(),
                Capacidad = Convert.ToInt32(row["Capacidad"]),
                TarifaNoche = Convert.ToDecimal(row["TarifaNoche"])
            };

            if (row.Table.Columns.Contains("CantidadHabitaciones") && row["CantidadHabitaciones"] != DBNull.Value)
                tipo.CantidadHabitaciones = Convert.ToInt32(row["CantidadHabitaciones"]);

            return tipo;
        }
    }
}
