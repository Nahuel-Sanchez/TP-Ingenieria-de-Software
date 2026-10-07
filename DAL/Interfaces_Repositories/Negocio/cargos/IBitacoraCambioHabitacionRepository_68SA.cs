using BE_08YS;
using System.Collections.Generic;

namespace DAL_08YS.Interfaces_Repositories.Negocio.cargos
{
    /// <summary>
    /// Bitácora de cambios de habitación. Solo lectura: los registros los escriben los triggers de Reservas
    /// y la base no admite bajas ni modificaciones.
    /// </summary>
    public interface IBitacoraCambioHabitacionRepository_68SA
    {
        List<BitacoraCambioHabitacion_68SA> Filtrar(BitacoraCambioHabitacionFiltro_68SA filtro);
    }
}
