using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class RangoFechasInvalidoException_68SA : Exception
    {
        public RangoFechasInvalidoException_68SA()
        : base("El rango de fechas ingresado no es válido.") { }

        public RangoFechasInvalidoException_68SA(string message) : base(message) { }
        public RangoFechasInvalidoException_68SA(string message, Exception innerException) : base(message, innerException) { }
    }

    public class HabitacionNoDisponibleException_68SA : Exception
    {
        public HabitacionNoDisponibleException_68SA()
        : base("La habitación seleccionada ya no está disponible para el rango de fechas indicado.") { }

        public HabitacionNoDisponibleException_68SA(string message) : base(message) { }
        public HabitacionNoDisponibleException_68SA(string message, Exception innerException) : base(message, innerException) { }
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
}
