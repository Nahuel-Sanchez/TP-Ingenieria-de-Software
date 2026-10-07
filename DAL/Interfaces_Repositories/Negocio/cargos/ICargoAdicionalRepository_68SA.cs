using BE_08YS;
using System.Collections.Generic;

namespace DAL_08YS.Interfaces_Repositories.Negocio.cargos
{
    /// <summary>Lectura de la cuenta del huésped. Los cargos los crean los SP de upgrade y de room service.</summary>
    public interface ICargoAdicionalRepository_68SA
    {
        List<CargoAdicional_68SA> GetByReserva(int reservaId);
        decimal GetTotalByReserva(int reservaId);
    }
}
