using BE_08YS;
using FontAwesome.Sharp;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormAccionesHabitacion_68SA : Form
    {
        public AccionHabitacion? AccionSeleccionada { get; private set; }

        public FormAccionesHabitacion_68SA(Habitacion_68SA habitacion, List<(string Texto, IconChar Icono, AccionHabitacion Accion)> acciones)
        {
            InitializeComponent();

            lblNumero.Text = habitacion.NroHabitacion;
            lblTipo.Text = habitacion.Tipo.Nombre;

            var (texto, fondo, acento) = EstiloEstadoHabitacion_68SA.Obtener(habitacion.EstadoVisual);
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

            int alturaAcciones = acciones.Count * 66;
            flpAcciones.Height = alturaAcciones;
            ClientSize = new Size(ClientSize.Width, flpAcciones.Top + alturaAcciones + 24);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
    

