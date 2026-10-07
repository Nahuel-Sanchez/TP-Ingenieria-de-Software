using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace MPP_08YS
{
    /// <summary>Mapeo de los resultados de sp_AnalisisDemanda.</summary>
    public static class AnalisisDemandaMapper_68SA
    {
        #region Mapeo

        public static List<TipoHabitacionDemanda_68SA> TiposFromDataTable(DataTable dt) => Fila_68SA.Lista(dt, row =>
            new TipoHabitacionDemanda_68SA
            {
                Id = Convert.ToInt32(row["TipoHabitacionID"]),
                Nombre = Fila_68SA.Texto(row, "Nombre"),
                Tarifa = Fila_68SA.Decimal(row, "TarifaNoche"),
                Capacidad = Fila_68SA.Entero(row, "Capacidad") ?? 0,
                Habitaciones = Fila_68SA.Entero(row, "Operativas") ?? 0,
                HabitacionesTotales = Fila_68SA.Entero(row, "Habitaciones") ?? 0
            });

        public static List<ReservaDemanda_68SA> ReservasFromDataTable(DataTable dt) => Fila_68SA.Lista(dt, row =>
            new ReservaDemanda_68SA
            {
                Id = Convert.ToInt32(row["ReservaID"]),
                HuespedId = Convert.ToInt32(row["HuespedTitularID"]),
                Huesped = Fila_68SA.NombreCompleto(row, "Nombre", "Apellido"),
                Documento = Fila_68SA.Texto(row, "Documento"),
                TipoHabitacionId = Convert.ToInt32(row["TipoHabitacionID"]),
                Registro = Fila_68SA.Fecha(row, "FechaRegistro") ?? Convert.ToDateTime(row["FechaIngreso"]),
                Ingreso = Convert.ToDateTime(row["FechaIngreso"]),
                Egreso = Convert.ToDateTime(row["FechaEgreso"]),
                Estado = (EstadoReserva)Convert.ToInt32(row["Estado"]),
                TarifaNoche = Fila_68SA.Decimal(row, "TarifaNoche"),
                Monto = Fila_68SA.Decimal(row, "MontoTotal"),
                Personas = Fila_68SA.Entero(row, "Personas") ?? 1
            });

        public static List<CargoDemanda_68SA> CargosFromDataTable(DataTable dt) => Fila_68SA.Lista(dt, row =>
            new CargoDemanda_68SA
            {
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                Tipo = (TipoCargo)Convert.ToInt32(row["Tipo"]),
                Monto = Fila_68SA.Decimal(row, "Monto"),
                Fecha = Convert.ToDateTime(row["FechaHora"])
            });

        public static List<ProductoDemanda_68SA> ProductosFromDataTable(DataTable dt) => Fila_68SA.Lista(dt, row =>
            new ProductoDemanda_68SA
            {
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                Nombre = Fila_68SA.Texto(row, "Nombre"),
                Cantidad = Fila_68SA.Entero(row, "Cantidad") ?? 0,
                Monto = Fila_68SA.Decimal(row, "Monto"),
                Fecha = Convert.ToDateTime(row["FechaHora"])
            });

        #endregion
    }
}
