using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS
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
        Login
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
        UsuarioModificado,
        CambioContraseña
    }

    public class BitacoraEvento_08YS
    {
        private string _login_08YS;
        private DateTime _fechaHora_08YS;
        private Modulo _modulo_08YS;
        private Evento _evento;
        private Criticidad _criticidad_08YS;


        public string Login { get { return _login_08YS; } set { _login_08YS = value; } }
        public DateTime FechaHora { get { return _fechaHora_08YS; } set { _fechaHora_08YS = value; } }
        public Modulo Modulo { get { return _modulo_08YS; } set { _modulo_08YS = value; } }
        public Evento Evento { get { return _evento; } set { _evento = value; } }
        public Criticidad Criticidad { get { return _criticidad_08YS; } set { _criticidad_08YS = value; } }

        public BitacoraEvento_08YS(string login, DateTime fechahora, Modulo modulo, Evento evento, Criticidad criticidad)
        {
            Login = login;
            FechaHora = fechahora;
            Modulo = modulo;
            Evento = evento;
            Criticidad = criticidad;
        }
        public BitacoraEvento_08YS()
        {

        }
    }

    public class BitacoraFiltro_08YS
    {
        public string Username { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        public Modulo? Modulo { get; set; }

        public Evento? Evento { get; set; }

        public Criticidad? Criticidad { get; set; }
    }
}
