using System;
using System.Collections.Generic;
using System.Linq;

namespace BE_08YS
{
    /// <summary>Origen de un cargo que se suma a la cuenta del huésped (PN2).</summary>
    public enum TipoCargo
    {
        RoomService = 0,
        Upgrade = 1
    }

    /// <summary>
    /// Consumo o diferencia de tarifa que se carga a la cuenta de una reserva durante la estadía
    /// y se liquida en el check-out junto con el saldo del alojamiento.
    /// </summary>
    public class CargoAdicional_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public int ReservaId { get; set; }
        public TipoCargo Tipo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal Monto { get; set; }
        public int? UsuarioRegistroDni { get; set; }
        public string UsuarioRegistroNombre { get; set; } // solo para mostrar (Nombre + Apellido)

        #endregion
    }

    /// <summary>
    /// Cuenta de una estadía: alojamiento + cargos adicionales - pagos.
    /// Es la única fuente del saldo que se cobra en el check-out y del total que muestra la factura.
    /// </summary>
    public class EstadoCuentaReserva_68SA
    {
        #region Propiedades

        public int ReservaId { get; set; }
        /// <summary>Alojamiento total (lo reservado + las renovaciones).</summary>
        public decimal Alojamiento { get; set; }
        /// <summary>Alojamiento tal como se reservó (antes de renovar).</summary>
        public decimal AlojamientoReservado { get; set; }
        public List<CargoAdicional_68SA> Cargos { get; set; } = new List<CargoAdicional_68SA>();
        public decimal TotalPagado { get; set; }

        /// <summary>Recargo por renovación de la estadía (noches agregadas).</summary>
        public decimal Renovacion => Math.Max(0, Alojamiento - AlojamientoReservado);
        public decimal TotalCargos => Cargos.Sum(c => c.Monto);
        public decimal TotalUpgrades => Cargos.Where(c => c.Tipo == TipoCargo.Upgrade).Sum(c => c.Monto);
        public decimal TotalRoomService => Cargos.Where(c => c.Tipo == TipoCargo.RoomService).Sum(c => c.Monto);
        public decimal Total => Alojamiento + TotalCargos;
        public decimal Saldo => Math.Max(0, Total - TotalPagado);

        #endregion
    }
}
