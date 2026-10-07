using System;

namespace BE_08YS
{
    /// <summary>Por qué se cambió de habitación a un huésped con la estadía en curso.</summary>
    public enum MotivoCambioHabitacion
    {
        /// <summary>Lo pide el huésped: solo a una categoría superior y se cobra la diferencia.</summary>
        Upgrade = 0,
        /// <summary>Lo decide el hotel (mantenimiento, problema físico, etc.): a cualquier habitación libre, sin cargo.</summary>
        Administrativo = 1
    }

    /// <summary>Pedido de cambio que arma la pantalla y valida la BLL.</summary>
    public class SolicitudCambioHabitacion_68SA
    {
        #region Propiedades

        public int ReservaId { get; set; }
        public int HabitacionNuevaId { get; set; }
        public MotivoCambioHabitacion Motivo { get; set; }
        public string Observacion { get; set; }

        #endregion
    }

    /// <summary>
    /// Vista previa de lo que va a costar un cambio. La BLL la calcula con la misma fórmula que el SP:
    /// (tarifa nueva - tarifa vigente) x noches restantes, solo para el upgrade.
    /// </summary>
    public class CotizacionCambioHabitacion_68SA
    {
        #region Propiedades

        public MotivoCambioHabitacion Motivo { get; set; }
        public decimal TarifaVigente { get; set; }
        public decimal TarifaNueva { get; set; }
        public int NochesRestantes { get; set; }
        public decimal Monto { get; set; }

        public decimal DiferenciaPorNoche => TarifaNueva - TarifaVigente;

        #endregion
    }

    /// <summary>Cambio de habitación registrado (tabla HistorialCambiosHabitacion).</summary>
    public class CambioHabitacion_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public int ReservaId { get; set; }
        public MotivoCambioHabitacion Motivo { get; set; }
        public string Observacion { get; set; }
        public int? CargoId { get; set; }
        public int HabitacionAnteriorId { get; set; }
        public string NroHabitacionAnterior { get; set; }
        public int HabitacionNuevaId { get; set; }
        public string NroHabitacionNueva { get; set; }
        public decimal TarifaAnterior { get; set; }
        public decimal TarifaNueva { get; set; }
        public int NochesRestantes { get; set; }
        public DateTime FechaCambio { get; set; }
        public decimal Monto { get; set; }

        #endregion
    }

    /// <summary>Códigos de @Resultado de sp_RegistrarCambioHabitacion.</summary>
    public enum ResultadoCambioHabitacion
    {
        Ok = 0,
        ReservaNoEnCurso = 1,   // la reserva no está en un estado que admita ese cambio
        HabitacionNoDisponible = 2,
        MismaHabitacion = 3,
        SinNochesRestantes = 4,
        TarifaNoSuperior = 5,
        CapacidadInsuficiente = 6,
        MotivoInvalido = 7
    }

    /// <summary>Lo que devuelve la base al registrar el cambio.</summary>
    public class ResultadoRegistroCambio_68SA
    {
        #region Propiedades

        public ResultadoCambioHabitacion Resultado { get; set; }
        public int? CambioId { get; set; }
        public decimal Monto { get; set; }

        #endregion
    }
}
