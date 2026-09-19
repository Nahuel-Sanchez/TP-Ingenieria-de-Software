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
    public class HuespedBLL_68SA
    {
        private readonly IHuespedRepository_68SA _huespedRepo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        public HuespedBLL_68SA(IHuespedRepository_68SA huespedRepo, BitacoraBLL_08YS bitacoraBll)
        {
            _huespedRepo = huespedRepo;
            _bitacoraBll = bitacoraBll;
        }

        public Huesped_68SA GetByDocumento(string documento)
        {
            return _huespedRepo.GetByDocumento(documento);
        }

        public bool ExistePorClaveCompleta(string documento, TipoDocumento tipoDocumento, string nacionalidad)
        {
            return _huespedRepo.GetPorClaveCompleta(documento, tipoDocumento, nacionalidad) != null;
        }

        public Huesped_68SA ObtenerOCrear(Huesped_68SA huesped)
        {
            var existente = _huespedRepo.GetPorClaveCompleta(huesped.Documento, huesped.TipoDocumento, huesped.Nacionalidad);
            if (existente != null)
                return existente;

            huesped.Id = _huespedRepo.Create(huesped);
            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.HuespedRegistrado, targetUsername: huesped.Documento);
            return huesped;
        }

        public static bool EsMayorDeEdad(Huesped_68SA huesped)
        {
            return huesped.Edad >= 18;
        }

        public static bool EsNino(Huesped_68SA huesped)
        {
            return huesped.Edad >= 2 && huesped.Edad <= 11;
        }
    }
}