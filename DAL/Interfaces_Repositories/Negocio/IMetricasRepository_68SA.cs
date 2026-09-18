using BE_08YS.Metricas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    public interface IMetricasRepository_68SA
    {
        ResumenOcupacion_68SA GetResumenOcupacion();
        List<OcupacionPorTipoItem_68SA> GetOcupacionPorTipo();
        ResumenIngresos_68SA GetResumenIngresos(DateTime? desde, DateTime? hasta);
        ResumenHuespedes_68SA GetResumenHuespedes(DateTime desde, DateTime hasta);
    }
}
