using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_08YS
{
    public enum EstadoHabitacion
    {
        Disponible,
        Reservada,
        Ocupada,
        EnLimpieza,
        FueraDeServicio
    }

    public class Habitacion_68SA
    {
        public int Id { get; set; }
        public string NroHabitacion { get; set; }
        public Piso_68SA Piso { get; set; }
        public TipoHabitacion Tipo { get; set; }
        public EstadoHabitacion Estado { get; set; }

        public bool TieneReservaHoy { get; set; }

        // Estado a mostrar: si está físicamente Disponible pero ya hay una reserva Confirmada que cubre la fecha de hoy
        // (todavía sin check-in), se muestra como Reservada para que el recepcionista no la ofrezca para walk-in.
        public EstadoHabitacion EstadoVisual =>
            Estado == EstadoHabitacion.Disponible && TieneReservaHoy ? EstadoHabitacion.Reservada : Estado;

        //para listado
        public int CantidadReservas { get; set; }   // todas, incluido el historial
        public int CantidadReservasActivas { get; set; }

    }
    public class HabitacionFiltro_68SA
    {
        public string NroHabitacion { get; set; }
        public int? PisoId { get; set; }
        public int? TipoHabitacionId { get; set; }
        public EstadoHabitacion? Estado { get; set; } // se compara contra EstadoVisual (mismo criterio que Control de habitaciones)
    }

    public class TipoHabitacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }
        public decimal TarifaNoche { get; set; }

        // para listado
        public int CantidadHabitaciones { get; set; }
    }

    public class TipoHabitacionFiltro_68SA
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Capacidad { get; set; }
        public decimal? TarifaDesde { get; set; }
        public decimal? TarifaHasta { get; set; }
    }
}