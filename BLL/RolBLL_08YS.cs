using DAL_08YS.Interfaces_Repositories;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS
{
    public class RolBLL_08YS
    {
        private readonly IRolRepository_08YS _rolRepo;
        private readonly IFamiliaRepository_08YS _familiaRepo;
        private readonly IPermisoRepository_08YS _permisoRepo;

        public RolBLL_08YS(IRolRepository_08YS rolRepo, IFamiliaRepository_08YS familiaRepo, IPermisoRepository_08YS permisoRepo)
        {
            _rolRepo = rolRepo;
            _familiaRepo = familiaRepo;
            _permisoRepo = permisoRepo;
        }

        // FormGestion: listado principal
        public List<Rol_08YS> GetAll() => _rolRepo.GetAll();

        // FormABM lado derecho: todos los permisos + todas las familias
        // Los ya seleccionados los filtra la GUI igual que en Familia
        public List<AccessComponent> GetComponentesDisponibles()
        {
            var permisos = _permisoRepo.GetAll().Cast<AccessComponent>();
            var familias = _familiaRepo.GetAll().Cast<AccessComponent>();
            return permisos.Concat(familias).ToList();
        }

        public void Crear(string nombre, List<AccessComponent> componentes)
        {
            ValidarDatosEntrada(nombre, componentes);
            _rolRepo.Create(nombre, componentes);
        }

        public void Modificar(int rolId, string nombre, List<AccessComponent> componentes)
        {
            ValidarDatosEntrada(nombre, componentes);
            _rolRepo.Modify(rolId, nombre, componentes);
        }

        public void Eliminar(int rolId)
        {
            if (_rolRepo.IsInUse(rolId))
                throw new InvalidOperationException(
                    "No se puede eliminar: el rol está asignado a uno o más usuarios.");

            _rolRepo.Delete(rolId);
        }

        private void ValidarDatosEntrada(string nombre, List<AccessComponent> componentes)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new InvalidOperationException("El nombre del rol no puede estar vacío.");
            if (!componentes.Any())
                throw new InvalidOperationException("El rol debe contener al menos un permiso o familia.");
        }
    }
}
