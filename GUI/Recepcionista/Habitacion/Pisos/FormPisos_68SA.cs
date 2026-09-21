using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using CustomControls;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormPisos_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly PisoBLL_68SA _pisoBLL = BLLFactory_08YS.CreatePisoBLL();

        public FormPisos_68SA()
        {
            InitializeComponent();

            CrudEstilo_68SA.AplicarGrilla(dgvPisos);
            dgvPisos.CellClick += DgvPisos_CellClick;

            btnNuevo.Click += (s, e) => AbrirEdicion(null);
            btnFiltrar.Click += (s, e) => CargarLista();
            btnLimpiar.Click += (s, e) =>
            {
                txtNumeroFiltro.Text = string.Empty;
                txtNombreFiltro.Text = string.Empty;
                CargarLista();
            };

            AplicarTextos();

            Shown += (s, e) => CargarLista();
        }

        // ---------- Idioma ----------

        public void UpdateIdioma() => AplicarTextos();

        private void AplicarTextos()
        {
            TraducirControles(this);

            txtNumeroFiltro.PlaceholderText = TraductorManager_08YS.Instance.GetTexto("Pisos_colNumero");
            txtNombreFiltro.PlaceholderText = TraductorManager_08YS.Instance.GetTexto("Pisos_colNombre");

            AplicarEncabezados();
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

        private void AplicarEncabezados()
        {
            var t = TraductorManager_08YS.Instance;
            if (dgvPisos.Columns["Numero"] != null) dgvPisos.Columns["Numero"].HeaderText = t.GetTexto("Pisos_colNumero");
            if (dgvPisos.Columns["Nombre"] != null) dgvPisos.Columns["Nombre"].HeaderText = t.GetTexto("Pisos_colNombre");
            if (dgvPisos.Columns["CantidadHabitaciones"] != null) dgvPisos.Columns["CantidadHabitaciones"].HeaderText = t.GetTexto("Pisos_colHabitaciones");
        }

        // ---------- Carga ----------

        private void CargarLista()
        {
            var filtro = new PisoFiltro_68SA
            {
                Numero = string.IsNullOrWhiteSpace(txtNumeroFiltro.RealText) ? null : txtNumeroFiltro.RealText.Trim(),
                Nombre = string.IsNullOrWhiteSpace(txtNombreFiltro.RealText) ? null : txtNombreFiltro.RealText.Trim()
            };

            dgvPisos.DataSource = null;
            dgvPisos.DataSource = _pisoBLL.GetAll(filtro);
            AplicarColumnas();
        }

        private void AplicarColumnas()
        {
            if (dgvPisos.Columns["PisoId"] != null) dgvPisos.Columns["PisoId"].Visible = false;
            if (dgvPisos.Columns["Numero"] != null) dgvPisos.Columns["Numero"].FillWeight = 25;
            if (dgvPisos.Columns["Nombre"] != null) dgvPisos.Columns["Nombre"].FillWeight = 55;
            if (dgvPisos.Columns["CantidadHabitaciones"] != null) dgvPisos.Columns["CantidadHabitaciones"].FillWeight = 20;

            // Evita duplicar las columnas de acciones en cada refresco
            foreach (var nombre in new[] { "colEditar", "colEliminar" })
                if (dgvPisos.Columns.Contains(nombre))
                    dgvPisos.Columns.Remove(nombre);

            dgvPisos.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono("colEditar"));
            dgvPisos.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono("colEliminar"));

            Image icoEditar = IconCache.Get(IconChar.PenToSquare, IconFont.Auto, 36, Color.Goldenrod);
            Image icoEliminar = IconCache.Get(IconChar.Trash, IconFont.Auto, 34, Color.FromArgb(235, 90, 90));
            Image icoEliminarInactivo = IconCache.Get(IconChar.Trash, IconFont.Auto, 34, Color.FromArgb(110, 115, 130));

            foreach (DataGridViewRow fila in dgvPisos.Rows)
            {
                if (!(fila.DataBoundItem is Piso_68SA piso)) continue;
                fila.Cells["colEditar"].Value = icoEditar;
                fila.Cells["colEliminar"].Value = piso.CantidadHabitaciones == 0 ? icoEliminar : icoEliminarInactivo;
            }

            AplicarEncabezados();
            dgvPisos.Invalidate();
        }

        // ---------- Acciones ----------

        private void DgvPisos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (!(dgvPisos.Rows[e.RowIndex].DataBoundItem is Piso_68SA piso)) return;

            string columna = dgvPisos.Columns[e.ColumnIndex].Name;
            if (columna == "colEditar") AbrirEdicion(piso);
            else if (columna == "colEliminar") EliminarPiso(piso);
        }

        private void AbrirEdicion(Piso_68SA piso)
        {
            using (var popup = new FormEditarPiso_68SA(piso))
            {
                if (popup.ShowDialog(this) == DialogResult.OK)
                    CargarLista();
            }
        }

        private void EliminarPiso(Piso_68SA piso)
        {
            var t = TraductorManager_08YS.Instance;

            if (piso.CantidadHabitaciones > 0)
            {
                MessageBox.Show(t.GetTexto("Pisos_msgConHabitaciones"), t.GetTexto("Pisos_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(string.Format(t.GetTexto("Pisos_msgConfirmarEliminar"), piso.Numero),
                t.GetTexto("Comun_tituloConfirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _pisoBLL.Eliminar(piso.PisoId);
            }
            catch (PisoConHabitacionesException_68SA)
            {
                MessageBox.Show(t.GetTexto("Pisos_msgConHabitaciones"), t.GetTexto("Pisos_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (PisoNoEncontradoException_68SA)
            {
                MessageBox.Show(t.GetTexto("Pisos_msgNoEncontrado"), t.GetTexto("Pisos_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CargarLista();
        }
    }
}