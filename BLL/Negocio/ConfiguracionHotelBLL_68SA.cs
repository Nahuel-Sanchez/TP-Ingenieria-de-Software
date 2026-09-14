using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class ConfiguracionHotelBLL_68SA
    {
        private readonly IConfiguracionHotelRepository_68SA _repo;

        public ConfiguracionHotelBLL_68SA(IConfiguracionHotelRepository_68SA repo)
        {
            _repo = repo;
        }

        public ConfiguracionHotel_68SA GetConfiguracion() => _repo.GetConfiguracion();
        public void Actualizar(ConfiguracionHotel_68SA configuracion) => _repo.Actualizar(configuracion);
    }
}
