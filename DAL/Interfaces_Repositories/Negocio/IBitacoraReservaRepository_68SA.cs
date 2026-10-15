using BE_08YS;
using System.Collections.Generic;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    /// <summary>
    /// Bitácora de cambios de reservas. Solo lectura: los registros los escribe el trigger de Reservas
    /// y la base no admite bajas ni modificaciones.
    /// </summary>
    public interface IBitacoraReservaRepository_68SA
    {
        List<BitacoraReserva_68SA> Filtrar(BitacoraReservaFiltro_68SA filtro);
    }
}
