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
    public partial class FormDisponibilidad : Form
    {
        private readonly Action<Form> _abrirForm;

        public FormDisponibilidad(Action<Form> abrirForm)
        {
            InitializeComponent();
            _abrirForm = abrirForm;
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

        private void calRangoReservas_RangeChanged(object sender, EventArgs e)
        {
            //if (calRangoReservas.RangeStart.HasValue && calRangoReservas.RangeEnd.HasValue)
            //{
            //    int noches = calRangoReservas.Nights ?? 0;
            //    _lblResumen.Text = $"Check-in: {calRangoReservas.RangeStart:dd/MM/yyyy}   →   " +
            //                        $"Check-out: {calRangoReservas.RangeEnd:dd/MM/yyyy}   " +
            //                        $"({noches} {(noches == 1 ? "noche" : "noches")})";
            //    _btnBuscar.Enabled = true;
            //}
            //else if (calRangoReservas.RangeStart.HasValue)
            //{
            //    _lblResumen.Text = $"Check-in: {calRangoReservas.RangeStart:dd/MM/yyyy}   —   Seleccioná la fecha de check-out.";
            //    _btnBuscar.Enabled = false;
            //}
            //else
            //{
            //    _lblResumen.Text = "Seleccioná la fecha de check-in y la de check-out.";
            //    _btnBuscar.Enabled = false;
            //}
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!calRangoReservas.RangeStart.HasValue || !calRangoReservas.RangeEnd.HasValue)
                return;

            DateTime fechaIngreso = calRangoReservas.RangeStart.Value;
            DateTime fechaEgreso = calRangoReservas.RangeEnd.Value;

            // TODO: cuando exista FormControlHabitaciones_08YS, reemplazar el bloque de abajo por:
            // _openChildForm(new FormControlHabitaciones_08YS(ModoHabitaciones.Reserva, fechaIngreso, fechaEgreso));

            // Smoke test provisorio: confirma que la BLL ya devuelve datos reales para el rango elegido.
            var habitacionBLL = BLL_08YS.BLLFactory_08YS.CreateHabitacionBLL();
            var disponibles = habitacionBLL.GetDisponibles(fechaIngreso, fechaEgreso);

            MessageBox.Show(
                $"{disponibles.Count} habitación/es disponibles del {fechaIngreso:dd/MM/yyyy} al {fechaEgreso:dd/MM/yyyy}.",
                "Disponibilidad",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
