using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace MPP_08YS
{
    /// <summary>Lectura segura de columnas opcionales / con NULL (compartida por los mappers del PN2).</summary>
    internal static class Fila_68SA
    {
        #region Mapeo

        public static bool Tiene(DataRow row, string columna)
            => row.Table.Columns.Contains(columna) && row[columna] != DBNull.Value;

        public static string Texto(DataRow row, string columna)
            => Tiene(row, columna) ? row[columna].ToString() : null;

        public static int? Entero(DataRow row, string columna)
            => Tiene(row, columna) ? Convert.ToInt32(row[columna]) : (int?)null;

        public static decimal Decimal(DataRow row, string columna)
            => Tiene(row, columna) ? Convert.ToDecimal(row[columna]) : 0m;

        public static DateTime? Fecha(DataRow row, string columna)
            => Tiene(row, columna) ? Convert.ToDateTime(row[columna]) : (DateTime?)null;

        public static string NombreCompleto(DataRow row, string columnaNombre, string columnaApellido)
        {
            string nombre = Texto(row, columnaNombre);
            string apellido = Texto(row, columnaApellido);
            if (nombre == null && apellido == null) return null;
            return $"{nombre} {apellido}".Trim();
        }

        public static List<T> Lista<T>(DataTable dt, Func<DataRow, T> mapear)
        {
            var lista = new List<T>(dt.Rows.Count);
            foreach (DataRow row in dt.Rows)
                lista.Add(mapear(row));
            return lista;
        }

        #endregion
    }

    public static class CargoAdicionalMapper_68SA
    {
        public static List<CargoAdicional_68SA> FromDataTable(DataTable dt) => Fila_68SA.Lista(dt, FromDataRow);

        public static CargoAdicional_68SA FromDataRow(DataRow row)
        {
            return new CargoAdicional_68SA
            {
                Id = Convert.ToInt32(row["CargoID"]),
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                Tipo = (TipoCargo)Convert.ToInt32(row["Tipo"]),
                Descripcion = row["Descripcion"].ToString(),
                FechaHora = Convert.ToDateTime(row["FechaHora"]),
                Monto = Convert.ToDecimal(row["Monto"]),
                UsuarioRegistroDni = Fila_68SA.Entero(row, "UsuarioRegistroDNI"),
                UsuarioRegistroNombre = Fila_68SA.NombreCompleto(row, "NombreUsuario", "ApellidoUsuario")
            };
        }
    }

    public static class ItemRoomServiceMapper_68SA
    {
        public static List<ItemRoomService_68SA> FromDataTable(DataTable dt) => Fila_68SA.Lista(dt, FromDataRow);

        public static ItemRoomService_68SA FromDataRow(DataRow row)
        {
            return new ItemRoomService_68SA
            {
                Id = Convert.ToInt32(row["ItemID"]),
                Nombre = row["Nombre"].ToString(),
                Descripcion = Fila_68SA.Texto(row, "Descripcion"),
                PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"]),
                Activo = Convert.ToBoolean(row["Activo"])
            };
        }
    }

    public static class PedidoRoomServiceMapper_68SA
    {
        #region Mapeo

        public static List<PedidoRoomService_68SA> FromDataTable(DataTable dt) => Fila_68SA.Lista(dt, FromDataRow);

        public static PedidoRoomService_68SA FromDataRow(DataRow row)
        {
            return new PedidoRoomService_68SA
            {
                Id = Convert.ToInt32(row["PedidoID"]),
                CargoId = Convert.ToInt32(row["CargoID"]),
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                NroHabitacion = Fila_68SA.Texto(row, "NroHabitacion"),
                Huesped = Fila_68SA.NombreCompleto(row, "NombreTitular", "ApellidoTitular"),
                Descripcion = Fila_68SA.Texto(row, "Descripcion"),
                FechaHoraPedido = Convert.ToDateTime(row["FechaHora"]),
                Monto = Convert.ToDecimal(row["Monto"]),
                Estado = (EstadoPedidoRoomService)Convert.ToInt32(row["Estado"]),
                FechaHoraEntrega = Fila_68SA.Fecha(row, "FechaHoraEntrega"),
                UsuarioRegistroNombre = Fila_68SA.NombreCompleto(row, "NombreUsuario", "ApellidoUsuario")
            };
        }

        public static List<DetallePedidoRoomService_68SA> DetalleFromDataTable(DataTable dt)
            => Fila_68SA.Lista(dt, row => new DetallePedidoRoomService_68SA
            {
                ItemId = Convert.ToInt32(row["ItemID"]),
                NombreItem = row["Nombre"].ToString(),
                Cantidad = Convert.ToInt32(row["Cantidad"]),
                PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"])
            });

        #endregion
    }

    public static class CambioHabitacionMapper_68SA
    {
        public static List<CambioHabitacion_68SA> FromDataTable(DataTable dt) => Fila_68SA.Lista(dt, FromDataRow);

        public static CambioHabitacion_68SA FromDataRow(DataRow row)
        {
            return new CambioHabitacion_68SA
            {
                Id = Convert.ToInt32(row["CambioID"]),
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                Motivo = (MotivoCambioHabitacion)Convert.ToInt32(row["Motivo"]),
                Observacion = Fila_68SA.Texto(row, "Observacion"),
                CargoId = Fila_68SA.Entero(row, "CargoID"),
                HabitacionAnteriorId = Convert.ToInt32(row["HabitacionAnteriorID"]),
                NroHabitacionAnterior = Fila_68SA.Texto(row, "NroHabitacionAnterior"),
                HabitacionNuevaId = Convert.ToInt32(row["HabitacionNuevaID"]),
                NroHabitacionNueva = Fila_68SA.Texto(row, "NroHabitacionNueva"),
                TarifaAnterior = Convert.ToDecimal(row["TarifaAnterior"]),
                TarifaNueva = Convert.ToDecimal(row["TarifaNueva"]),
                NochesRestantes = Convert.ToInt32(row["NochesRestantes"]),
                FechaCambio = Convert.ToDateTime(row["FechaCambio"]),
                Monto = Fila_68SA.Decimal(row, "Monto")
            };
        }
    }

    /// <summary>Renglones del reporte de servicios consumidos (sp_ReporteServiciosConsumidos).</summary>
    public static class ServicioConsumidoMapper_68SA
    {
        #region Mapeo

        public static List<ServicioConsumido_68SA> FromDataTable(DataTable dt) => Fila_68SA.Lista(dt, FromDataRow);

        public static ServicioConsumido_68SA FromDataRow(DataRow row)
        {
            return new ServicioConsumido_68SA
            {
                CargoId = Convert.ToInt32(row["CargoID"]),
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                HuespedId = Convert.ToInt32(row["HuespedID"]),
                Documento = Fila_68SA.Texto(row, "Documento"),
                Nombre = Fila_68SA.Texto(row, "Nombre"),
                Apellido = Fila_68SA.Texto(row, "Apellido"),
                NroHabitacion = Fila_68SA.Texto(row, "NroHabitacion"),
                Tipo = (TipoCargo)Convert.ToInt32(row["Tipo"]),
                Descripcion = Fila_68SA.Texto(row, "Descripcion"),
                Fecha = Convert.ToDateTime(row["FechaHora"]),
                Cantidad = Fila_68SA.Entero(row, "Cantidad") ?? 0,
                Detalle = Fila_68SA.Texto(row, "Detalle"),
                Monto = Fila_68SA.Decimal(row, "Monto"),
                Estado = (EstadoServicio)Convert.ToInt32(row["EstadoServicio"]),
                FechaEntrega = Fila_68SA.Fecha(row, "FechaHoraEntrega"),
                PedidoId = Fila_68SA.Entero(row, "PedidoID"),
                Usuario = Fila_68SA.NombreCompleto(row, "NombreUsuario", "ApellidoUsuario"),
                HabitacionAnterior = Fila_68SA.Texto(row, "HabitacionAnterior"),
                TipoHabitacionNueva = Fila_68SA.Texto(row, "TipoNuevo"),
                DiferenciaTarifa = Fila_68SA.Tiene(row, "Diferencia") ? Convert.ToDecimal(row["Diferencia"]) : (decimal?)null
            };
        }

        public static List<ProductoConsumido_68SA> ProductosFromDataTable(DataTable dt)
            => Fila_68SA.Lista(dt, row => new ProductoConsumido_68SA
            {
                Nombre = row["Nombre"].ToString(),
                Cantidad = Convert.ToInt32(row["Cantidad"]),
                Monto = Convert.ToDecimal(row["Monto"])
            });

        #endregion
    }
}
