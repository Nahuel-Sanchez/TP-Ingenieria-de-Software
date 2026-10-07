using BLL_08YS.Negocio;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Utilidades compartidas por las pantallas de cargos adicionales (PN2): textos, estilo de chips
    /// y traducción de las excepciones de negocio a un mensaje para el usuario.
    /// </summary>
    internal static class UiCargos_68SA
    {
        #region Campos

        public static readonly Color Fondo = Color.FromArgb(5, 10, 40);
        public static readonly Color FondoBarra = Color.FromArgb(10, 18, 50);
        public static readonly Color FondoCampo = Color.FromArgb(5, 15, 45);
        public static readonly Color TextoSuave = Color.FromArgb(160, 165, 180);
        public static readonly Color ColorError = Color.FromArgb(235, 90, 90);
        public static readonly Color ColorExito = Color.FromArgb(76, 217, 100);
        public static readonly Color ColorAviso = Color.FromArgb(240, 180, 60);
        private static readonly Color ChipFondo = Color.FromArgb(15, 25, 55);
        private static readonly Color ChipTexto = Color.FromArgb(200, 200, 210);

        // Excepción de negocio -> clave de idiomas.json (si no está mapeada, se muestra el mensaje de la BLL)
        private static readonly Dictionary<Type, string> _mensajes = new Dictionary<Type, string>
        {
            { typeof(TarifaNoSuperiorException_68SA),            "Cargos_excTarifaNoSuperior" },
            { typeof(SinNochesRestantesException_68SA),          "Cargos_excSinNochesRestantes" },
            { typeof(MismaHabitacionException_68SA),             "Cargos_excMismaHabitacion" },
            { typeof(HabitacionNoDisponibleException_68SA),      "Cargos_excHabitacionNoDisponible" },
            { typeof(CapacidadExcedidaException_68SA),           "Cargos_excCapacidad" },
            { typeof(EstadoReservaInvalidoException_68SA),       "Cargos_excReservaNoEnCurso" },
            { typeof(ReservaNoEncontradaException_68SA),         "Cargos_excReservaNoEncontrada" },
            { typeof(PedidoRoomServiceInvalidoException_68SA),   "Cargos_excPedidoInvalido" },
            { typeof(PedidoNoPendienteException_68SA),           "Cargos_excPedidoNoPendiente" },
            { typeof(ItemRoomServiceDuplicadoException_68SA),    "Cargos_excItemDuplicado" },
            { typeof(ItemRoomServiceNoEncontradoException_68SA), "Cargos_excItemNoEncontrado" },
            { typeof(UnauthorizedAccessException),               "Cargos_excSinPermiso" },
        };

        #endregion

        #region Idioma

        /// <summary>Aplica la traducción a todos los controles que tienen la clave en Tag.</summary>
        public static void Traducir(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag is string clave && !string.IsNullOrWhiteSpace(clave))
                    c.Text = T(clave);
                if (c.HasChildren)
                    Traducir(c);
            }
        }

        #endregion

        #region Métodos públicos

        public static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        public static string Moneda(decimal valor) => $"${valor:N2}";

        /// <summary>Chip (RadioButton con aspecto de botón) con el mismo estilo que los filtros de Control de habitaciones.</summary>
        public static void EstiloChip(RadioButton chip)
        {
            void Pintar()
            {
                chip.BackColor = chip.Checked ? Color.Goldenrod : ChipFondo;
                chip.ForeColor = chip.Checked ? Color.FromArgb(10, 15, 35) : ChipTexto;
            }
            chip.FlatAppearance.CheckedBackColor = Color.Goldenrod;
            chip.CheckedChanged += (s, e) => Pintar();
            Pintar();
        }

        /// <summary>
        /// Columna de ícono (acciones) siempre al final de la grilla. Al volver a enlazar el DataSource, las columnas
        /// generadas se recrean después de las que ya estaban: sin esto el ícono quedaría como primera columna.
        /// </summary>
        public static void ColumnaIconoAlFinal(DataGridView dgv, string nombre)
        {
            if (!dgv.Columns.Contains(nombre))
                dgv.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono(nombre));
            dgv.Columns[nombre].DisplayIndex = dgv.Columns.Count - 1;
        }

        public static void Aviso(IWin32Window owner, string mensaje, string titulo = null)
            => MessageBox.Show(owner, mensaje, titulo ?? T("Cargos_tituloAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static void Info(IWin32Window owner, string mensaje, string titulo)
            => MessageBox.Show(owner, mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static bool Confirmar(IWin32Window owner, string mensaje, string titulo = null)
            => MessageBox.Show(owner, mensaje, titulo ?? T("Comun_tituloConfirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        /// <summary>
        /// Texto para el usuario: las validaciones de datos (DatosInvalidos / RangoFechas) se muestran tal cual
        /// (traen el detalle del campo); el resto de las excepciones de negocio, con su texto traducido.
        /// </summary>
        public static string Describir(Exception ex)
        {
            if (ex is DatosInvalidosException_68SA || ex is RangoFechasInvalidoException_68SA)
                return ex.Message;
            if (_mensajes.TryGetValue(ex.GetType(), out string clave))
            {
                string texto = T(clave);
                if (!string.IsNullOrEmpty(texto) && texto != $"[{clave}]") return texto;
            }
            return ex.Message;
        }

        /// <summary>true si es un error esperable de negocio (se informa con un aviso, no como falla).</summary>
        public static bool EsErrorDeNegocio(Exception ex)
            => ex is UnauthorizedAccessException || _mensajes.ContainsKey(ex.GetType())
               || ex is DatosInvalidosException_68SA || ex is RangoFechasInvalidoException_68SA;

        #endregion
    }
}
