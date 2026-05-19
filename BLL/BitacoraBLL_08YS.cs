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

        public void RegistrarEvento(Modulo modulo, Evento evento, Criticidad criticidad)
        {
            string usuarioActual = SessionManager.Instance.IsLogged
            ? SessionManager.Instance.Current.Username
            : throw new Exception("Error de instancia de sesión: No hay un usuario actualmente logueado.");

            BitacoraEvento_08YS bitacora = new BitacoraEvento_08YS(
                usuarioActual,
                DateTime.Now,
                modulo,
                evento,
                criticidad
            );

            _repo.RegistrarEvento(bitacora);

        }
        public void RegistrarEvento(string Username, Modulo modulo, Evento evento, Criticidad criticidad)
        {
            BitacoraEvento_08YS bitacora = new BitacoraEvento_08YS(
                Username,
                DateTime.Now,
                modulo,
                evento,
                criticidad
            );

            _repo.RegistrarEvento(bitacora);

        }

        public List<BitacoraEvento_08YS> GetAll()
            => _repo.GetAll();

        public List<BitacoraEvento_08YS> Filtrar(BitacoraFiltro_08YS filtro)
            => _repo.Filtrar(filtro);
    }
}
