using BLL_08YS;
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

namespace GUI
{
    public partial class FormBitacora : Form
    {
        private BitacoraBLL_08YS _bll = BLLFactory_08YS.CreateBitacoraBLL();
        public FormBitacora()
        {
            InitializeComponent();
            comboBoxModulo.DataSource = Enum.GetValues(typeof(Modulo));
            comboBoxEvento.DataSource = Enum.GetValues(typeof(Evento));
            comboBoxCriticidad.DataSource = Enum.GetValues(typeof(Criticidad));

            comboBoxModulo.SelectedIndex = -1;
            comboBoxEvento.SelectedIndex = -1;
            comboBoxCriticidad.SelectedIndex = -1;

            dtpDesde.Checked = false;
            dtpHasta.Checked = false;
        }

        private void FormBitacora_Load(object sender, EventArgs e)
            => CargarGrid();

        private void CargarGrid() 
        {
            try
            {
                dgvEventos.DataSource = null;

                dgvEventos.DataSource = _bll.GetAll();
                dgvEventos.Columns["FechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                BitacoraFiltro_08YS filtro =
                    new BitacoraFiltro_08YS
                    {
                        Username = txtUsername.Text,

                        FechaDesde = dtpDesde.Checked ? (DateTime?)dtpDesde.Value : null,

                        FechaHasta = dtpHasta.Checked ? (DateTime?)dtpHasta.Value : null,

                        Modulo = comboBoxModulo.SelectedItem != null ? (Modulo?)comboBoxModulo.SelectedItem : null,

                        Evento = comboBoxEvento.SelectedItem != null ? (Evento?)comboBoxEvento.SelectedItem : null,

                        Criticidad = comboBoxCriticidad.SelectedItem != null ? (Criticidad?)comboBoxCriticidad.SelectedItem : null
                    };

                dgvEventos.DataSource = null;

                dgvEventos.DataSource = _bll.Filtrar(filtro);
                dgvEventos.Columns["FechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            comboBoxModulo.SelectedIndex = -1;
            comboBoxEvento.SelectedIndex = -1;
            comboBoxCriticidad.SelectedIndex = -1;
            dtpDesde.Checked = false;
            dtpHasta.Checked = false;
            CargarGrid();
        }
    }
}
