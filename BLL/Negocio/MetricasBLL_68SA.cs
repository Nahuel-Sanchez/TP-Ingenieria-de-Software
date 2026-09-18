using BE_08YS.Metricas;
using DAL_08YS.Interfaces_Repositories.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class MetricasBLL_68SA
    {
        private readonly IMetricasRepository_68SA _repo;

        public MetricasBLL_68SA(IMetricasRepository_68SA repo)
        {
            _repo = repo;
        }

        public ResumenOcupacion_68SA GetResumenOcupacion() => _repo.GetResumenOcupacion();
        public List<OcupacionPorTipoItem_68SA> GetOcupacionPorTipo() => _repo.GetOcupacionPorTipo();
        public ResumenIngresos_68SA GetResumenIngresos(DateTime? desde, DateTime? hasta) => _repo.GetResumenIngresos(desde, hasta);
        public ResumenHuespedes_68SA GetResumenHuespedes(DateTime desde, DateTime hasta) => _repo.GetResumenHuespedes(desde, hasta);
    }
}
