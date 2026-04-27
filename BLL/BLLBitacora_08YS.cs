using DAL;
using Service;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLBitacora_08YS
    {
        private DALBitacora_08YS repo = new DALBitacora_08YS();
        public void RegistrarEvento(Modulo modulo, string descripcion, Criticidad criticidad)
        {
            string usuarioActual = SessionManager.Instance.IsLogged
            ? SessionManager.Instance.Current.Username
            : "SISTEMA"; // Por si ocurre algo sin usuario logueado (como un login fallido)

            BitacoraEvento_08YS evento = new BitacoraEvento_08YS(
                usuarioActual,
                DateTime.Now,
                modulo,
                descripcion,
                criticidad
            );

            _repo.RegistrarEvento(evento);

        }

        public List<BitacoraEvento_08YS> ObtenerTodos()
        {
            return repo.ObtenerTodos();
        }
}
