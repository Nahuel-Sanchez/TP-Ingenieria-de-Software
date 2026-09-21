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
    public partial class FormHuespedes_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly HuespedBLL_68SA _huespedBLL = BLLFactory_08YS.CreateHuespedBLL();

        public FormHuespedes_68SA()
        {
            InitializeComponent();

            // Fechas opcionales: el control arranca con la tilde marcada pero sin fecha; sin tilde = sin filtro
            foreach (var dtp in new[] { dtpNacDesde, dtpNacHasta })
            {
                dtp.MinDate = new DateTime(1900, 1, 1);
                dtp.MaxDate = DateTime.Today;
                dtp.Checked = false;
            }

            CrudEstilo_68SA.AplicarGrilla(dgvHuespedes);
            dgvHuespedes.CellClick += DgvHuespedes_CellClick;

            btnNuevo.Click += (s, e) => AbrirEdicion(null);
            btnFiltrar.Click += (s, e) => CargarLista();
            btnLimpiar.Click += (s, e) => LimpiarFiltros();

            AplicarTextos();

            // Igual que FormReservas_68SA: la primera carga va en Shown, con el handle creado, para que los íconos de la grilla se pinten
            Shown += (s, e) => CargarLista();
        }

        // ---------- Idioma ----------

        public void UpdateIdioma() => AplicarTextos();

        private void AplicarTextos()
        {
            TraducirControles(this);

            // Los campos de texto no llevan Tag (el Tag escribiría en .Text): se traduce el placeholder.
            // Reutilizo las claves de FormRegistrarHuesped_68SA.
            var t = TraductorManager_08YS.Instance;
            txtNombreFiltro.PlaceholderText = t.GetTexto("RegistrarHuesped_lblNombre");
            txtApellidoFiltro.PlaceholderText = t.GetTexto("RegistrarHuesped_lblApellido");
            txtDocumentoFiltro.PlaceholderText = t.GetTexto("RegistrarHuesped_lblNroDocumento");
            txtNacionalidadFiltro.PlaceholderText = t.GetTexto("RegistrarHuesped_lblNacionalidad");
            txtEmailFiltro.PlaceholderText = t.GetTexto("RegistrarHuesped_lblEmail");
            txtTelefonoFiltro.PlaceholderText = t.GetTexto("RegistrarHuesped_lblTelefono");

            ReconstruirComboTipoDocumento();
            AplicarEncabezados();

            if (lblAvisoLimite.Visible)
                lblAvisoLimite.Text = string.Format(t.GetTexto("Huespedes_msgLimite"), HuespedBLL_68SA.LimiteListado);
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
            PonerEncabezado("Apellido", t.GetTexto("RegistrarHuesped_lblApellido"));
            PonerEncabezado("Nombre", t.GetTexto("RegistrarHuesped_lblNombre"));
            PonerEncabezado("TipoDocumento", t.GetTexto("RegistrarHuesped_lblTipoDocumento"));
            PonerEncabezado("Documento", t.GetTexto("RegistrarHuesped_lblNroDocumento"));
            PonerEncabezado("Nacionalidad", t.GetTexto("RegistrarHuesped_lblNacionalidad"));
            PonerEncabezado("FechaNacimiento", t.GetTexto("RegistrarHuesped_lblFechaNacimiento"));
            PonerEncabezado("Edad", t.GetTexto("Huespedes_colEdad"));
            PonerEncabezado("Telefono", t.GetTexto("RegistrarHuesped_lblTelefono"));
            PonerEncabezado("Email", t.GetTexto("RegistrarHuesped_lblEmail"));
        }

        private void PonerEncabezado(string columna, string texto)
        {
            if (dgvHuespedes.Columns[columna] != null)
                dgvHuespedes.Columns[columna].HeaderText = texto;
        }

        // ---------- Filtros ----------

        // Solo cambia el texto de "Todos" al traducir; los tipos de documento se muestran como en FormRegistrarHuesped_68SA (nombre del enum)
        private void ReconstruirComboTipoDocumento()
        {
            int? seleccion = ItemCombo_68SA.IdSeleccionado(cmbTipoDocFiltro);

            cmbTipoDocFiltro.Items.Clear();
            cmbTipoDocFiltro.Items.Add(new ItemCombo_68SA(null, TraductorManager_08YS.Instance.GetTexto("Crud_todos")));
            foreach (TipoDocumento tipo in Enum.GetValues(typeof(TipoDocumento)))
                cmbTipoDocFiltro.Items.Add(new ItemCombo_68SA((int)tipo, tipo.ToString()));

            ItemCombo_68SA.Seleccionar(cmbTipoDocFiltro, seleccion);
        }

        private void LimpiarFiltros()
        {
            foreach (var txt in new[] { txtNombreFiltro, txtApellidoFiltro, txtDocumentoFiltro, txtNacionalidadFiltro, txtEmailFiltro, txtTelefonoFiltro })
                txt.Text = string.Empty;

            ItemCombo_68SA.Seleccionar(cmbTipoDocFiltro, null);
            dtpNacDesde.Checked = false;
            dtpNacHasta.Checked = false;

            CargarLista();
        }

        private static string Texto(IconPlaceholderTextBox txt) =>
            string.IsNullOrWhiteSpace(txt.RealText) ? null : txt.RealText.Trim();

        // ---------- Carga ----------

        private void CargarLista()
        {
            var filtro = new HuespedFiltro_68SA
            {
                Nombre = Texto(txtNombreFiltro),
                Apellido = Texto(txtApellidoFiltro),
                Documento = Texto(txtDocumentoFiltro),
                Nacionalidad = Texto(txtNacionalidadFiltro),
                Email = Texto(txtEmailFiltro),
                Telefono = Texto(txtTelefonoFiltro),
                TipoDocumento = (TipoDocumento?)ItemCombo_68SA.IdSeleccionado(cmbTipoDocFiltro),
                NacimientoDesde = dtpNacDesde.Value?.Date, // null si la tilde está sin marcar
                NacimientoHasta = dtpNacHasta.Value?.Date
            };

            var lista = _huespedBLL.GetListado(filtro);

            dgvHuespedes.DataSource = null;
            dgvHuespedes.DataSource = lista;
            AplicarColumnas();

            // Si se llegó al tope, se avisa que hay más resultados y que conviene acotar con filtros
            lblAvisoLimite.Visible = lista.Count >= HuespedBLL_68SA.LimiteListado;
            if (lblAvisoLimite.Visible)
                lblAvisoLimite.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("Huespedes_msgLimite"), HuespedBLL_68SA.LimiteListado);
        }

        private void AplicarColumnas()
        {
            if (dgvHuespedes.Columns["Id"] != null) dgvHuespedes.Columns["Id"].Visible = false;
            if (dgvHuespedes.Columns["CantidadReservas"] != null) dgvHuespedes.Columns["CantidadReservas"].Visible = false;

            // Orden y anchos: Apellido primero, como se ordena el listado
            var columnas = new (string Nombre, float Peso)[]
            {
                ("Apellido", 13), ("Nombre", 13), ("TipoDocumento", 9), ("Documento", 11), ("Nacionalidad", 12),
                ("FechaNacimiento", 11), ("Edad", 5), ("Telefono", 11), ("Email", 15)
            };
            for (int i = 0; i < columnas.Length; i++)
            {
                var col = dgvHuespedes.Columns[columnas[i].Nombre];
                if (col == null) continue;
                col.DisplayIndex = i;
                col.FillWeight = columnas[i].Peso;
            }

            if (dgvHuespedes.Columns["FechaNacimiento"] != null)
                dgvHuespedes.Columns["FechaNacimiento"].DefaultCellStyle.Format = "d";

            // Evita duplicar las columnas de acciones en cada refresco
            foreach (var nombre in new[] { "colEditar", "colEliminar" })
                if (dgvHuespedes.Columns.Contains(nombre))
                    dgvHuespedes.Columns.Remove(nombre);

            dgvHuespedes.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono("colEditar"));
            dgvHuespedes.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono("colEliminar"));

            Image icoEditar = IconCache.Get(IconChar.PenToSquare, IconFont.Auto, 36, Color.Goldenrod);
            Image icoEliminar = IconCache.Get(IconChar.Trash, IconFont.Auto, 36, Color.FromArgb(235, 90, 90));
            Image icoEliminarInactivo = IconCache.Get(IconChar.Trash, IconFont.Auto, 36, Color.FromArgb(110, 115, 130));

            foreach (DataGridViewRow fila in dgvHuespedes.Rows)
            {
                if (!(fila.DataBoundItem is Huesped_68SA huesped)) continue;
                fila.Cells["colEditar"].Value = icoEditar;
                fila.Cells["colEliminar"].Value = huesped.CantidadReservas == 0 ? icoEliminar : icoEliminarInactivo;
            }

            AplicarEncabezados();
            dgvHuespedes.Invalidate();
        }

        // ---------- Acciones ----------

        private void DgvHuespedes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (!(dgvHuespedes.Rows[e.RowIndex].DataBoundItem is Huesped_68SA huesped)) return;

            string columna = dgvHuespedes.Columns[e.ColumnIndex].Name;
            if (columna == "colEditar") AbrirEdicion(huesped);
            else if (columna == "colEliminar") EliminarHuesped(huesped);
        }

        // Mismo popup que usan check-in y reserva: alta si huesped == null, edición si viene uno
        private void AbrirEdicion(Huesped_68SA huesped)
        {
            using (var popup = new FormRegistrarHuesped_68SA(null, huesped))
            {
                if (popup.ShowDialog(this) == DialogResult.OK)
                    CargarLista();
            }
        }

        private void EliminarHuesped(Huesped_68SA huesped)
        {
            var t = TraductorManager_08YS.Instance;

            if (huesped.CantidadReservas > 0)
            {
                MessageBox.Show(t.GetTexto("Huespedes_msgConReservas"), t.GetTexto("Huespedes_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                string.Format(t.GetTexto("Huespedes_msgConfirmarEliminar"), $"{huesped.Apellido}, {huesped.Nombre}"),
                t.GetTexto("Comun_tituloConfirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _huespedBLL.Eliminar(huesped.Id);
            }
            catch (HuespedConReservasException_68SA)
            {
                MessageBox.Show(t.GetTexto("Huespedes_msgConReservas"), t.GetTexto("Huespedes_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HuespedNoEncontradoException_68SA)
            {
                MessageBox.Show(t.GetTexto("Huespedes_msgNoEncontrado"), t.GetTexto("Huespedes_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CargarLista();
        }
    }
}