using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    public interface IFacturaRepository_68SA
    {
        #region Consultas

        List<Factura_68SA> GetByReserva(int reservaId);
        decimal GetTotalPagado(int reservaId);

        #endregion

        #region Operaciones

        int Registrar(Factura_68SA factura);

        #endregion
    }
}
