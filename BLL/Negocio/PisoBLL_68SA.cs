using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using Service_08YS.Entities.Bitacora;
using System;
using System.Collections.Generic;

namespace BLL_08YS.Negocio
{
    public class PisoBLL_68SA
    {
        private readonly IPisoRepository_68SA _pisoRepo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        public PisoBLL_68SA(IPisoRepository_68SA pisoRepo, BitacoraBLL_08YS bitacoraBll)
        {
            _pisoRepo = pisoRepo;
            _bitacoraBll = bitacoraBll;
        }

        public List<Piso_68SA> GetAll() => _pisoRepo.GetAll();

        public List<Piso_68SA> GetAll(PisoFiltro_68SA filtro) => _pisoRepo.GetAll(filtro ?? new PisoFiltro_68SA());

        public Piso_68SA GetById(int pisoId) => _pisoRepo.GetById(pisoId);

        public int Crear(Piso_68SA piso)
        {
            Validar(piso);

            if (_pisoRepo.ExisteNumero(piso.Numero))
                throw new PisoNumeroDuplicadoException_68SA();

            piso.PisoId = _pisoRepo.Crear(piso);
            _bitacoraBll.RegistrarEvento(Evento.PisoCreado, targetUsername: piso.PisoId.ToString());
            return piso.PisoId;
        }

        public void Modificar(Piso_68SA piso)
        {
            Validar(piso);

            if (_pisoRepo.GetById(piso.PisoId) == null)
                throw new PisoNoEncontradoException_68SA();

            if (_pisoRepo.ExisteNumero(piso.Numero, piso.PisoId))
                throw new PisoNumeroDuplicadoException_68SA();

            _pisoRepo.Modificar(piso);
            _bitacoraBll.RegistrarEvento(Evento.PisoModificado, targetUsername: piso.PisoId.ToString());
        }

        public void Eliminar(int pisoId)
        {
            if (_pisoRepo.GetById(pisoId) == null)
                throw new PisoNoEncontradoException_68SA();

            if (!_pisoRepo.Eliminar(pisoId))
                throw new PisoConHabitacionesException_68SA();

            _bitacoraBll.RegistrarEvento(Evento.PisoEliminado, targetUsername: pisoId.ToString());
        }

        private static void Validar(Piso_68SA piso)
        {
            if (piso == null) throw new ArgumentNullException(nameof(piso));

            piso.Nombre = piso.Nombre?.Trim();
            if (string.IsNullOrEmpty(piso.Nombre))
                throw new DatosInvalidosException_68SA("El nombre del piso es obligatorio.");
            if (piso.Nombre.Length > 50)
                throw new DatosInvalidosException_68SA("El nombre del piso no puede superar los 50 caracteres.");
        }
    }
}