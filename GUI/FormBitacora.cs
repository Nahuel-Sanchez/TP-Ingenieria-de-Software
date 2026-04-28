using BLL_08YS;
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
                dgvEventos.DataSource = null; // Limpiamos
                dgvEventos.DataSource = _bll.GetAll();

                // Opcional: Ajustar el formato de las columnas
                dgvEventos.Columns["FechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
