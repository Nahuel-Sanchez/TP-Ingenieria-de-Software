using DAL_08YS.Interfaces_Repositories;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS
{
    public class FamiliaBLL_08YS : AccesoBLL
    {
        private readonly IFamiliaRepository_08YS _familiaRepo;

        public FamiliaBLL_08YS(IFamiliaRepository_08YS familiaRepo, IPermisoRepository_08YS permisoRepo) : base(permisoRepo)
        {
            _familiaRepo = familiaRepo;
        }

        // FormGestion: listado principal
        public List<Familia_08YS> GetAll() => _familiaRepo.GetAll();

        // FormABM lado derecho (disponibles):
        // familiaIdExcluir null  → Alta: sin exclusiones por ciclos
        // familiaIdExcluir int   → Modificacion: excluye la familia actual + sus contenedoras
        // Los ya seleccionados (DGV izquierdo) los filtra la GUI quitándolos del DGV derecho
        public List<AccessComponent> GetComponentesDisponibles(int? familiaIdExcluir = null)
        {
            var permisos = _permisoRepo.GetAll().Cast<AccessComponent>();
            var familias = _familiaRepo.GetAll().Cast<AccessComponent>().ToList();

            if (familiaIdExcluir.HasValue)
            {
                // Excluir la familia que se está editando
                familias = familias
                    .Where(f => ((Familia_08YS)f).FamiliaID != familiaIdExcluir.Value)
                    .ToList();

                // Excluir familias que ya contienen a la que se edita (evita ciclos transitivos)
                var contenedoras = ResolverContenedoras(familiaIdExcluir.Value,
                    familias.Cast<Familia_08YS>().ToList());

                familias = familias
                    .Where(f => !contenedoras.Contains(((Familia_08YS)f).FamiliaID))
                    .ToList();
            }

            // Permisos primero por convención visual
            return permisos.Concat(familias).ToList();
        }

        // BFS sobre el árbol ya cargado en memoria: sin viajes extra a la BD
        private HashSet<int> ResolverContenedoras(int familiaId, List<Familia_08YS> todasFamilias)
        {
            var contenedoras = new HashSet<int>();
            var cola = new Queue<int>();
            cola.Enqueue(familiaId);

            while (cola.Count > 0)
            {
                int buscado = cola.Dequeue();
                foreach (var f in todasFamilias)
                {
                    bool contieneAlBuscado = f.Hijos
                        .OfType<Familia_08YS>()
                        .Any(h => h.FamiliaID == buscado);

                    if (contieneAlBuscado && contenedoras.Add(f.FamiliaID))
                        cola.Enqueue(f.FamiliaID);
                }
            }

            return contenedoras;
        }

        // Las validaciones de negocio (nombre vacío, sin componentes) viven acá,
        // no en la GUI. La GUI solo arma la lista y llama.
        public void Crear(string nombre, List<AccessComponent> componentes)
        {
            ValidarDatosEntrada(nombre, componentes);
            ValidarFamiliaNoExistente(componentes, null);
            _familiaRepo.Create(nombre, componentes);
        }

        public void Modificar(int familiaId, string nombre, List<AccessComponent> componentes)
        {
            ValidarDatosEntrada(nombre, componentes);
            ValidarFamiliaNoExistente(componentes, familiaId);
            _familiaRepo.Modify(familiaId, nombre, componentes);
        }

        public void Eliminar(int familiaId)
        {
            if (_familiaRepo.IsInUse(familiaId))
                throw new InvalidOperationException(
                    "No se puede eliminar: la familia está siendo utilizada por un rol u otra familia.");

            _familiaRepo.Delete(familiaId);
        }

        private void ValidarFamiliaNoExistente(List<AccessComponent> componentes, int? excluirId)
        {
            var permsNuevos = componentes
                .SelectMany(c => c.GetPermisos())
                .Select(p => p.PermisoID)
                .ToHashSet();

            bool existeIdentica = _familiaRepo.GetAll()
                .Where(f => !excluirId.HasValue || f.FamiliaID != excluirId.Value)
                .Any(f => f.GetPermisos()
                           .Select(p => p.PermisoID)
                           .ToHashSet()
                           .SetEquals(permsNuevos));

            if (existeIdentica)
                throw new InvalidOperationException(
                    "Ya existe una familia con exactamente el mismo conjunto de permisos.");
        }
    }
}
