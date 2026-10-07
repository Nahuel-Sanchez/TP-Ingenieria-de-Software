using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    #region Reserva
    public class RangoFechasInvalidoException_68SA : Exception
    {
        #region Constructor

        public RangoFechasInvalidoException_68SA()
        : base("El rango de fechas ingresado no es válido.") { }

        public RangoFechasInvalidoException_68SA(string message) : base(message) { }
        public RangoFechasInvalidoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class ReservaNoEncontradaException_68SA : Exception
    {
        #region Constructor

        public ReservaNoEncontradaException_68SA()
        : base("No se encontró la reserva solicitada.") { }

        public ReservaNoEncontradaException_68SA(string message) : base(message) { }
        public ReservaNoEncontradaException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class EstadoReservaInvalidoException_68SA : Exception
    {
        #region Constructor

        public EstadoReservaInvalidoException_68SA()
        : base("La reserva no se encuentra en un estado válido para realizar esta operación.") { }

        public EstadoReservaInvalidoException_68SA(string message) : base(message) { }
        public EstadoReservaInvalidoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class TitularMenorDeEdadException_68SA : Exception
    {
        #region Constructor

        public TitularMenorDeEdadException_68SA()
        : base("El titular de una reserva debe ser mayor de edad.") { }

        public TitularMenorDeEdadException_68SA(string message) : base(message) { }
        public TitularMenorDeEdadException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class TitularEntreAcompanantesException_68SA : Exception
    {
        #region Constructor

        public TitularEntreAcompanantesException_68SA()
        : base("El titular de la reserva no puede figurar también como acompañante.") { }

        public TitularEntreAcompanantesException_68SA(string message) : base(message) { }
        public TitularEntreAcompanantesException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class AcompananteDuplicadoException_68SA : Exception
    {
        #region Constructor

        public AcompananteDuplicadoException_68SA()
        : base("Hay un acompañante cargado más de una vez.") { }

        public AcompananteDuplicadoException_68SA(string message) : base(message) { }
        public AcompananteDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class HabitacionNoDisponibleException_68SA : Exception
    {
        #region Constructor

        public HabitacionNoDisponibleException_68SA()
        : base("La habitación seleccionada ya no está disponible para el rango de fechas indicado.") { }

        public HabitacionNoDisponibleException_68SA(string message) : base(message) { }
        public HabitacionNoDisponibleException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class CapacidadExcedidaException_68SA : Exception
    {
        #region Constructor

        public CapacidadExcedidaException_68SA()
        : base("La cantidad de huéspedes supera la capacidad de la habitación.") { }

        public CapacidadExcedidaException_68SA(string message) : base(message) { }
        public CapacidadExcedidaException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class CheckInAnticipadoException_68SA : Exception
    {
        #region Constructor

        public CheckInAnticipadoException_68SA()
        : base("Todavía no se puede hacer check-in: la reserva ingresa en una fecha futura.") { }

        public CheckInAnticipadoException_68SA(string message) : base(message) { }
        public CheckInAnticipadoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    #endregion

    #region Piso
    public class PisoNoEncontradoException_68SA : Exception
    {
        #region Constructor

        public PisoNoEncontradoException_68SA() : base("No se encontró el piso solicitado.") { }
        public PisoNoEncontradoException_68SA(string message) : base(message) { }
        public PisoNoEncontradoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class PisoNumeroDuplicadoException_68SA : Exception
    {
        #region Constructor

        public PisoNumeroDuplicadoException_68SA() : base("Ya existe un piso con ese número.") { }
        public PisoNumeroDuplicadoException_68SA(string message) : base(message) { }
        public PisoNumeroDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class PisoConHabitacionesException_68SA : Exception
    {
        #region Constructor

        public PisoConHabitacionesException_68SA() : base("El piso tiene habitaciones asociadas y no puede eliminarse.") { }
        public PisoConHabitacionesException_68SA(string message) : base(message) { }
        public PisoConHabitacionesException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
    #endregion

    #region TipoHabitacion
    public class TipoHabitacionNoEncontradoException_68SA : Exception
    {
        #region Constructor

        public TipoHabitacionNoEncontradoException_68SA() : base("No se encontró el tipo de habitación solicitado.") { }
        public TipoHabitacionNoEncontradoException_68SA(string message) : base(message) { }
        public TipoHabitacionNoEncontradoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class TipoHabitacionNombreDuplicadoException_68SA : Exception
    {
        #region Constructor

        public TipoHabitacionNombreDuplicadoException_68SA() : base("Ya existe un tipo de habitación con ese nombre.") { }
        public TipoHabitacionNombreDuplicadoException_68SA(string message) : base(message) { }
        public TipoHabitacionNombreDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class TipoHabitacionConHabitacionesException_68SA : Exception
    {
        #region Constructor

        public TipoHabitacionConHabitacionesException_68SA() : base("El tipo de habitación tiene habitaciones asociadas y no puede eliminarse.") { }
        public TipoHabitacionConHabitacionesException_68SA(string message) : base(message) { }
        public TipoHabitacionConHabitacionesException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
    #endregion

    #region Habitacion
    public class HabitacionNoEncontradaException_68SA : Exception
    {
        #region Constructor

        public HabitacionNoEncontradaException_68SA() : base("No se encontró la habitación solicitada.") { }
        public HabitacionNoEncontradaException_68SA(string message) : base(message) { }
        public HabitacionNoEncontradaException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class HabitacionNumeroDuplicadoException_68SA : Exception
    {
        #region Constructor

        public HabitacionNumeroDuplicadoException_68SA() : base("Ya existe una habitación con ese número.") { }
        public HabitacionNumeroDuplicadoException_68SA(string message) : base(message) { }
        public HabitacionNumeroDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class HabitacionConReservasException_68SA : Exception
    {
        #region Constructor

        public HabitacionConReservasException_68SA() : base("La habitación tiene reservas asociadas y no puede eliminarse.") { }
        public HabitacionConReservasException_68SA(string message) : base(message) { }
        public HabitacionConReservasException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class HabitacionConReservasActivasException_68SA : Exception
    {
        #region Constructor

        public HabitacionConReservasActivasException_68SA() : base("No se puede cambiar el tipo de una habitación con reservas activas.") { }
        public HabitacionConReservasActivasException_68SA(string message) : base(message) { }
        public HabitacionConReservasActivasException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
    #endregion

    #region Huesped
    public class HuespedNoEncontradoException_68SA : Exception
    {
        #region Constructor

        public HuespedNoEncontradoException_68SA() : base("No se encontró el huésped solicitado.") { }
        public HuespedNoEncontradoException_68SA(string message) : base(message) { }
        public HuespedNoEncontradoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class HuespedDuplicadoException_68SA : Exception
    {
        #region Constructor

        public HuespedDuplicadoException_68SA() : base("Ya existe otro huésped con el mismo tipo de documento, número y nacionalidad.") { }
        public HuespedDuplicadoException_68SA(string message) : base(message) { }
        public HuespedDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class HuespedConReservasException_68SA : Exception
    {
        #region Constructor

        public HuespedConReservasException_68SA() : base("El huésped figura en reservas y no puede eliminarse.") { }
        public HuespedConReservasException_68SA(string message) : base(message) { }
        public HuespedConReservasException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
    #endregion

    #region Cargos adicionales (PN2)
    public class TarifaNoSuperiorException_68SA : Exception
    {
        #region Constructor

        public TarifaNoSuperiorException_68SA() : base("Un upgrade solo puede hacerse a una habitación de tarifa mayor a la vigente de la estadía.") { }
        public TarifaNoSuperiorException_68SA(string message) : base(message) { }
        public TarifaNoSuperiorException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class SinNochesRestantesException_68SA : Exception
    {
        #region Constructor

        public SinNochesRestantesException_68SA() : base("La estadía egresa hoy: no quedan noches sobre las que cobrar un upgrade.") { }
        public SinNochesRestantesException_68SA(string message) : base(message) { }
        public SinNochesRestantesException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class MismaHabitacionException_68SA : Exception
    {
        #region Constructor

        public MismaHabitacionException_68SA() : base("El huésped ya está en esa habitación.") { }
        public MismaHabitacionException_68SA(string message) : base(message) { }
        public MismaHabitacionException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class PedidoRoomServiceInvalidoException_68SA : Exception
    {
        #region Constructor

        public PedidoRoomServiceInvalidoException_68SA() : base("El pedido está vacío o tiene ítems que ya no están en la carta.") { }
        public PedidoRoomServiceInvalidoException_68SA(string message) : base(message) { }
        public PedidoRoomServiceInvalidoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class PedidoNoPendienteException_68SA : Exception
    {
        #region Constructor

        public PedidoNoPendienteException_68SA() : base("El pedido no existe o ya fue entregado.") { }
        public PedidoNoPendienteException_68SA(string message) : base(message) { }
        public PedidoNoPendienteException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class ItemRoomServiceNoEncontradoException_68SA : Exception
    {
        #region Constructor

        public ItemRoomServiceNoEncontradoException_68SA() : base("No se encontró el ítem de la carta.") { }
        public ItemRoomServiceNoEncontradoException_68SA(string message) : base(message) { }
        public ItemRoomServiceNoEncontradoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

    public class ItemRoomServiceDuplicadoException_68SA : Exception
    {
        #region Constructor

        public ItemRoomServiceDuplicadoException_68SA() : base("Ya existe un ítem de la carta con ese nombre.") { }
        public ItemRoomServiceDuplicadoException_68SA(string message) : base(message) { }
        public ItemRoomServiceDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
    #endregion

    public class DatosInvalidosException_68SA : Exception
    {
        #region Constructor

        public DatosInvalidosException_68SA() : base("Los datos ingresados no son válidos.") { }
        public DatosInvalidosException_68SA(string message) : base(message) { }
        public DatosInvalidosException_68SA(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }

}
