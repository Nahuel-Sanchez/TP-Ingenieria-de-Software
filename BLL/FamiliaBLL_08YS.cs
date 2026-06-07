using DAL_08YS.Interfaces_Repositories;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS.Entities.Bitacora;
using Service_08YS;

namespace BLL_08YS
{
    public class FamiliaBLL_08YS : AccesoBLL_08YS
    {
        private readonly BitacoraBLL_08YS _bitacoraBll;
        private readonly IFamiliaRepository_08YS _familiaRepo;

        public FamiliaBLL_08YS(IFamiliaRepository_08YS familiaRepo, IPermisoRepository_08YS permisoRepo, BitacoraBLL_08YS bitacoraBll) : base(permisoRepo)
        {
            _familiaRepo = familiaRepo;
            _bitacoraBll = bitacoraBll;
        }

        public List<Familia_08YS> GetAll() => _familiaRepo.GetAll();

        // FormAM lado derecho (disponibles):
        // familiaIdExcluir null  → Alta: sin exclusiones por ciclos
        // familiaIdExcluir int   → Modificacion: excluye la familia actual + sus contenedoras
        // Los ya seleccionados (DGV izquierdo) los filtra la GUI quitándolos del DGV derecho
        public List<AccessComponent_08YS> GetComponentesDisponibles(int? familiaIdExcluir = null)
        {
            var permisos = _permisoRepo.GetAll().Cast<AccessComponent_08YS>();
            var familias = _familiaRepo.GetAll().Cast<AccessComponent_08YS>().ToList();

            if (familiaIdExcluir.HasValue)
            {
                familias = familias             // Excluir la familia que se está editando
                    .Where(f => ((Familia_08YS)f).FamiliaID != familiaIdExcluir.Value)
                    .ToList();

                // Excluir familias que ya contienen a la que se edita (evitando ciclos transitivos)
                var contenedoras = ResolverContenedoras(familiaIdExcluir.Value,
                    familias.Cast<Familia_08YS>().ToList());

                familias = familias
                    .Where(f => !contenedoras.Contains(((Familia_08YS)f).FamiliaID))
                    .ToList();
            }

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

        public void Crear(string nombre, HashSet<AccessComponent_08YS> componentes)
        {
            ValidarDatosEntrada(nombre, componentes);
            ValidarFamiliaNoExistente(componentes, null);
            SessionManager_08YS.Instance.ValidatePermission(Permisos.CrearFamilias);
            _familiaRepo.Create(nombre, componentes.ToList());
            _bitacoraBll.RegistrarEvento(Evento.FamiliaCreada);
        }

        public void Modificar(int familiaId, string nombre, HashSet<AccessComponent_08YS> componentes)
        {
            ValidarDatosEntrada(nombre, componentes);
            ValidarFamiliaNoExistente(componentes, familiaId);
            SessionManager_08YS.Instance.ValidatePermission(Permisos.ModificarFamilias);
            _familiaRepo.Modify(familiaId, nombre, componentes.ToList());
            _bitacoraBll.RegistrarEvento(Evento.FamiliaModificada);
        }

        public void Eliminar(int familiaId)
        {
            if (_familiaRepo.IsInUse(familiaId))
                throw new InvalidOperationException(
                    "No se puede eliminar: la familia está siendo utilizada por un rol u otra familia.");
            SessionManager_08YS.Instance.ValidatePermission(Permisos.EliminarFamilias);
            _familiaRepo.Delete(familiaId);
            _bitacoraBll.RegistrarEvento(Evento.FamiliaEliminada);
        }

        private void ValidarFamiliaNoExistente(HashSet<AccessComponent_08YS> componentes, int? excluirId)
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
