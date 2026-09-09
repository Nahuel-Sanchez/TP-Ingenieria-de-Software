using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_08YS
{
    public enum EstadoReserva
    {
        Confirmada = 0,
        EnCurso = 1,     // check-in realizado
        Finalizada = 2,  // check-out realizado
        Cancelada = 3
    }
    public class Reserva_68SA
    {
        public int Id { get; set; }
        public Huesped_68SA Titular { get; set; }
        public Habitacion_68SA Habitacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public EstadoReserva Estado { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }

        public decimal TarifaNoche { get; set; }
        public decimal MontoTotal { get; set; }

        // Se completa recién en el check-in, no al momento de reservar (PN1)
        public List<Huesped_68SA> Acompanantes { get; set; } = new List<Huesped_68SA>();

        public int Noches => (FechaEgreso - FechaIngreso).Days;
    }
}
