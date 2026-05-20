using BLL_08YS;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBitacora_08YS : Form
    {
        private BitacoraBLL_08YS _bll = BLLFactory_08YS.CreateBitacoraBLL();

        // NUEVA VARIABLE: Guarda el progreso de la fila actual entre páginas
        private int _indiceFilaActual = 0;

        public FormBitacora_08YS()
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

        private void dgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEventos.CurrentRow != null)
            {
                BitacoraEvento_08YS seleccionado = dgvEventos.CurrentRow.DataBoundItem as BitacoraEvento_08YS;
                if (seleccionado != null)
                {
                    lblNombre.Text = seleccionado.Login;
                }
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Configuración de Tipografías y Colores
            Font fuenteTitulo = new Font("Arial", 16, FontStyle.Bold);
            Font fuenteInfo = new Font("Arial", 10, FontStyle.Italic);
            Font fuenteEncabezado = new Font("Arial", 10, FontStyle.Bold);
            Font fuenteCuerpo = new Font("Arial", 9, FontStyle.Regular);

            Brush pincelNegro = Brushes.Black;
            Pen lapizGris = new Pen(Color.LightGray, 1);

            // Coordenadas iniciales de dibujo (Márgenes)
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            // 1. Dibujar Título del Reporte
            g.DrawString("REPORTE DE AUDITORÍA Y BITÁCORA", fuenteTitulo, pincelNegro, x + 200, y);
            y += 30;

            // 2. Dibujar Metadatos
            g.DrawString($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fuenteInfo, pincelNegro, x, y);
            g.DrawString("Sistema: GastroGest", fuenteInfo, pincelNegro, x + 600, y);
            y += 40; // Espacio antes de la tabla

            // 3. Definir el ancho fijo de cada columna
            int[] anchos = { 120, 180, 120, 300, 100 };
            string[] encabezados = { "Usuario", "Fecha y Hora", "Módulo", "Evento", "Criticidad" };

            // 4. Dibujar Encabezados de la Tabla
            int xTemporal = x;
            for (int i = 0; i < encabezados.Length; i++)
            {
                g.FillRectangle(Brushes.LightGray, xTemporal, y, anchos[i], 25);
                g.DrawRectangle(Pens.Black, xTemporal, y, anchos[i], 25);
                g.DrawString(encabezados[i], fuenteEncabezado, pincelNegro, xTemporal + 5, y + 5);

                xTemporal += anchos[i];
            }
            y += 25; // Bajamos el cursor al cuerpo de la tabla

            // 5. NUEVO: Recorrer con un bucle WHILE usando el puntero global para soportar múltiples páginas
            while (_indiceFilaActual < dgvEventos.Rows.Count)
            {
                DataGridViewRow fila = dgvEventos.Rows[_indiceFilaActual];

                if (fila.DataBoundItem is BitacoraEvento_08YS ev)
                {
                    xTemporal = x;

                    string[] datosFila = {
                        ev.Login ?? "SISTEMA",
                        ev.FechaHora.ToString("dd/MM/yyyy HH:mm:ss"),
                        ev.Modulo.ToString(),
                        ev.Evento.ToString(),
                        ev.Criticidad.ToString()
                    };

                    // Dibujar cada celda de la fila actual
                    for (int i = 0; i < datosFila.Length; i++)
                    {
                        g.DrawRectangle(lapizGris, xTemporal, y, anchos[i], 22);
                        g.DrawString(datosFila[i], fuenteCuerpo, pincelNegro, xTemporal + 5, y + 4);

                        xTemporal += anchos[i];
                    }

                    y += 22; // Avanzamos a la siguiente fila hacia abajo
                }

                _indiceFilaActual++; // Avanzamos el contador general de registros

                // Control de salto de página
                if (y > e.MarginBounds.Bottom - 40 && _indiceFilaActual < dgvEventos.Rows.Count)
                {
                    e.HasMorePages = true; // Le avisa a Windows que genere otra hoja
                    return; // Sale del método guardando la posición en _indiceFilaActual
                }
            }

            e.HasMorePages = false; // Terminó de dibujar todo el set de datos
        }

        #region Métodos Vacíos Preservados (Para evitar que explote el Diseñador)
        private void lblLogin_Click(object sender, EventArgs e) { }

        private void lblNombre_Click(object sender, EventArgs e) { }

        private void lblEvento_Click(object sender, EventArgs e) { }

        private void lblModulo_Click(object sender, EventArgs e) { }

        private void lblFechaIni_Click(object sender, EventArgs e) { }

        private void lblCriticidad_Click(object sender, EventArgs e) { }

        private void lblFechaFin_Click(object sender, EventArgs e) { }
        #endregion

        private void dgvEventos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEventos.Columns[e.ColumnIndex].Name == "Criticidad")
            {
                if (e.Value != null)
                {
                    string valor = e.Value.ToString();
                    if (valor == "Critico")
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    else if (valor == "Alto")
                    {
                        e.CellStyle.ForeColor = Color.OrangeRed;
                    }
                    else if (valor == "Medio")
                    {
                        e.CellStyle.ForeColor = Color.Gold;
                    }
                    else if (valor == "Bajo")
                    {
                        e.CellStyle.ForeColor = Color.LimeGreen;
                    }
                }
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dgvEventos.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos en la grilla para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // REINICIO CLAVE: Volvemos a cero el contador antes de mandar a imprimir
            _indiceFilaActual = 0;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;

            if (printDialog.ShowDialog() == DialogResult.OK)
                pd.Print();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                BitacoraFiltro_08YS filtro = new BitacoraFiltro_08YS
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

        private void iconButton2_Click(object sender, EventArgs e)
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

