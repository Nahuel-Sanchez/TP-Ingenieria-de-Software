using CustomControls;
using Service_08YS;
using System;
using System.Globalization;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// Ítem de IconComboBox que envuelve un valor de enum y muestra su texto traducido
    /// (clave "Enum_{Tipo}_{Valor}" en idiomas.json). Equals compara contra el enum,
    /// así <c>combo.SelectedItem = valorEnum</c> sigue seleccionando el ítem correcto.
    /// </summary>
    public sealed class OpcionEnum_68SA
    {
        public Enum Valor { get; }

        public OpcionEnum_68SA(Enum valor)
        {
            Valor = valor ?? throw new ArgumentNullException(nameof(valor));
        }

        public override string ToString() => Texto(Valor);

        public override bool Equals(object obj)
        {
            if (obj is OpcionEnum_68SA otra) return Valor.Equals(otra.Valor);
            return obj != null && Valor.Equals(obj);
        }

        public override int GetHashCode() => Valor.GetHashCode();

        /// <summary>Texto traducido de un valor de enum; si no hay traducción, el nombre del valor.</summary>
        public static string Texto(Enum valor)
        {
            if (valor == null) return string.Empty;
            string clave = $"Enum_{valor.GetType().Name}_{valor}";
            string texto = TraductorManager_08YS.Instance.GetTexto(clave);
            return texto == $"[{clave}]" ? valor.ToString() : texto;
        }

        /// <summary>
        /// Llena el combo con todos los valores del enum traducidos. Si se pasa textoTodos,
        /// agrega primero una opción "Todos" (SelectedItem = string). Conserva la selección previa.
        /// </summary>
        public static void Cargar(IconComboBox combo, Type tipoEnum, string textoTodos = null)
        {
            object seleccionAnterior = combo.SelectedItem;
            int indiceAnterior = combo.SelectedIndex;

            combo.Items.Clear();
            if (textoTodos != null)
                combo.Items.Add(textoTodos);
            foreach (Enum valor in Enum.GetValues(tipoEnum))
                combo.Items.Add(new OpcionEnum_68SA(valor));

            if (seleccionAnterior is OpcionEnum_68SA opcion)
                combo.SelectedItem = opcion.Valor;
            else if (indiceAnterior >= 0 && indiceAnterior < combo.Items.Count)
                combo.SelectedIndex = indiceAnterior;
        }

        /// <summary>Valor de enum seleccionado en el combo, o null si no hay (o si está "Todos").</summary>
        public static T? Seleccionado<T>(IconComboBox combo) where T : struct
        {
            if (combo.SelectedItem is OpcionEnum_68SA opcion && opcion.Valor is T)
                return (T)(object)opcion.Valor;
            return null;
        }

        /// <summary>
        /// Cultura del idioma activo (clave "Comun_cultura"), para formatear fechas y nombres de días/meses.
        /// </summary>
        public static CultureInfo CulturaActual()
        {
            try
            {
                return CultureInfo.GetCultureInfo(TraductorManager_08YS.Instance.GetTexto("Comun_cultura"));
            }
            catch (CultureNotFoundException)
            {
                return CultureInfo.CurrentCulture;
            }
        }
    }
}
