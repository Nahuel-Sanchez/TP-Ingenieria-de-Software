using System;

namespace BE_08YS
{
    /// <summary>
    /// Registro de la bitácora de cambios de habitación (tabla Bitacora_CambiosHabitacion, patrón de la cátedra):
    /// cada asignación de habitación que tuvo una reserva. Act = true marca la vigente.
    /// Es de solo lectura: los registros los escriben los triggers de la base.
    /// </summary>
    public class BitacoraCambioHabitacion_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public int ReservaId { get; set; }
        public string Huesped { get; set; }              // titular de la reserva (solo para mostrar)
        public EstadoReserva EstadoReserva { get; set; }
        public int HabitacionId { get; set; }
        public string NroHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public decimal TarifaNoche { get; set; }
        public int? HabitacionAnteriorId { get; set; }
        public string NroHabitacionAnterior { get; set; }
        public int? CambioId { get; set; }
        public decimal MontoCargo { get; set; }
        public string Operacion { get; set; }            // Alta | Upgrade | Cambio administrativo | Cambio en BD
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string UsuarioDB { get; set; }   // login de SQL Server (no se muestra: en Windows incluye el nombre de la PC)
        public string UsuarioApp { get; set; }  // usuario del sistema que hizo el cambio (NULL = cambio hecho fuera del sistema)
        public bool Act { get; set; }

        public DateTime FechaHora => Fecha.Date + Hora;

        #endregion
    }

    /// <summary>Filtro de la pantalla: código (de reserva), nombre (huésped o habitación) y rango de fechas.</summary>
    public class BitacoraCambioHabitacionFiltro_68SA
    {
        #region Propiedades

        public int? Codigo { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public bool SoloActivos { get; set; }

        #endregion
    }
}
