using BE_08YS;
using FontAwesome.Sharp;
using Service_08YS;
using GUI_08YS.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormAccionesHabitacion_68SA : Form
    {
        #region Propiedades

        public AccionHabitacion? AccionSeleccionada { get; private set; }

        #endregion

        #region Constructor

        public FormAccionesHabitacion_68SA(Habitacion_68SA habitacion, List<(string Texto, IconChar Icono, AccionHabitacion Accion)> acciones)
        {
            InitializeComponent();
            TraducirControles(this);

            lblNumero.Text = habitacion.NroHabitacion;
            lblTipo.Text = habitacion.Tipo.Nombre;

            var (texto, fondo, acento) = EstiloEstadoHabitacion_68SA.Get(habitacion.EstadoVisual);
            pnlBadgeEstado.BackColor = fondo;
            pnlPuntoEstado.BackColor = acento;
            lblEstadoBadge.Text = texto;
            lblEstadoBadge.ForeColor = acento;

            foreach (var (accionTexto, icono, accion) in acciones)
            {
                var boton = new IconButton
                {
                    Text = accionTexto,
                    Tag = accion,
                    IconChar = icono,
                    IconColor = Color.Gold,
                    IconSize = 30,
                    Size = new Size(594, 56),
                    Margin = new Padding(0, 0, 0, 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(5, 15, 45),
                    ForeColor = Color.Goldenrod,
                    Font = new Font("Segoe UI", 11F),
                    TextAlign = ContentAlignment.MiddleLeft,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Padding = new Padding(18, 0, 0, 0),
                    UseVisualStyleBackColor = false
                };
                boton.Click += (s, e) =>
                {
                    AccionSeleccionada = (AccionHabitacion)boton.Tag;
                    DialogResult = DialogResult.OK;
                    Close();
                };
                flpAcciones.Controls.Add(boton);
            }

            AjustarAlContenido();
        }

        /// <summary>
        /// Los botones ocupan todo el ancho del panel (nunca hay barra horizontal) y la ventana toma el alto
        /// justo para mostrar todas las acciones. Solo si no entran en la pantalla se limita el alto y
        /// aparece la barra vertical.
        /// </summary>
        private void AjustarAlContenido()
        {
            int altoAcciones = flpAcciones.Padding.Vertical;
            foreach (Control boton in flpAcciones.Controls)
                altoAcciones += boton.Height + boton.Margin.Vertical;

            int fijo = flpAcciones.Top + 24 + Padding.Vertical;   // encabezado y datos de la habitación + margen inferior
            int altoMaximo = Screen.FromPoint(Cursor.Position).WorkingArea.Height - 40;
            int altoLista = Math.Max(66, Math.Min(altoAcciones, altoMaximo - fijo));

            flpAcciones.Height = altoLista;
            ClientSize = new Size(ClientSize.Width, fijo + altoLista);
            flpAcciones.Width = pnlBorde.ClientSize.Width - flpAcciones.Left * 2;
            flpAcciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // Con barra vertical o sin ella, los botones se ajustan al ancho visible
            LayoutAdaptable_68SA.EstirarHijosAlAncho(flpAcciones);
        }

        #endregion

        #region Idioma

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag != null && c.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
                    c.Text = TraductorManager_08YS.Instance.GetTexto(tag);

                if (c.HasChildren)
                    TraducirControles(c);
            }
        }

        #endregion

        #region Sobrescrituras

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion
    }
}
    

