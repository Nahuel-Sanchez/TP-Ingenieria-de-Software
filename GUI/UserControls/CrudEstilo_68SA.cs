using CustomControls;
using FontAwesome.Sharp;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>Estilo común de los CRUDs: mismos colores y medidas que la grilla de FormReservas_68SA y los popups existentes.</summary>
    public static class CrudEstilo_68SA
    {
        public static void AplicarGrilla(DataGridView dgv, int alturaFila = 44)
        {
            dgv.AutoGenerateColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.FromArgb(5, 10, 40);
            dgv.GridColor = Color.FromArgb(35, 48, 85);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowTemplate.Height = alturaFila;
            dgv.ColumnHeadersHeight = 36;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 18, 50);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Goldenrod;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(10, 18, 50);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(10, 18, 50);
            dgv.DefaultCellStyle.ForeColor = Color.WhiteSmoke;
            dgv.DefaultCellStyle.SelectionBackColor = Color.Goldenrod;
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 15, 35);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(13, 22, 58);
        }

        public static DataGridViewImageColumn CrearColumnaIcono(string nombre)
        {
            var columna = new DataGridViewImageColumn
            {
                Name = nombre,
                HeaderText = "",
                Width = 42,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            columna.DefaultCellStyle.BackColor = Color.FromArgb(10, 18, 50);
            columna.DefaultCellStyle.SelectionBackColor = Color.FromArgb(15, 25, 55);
            columna.DefaultCellStyle.NullValue = null;
            return columna;
        }

        public static void EstiloBoton(IconButton btn, IconChar icono, bool principal, int ancho, int alto = 49)
        {
            Color frente = principal ? Color.FromArgb(10, 15, 35) : Color.Goldenrod;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = principal ? Color.Goldenrod : Color.FromArgb(5, 15, 45);
            btn.ForeColor = frente;
            btn.IconChar = icono;
            btn.IconColor = frente;
            btn.IconSize = 22;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.UseVisualStyleBackColor = false;
            btn.Size = new Size(ancho, alto);
        }

        public static void EstiloCampo(IconPlaceholderTextBox txt, IconChar icono, int maxLength)
        {
            txt.BackColor = Color.FromArgb(5, 15, 45);
            txt.BorderColor = Color.Goldenrod;
            txt.BorderFocusColor = Color.Goldenrod;
            txt.BorderWidth = 2;
            txt.Font = new Font("Segoe UI", 10F);
            txt.ForeColor = SystemColors.ControlLightLight;
            txt.IconChar = icono;
            txt.IconColor = Color.Goldenrod;
            txt.IconColorRight = Color.DimGray;
            txt.IconPadding = 4;
            txt.IconSize = 20;
            txt.MaxLength = maxLength;
            txt.PlaceholderColor = Color.LightGray;
            txt.ScrollBars = ScrollBars.None;
        }
    }
}