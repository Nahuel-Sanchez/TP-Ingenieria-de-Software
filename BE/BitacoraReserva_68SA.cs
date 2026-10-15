using System;
using System.Collections.Generic;
using System.Linq;

namespace BE_08YS
{
    /// <summary>Operación que generó un registro de la bitácora de reservas.</summary>
    public enum OperacionBitacoraReserva
    {
        CargaInicial,
        Alta,
        Modificacion,
        Cancelacion,
        NoShow,
        CheckIn,
        CheckOut,
        Renovacion,
        Upgrade,
        CambioAdministrativo,
        CambioHabitacion
    }

    /// <summary>
    /// Registro de la bitácora de cambios de reservas (tabla Bitacora_Reservas, patrón de la cátedra):
    /// una foto completa de la reserva cada vez que se dio de alta o cambió cualquiera de sus datos.
    /// Act = true marca el estado vigente. Es de solo lectura: los registros los escribe el trigger de Reservas.
    /// </summary>
    public class BitacoraReserva_68SA
    {
        #region Campos

        // Texto con el que la base guarda cada operación (lo escribe el trigger, en español)
        private static readonly Dictionary<OperacionBitacoraReserva, string> _textoOperacion =
            new Dictionary<OperacionBitacoraReserva, string>
            {
                { OperacionBitacoraReserva.CargaInicial,         "Carga inicial" },
                { OperacionBitacoraReserva.Alta,                 "Alta" },
                { OperacionBitacoraReserva.Modificacion,         "Modificación" },
                { OperacionBitacoraReserva.Cancelacion,          "Cancelación" },
                { OperacionBitacoraReserva.NoShow,               "No-show" },
                { OperacionBitacoraReserva.CheckIn,              "Check-in" },
                { OperacionBitacoraReserva.CheckOut,             "Check-out" },
                { OperacionBitacoraReserva.Renovacion,           "Renovación" },
                { OperacionBitacoraReserva.Upgrade,              "Upgrade" },
                { OperacionBitacoraReserva.CambioAdministrativo, "Cambio administrativo" },
                { OperacionBitacoraReserva.CambioHabitacion,     "Cambio de habitación" },
            };

        #endregion

        #region Propiedades

        public int Id { get; set; }
        public OperacionBitacoraReserva Operacion { get; set; }
        public int? CambioId { get; set; }          // upgrade o reubicación que lo originó (HistorialCambiosHabitacion)
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string UsuarioDB { get; set; }       // login de SQL Server (no se muestra: en Windows incluye el nombre de la PC)
        public string UsuarioApp { get; set; }      // usuario del sistema (null = cambio hecho fuera del sistema)
        public bool Act { get; set; }

        /// <summary>Datos de la reserva en este registro.</summary>
        public FotoReserva_68SA Reserva { get; set; } = new FotoReserva_68SA();

        /// <summary>Datos del registro anterior de la misma reserva (null si es el primero).</summary>
        public FotoReserva_68SA Anterior { get; set; }

        public DateTime FechaHora => Fecha.Date + Hora;

        #endregion

        #region Métodos públicos

        /// <summary>Texto de la operación tal como lo guarda la base.</summary>
        public static string TextoBase(OperacionBitacoraReserva operacion) => _textoOperacion[operacion];

        /// <summary>Operación a partir del texto de la base (desconocido = Modificación).</summary>
        public static OperacionBitacoraReserva DesdeTextoBase(string texto)
        {
            foreach (var par in _textoOperacion)
                if (string.Equals(par.Value, texto, StringComparison.OrdinalIgnoreCase))
                    return par.Key;
            return OperacionBitacoraReserva.Modificacion;
        }

        /// <summary>Campos que cambiaron respecto del registro anterior de la misma reserva.</summary>
        public List<CampoReserva> CamposModificados()
        {
            if (Anterior == null) return new List<CampoReserva>();
            return Enum.GetValues(typeof(CampoReserva)).Cast<CampoReserva>()
                       .Where(c => !Equals(Reserva.Valor(c), Anterior.Valor(c)))
                       .ToList();
        }

        #endregion
    }

    /// <summary>Datos de la reserva que se guardan en cada registro de la bitácora.</summary>
    public enum CampoReserva
    {
        Titular,
        Habitacion,
        FechaIngreso,
        FechaEgreso,
        CheckIn,
        CheckOut,
        Estado,
        Adultos,
        Ninos,
        TarifaNoche,
        MontoTotal,
        MontoOriginal,
        UsuarioRegistro,
        FechaRegistro
    }

    /// <summary>Foto de una reserva en un momento de la bitácora (las columnas de Reservas, con nombres para mostrar).</summary>
    public class FotoReserva_68SA
    {
        #region Propiedades

        public int ReservaId { get; set; }
        public int HuespedTitularId { get; set; }
        public string Titular { get; set; }
        public int HabitacionId { get; set; }
        public string NroHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public EstadoReserva Estado { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
        public decimal TarifaNoche { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoOriginal { get; set; }
        public int? UsuarioRegistroDni { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }

        #endregion

        #region Métodos públicos

        /// <summary>Valor de un campo, para comparar dos fotos de la misma reserva.</summary>
        public object Valor(CampoReserva campo)
        {
            switch (campo)
            {
                case CampoReserva.Titular: return HuespedTitularId;
                case CampoReserva.Habitacion: return HabitacionId;
                case CampoReserva.FechaIngreso: return FechaIngreso.Date;
                case CampoReserva.FechaEgreso: return FechaEgreso.Date;
                case CampoReserva.CheckIn: return CheckIn;
                case CampoReserva.CheckOut: return CheckOut;
                case CampoReserva.Estado: return Estado;
                case CampoReserva.Adultos: return CantidadAdultos;
                case CampoReserva.Ninos: return CantidadNinos;
                case CampoReserva.TarifaNoche: return TarifaNoche;
                case CampoReserva.MontoTotal: return MontoTotal;
                case CampoReserva.MontoOriginal: return MontoOriginal;
                case CampoReserva.UsuarioRegistro: return UsuarioRegistroDni;
                case CampoReserva.FechaRegistro: return FechaRegistro;
                default: return null;
            }
        }

        #endregion
    }

    /// <summary>Filtro de la pantalla: código de reserva, nombre (huésped o habitación), operación y rango de fechas.</summary>
    public class BitacoraReservaFiltro_68SA
    {
        #region Propiedades

        public int? Codigo { get; set; }
        public string Nombre { get; set; }
        public OperacionBitacoraReserva? Operacion { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public bool SoloActivos { get; set; }

        #endregion
    }
}
