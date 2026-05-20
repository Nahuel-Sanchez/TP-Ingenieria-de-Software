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

        public BitacoraBLL_08YS(IBitacoraRepository_08YS repo) => _repo = repo;

        // login = null → toma el usuario de sesión (flujo normal post-login)
        // login = "username" → se pasa explícitamente (flujo de intento fallido, sin sesión aún)
        public void RegistrarEvento(Modulo modulo, Evento evento, Criticidad criticidad, string login = null)
        {
            login = string.IsNullOrWhiteSpace(login) ? SessionManager.Instance.Current?.Username ?? "Desconocido" : login;

            _repo.RegistrarEvento(new BitacoraEvento_08YS(login, DateTime.Now, modulo, evento, criticidad));
        }

        public int ContarIntentosFallidos(string username, int ventanaHoras = 2)
            => _repo.ContarIntentosFallidos(username, ventanaHoras);

        public List<BitacoraEvento_08YS> GetAll()
            => _repo.GetAll();

        public List<BitacoraEvento_08YS> Filtrar(BitacoraFiltro_08YS filtro)
            => _repo.Filtrar(filtro);
    }
}
