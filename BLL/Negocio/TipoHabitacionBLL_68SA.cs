using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.habitacion;
using Service_08YS.Entities.Bitacora;
using System;
using System.Collections.Generic;

namespace BLL_08YS.Negocio
{
    public class TipoHabitacionBLL_68SA
    {
        private const decimal TarifaMaxima = 99999999.99m; // decimal(10,2)

        private readonly ITipoHabitacionRepository_68SA _tipoRepo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        public TipoHabitacionBLL_68SA(ITipoHabitacionRepository_68SA tipoRepo, BitacoraBLL_08YS bitacoraBll)
        {
            _tipoRepo = tipoRepo;
            _bitacoraBll = bitacoraBll;
        }

        public List<TipoHabitacion> GetAll() => _tipoRepo.GetAll();

        public List<TipoHabitacion> GetAll(TipoHabitacionFiltro_68SA filtro) =>
            _tipoRepo.GetAll(filtro ?? new TipoHabitacionFiltro_68SA());

        public TipoHabitacion GetById(int tipoHabitacionId) => _tipoRepo.GetById(tipoHabitacionId);

        public int Crear(TipoHabitacion tipo)
        {
            Validar(tipo);

            if (_tipoRepo.ExisteNombre(tipo.Nombre))
                throw new TipoHabitacionNombreDuplicadoException_68SA();

            tipo.Id = _tipoRepo.Crear(tipo);
            _bitacoraBll.RegistrarEvento(Evento.TipoHabitacionCreado, targetUsername: tipo.Id.ToString());
            return tipo.Id;
        }

        public void Modificar(TipoHabitacion tipo)
        {
            Validar(tipo);

            if (_tipoRepo.GetById(tipo.Id) == null)
                throw new TipoHabitacionNoEncontradoException_68SA();

            if (_tipoRepo.ExisteNombre(tipo.Nombre, tipo.Id))
                throw new TipoHabitacionNombreDuplicadoException_68SA();

            _tipoRepo.Modificar(tipo);
            _bitacoraBll.RegistrarEvento(Evento.TipoHabitacionModificado, targetUsername: tipo.Id.ToString());
        }

        public void Eliminar(int tipoHabitacionId)
        {
            if (_tipoRepo.GetById(tipoHabitacionId) == null)
                throw new TipoHabitacionNoEncontradoException_68SA();

            if (!_tipoRepo.Eliminar(tipoHabitacionId))
                throw new TipoHabitacionConHabitacionesException_68SA();

            _bitacoraBll.RegistrarEvento(Evento.TipoHabitacionEliminado, targetUsername: tipoHabitacionId.ToString());
        }

        private static void Validar(TipoHabitacion tipo)
        {
            if (tipo == null) throw new ArgumentNullException(nameof(tipo));

            tipo.Nombre = tipo.Nombre?.Trim();
            tipo.Descripcion = string.IsNullOrWhiteSpace(tipo.Descripcion) ? null : tipo.Descripcion.Trim();
            tipo.TarifaNoche = Math.Round(tipo.TarifaNoche, 2);

            if (string.IsNullOrEmpty(tipo.Nombre))
                throw new DatosInvalidosException_68SA("El nombre del tipo de habitación es obligatorio.");
            if (tipo.Nombre.Length > 50)
                throw new DatosInvalidosException_68SA("El nombre del tipo de habitación no puede superar los 50 caracteres.");
            if (tipo.Descripcion != null && tipo.Descripcion.Length > 200)
                throw new DatosInvalidosException_68SA("La descripción no puede superar los 200 caracteres.");
            if (tipo.Capacidad < 1)
                throw new DatosInvalidosException_68SA("La capacidad debe ser de al menos 1 persona.");

            // Reservas.TarifaNoche tiene CHECK (> 0): una tarifa nula impediría crear reservas de este tipo
            if (tipo.TarifaNoche <= 0)
                throw new DatosInvalidosException_68SA("La tarifa por noche debe ser mayor a 0.");
            if (tipo.TarifaNoche > TarifaMaxima)
                throw new DatosInvalidosException_68SA("La tarifa por noche supera el máximo permitido.");
        }
    }
}