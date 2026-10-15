using Newtonsoft.Json;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GUI_08YS.Ayuda
{
    public class SeccionAyuda_68SA
    {
        [JsonProperty("titulo")] public string Titulo { get; set; }
        [JsonProperty("parrafos")] public List<string> Parrafos { get; set; } = new List<string>();
    }

    /// <summary>Explicación de un elemento de una pantalla (botón, filtro, grilla...).</summary>
    public class ElementoAyuda_68SA
    {
        #region Propiedades

        /// <summary>Nombres de los controles que explica (propiedad Name).</summary>
        [JsonProperty("controles")] public List<string> Controles { get; set; } = new List<string>();
        [JsonProperty("nombre")] public string Nombre { get; set; }
        [JsonProperty("texto")] public string Texto { get; set; }

        /// <summary>Permisos que muestran ese control en la pantalla (con uno alcanza). Vacío = lo ve cualquiera.</summary>
        [JsonProperty("permisos")] public List<string> Permisos { get; set; } = new List<string>();

        #endregion
    }

    /// <summary>Un tema de la ayuda: una pantalla o un grupo de pantallas del sistema.</summary>
    public class TemaAyuda_68SA
    {
        #region Propiedades

        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("padre")] public string Padre { get; set; }
        [JsonProperty("titulo")] public string Titulo { get; set; }
        [JsonProperty("icono")] public string Icono { get; set; }
        [JsonProperty("pantallas")] public List<string> Pantallas { get; set; } = new List<string>();
        /// <summary>
        /// Permisos que dan acceso al tema (con uno alcanza). Vacío = tema general, o grupo que se ve
        /// si el usuario puede ver al menos uno de sus temas.
        /// </summary>
        [JsonProperty("permisos")] public List<string> Permisos { get; set; } = new List<string>();
        [JsonProperty("resumen")] public string Resumen { get; set; }
        [JsonProperty("secciones")] public List<SeccionAyuda_68SA> Secciones { get; set; } = new List<SeccionAyuda_68SA>();
        [JsonProperty("pasos")] public List<string> Pasos { get; set; } = new List<string>();
        [JsonProperty("elementos")] public List<ElementoAyuda_68SA> Elementos { get; set; } = new List<ElementoAyuda_68SA>();
        [JsonProperty("consejos")] public List<string> Consejos { get; set; } = new List<string>();
        [JsonProperty("relacionados")] public List<string> Relacionados { get; set; } = new List<string>();

        [JsonIgnore] public TemaAyuda_68SA PadreTema { get; internal set; }
        [JsonIgnore] public List<TemaAyuda_68SA> Hijos { get; } = new List<TemaAyuda_68SA>();
        /// <summary>Número en el índice ("2", "2.3"...).</summary>
        [JsonIgnore] public string Numero { get; internal set; }
        [JsonIgnore] public int Nivel => PadreTema == null ? 0 : PadreTema.Nivel + 1;

        /// <summary>Elementos de la pantalla que el usuario conectado puede ver (según sus permisos).</summary>
        [JsonIgnore] public List<ElementoAyuda_68SA> ElementosVisibles => AyudaContenido_68SA.FiltrarElementos(Elementos);

        #endregion

        #region Métodos públicos

        /// <summary>Elemento que explica el control con ese nombre (o null).</summary>
        public ElementoAyuda_68SA ElementoDe(string nombreControl)
            => string.IsNullOrEmpty(nombreControl) ? null
             : ElementosVisibles.FirstOrDefault(e => e.Controles != null && e.Controles.Contains(nombreControl));

        /// <summary>Texto completo del tema (para el buscador).</summary>
        internal string TextoCompleto()
            => string.Join(" ", new[] { Titulo, Resumen }
                .Concat(Secciones.SelectMany(s => new[] { s.Titulo }.Concat(s.Parrafos ?? new List<string>())))
                .Concat(Pasos).Concat(Consejos)
                .Concat(ElementosVisibles.SelectMany(e => new[] { e.Nombre, e.Texto }))
                .Where(x => !string.IsNullOrEmpty(x)));

        #endregion
    }

    /// <summary>
    /// Contenido de la ayuda en línea (archivo ayuda.json, junto al ejecutable), en el idioma activo.
    /// Arma el árbol de temas ("menú escalera") y permite buscar el tema de cada pantalla.
    /// La ayuda depende de los permisos del usuario conectado: solo se incluyen los temas (y los elementos
    /// de cada pantalla) a los que tiene acceso; el árbol, el buscador, F1 y el manual en PDF usan esta lista.
    /// </summary>
    public static class AyudaContenido_68SA
    {
        #region Campos

        private static Dictionary<string, Idioma> _archivo;
        private static List<TemaAyuda_68SA> _armados;
        private static string _firmaArmados;   // idioma + permisos del usuario con que se armó la lista

        #endregion

        #region Propiedades

        /// <summary>"es" o "en", según el idioma activo del sistema.</summary>
        public static string IdiomaActual
        {
            get
            {
                string cultura = TraductorManager_08YS.Instance.GetTexto("Comun_cultura") ?? "es";
                return cultura.StartsWith("en", StringComparison.OrdinalIgnoreCase) ? "en" : "es";
            }
        }

        /// <summary>
        /// Temas visibles para el usuario conectado, en orden de índice (recorrido en profundidad del árbol).
        /// Se vuelve a armar si cambia el idioma o el usuario (o sus permisos); la numeración es la del árbol visible.
        /// </summary>
        public static List<TemaAyuda_68SA> Temas
        {
            get
            {
                string idioma = IdiomaActual;
                var permisos = PermisosUsuario();
                string firma = idioma + "|" + string.Join(",", permisos.OrderBy(p => p, StringComparer.Ordinal));
                if (_armados != null && firma == _firmaArmados) return _armados;

                Cargar();
                Idioma datos;
                if (!_archivo.TryGetValue(idioma, out datos) && !_archivo.TryGetValue("es", out datos))
                    datos = new Idioma();
                var temas = datos.Temas ?? new List<TemaAyuda_68SA>();

                // Árbol completo (sin filtrar) para decidir qué se ve
                var porId = temas.Where(t => !string.IsNullOrEmpty(t.Id)).GroupBy(t => t.Id).ToDictionary(g => g.Key, g => g.First());
                var hijosDe = temas.ToDictionary(t => t, t => new List<TemaAyuda_68SA>());
                var raices = new List<TemaAyuda_68SA>();
                foreach (var tema in temas)
                {
                    if (!string.IsNullOrEmpty(tema.Padre) && porId.TryGetValue(tema.Padre, out var padre) && !ReferenceEquals(padre, tema))
                        hijosDe[padre].Add(tema);
                    else
                        raices.Add(tema);
                }

                var visibles = new HashSet<TemaAyuda_68SA>();
                foreach (var raiz in raices)
                    MarcarVisibles(raiz, true, hijosDe, permisos, visibles);

                // Se rearma el árbol desde cero solo con los temas visibles (sin hijos duplicados ni de otro usuario)
                foreach (var t in temas) { t.Hijos.Clear(); t.PadreTema = null; }
                foreach (var tema in temas.Where(visibles.Contains))
                    foreach (var hijo in hijosDe[tema].Where(visibles.Contains))
                    {
                        hijo.PadreTema = tema;
                        tema.Hijos.Add(hijo);
                    }

                var ordenados = new List<TemaAyuda_68SA>();
                int n = 0;
                foreach (var raiz in raices.Where(visibles.Contains))
                    Numerar(raiz, (++n).ToString(), ordenados);
                _armados = ordenados;
                _firmaArmados = firma;
                return ordenados;
            }
        }

        public static IEnumerable<TemaAyuda_68SA> Raices => Temas.Where(t => t.PadreTema == null);

        #endregion

        #region Carga de datos

        private static void Cargar()
        {
            if (_archivo != null) return;
            try
            {
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ayuda.json");
                _archivo = File.Exists(ruta)
                    ? JsonConvert.DeserializeObject<Dictionary<string, Idioma>>(File.ReadAllText(ruta))
                    : null;
            }
            catch (Exception)
            {
                _archivo = null;
            }
            if (_archivo == null) _archivo = new Dictionary<string, Idioma>();
        }

        #endregion

        #region Métodos públicos

        public static TemaAyuda_68SA Tema(string id) => Temas.FirstOrDefault(t => t.Id == id);

        /// <summary>Tema que explica la pantalla (por nombre de clase del formulario), o null.</summary>
        public static TemaAyuda_68SA TemaDePantalla(string nombreFormulario)
            => string.IsNullOrEmpty(nombreFormulario) ? null
             : Temas.FirstOrDefault(t => t.Pantallas != null && t.Pantallas.Contains(nombreFormulario));

        /// <summary>Elementos que el usuario conectado puede ver (los que no piden permiso, o con uno que tenga).</summary>
        public static List<ElementoAyuda_68SA> FiltrarElementos(IEnumerable<ElementoAyuda_68SA> elementos)
        {
            var permisos = PermisosUsuario();
            return (elementos ?? Enumerable.Empty<ElementoAyuda_68SA>()).Where(e => Permite(e.Permisos, permisos)).ToList();
        }

        #endregion

        #region Métodos auxiliares

        /// <summary>
        /// Un tema se ve si el usuario tiene alguno de sus permisos (o no pide ninguno) y su grupo se ve.
        /// Un grupo sin permisos propios se ve solo si se ve alguno de sus temas.
        /// </summary>
        private static bool MarcarVisibles(TemaAyuda_68SA tema, bool padreVisible, Dictionary<TemaAyuda_68SA, List<TemaAyuda_68SA>> hijosDe,
                                           HashSet<string> permisos, HashSet<TemaAyuda_68SA> visibles)
        {
            bool propio = padreVisible && Permite(tema.Permisos, permisos);
            bool algunHijo = false;
            foreach (var hijo in hijosDe[tema])
                algunHijo |= MarcarVisibles(hijo, propio, hijosDe, permisos, visibles);

            bool esGrupo = (tema.Permisos == null || tema.Permisos.Count == 0) && hijosDe[tema].Count > 0;
            bool visible = propio && (!esGrupo || algunHijo);
            if (visible) visibles.Add(tema);
            return visible;
        }

        private static bool Permite(IEnumerable<string> requeridos, HashSet<string> permisos)
        {
            var lista = (requeridos ?? Enumerable.Empty<string>()).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            return lista.Count == 0 || lista.Any(permisos.Contains);
        }

        /// <summary>Nombres de los permisos del usuario conectado (vacío si nadie inició sesión).</summary>
        private static HashSet<string> PermisosUsuario()
        {
            try
            {
                var rol = SessionManager_08YS.Instance.Current?.Rol;
                return rol == null ? new HashSet<string>() : new HashSet<string>(rol.GetPermisos().Select(p => p.Nombre));
            }
            catch (Exception)
            {
                return new HashSet<string>();
            }
        }

        private static void Numerar(TemaAyuda_68SA tema, string numero, List<TemaAyuda_68SA> salida)
        {
            tema.Numero = numero;
            salida.Add(tema);
            int i = 0;
            foreach (var hijo in tema.Hijos)
                Numerar(hijo, numero + "." + (++i), salida);
        }

        #endregion

        #region Tipos anidados

        private class Idioma
        {
            [JsonProperty("temas")] public List<TemaAyuda_68SA> Temas { get; set; } = new List<TemaAyuda_68SA>();
        }

        #endregion
    }
}
