using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
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

        public HuespedBLL_68SA(IHuespedRepository_68SA huespedRepo)
        {
            _huespedRepo = huespedRepo;
        }

        public Huesped_68SA GetByDocumento(string documento)
        {
            return _huespedRepo.GetByDocumento(documento);
        }

        public bool ExistePorClaveCompleta(string documento, TipoDocumento tipoDocumento, string nacionalidad)
        {
            return _huespedRepo.GetPorClaveCompleta(documento, tipoDocumento, nacionalidad) != null;
        }

        // PN1 paso 7: si el huésped ya existe lo reutiliza, si no lo da de alta
        public Huesped_68SA ObtenerOCrear(Huesped_68SA huesped)
        {
            var existente = _huespedRepo.GetPorClaveCompleta(huesped.Documento, huesped.TipoDocumento, huesped.Nacionalidad);
            if (existente != null)
                return existente;

            huesped.Id = _huespedRepo.Create(huesped);
            return huesped;
        }

        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;
            return edad;
        }

        public static bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            return CalcularEdad(fechaNacimiento) >= 18;
        }

        // Disponible para cuando se clasifiquen acompañantes al check-in (PN1: "niños de 2 a 11 años")
        public static bool EsNino(DateTime fechaNacimiento)
        {
            int edad = CalcularEdad(fechaNacimiento);
            return edad >= 2 && edad <= 11;
        }
    }
}
