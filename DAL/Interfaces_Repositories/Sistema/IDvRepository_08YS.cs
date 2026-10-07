using Service_08YS.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories
{
    public interface IDvRepository_08YS
    {
        #region Consultas

        List<DataTable> GetTodasLasTablas();
        List<DigitoVerificador_08YS> GetDVGuardado();

        #endregion

        #region Operaciones

        void GuardarDVTabla(DigitoVerificador_08YS entrada);

        #endregion
    }
}
