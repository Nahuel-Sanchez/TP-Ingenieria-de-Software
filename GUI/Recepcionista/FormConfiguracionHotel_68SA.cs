using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using Service_08YS;
using System;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormConfiguracionHotel_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly ConfiguracionHotelBLL_68SA _configBLL = BLLFactory_08YS.CreateConfiguracionHotelBLL();
        private ConfiguracionHotel_68SA _configuracion; // null = no hay fila cargada en la base

        public FormConfiguracionHotel_68SA()
        {
            InitializeComponent();

            btnGuardar.Click += (s, e) => Guardar();
            btnDescartar.Click += (s, e) => CargarConfiguracion();

            AplicarTextos();

            // Igual que el resto de las pantallas: la carga va en Shown, con el handle ya creado
            Shown += (s, e) => CargarConfiguracion();
        }

        // ---------- Idioma ----------

        public void UpdateIdioma() => AplicarTextos();

        private void AplicarTextos()
        {
            TraducirControles(this);

            if (lblAviso.Visible)
                lblAviso.Text = TraductorManager_08YS.Instance.GetTexto("Config_msgSinConfiguracion");
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
                    c.Text = TraductorManager_08YS.Instance.GetTexto(tag);

                if (c.HasChildren)
                    TraducirControles(c);
            }
        }

        // ---------- Carga ----------

        private void CargarConfiguracion()
        {
            _configuracion = _configBLL.GetConfiguracion();
            bool hayConfiguracion = _configuracion != null;

            btnGuardar.Enabled = hayConfiguracion;
            btnDescartar.Enabled = hayConfiguracion;
            lblAviso.Visible = !hayConfiguracion;

            if (!hayConfiguracion)
            {
                lblAviso.Text = TraductorManager_08YS.Instance.GetTexto("Config_msgSinConfiguracion");
                return;
            }

            dtpCheckIn.TimeValue = _configuracion.HoraCheckIn;
            dtpCheckOut.TimeValue = _configuracion.HoraCheckOut;
            nudGracia.Value = _configuracion.GraciaNoShowHoras;
        }

        // ---------- Guardar ----------

        private void Guardar()
        {
            if (_configuracion == null) return;

            var t = TraductorManager_08YS.Instance;

            if (!dtpCheckIn.TimeValue.HasValue || !dtpCheckOut.TimeValue.HasValue)
            {
                MessageBox.Show(t.GetTexto("Config_msgIngreseHoras"), t.GetTexto("Comun_msgDatosIncompletos"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Es una configuración global: afecta la cancelación automática de no-shows
            var confirmacion = MessageBox.Show(t.GetTexto("Config_msgConfirmarGuardar"), t.GetTexto("Comun_tituloConfirmar"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            var nueva = new ConfiguracionHotel_68SA
            {
                Id = _configuracion.Id,
                HoraCheckIn = dtpCheckIn.TimeValue.Value,
                HoraCheckOut = dtpCheckOut.TimeValue.Value,
                GraciaNoShowHoras = nudGracia.Value
            };

            try
            {
                _configBLL.Actualizar(nueva);
                _configuracion = nueva;

                MessageBox.Show(t.GetTexto("Config_msgGuardado"), t.GetTexto("Config_titulo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DatosInvalidosException_68SA ex)
            {
                MessageBox.Show(ex.Message, t.GetTexto("Comun_tituloErrorValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}