using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service_08YS
{
    public interface IIdiomaObserver_08YS
    {
        void UpdateIdioma();
    }

    public class TraductorManager_08YS
    {
        #region Campos

        private static TraductorManager_08YS _instance;
        private readonly List<IIdiomaObserver_08YS> _observadores = new List<IIdiomaObserver_08YS>();
        private Dictionary<string, Dictionary<string, string>> _traducciones;
        private string _idiomaActual = "es"; // Idioma por defecto

        #endregion

        #region Propiedades

        public static TraductorManager_08YS Instance
            => _instance ?? (_instance = new TraductorManager_08YS());

        #endregion

        #region Constructor

        private TraductorManager_08YS()
        {
            CargarJson();
        }

        #endregion

        #region Idioma

        public void CambiarIdioma(string codigoIdioma)
        {
            if (_traducciones.ContainsKey(codigoIdioma))
            {
                _idiomaActual = codigoIdioma;
                Notificar();
            }
        }

        #endregion

        #region Consultas

        // Función auxiliar para recuperar un texto por su clave
        public string GetTexto(string clave)
        {
            // Se llama muchísimas veces (cada texto de cada pantalla, celdas de grillas): una sola búsqueda por diccionario
            if (clave != null
                && _traducciones.TryGetValue(_idiomaActual, out var textos)
                && textos.TryGetValue(clave, out var texto))
            {
                return texto;
            }
            return $"[{clave}]";
        }

        #endregion

        #region Métodos públicos

        public void Suscribir(IIdiomaObserver_08YS observador)
        {
            if (!_observadores.Contains(observador))
                _observadores.Add(observador);
        }

        public void Desuscribir(IIdiomaObserver_08YS observador)
        {
            _observadores.Remove(observador);
        }

        #endregion

        #region Métodos auxiliares

        private void CargarJson()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "idiomas.json");

                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    _traducciones = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
                    
                }
                else
                {
                    _traducciones = new Dictionary<string, Dictionary<string, string>>();
                }
            }
            catch (Exception)
            {
              
                _traducciones = new Dictionary<string, Dictionary<string, string>>();
            }
        }

        private void Notificar()
        {
            foreach (var obs in _observadores)
            {
                obs.UpdateIdioma();
            }
        }

        #endregion
    }
}
