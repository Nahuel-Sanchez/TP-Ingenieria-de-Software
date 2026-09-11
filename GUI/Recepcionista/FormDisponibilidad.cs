using Service_08YS;
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
    public partial class FormDisponibilidad : Form, IIdiomaObserver_08YS
    {
        private readonly Action<Form> _openChildForm;

        public FormDisponibilidad(Action<Form> abrirForm)
        {
            InitializeComponent();
            _openChildForm = abrirForm;
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
            btnContinuar.Enabled = calRangoReservas.RangeStart.HasValue && calRangoReservas.RangeEnd.HasValue;
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

            _openChildForm(new FormControlHabitaciones_68SA(_openChildForm, ModoHabitaciones.Reserva, fechaIngreso, fechaEgreso));
        }

        public void UpdateIdioma()
        {
            TraducirControles(this);
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag != null && !string.IsNullOrWhiteSpace(c.Tag.ToString()))
                {
                    c.Text = TraductorManager_08YS.Instance.GetTexto(c.Tag.ToString());
                }

                if (c.HasChildren)
                {
                    TraducirControles(c);
                }
            }
        }
    }
}
