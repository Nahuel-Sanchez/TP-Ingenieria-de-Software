using Service_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_08YS;
using DAL_08YS.Repositories_Interfaces;

namespace BLL_08YS
{
    public class BitacoraBLL_08YS
    {
        private readonly IBitacoraRepository_08YS _repo;

        public BitacoraBLL_08YS(IBitacoraRepository_08YS repo)
        {
            _repo = repo;
        }

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

        public List<BitacoraEvento_08YS> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
