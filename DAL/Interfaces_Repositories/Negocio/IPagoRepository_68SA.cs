using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    public interface IPagoRepository_68SA
    {
        int Registrar(Pago_68SA pago);
        List<Pago_68SA> GetByReserva(int reservaId);
        decimal GetTotalPagado(int reservaId);
    }
}
