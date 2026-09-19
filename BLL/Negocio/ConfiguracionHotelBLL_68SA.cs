using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using Service_08YS.Entities.Bitacora;
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
        private readonly BitacoraBLL_08YS _bitacoraBll;

        public ConfiguracionHotelBLL_68SA(IConfiguracionHotelRepository_68SA repo, BitacoraBLL_08YS bitacoraBll)
        {
            _repo = repo;
            _bitacoraBll = bitacoraBll;
        }

        public ConfiguracionHotel_68SA GetConfiguracion() => _repo.GetConfiguracion();

        public void Actualizar(ConfiguracionHotel_68SA configuracion)
        {
            _repo.Actualizar(configuracion);
            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.ConfiguracionHotelActualizada);
        }
    }
}