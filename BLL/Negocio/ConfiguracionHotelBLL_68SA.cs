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
            if (configuracion == null) throw new ArgumentNullException(nameof(configuracion));

            // Columnas time(7): la hora tiene que caer dentro de un mismo día
            var dia = TimeSpan.FromHours(24);
            if (configuracion.HoraCheckIn < TimeSpan.Zero || configuracion.HoraCheckIn >= dia
                || configuracion.HoraCheckOut < TimeSpan.Zero || configuracion.HoraCheckOut >= dia)
                throw new DatosInvalidosException_68SA("Las horas de check-in y check-out deben estar entre las 00:00 y las 23:59.");

            // GraciaNoShowHoras es decimal(5,2)
            configuracion.GraciaNoShowHoras = Math.Round(configuracion.GraciaNoShowHoras, 2);
            if (configuracion.GraciaNoShowHoras < 0 || configuracion.GraciaNoShowHoras > 999.99m)
                throw new DatosInvalidosException_68SA("La tolerancia de no-show debe estar entre 0 y 999,99 horas.");

            _repo.Actualizar(configuracion);
            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.ConfiguracionHotelActualizada);
        }
    }
}