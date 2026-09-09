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
    }

    public class TipoHabitacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int capacidad { get; set; }
        public decimal tarifaNoche { get; set; }
    }
}
