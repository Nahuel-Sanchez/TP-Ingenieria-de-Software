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
        public decimal MontoOriginal { get; set; }

        // Se completa recién en el check-in, no al momento de reservar (PN1)
        public List<Huesped_68SA> Acompanantes { get; set; } = new List<Huesped_68SA>();

        public int Noches => (FechaEgreso - FechaIngreso).Days;

        public int? UsuarioRegistroDni { get; set; }
        public string UsuarioRegistroNombre { get; set; } // solo para mostrar (Nombre + Apellido), no se inserta directo
        public DateTime FechaRegistro { get; set; }
    }

    public class ReservaFiltro_68SA
    {
        public string Huesped { get; set; }
        public string Habitacion { get; set; }
        public string Registro { get; set; }
        public EstadoReserva? Estado { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        public DateTime? FechaEgresoDesde { get; set; }
        public DateTime? FechaEgresoHasta { get; set; }
        public decimal? CostoDesde { get; set; }
        public decimal? CostoHasta { get; set; }
    }
}
