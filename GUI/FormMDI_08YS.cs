using GUI_08YS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS
{
    public partial class FormMDI_08YS : Form
    {
        public event Action CerrarSesion;
        public FormMDI_08YS()
        {
            InitializeComponent();
        }

        private void FormMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarSesion?.Invoke();
        }

        private void FormMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show($"{Resources.ContinuarCierreSesion} \n\n{Resources.ConfirmarCierreSesion}", Resources.Advertencia, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            //{
            //    e.Cancel = true;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dropdownMenuStrip_08YS1.Show(button1, button1.Width, 0);
        }

        private void FormMDI_Load(object sender, EventArgs e)
        {
            dropdownMenuStrip_08YS1.IsMainMenu = true;
        }
    }
}
