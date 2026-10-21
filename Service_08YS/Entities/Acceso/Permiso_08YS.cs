using Service_08YS.Entities.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Entities.Acceso
{
    public enum Permisos
    {
        VerBitacora,
        VerUsuarios,
        CrearUsuario,
        ModificarUsuario,
        DesActivarUsuario,
        DesbloquearUsuario,
        VerRoles,
        CrearRoles,
        ModificarRoles,
        EliminarRoles,
        VerFamilias,
        CrearFamilias,
        ModificarFamilias,
        EliminarFamilias,
        ManejarInconsistencias,
        VerRespaldos,
        RealizarBackup,
        RealizarRestore,

        // Negocio - Reservas
        VerReservas,
        RegistrarReserva,
        ModificarReserva,
        CancelarReserva,
        ImprimirComprobante,
        Cobrar,

        // Negocio - Estadías
        ExtenderEstadia,
        VerControlHabitaciones,
        CambiarEstadoHabitacion,
        RegistrarCheckIn,
        RegistrarCheckOut,

        // Negocio - Huéspedes
        VerHuespedes,
        CrearHuesped,
        ModificarHuesped,
        EliminarHuesped,
        SerializarHuespedes,
        DeserializarHuespedes,

        // Negocio - Habitaciones
        VerHabitaciones,
        CrearHabitacion,
        ModificarHabitacion,
        EliminarHabitacion,
        VerTiposHabitacion,
        CrearTipoHabitacion,
        ModificarTipoHabitacion,
        EliminarTipoHabitacion,
        VerPisos,
        CrearPiso,
        ModificarPiso,
        EliminarPiso,

        // Negocio - Reportes y configuración
        VerDashboard,
        GenerarReporteReservas,
        VerConfiguracionHotel,
        ModificarConfiguracionHotel,

        // Negocio - Cargos adicionales durante la estadía (PN2)
        RegistrarUpgradeHabitacion,     // upgrade pedido por el huésped (genera cargo)
        ReubicarHuesped,                // cambio de habitación por motivos del hotel (sin cargo)
        RegistrarRoomService,
        GestionarItemsRoomService,
        ConfirmarEntregaRoomService,
        VerBitacoraReservas,
        GenerarReporteServicios,        // reporte de servicios consumidos por huésped
        GenerarAnalisisDemanda,         // análisis predictivo de ocupación y demanda (reporte inteligente)
        RollbackBitacoraReservas        // restaurar un registro histórico de la bitácora de reservas (Administrador y Supervisor)
    }

    public class Permiso_08YS : AccessComponent_08YS
    {
        #region Propiedades

        public int PermisoID { get => ID; set => ID = value; }
        public string Descripcion { get; set; }

        #endregion

        #region Sobrescrituras

        // El leaf devuelve un HashSet con sí mismo — ya usa el comparer correcto
        public override HashSet<Permiso_08YS> GetPermisos()
            => new HashSet<Permiso_08YS>(new[] { this }, PermisoComparer.Instance);

        #endregion
    }
}
