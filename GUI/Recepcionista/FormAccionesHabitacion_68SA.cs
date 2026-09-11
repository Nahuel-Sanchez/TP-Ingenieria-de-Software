using BE_08YS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormAccionesHabitacion_68SA : Form
    {
        public string AccionSeleccionada { get; private set; }

        public FormAccionesHabitacion_68SA(Habitacion_68SA habitacion, List<(string Texto, string Accion)> acciones)
        {
            InitializeComponent();

            lblNumero.Text = habitacion.NroHabitacion;
            lblTipo.Text = habitacion.Tipo.Nombre.ToUpperInvariant();

            foreach (var (texto, accion) in acciones)
            {
                var boton = new Button
                {
                    Text = texto,
                    Tag = accion,
                    Width = flpAcciones.ClientSize.Width - 4,
                    Height = 44,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(15, 25, 55),
                    ForeColor = Color.FromArgb(230, 230, 235),
                    Font = new Font("Segoe UI", 10F),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(16, 0, 0, 0),
                    Margin = new Padding(0, 0, 0, 6)
                };
                boton.FlatAppearance.BorderColor = Color.FromArgb(60, 75, 110);
                boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 40, 80);
                boton.Click += (s, e) =>
                {
                    AccionSeleccionada = (string)boton.Tag;
                    DialogResult = DialogResult.OK;
                    Close();
                };
                flpAcciones.Controls.Add(boton);
            }

            Height = 140 + acciones.Count * 50 + 50;
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
