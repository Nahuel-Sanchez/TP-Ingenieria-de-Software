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
            if (string.IsNullOrWhiteSpace(huesped.Nacionalidad))
                throw new DatosInvalidosException_68SA("La nacionalidad es obligatoria.");

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

        public const int LimiteListado = 500;

        // --- CRUD ---
        public List<Huesped_68SA> GetListado(HuespedFiltro_68SA filtro)
        {
            filtro = filtro ?? new HuespedFiltro_68SA();
            filtro.Maximo = LimiteListado;
            return _huespedRepo.GetListado(filtro);
        }

        public void Modificar(Huesped_68SA huesped)
        {
            Validar(huesped);

            if (_huespedRepo.GetById(huesped.Id) == null)
                throw new HuespedNoEncontradoException_68SA();

            if (_huespedRepo.ExisteOtroConClave(huesped.Id, huesped.Documento, huesped.TipoDocumento, huesped.Nacionalidad))
                throw new HuespedDuplicadoException_68SA();

            _huespedRepo.Modificar(huesped);
            _bitacoraBll.RegistrarEvento(Evento.HuespedModificado, targetUsername: huesped.Documento);
        }

        public void Eliminar(int huespedId)
        {
            var huesped = _huespedRepo.GetById(huespedId);
            if (huesped == null)
                throw new HuespedNoEncontradoException_68SA();

            if (!_huespedRepo.Eliminar(huespedId))
                throw new HuespedConReservasException_68SA();

            _bitacoraBll.RegistrarEvento(Evento.HuespedEliminado, targetUsername: huesped.Documento);
        }

        private static void Validar(Huesped_68SA huesped)
        {
            if (huesped == null) throw new ArgumentNullException(nameof(huesped));

            huesped.Nombre = huesped.Nombre?.Trim();
            huesped.Apellido = huesped.Apellido?.Trim();
            huesped.Documento = huesped.Documento?.Trim();
            huesped.Nacionalidad = huesped.Nacionalidad?.Trim();
            huesped.Email = huesped.Email?.Trim();
            huesped.Telefono = huesped.Telefono?.Trim();

            Requerido(huesped.Nombre, 100, "El nombre");
            Requerido(huesped.Apellido, 100, "El apellido");
            Requerido(huesped.Documento, 20, "El documento");
            Requerido(huesped.Nacionalidad, 50, "La nacionalidad");

            if (!Enum.IsDefined(typeof(TipoDocumento), huesped.TipoDocumento))
                throw new DatosInvalidosException_68SA("El tipo de documento no es válido.");
            if (huesped.FechaNacimiento.Date > DateTime.Today)
                throw new DatosInvalidosException_68SA("La fecha de nacimiento no puede ser futura.");
            if (huesped.Email != null && huesped.Email.Length > 150)
                throw new DatosInvalidosException_68SA("El email no puede superar los 150 caracteres.");
            if (huesped.Telefono != null && huesped.Telefono.Length > 30)
                throw new DatosInvalidosException_68SA("El teléfono no puede superar los 30 caracteres.");
        }

        private static void Requerido(string valor, int max, string campo)
        {
            if (string.IsNullOrEmpty(valor))
                throw new DatosInvalidosException_68SA($"{campo} es obligatorio.");
            if (valor.Length > max)
                throw new DatosInvalidosException_68SA($"{campo} no puede superar los {max} caracteres.");
        }
    }
}