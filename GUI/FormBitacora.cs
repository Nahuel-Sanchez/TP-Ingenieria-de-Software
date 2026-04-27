using BLL;
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
        private BLLBitacora_08YS _bll = new BLLBitacora_08YS();
        public FormBitacora()
        {
            InitializeComponent();
        }

        private void FormBitacora_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla() 
        {
            try
            {
                // Suponiendo que instanciaste tu BLL como _bitacoraBLL
                dgvBitacora.DataSource = null; // Limpiamos
                dgvBitacora.DataSource = _bitacoraBLL.ListarBitacora();

                // Opcional: Ajustar el formato de las columnas
                dgvBitacora.Columns["FechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
