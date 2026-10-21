using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace MPP_08YS
{
    /// <summary>Registros de la bitácora de reservas, con la foto anterior de la misma reserva (columnas "Ant_").</summary>
    public static class BitacoraReservaMapper_68SA
    {
        #region Mapeo

        public static List<BitacoraReserva_68SA> FromDataTable(DataTable dt) => Fila_68SA.Lista(dt, FromDataRow);

        public static BitacoraReserva_68SA FromDataRow(DataRow row)
        {
            return new BitacoraReserva_68SA
            {
                Id = Convert.ToInt32(row["BitacoraID"]),
                Operacion = BitacoraReserva_68SA.DesdeTextoBase(row["Operacion"].ToString()),
                CambioId = Fila_68SA.Entero(row, "CambioID"),
                Fecha = Convert.ToDateTime(row["Fecha"]),
                Hora = (TimeSpan)row["Hora"],
                UsuarioDB = row["UsuarioDB"].ToString(),
                UsuarioApp = Fila_68SA.Texto(row, "UsuarioApp"),
                Act = Convert.ToBoolean(row["Act"]),
                RollbackDeId = Fila_68SA.Entero(row, "RollbackDeID"),
                Reserva = Foto(row, ""),
                Anterior = row["Ant_BitacoraID"] == DBNull.Value ? null : Foto(row, "Ant_")
            };
        }

        #endregion

        #region Métodos auxiliares

        // Lee una foto de la reserva; prefijo "" = la del registro, "Ant_" = la del registro anterior
        private static FotoReserva_68SA Foto(DataRow row, string prefijo)
        {
            return new FotoReserva_68SA
            {
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                HuespedTitularId = Convert.ToInt32(row[prefijo + "HuespedTitularID"]),
                Titular = Fila_68SA.NombreCompleto(row, prefijo + "NombreTitular", prefijo + "ApellidoTitular"),
                HabitacionId = Convert.ToInt32(row[prefijo + "HabitacionID"]),
                NroHabitacion = Fila_68SA.Texto(row, prefijo + "NroHabitacion"),
                TipoHabitacion = Fila_68SA.Texto(row, prefijo + "TipoHabitacion"),
                FechaIngreso = Convert.ToDateTime(row[prefijo + "FechaIngreso"]),
                FechaEgreso = Convert.ToDateTime(row[prefijo + "FechaEgreso"]),
                CheckIn = Fila_68SA.Fecha(row, prefijo + "CheckIn"),
                CheckOut = Fila_68SA.Fecha(row, prefijo + "CheckOut"),
                Estado = (EstadoReserva)Convert.ToInt32(row[prefijo + "Estado"]),
                CantidadAdultos = Convert.ToInt32(row[prefijo + "CantidadAdultos"]),
                CantidadNinos = Convert.ToInt32(row[prefijo + "CantidadNinos"]),
                TarifaNoche = Convert.ToDecimal(row[prefijo + "TarifaNoche"]),
                MontoTotal = Convert.ToDecimal(row[prefijo + "MontoTotal"]),
                MontoOriginal = Convert.ToDecimal(row[prefijo + "MontoOriginal"]),
                UsuarioRegistroDni = Fila_68SA.Entero(row, prefijo + "UsuarioRegistroDNI"),
                UsuarioRegistro = Fila_68SA.Texto(row, prefijo + "UsuarioRegistro"),
                FechaRegistro = Convert.ToDateTime(row[prefijo + "FechaRegistro"])
            };
        }

        #endregion
    }
}
