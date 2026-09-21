using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using CustomControls;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormHabitaciones_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly HabitacionBLL_68SA _habitacionBLL = BLLFactory_08YS.CreateHabitacionBLL();
        private readonly PisoBLL_68SA _pisoBLL = BLLFactory_08YS.CreatePisoBLL();
        private readonly TipoHabitacionBLL_68SA _tipoBLL = BLLFactory_08YS.CreateTipoHabitacionBLL();

        // Caches para reconstruir combos y grilla al cambiar de idioma sin volver a consultar la base
        private List<Piso_68SA> _pisos;
        private List<TipoHabitacion> _tipos;
        private List<Habitacion_68SA> _listado;

        public FormHabitaciones_68SA()
        {
            InitializeComponent();

            CrudEstilo_68SA.AplicarGrilla(dgvHabitaciones);
            dgvHabitaciones.CellClick += DgvHabitaciones_CellClick;
            dgvHabitaciones.CellFormatting += DgvHabitaciones_CellFormatting;

            btnNueva.Click += (s, e) => AbrirEdicion(null);
            btnFiltrar.Click += (s, e) => CargarLista();
            btnLimpiar.Click += (s, e) => LimpiarFiltros();

            AplicarTextos();

            // Igual que FormReservas_68SA: la primera carga va en Shown, con el handle creado, para que los íconos de la grilla se pinten
            Shown += (s, e) =>
            {
                _pisos = _pisoBLL.GetAll();
                _tipos = _tipoBLL.GetAll();
                ReconstruirCombos();
                CargarLista();
            };
        }

        // ---------- Idioma ----------

        public void UpdateIdioma() => AplicarTextos();

        private void AplicarTextos()
        {
            TraducirControles(this);

            // Los campos de texto no llevan Tag (el Tag escribiría en .Text): se traduce el placeholder
            txtNumeroFiltro.PlaceholderText = TraductorManager_08YS.Instance.GetTexto("Habitaciones_colNumero");

            ReconstruirCombos();
            AplicarEncabezados();
            if (_listado != null) MostrarListado(); // vuelve a traducir el estado de cada fila, sin consultar la base
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
            PonerEncabezado("NroHabitacion", t.GetTexto("Habitaciones_colNumero"));
            PonerEncabezado("Piso", t.GetTexto("Habitaciones_colPiso"));
            PonerEncabezado("Tipo", t.GetTexto("Habitaciones_colTipo"));
            PonerEncabezado("Capacidad", t.GetTexto("Habitaciones_colCapacidad"));
            PonerEncabezado("TarifaNoche", t.GetTexto("Habitaciones_colTarifa"));
            PonerEncabezado("Estado", t.GetTexto("Habitaciones_colEstado"));
        }

        private void PonerEncabezado(string columna, string texto)
        {
            if (dgvHabitaciones.Columns[columna] != null)
                dgvHabitaciones.Columns[columna].HeaderText = texto;
        }

        // ---------- Combos de filtro ----------

        // Reconstruye los tres combos conservando la selección (por Id). Se usa al cargar los catálogos y al cambiar de idioma.
        private void ReconstruirCombos()
        {
            var t = TraductorManager_08YS.Instance;
            string todos = t.GetTexto("Crud_todos");

            int? pisoSel = ItemCombo_68SA.IdSeleccionado(cmbPisoFiltro);
            int? tipoSel = ItemCombo_68SA.IdSeleccionado(cmbTipoFiltro);
            int? estadoSel = ItemCombo_68SA.IdSeleccionado(cmbEstadoFiltro);

            cmbPisoFiltro.Items.Clear();
            cmbPisoFiltro.Items.Add(new ItemCombo_68SA(null, todos));
            if (_pisos != null)
                foreach (var piso in _pisos)
                    cmbPisoFiltro.Items.Add(new ItemCombo_68SA(piso.PisoId, $"{piso.Numero} - {piso.Nombre}"));

            cmbTipoFiltro.Items.Clear();
            cmbTipoFiltro.Items.Add(new ItemCombo_68SA(null, todos));
            if (_tipos != null)
                foreach (var tipo in _tipos)
                    cmbTipoFiltro.Items.Add(new ItemCombo_68SA(tipo.Id, tipo.Nombre));

            // Mismos estados que los chips de Control de habitaciones (incluye Reservada, que es el estado visual)
            cmbEstadoFiltro.Items.Clear();
            cmbEstadoFiltro.Items.Add(new ItemCombo_68SA(null, todos));
            foreach (EstadoHabitacion estado in Enum.GetValues(typeof(EstadoHabitacion)))
                cmbEstadoFiltro.Items.Add(new ItemCombo_68SA((int)estado, EstiloEstadoHabitacion_68SA.Get(estado).Texto));

            ItemCombo_68SA.Seleccionar(cmbPisoFiltro, pisoSel);
            ItemCombo_68SA.Seleccionar(cmbTipoFiltro, tipoSel);
            ItemCombo_68SA.Seleccionar(cmbEstadoFiltro, estadoSel);
        }

        private void LimpiarFiltros()
        {
            txtNumeroFiltro.Text = string.Empty;
            ItemCombo_68SA.Seleccionar(cmbPisoFiltro, null);
            ItemCombo_68SA.Seleccionar(cmbTipoFiltro, null);
            ItemCombo_68SA.Seleccionar(cmbEstadoFiltro, null);
            CargarLista();
        }

        // ---------- Carga ----------

        private void CargarLista()
        {
            var filtro = new HabitacionFiltro_68SA
            {
                NroHabitacion = string.IsNullOrWhiteSpace(txtNumeroFiltro.RealText) ? null : txtNumeroFiltro.RealText.Trim(),
                PisoId = ItemCombo_68SA.IdSeleccionado(cmbPisoFiltro),
                TipoHabitacionId = ItemCombo_68SA.IdSeleccionado(cmbTipoFiltro),
                Estado = (EstadoHabitacion?)ItemCombo_68SA.IdSeleccionado(cmbEstadoFiltro)
            };

            _listado = _habitacionBLL.GetListado(filtro);
            MostrarListado();
        }

        private void MostrarListado()
        {
            var items = _listado.Select(h =>
            {
                var estilo = EstiloEstadoHabitacion_68SA.Get(h.EstadoVisual);
                return new HabitacionListItem_68SA
                {
                    Origen = h,
                    NroHabitacion = h.NroHabitacion,
                    Piso = $"{h.Piso.Numero} - {h.Piso.Nombre}",
                    Tipo = h.Tipo.Nombre,
                    Capacidad = h.Tipo.Capacidad,
                    TarifaNoche = h.Tipo.TarifaNoche,
                    Estado = estilo.Texto,
                    ColorEstado = estilo.Acento
                };
            }).ToList();

            dgvHabitaciones.DataSource = null;
            dgvHabitaciones.DataSource = items;
            AplicarColumnas();
        }

        private void AplicarColumnas()
        {
            PonerPeso("NroHabitacion", 14);
            PonerPeso("Piso", 22);
            PonerPeso("Tipo", 22);
            PonerPeso("Capacidad", 10);
            PonerPeso("TarifaNoche", 16);
            PonerPeso("Estado", 16);

            if (dgvHabitaciones.Columns["TarifaNoche"] != null)
                dgvHabitaciones.Columns["TarifaNoche"].DefaultCellStyle.Format = "C2";

            // Evita duplicar las columnas de acciones en cada refresco
            foreach (var nombre in new[] { "colEditar", "colEliminar" })
                if (dgvHabitaciones.Columns.Contains(nombre))
                    dgvHabitaciones.Columns.Remove(nombre);

            dgvHabitaciones.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono("colEditar"));
            dgvHabitaciones.Columns.Add(CrudEstilo_68SA.CrearColumnaIcono("colEliminar"));

            Image icoEditar = IconCache.Get(IconChar.PenToSquare, IconFont.Auto, 36, Color.Goldenrod);
            Image icoEliminar = IconCache.Get(IconChar.Trash, IconFont.Auto, 36, Color.FromArgb(235, 90, 90));
            Image icoEliminarInactivo = IconCache.Get(IconChar.Trash, IconFont.Auto, 36, Color.FromArgb(110, 115, 130));

            foreach (DataGridViewRow fila in dgvHabitaciones.Rows)
            {
                if (!(fila.DataBoundItem is HabitacionListItem_68SA item)) continue;
                fila.Cells["colEditar"].Value = icoEditar;
                fila.Cells["colEliminar"].Value = item.Origen.CantidadReservas == 0 ? icoEliminar : icoEliminarInactivo;
            }

            AplicarEncabezados();
            dgvHabitaciones.Invalidate();
        }

        private void PonerPeso(string columna, float peso)
        {
            if (dgvHabitaciones.Columns[columna] != null)
                dgvHabitaciones.Columns[columna].FillWeight = peso;
        }

        // El color sale del item (según el enum), no del texto traducido: no se rompe al cambiar de idioma
        private void DgvHabitaciones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvHabitaciones.Columns[e.ColumnIndex].Name != "Estado") return;

            if (dgvHabitaciones.Rows[e.RowIndex].DataBoundItem is HabitacionListItem_68SA item)
                e.CellStyle.ForeColor = item.ColorEstado;
        }

        // ---------- Acciones ----------

        private void DgvHabitaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (!(dgvHabitaciones.Rows[e.RowIndex].DataBoundItem is HabitacionListItem_68SA item)) return;

            string columna = dgvHabitaciones.Columns[e.ColumnIndex].Name;
            if (columna == "colEditar") AbrirEdicion(item.Origen);
            else if (columna == "colEliminar") EliminarHabitacion(item.Origen);
        }

        private void AbrirEdicion(Habitacion_68SA habitacion)
        {
            using (var popup = new FormEditarHabitacion_68SA(habitacion))
            {
                if (popup.ShowDialog(this) == DialogResult.OK)
                    CargarLista();
            }
        }

        private void EliminarHabitacion(Habitacion_68SA habitacion)
        {
            var t = TraductorManager_08YS.Instance;

            if (habitacion.CantidadReservas > 0)
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgConReservas"), t.GetTexto("Habitaciones_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(string.Format(t.GetTexto("Habitaciones_msgConfirmarEliminar"), habitacion.NroHabitacion),
                t.GetTexto("Comun_tituloConfirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _habitacionBLL.Eliminar(habitacion.Id);
            }
            catch (HabitacionConReservasException_68SA)
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgConReservas"), t.GetTexto("Habitaciones_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HabitacionNoEncontradaException_68SA)
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgNoEncontrada"), t.GetTexto("Habitaciones_tituloNoSePudoEliminar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CargarLista();
        }
    }

    /// <summary>Fila de la grilla: aplana Habitacion_68SA (la DGV no bindea propiedades anidadas).</summary>
    public class HabitacionListItem_68SA
    {
        [Browsable(false)] public Habitacion_68SA Origen { get; set; }
        [Browsable(false)] public Color ColorEstado { get; set; }

        public string NroHabitacion { get; set; }
        public string Piso { get; set; }
        public string Tipo { get; set; }
        public int Capacidad { get; set; }
        public decimal TarifaNoche { get; set; }
        public string Estado { get; set; }
    }

}