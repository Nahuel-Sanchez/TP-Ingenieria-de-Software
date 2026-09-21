using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using CustomControls;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormTiposHabitacion_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly TipoHabitacionBLL_68SA _tipoBLL = BLLFactory_08YS.CreateTipoHabitacionBLL();

        public FormTiposHabitacion_68SA()
        {
            InitializeComponent();

            CrudEstilo_68SA.AplicarGrilla(dgvTipos);
            dgvTipos.CellClick += DgvTipos_CellClick;

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

            // Los campos de texto no llevan Tag (el Tag escribiría en .Text): se traduce el placeholder
            var t = TraductorManager_08YS.Instance;
            txtNombreFiltro.PlaceholderText = t.GetTexto("Tipos_colNombre");
            txtDescripcionFiltro.PlaceholderText = t.GetTexto("Tipos_colDescripcion");
            txtCapacidadFiltro.PlaceholderText = t.GetTexto("Tipos_colCapacidad");
            txtTarifaDesdeFiltro.PlaceholderText = t.GetTexto("Tipos_phTarifaDesde");
            txtTarifaHastaFiltro.PlaceholderText = t.GetTexto("Tipos_phTarifaHasta");

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
            PonerEncabezado("Nombre", t.GetTexto("Tipos_colNombre"));
            PonerEncabezado("Descripcion", t.GetTexto("Tipos_colDescripcion"));
            PonerEncabezado("Capacidad", t.GetTexto("Tipos_colCapacidad"));
            PonerEncabezado("TarifaNoche", t.GetTexto("Tipos_colTarifa"));
            PonerEncabezado("CantidadHabitaciones", t.GetTexto("Tipos_colHabitaciones"));
        }

        private void PonerEncabezado(string columna, string texto)
        {
            if (dgvTipos.Columns[columna] != null)
                dgvTipos.Columns[columna].HeaderText = texto;
        }

        // ---------- Filtros ----------

        private void LimpiarFiltros()
        {
            txtNombreFiltro.Text = string.Empty;
            txtDescripcionFiltro.Text = string.Empty;
            txtCapacidadFiltro.Text = string.Empty;
            txtTarifaDesdeFiltro.Text = string.Empty;
            txtTarifaHastaFiltro.Text = string.Empty;
            CargarLista();
        }

        private bool TryConstruirFiltro(out TipoHabitacionFiltro_68SA filtro)
        {
            var t = TraductorManager_08YS.Instance;
            filtro = new TipoHabitacionFiltro_68SA
            {
                Nombre = string.IsNullOrWhiteSpace(txtNombreFiltro.RealText) ? null : txtNombreFiltro.RealText.Trim(),
                Descripcion = string.IsNullOrWhiteSpace(txtDescripcionFiltro.RealText) ? null : txtDescripcionFiltro.RealText.Trim()
            };

            string capacidad = txtCapacidadFiltro.RealText.Trim();
            if (capacidad.Length > 0)
            {
                if (!int.TryParse(capacidad, out int valorCapacidad))
                {
                    MessageBox.Show(t.GetTexto("Tipos_msgFiltroCapacidadInvalido"), t.GetTexto("Comun_tituloErrorValidacion"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                filtro.Capacidad = valorCapacidad;
            }

            if (!TryLeerTarifa(txtTarifaDesdeFiltro, out decimal? desde) || !TryLeerTarifa(txtTarifaHastaFiltro, out decimal? hasta))
            {
                MessageBox.Show(t.GetTexto("Tipos_msgFiltroTarifaInvalido"), t.GetTexto("Comun_tituloErrorValidacion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            filtro.TarifaDesde = desde;
            filtro.TarifaHasta = hasta;

            return true;
        }

        // Vacío = sin filtro. Acepta "." o "," como decimal, igual que IconNumericUpDown
        private static bool TryLeerTarifa(IconPlaceholderTextBox txt, out decimal? valor)
        {
            valor = null;
            string raw = txt.RealText.Trim();
            if (raw.Length == 0) return true;

            string decSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (decSep == "," && raw.Contains(".") && !raw.Contains(","))
                raw = raw.Replace(".", ",");

            if (!decimal.TryParse(raw, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal parsed))
                return false;

            valor = parsed;
            return true;
        }

        // ---------- Carga ----------

        private void CargarLista()
        {
            if (!TryConstruirFiltro(out var filtro)) return;

            dgvTipos.DataSource = null;
            dgvTipos.DataSource = _tipoBLL.GetAll(filtro);
            AplicarColumnas();
        }

        private void AplicarColumnas()
        {
            if (dgvTipos.Columns["Id"] != null) dgvTipos.Columns["Id"].Visible = false;
            PonerPeso("Nombre", 20);
            PonerPeso("Descripcion", 34);
            PonerPeso("Capacidad", 10);
            PonerPeso("TarifaNoche", 16);
            PonerPeso("CantidadHabitaciones", 14);

            if (dgvTipos.Columns["TarifaNoche"] != null)
                dgvTipos.Columns["TarifaNoche"].DefaultCellStyle.Format = "C2";

            // Evita duplicar las columnas de acciones en cada refresco
            foreach (var nombre in new[] { "colEditar", "colEliminar" })
                if (dgvTipos.Columns.Contains(nombre))
                    dgvTipos.Columns.Remove(nombre);

            dgvTipos.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono("colEditar"));
            dgvTipos.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono("colEliminar"));

            Image icoEditar = IconCache.Get(IconChar.PenToSquare, IconFont.Auto, 36, Color.Goldenrod);
            Image icoEliminar = IconCache.Get(IconChar.Trash, IconFont.Auto, 36, Color.FromArgb(235, 90, 90));
            Image icoEliminarInactivo = IconCache.Get(IconChar.Trash, IconFont.Auto, 36, Color.FromArgb(110, 115, 130));

            foreach (DataGridViewRow fila in dgvTipos.Rows)
            {
                if (!(fila.DataBoundItem is TipoHabitacion tipo)) continue;
                fila.Cells["colEditar"].Value = icoEditar;
                fila.Cells["colEliminar"].Value = tipo.CantidadHabitaciones == 0 ? icoEliminar : icoEliminarInactivo;
            }

            AplicarEncabezados();
            dgvTipos.Invalidate();
        }

        private void PonerPeso(string columna, float peso)
        {
            if (dgvTipos.Columns[columna] != null)
                dgvTipos.Columns[columna].FillWeight = peso;
        }

        // ---------- Acciones ----------

        private void DgvTipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (!(dgvTipos.Rows[e.RowIndex].DataBoundItem is TipoHabitacion tipo)) return;

            string columna = dgvTipos.Columns[e.ColumnIndex].Name;
            if (columna == "colEditar") AbrirEdicion(tipo);
            else if (columna == "colEliminar") EliminarTipo(tipo);
        }

        private void AbrirEdicion(TipoHabitacion tipo)
        {
            using (var popup = new FormEditarTipoHabitacion_68SA(tipo))
            {
                if (popup.ShowDialog(this) == DialogResult.OK)
                    CargarLista();
            }
        }

        private void EliminarTipo(TipoHabitacion tipo)
        {
            var t = TraductorManager_08YS.Instance;

            if (tipo.CantidadHabitaciones > 0)
            {
                MessageBox.Show(t.GetTexto("Tipos_msgConHabitaciones"), t.GetTexto("Tipos_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(string.Format(t.GetTexto("Tipos_msgConfirmarEliminar"), tipo.Nombre),
                t.GetTexto("Comun_tituloConfirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _tipoBLL.Eliminar(tipo.Id);
            }
            catch (TipoHabitacionConHabitacionesException_68SA)
            {
                MessageBox.Show(t.GetTexto("Tipos_msgConHabitaciones"), t.GetTexto("Tipos_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (TipoHabitacionNoEncontradoException_68SA)
            {
                MessageBox.Show(t.GetTexto("Tipos_msgNoEncontrado"), t.GetTexto("Tipos_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CargarLista();
        }
    }
}