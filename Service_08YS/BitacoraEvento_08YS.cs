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
        Usuarios
    }
    public class BitacoraEvento_08YS
    {
        private string _login_08YS;
        private DateTime _fechaHora_08YS;
        private Modulo _modulo_08YS;
        private string _descripcion_08YS;
        private Criticidad _criticidad_08YS;


        public string Login { get { return _login_08YS; } set { _login_08YS = value; } }
        public DateTime FechaHora { get { return _fechaHora_08YS; } set { _fechaHora_08YS = value; } }
        public Modulo Modulo { get { return _modulo_08YS; } set { _modulo_08YS = value; } }
        public string Descripcion { get { return _descripcion_08YS; } set { _descripcion_08YS = value; } }
        public Criticidad Criticidad { get { return _criticidad_08YS; } set { _criticidad_08YS = value; } }

        public BitacoraEvento_08YS(string login, DateTime fechahora, Modulo modulo, string descripcion, Criticidad criticidad)
        {
            Login = login;
            FechaHora = fechahora;
            Modulo = modulo;
            Descripcion = descripcion;
            Criticidad = criticidad;
        }
        public BitacoraEvento_08YS()
        {

        }
    }
}
