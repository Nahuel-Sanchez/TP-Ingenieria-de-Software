using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS.Entities.Bitacora;

namespace DAL_08YS.Interfaces_Repositories
{
    public interface IBitacoraRepository_08YS
    {
        #region Consultas

        List<BitacoraEvento_08YS> GetAll();

        int ContarIntentosFallidos(string username, int ventanaHoras);

        #endregion

        #region Operaciones

        void RegistrarEvento(BitacoraEvento_08YS evento);

        #endregion

        #region Métodos públicos

        List<BitacoraEvento_08YS> Filtrar(BitacoraFiltro_08YS filtro);

        #endregion
    }
}
