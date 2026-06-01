using DAL_08YS.Interfaces_Repositories;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS
{
    public class RolBLL_08YS : AccesoBLL
    {
        private readonly IRolRepository_08YS _rolRepo;
        private readonly IFamiliaRepository_08YS _familiaRepo;

        public RolBLL_08YS(IRolRepository_08YS rolRepo, IFamiliaRepository_08YS familiaRepo, IPermisoRepository_08YS permisoRepo) : base(permisoRepo)
        {
            _rolRepo = rolRepo;
            _familiaRepo = familiaRepo;
        }

        // FormGestion: listado principal
        public List<Rol_08YS> GetAll() => _rolRepo.GetAll();

        // FormABM lado derecho: todos los permisos + todas las familias
        // Los ya seleccionados los filtra la GUI igual que en Familia
        public List<AccessComponent_08YS> GetComponentesDisponibles()
        {
            var permisos = _permisoRepo.GetAll().Cast<AccessComponent_08YS>();
            var familias = _familiaRepo.GetAll().Cast<AccessComponent_08YS>();
            return permisos.Concat(familias).ToList();
        }

        public void Crear(string nombre, List<AccessComponent_08YS> componentes)
        {
            ValidarDatosEntrada(nombre, componentes);
            _rolRepo.Create(nombre, componentes);
        }

        public void Modificar(int rolId, string nombre, List<AccessComponent_08YS> componentes)
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

    }
}
