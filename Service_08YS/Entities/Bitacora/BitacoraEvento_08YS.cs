using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Entities.Bitacora
{
    public enum Criticidad
    {
        Critico,
        Alto,
        Medio,
        Bajo
    }

    public enum Modulo
    {
        Usuarios,
        Login,
        Perfiles,
        Reservas,
        Habitaciones,
        Huespedes,
        Pagos,
        Configuracion,
        CargosAdicionales
    }

    public enum Evento
    {
        LoginExitoso,
        LoginFallido,
        UsuarioCreado,
        UsuarioBloqueado,
        UsuarioDesbloqueado,
        UsuarioDeshabilitado,
        UsuarioHabilitado,
        UsuarioEmailModificado,
        UsuarioRolModificado,
        CambioContraseña,
        FamiliaCreada,
        FamiliaEliminada,
        FamiliaModificada,
        RolCreado,
        RolEliminado,
        RolModificado,
        ReservaCreada,
        ReservaCheckInRegistrado,
        ReservaCheckOutRegistrado,
        ReservaCancelada,
        ReservaEstadiaExtendida,
        ReservaNoShowProcesado,
        HabitacionPuestaEnMantenimiento,
        HabitacionEstadoCambiado,
        HuespedRegistrado,
        PagoRegistrado,
        ConfiguracionHotelActualizada,
        HabitacionCreada,
        HabitacionModificada,
        HabitacionEliminada,
        TipoHabitacionCreado,
        TipoHabitacionModificado,
        TipoHabitacionEliminado,
        PisoCreado,
        PisoModificado,
        PisoEliminado,
        HuespedModificado,
        HuespedEliminado,
        ReservaModificada,
        // PN2 (se agregan al final: la bitácora guarda el número del enum)
        HabitacionUpgradeRegistrado,
        HuespedReubicado,
        PedidoRoomServiceRegistrado,
        PedidoRoomServiceEntregado,
        ItemRoomServiceCreado,
        ItemRoomServiceModificado,
        CambioHabitacionReactivado   // ya no se genera (la bitácora de cambios es de solo lectura); se conserva por los registros guardados
    }

    public class BitacoraEvento_08YS
    {
        #region Propiedades

        public string Username { get; set; }  // actor: quien ejecuta
        public string TargetUsername { get; set; }  // sujeto: sobre quien se ejecuta (nullable)
        public DateTime FechaHora { get; set; }
        public Modulo Modulo { get; set; }
        public Evento Evento { get; set; }
        public Criticidad Criticidad { get; set; }

        #endregion

        #region Constructor

        public BitacoraEvento_08YS(string username, DateTime fechaHora, Modulo modulo, Evento evento, Criticidad criticidad, string targetUsername = null)
        {
            Username = username;
            TargetUsername = targetUsername;
            FechaHora = fechaHora;
            Modulo = modulo;
            Evento = evento;
            Criticidad = criticidad;
        }

        public BitacoraEvento_08YS() { }

        #endregion
    }

    public class BitacoraFiltro_08YS
    {
        #region Propiedades

        public string Username { get; set; } = null;

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        public Modulo? Modulo { get; set; }

        public Evento? Evento { get; set; }

        public Criticidad? Criticidad { get; set; }

        public string TargetUsername { get; set; } = null;

        #endregion
    }
}
