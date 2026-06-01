using DAL_08YS.Interfaces_Repositories;
using Service_08YS.Entities.Acceso;
using Service_08YS.Entities.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS
{
    public abstract class AccesoBLL
    {
        protected readonly IPermisoRepository_08YS _permisoRepo;

        protected AccesoBLL(IPermisoRepository_08YS permisoRepo)
        {
            _permisoRepo = permisoRepo;
        }

        /// <summary>
        /// Evalúa si el candidato puede agregarse a la lista actual.
        /// — Valido: sin solapamiento, proceder.
        /// — SugerenciaReemplazo: solapamiento total con items existentes que el candidato
        ///   engloba completamente → proponer reemplazo al usuario.
        /// — ConflictoIrresoluble: solapamiento con items que el candidato no engloba
        ///   completamente → error, no se puede resolver.
        /// </summary>
        public ResultadoEvaluacion_08YS EvaluarAgregarComponente(
            IEnumerable<AccessComponent> listaActual,
            AccessComponent candidato)
        {
            var permsCandidato = candidato.GetPermisos()
                .Select(p => p.PermisoID).ToHashSet();

            var listaList = listaActual.ToList();

            // Items de la lista cuyos permisos están TODOS dentro del candidato (subsumed)
            var subsumed = listaList
                .Where(item => item.GetPermisos()
                    .All(p => permsCandidato.Contains(p.PermisoID)))
                .ToList();

            // Permisos de items NO subsumed que solapan con el candidato
            var conflictoReal = listaList
                .Except(subsumed)
                .SelectMany(i => i.GetPermisos())
                .Select(p => p.PermisoID)
                .Where(id => permsCandidato.Contains(id))
                .ToList();

            if (conflictoReal.Any())
            {
                var nombres = _permisoRepo.GetAll()
                    .Where(p => conflictoReal.Contains(p.PermisoID))
                    .Select(p => p.Nombre);

                return new ResultadoEvaluacion_08YS
                {
                    Resultado = ResultadoEvaluacion_08YS.Tipo.ConflictoIrresoluble,
                    Mensaje = $"'{candidato.Nombre}' contiene permisos que ya existen " +
                                $"en otros componentes que no engloba: {string.Join(", ", nombres)}."
                };
            }

            if (subsumed.Any())
            {
                var nombresReemplazar = subsumed.Select(i => $"'{i.Nombre}'");
                return new ResultadoEvaluacion_08YS
                {
                    Resultado = ResultadoEvaluacion_08YS.Tipo.SugerenciaReemplazo,
                    ComponentesAReemplazar = subsumed,
                    Mensaje = $"Al agregar '{candidato.Nombre}' se reemplazarán " +
                                            $"{string.Join(", ", nombresReemplazar)}, que ya están " +
                                            $"contenidos en él. ¿Desea continuar?"
                };
            }

            return new ResultadoEvaluacion_08YS { Resultado = ResultadoEvaluacion_08YS.Tipo.Valido };
        }

        protected void ValidarDatosEntrada(string nombre, List<AccessComponent> componentes)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new InvalidOperationException("El nombre no puede estar vacío.");
            if (!componentes.Any())
                throw new InvalidOperationException("Debe contener al menos un permiso o subfamilia.");
        }
    }
}
