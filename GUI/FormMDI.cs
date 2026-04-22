using GUI.Properties;
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
    public partial class FormMDI : Form
    {
        public event Action CerrarSesion;
        public FormMDI()
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
    }
}
