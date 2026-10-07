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

        #endregion

        #region Métodos públicos

        /// <summary>Elemento que explica el control con ese nombre (o null).</summary>
        public ElementoAyuda_68SA ElementoDe(string nombreControl)
            => string.IsNullOrEmpty(nombreControl) ? null
             : Elementos.FirstOrDefault(e => e.Controles != null && e.Controles.Contains(nombreControl));

        /// <summary>Texto completo del tema (para el buscador).</summary>
        internal string TextoCompleto()
            => string.Join(" ", new[] { Titulo, Resumen }
                .Concat(Secciones.SelectMany(s => new[] { s.Titulo }.Concat(s.Parrafos ?? new List<string>())))
                .Concat(Pasos).Concat(Consejos)
                .Concat(Elementos.SelectMany(e => new[] { e.Nombre, e.Texto }))
                .Where(x => !string.IsNullOrEmpty(x)));

        #endregion
    }

    /// <summary>
    /// Contenido de la ayuda en línea (archivo ayuda.json, junto al ejecutable), en el idioma activo.
    /// Arma el árbol de temas ("menú escalera") y permite buscar el tema de cada pantalla.
    /// </summary>
    public static class AyudaContenido_68SA
    {
        #region Campos

        private static Dictionary<string, Idioma> _archivo;
        private static readonly Dictionary<string, List<TemaAyuda_68SA>> _armados = new Dictionary<string, List<TemaAyuda_68SA>>();

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

        /// <summary>Todos los temas en orden de índice (recorrido en profundidad del árbol).</summary>
        public static List<TemaAyuda_68SA> Temas
        {
            get
            {
                string idioma = IdiomaActual;
                if (_armados.TryGetValue(idioma, out var listos)) return listos;

                Cargar();
                Idioma datos;
                if (!_archivo.TryGetValue(idioma, out datos) && !_archivo.TryGetValue("es", out datos))
                    datos = new Idioma();
                var temas = datos.Temas ?? new List<TemaAyuda_68SA>();
                // Si falta un idioma se usa el español: se rearma el árbol desde cero (sin hijos duplicados)
                foreach (var t in temas) { t.Hijos.Clear(); t.PadreTema = null; }
                var porId = temas.Where(t => !string.IsNullOrEmpty(t.Id)).GroupBy(t => t.Id).ToDictionary(g => g.Key, g => g.First());
                foreach (var tema in temas)
                    if (!string.IsNullOrEmpty(tema.Padre) && porId.TryGetValue(tema.Padre, out var padre) && !ReferenceEquals(padre, tema))
                    {
                        tema.PadreTema = padre;
                        padre.Hijos.Add(tema);
                    }

                var ordenados = new List<TemaAyuda_68SA>();
                int n = 0;
                foreach (var raiz in temas.Where(t => t.PadreTema == null))
                    Numerar(raiz, (++n).ToString(), ordenados);
                _armados[idioma] = ordenados;
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

        #endregion

        #region Métodos auxiliares

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
