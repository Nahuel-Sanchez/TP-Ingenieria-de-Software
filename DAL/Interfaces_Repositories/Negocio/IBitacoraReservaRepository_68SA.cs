using BE_08YS;
using System.Collections.Generic;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    /// <summary>
    /// Bitácora de cambios de reservas. Los registros los escribe el trigger de Reservas y la base no admite
    /// bajas ni modificaciones. El rollback no edita la bitácora: restaura la reserva con la foto elegida
    /// (sp_RollbackReserva) y el trigger deja el registro nuevo.
    /// </summary>
    public interface IBitacoraReservaRepository_68SA
    {
        List<BitacoraReserva_68SA> Filtrar(BitacoraReservaFiltro_68SA filtro);

        /// <summary>Vuelve a poner como activo el registro histórico indicado (sp_RollbackReserva).</summary>
        ResultadoRollbackReserva Rollback(int bitacoraId);
    }
}
