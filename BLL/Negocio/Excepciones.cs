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
        public RangoFechasInvalidoException_68SA()
        : base("El rango de fechas ingresado no es válido.") { }

        public RangoFechasInvalidoException_68SA(string message) : base(message) { }
        public RangoFechasInvalidoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ReservaNoEncontradaException_68SA : Exception
    {
        public ReservaNoEncontradaException_68SA()
        : base("No se encontró la reserva solicitada.") { }

        public ReservaNoEncontradaException_68SA(string message) : base(message) { }
        public ReservaNoEncontradaException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class EstadoReservaInvalidoException_68SA : Exception
    {
        public EstadoReservaInvalidoException_68SA()
        : base("La reserva no se encuentra en un estado válido para realizar esta operación.") { }

        public EstadoReservaInvalidoException_68SA(string message) : base(message) { }
        public EstadoReservaInvalidoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TitularMenorDeEdadException_68SA : Exception
    {
        public TitularMenorDeEdadException_68SA()
        : base("El titular de una reserva debe ser mayor de edad.") { }

        public TitularMenorDeEdadException_68SA(string message) : base(message) { }
        public TitularMenorDeEdadException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TitularEntreAcompanantesException_68SA : Exception
    {
        public TitularEntreAcompanantesException_68SA()
        : base("El titular de la reserva no puede figurar también como acompañante.") { }

        public TitularEntreAcompanantesException_68SA(string message) : base(message) { }
        public TitularEntreAcompanantesException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class AcompananteDuplicadoException_68SA : Exception
    {
        public AcompananteDuplicadoException_68SA()
        : base("Hay un acompañante cargado más de una vez.") { }

        public AcompananteDuplicadoException_68SA(string message) : base(message) { }
        public AcompananteDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class HabitacionNoDisponibleException_68SA : Exception
    {
        public HabitacionNoDisponibleException_68SA()
        : base("La habitación seleccionada ya no está disponible para el rango de fechas indicado.") { }

        public HabitacionNoDisponibleException_68SA(string message) : base(message) { }
        public HabitacionNoDisponibleException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CapacidadExcedidaException_68SA : Exception
    {
        public CapacidadExcedidaException_68SA()
        : base("La cantidad de huéspedes supera la capacidad de la habitación.") { }

        public CapacidadExcedidaException_68SA(string message) : base(message) { }
        public CapacidadExcedidaException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    #endregion

    #region Piso
    public class PisoNoEncontradoException_68SA : Exception
    {
        public PisoNoEncontradoException_68SA() : base("No se encontró el piso solicitado.") { }
        public PisoNoEncontradoException_68SA(string message) : base(message) { }
        public PisoNoEncontradoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class PisoNumeroDuplicadoException_68SA : Exception
    {
        public PisoNumeroDuplicadoException_68SA() : base("Ya existe un piso con ese número.") { }
        public PisoNumeroDuplicadoException_68SA(string message) : base(message) { }
        public PisoNumeroDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class PisoConHabitacionesException_68SA : Exception
    {
        public PisoConHabitacionesException_68SA() : base("El piso tiene habitaciones asociadas y no puede eliminarse.") { }
        public PisoConHabitacionesException_68SA(string message) : base(message) { }
        public PisoConHabitacionesException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }
    #endregion

    #region TipoHabitacion
    public class TipoHabitacionNoEncontradoException_68SA : Exception
    {
        public TipoHabitacionNoEncontradoException_68SA() : base("No se encontró el tipo de habitación solicitado.") { }
        public TipoHabitacionNoEncontradoException_68SA(string message) : base(message) { }
        public TipoHabitacionNoEncontradoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TipoHabitacionNombreDuplicadoException_68SA : Exception
    {
        public TipoHabitacionNombreDuplicadoException_68SA() : base("Ya existe un tipo de habitación con ese nombre.") { }
        public TipoHabitacionNombreDuplicadoException_68SA(string message) : base(message) { }
        public TipoHabitacionNombreDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TipoHabitacionConHabitacionesException_68SA : Exception
    {
        public TipoHabitacionConHabitacionesException_68SA() : base("El tipo de habitación tiene habitaciones asociadas y no puede eliminarse.") { }
        public TipoHabitacionConHabitacionesException_68SA(string message) : base(message) { }
        public TipoHabitacionConHabitacionesException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }
    #endregion

    #region Habitacion
    public class HabitacionNoEncontradaException_68SA : Exception
    {
        public HabitacionNoEncontradaException_68SA() : base("No se encontró la habitación solicitada.") { }
        public HabitacionNoEncontradaException_68SA(string message) : base(message) { }
        public HabitacionNoEncontradaException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class HabitacionNumeroDuplicadoException_68SA : Exception
    {
        public HabitacionNumeroDuplicadoException_68SA() : base("Ya existe una habitación con ese número.") { }
        public HabitacionNumeroDuplicadoException_68SA(string message) : base(message) { }
        public HabitacionNumeroDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class HabitacionConReservasException_68SA : Exception
    {
        public HabitacionConReservasException_68SA() : base("La habitación tiene reservas asociadas y no puede eliminarse.") { }
        public HabitacionConReservasException_68SA(string message) : base(message) { }
        public HabitacionConReservasException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class HabitacionConReservasActivasException_68SA : Exception
    {
        public HabitacionConReservasActivasException_68SA() : base("No se puede cambiar el tipo de una habitación con reservas activas.") { }
        public HabitacionConReservasActivasException_68SA(string message) : base(message) { }
        public HabitacionConReservasActivasException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }
    #endregion

    #region Huesped
    public class HuespedNoEncontradoException_68SA : Exception
    {
        public HuespedNoEncontradoException_68SA() : base("No se encontró el huésped solicitado.") { }
        public HuespedNoEncontradoException_68SA(string message) : base(message) { }
        public HuespedNoEncontradoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class HuespedDuplicadoException_68SA : Exception
    {
        public HuespedDuplicadoException_68SA() : base("Ya existe otro huésped con el mismo tipo de documento, número y nacionalidad.") { }
        public HuespedDuplicadoException_68SA(string message) : base(message) { }
        public HuespedDuplicadoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class HuespedConReservasException_68SA : Exception
    {
        public HuespedConReservasException_68SA() : base("El huésped figura en reservas y no puede eliminarse.") { }
        public HuespedConReservasException_68SA(string message) : base(message) { }
        public HuespedConReservasException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }
    #endregion

    public class DatosInvalidosException_68SA : Exception
    {
        public DatosInvalidosException_68SA() : base("Los datos ingresados no son válidos.") { }
        public DatosInvalidosException_68SA(string message) : base(message) { }
        public DatosInvalidosException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

}
