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
    public partial class UC_HabitacionCard_68SA : UserControl
    {
        public event EventHandler<Habitacion_68SA> HabitacionSeleccionada;
        public Habitacion_68SA Habitacion { get; private set; }

        public UC_HabitacionCard_68SA()
        {
            InitializeComponent();
            HacerClickeableRecursivo(this);
        }

        public void Cargar(Habitacion_68SA habitacion, bool forzarDisponible = false)
        {
            Habitacion = habitacion;
            lblNumero.Text = habitacion.NroHabitacion;
            lblTipo.Text = habitacion.Tipo.Nombre;
            lblCapacidad.Text = $"Max: {habitacion.Tipo.Capacidad}";

            AplicarEstiloEstado(forzarDisponible ? EstadoHabitacion.Disponible : habitacion.EstadoVisual);
        }

        private void AplicarEstiloEstado(EstadoHabitacion estado)
        {
            var (texto, fondo, acento) = EstiloEstadoHabitacion_68SA.Obtener(estado);
            pnlEstado.BackColor = fondo;
            lblEstado.Text = texto;
            lblEstado.ForeColor = acento;
            pnlPunto.BackColor = acento;
        }

        private void HacerClickeableRecursivo(Control control)
        {
            control.Click += (s, e) => HabitacionSeleccionada?.Invoke(this, Habitacion);
            foreach (Control hijo in control.Controls)
                HacerClickeableRecursivo(hijo);
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
