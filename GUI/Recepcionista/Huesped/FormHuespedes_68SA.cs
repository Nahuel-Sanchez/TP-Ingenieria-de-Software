using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using CustomControls;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GUI_08YS.Recepcionista
{
    public partial class FormHuespedes_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos y tipos auxiliares

        // Se identifica por id (no por el texto del combo) para que no se rompa al cambiar de idioma
        private enum OperadorEdad { Cualquiera = 0, MayorA = 1, MenorA = 2, Entre = 3 }

        // Columnas de datos en el orden en que se muestran (Apellido primero, como se ordena el listado)
        private static readonly string[] ColumnasDatos =
        {
            "Apellido", "Nombre", "TipoDocumento", "Documento", "Nacionalidad",
            "FechaNacimiento", "Edad", "Telefono", "Email"
        };

        private readonly HuespedBLL_68SA _huespedBLL = BLLFactory_08YS.CreateHuespedBLL();

        // Lo que está actualmente en la grilla: viene de la base (CargarLista) o de un XML deserializado.
        // Es lo único que lee Serializar; en ningún caso se escribe en la base de datos.
        private List<Huesped_68SA> _listadoActual;

        // Último ancho para el que se repartió el espacio de los filtros (evita recalcular sin necesidad)
        private int _anchoFiltrosAplicado = -1;

        #endregion

        public FormHuespedes_68SA()
        {
            InitializeComponent();

            CrudEstilo_68SA.AplicarGrilla(dgvHuespedes);
            // Encabezados en una sola línea: así el ancho que se mide para cada columna es el del texto completo
            dgvHuespedes.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvHuespedes.CellClick += DgvHuespedes_CellClick;

            flpFiltros.SizeChanged += (s, e) => DistribuirFiltros();
            cmbOperadorEdad.SelectedIndexChanged += (s, e) => ActualizarVisibilidadEdad();

            btnNuevo.Click += (s, e) => AbrirEdicion(null);
            btnFiltrar.Click += (s, e) => CargarLista();
            btnLimpiar.Click += (s, e) => LimpiarFiltros();
            btnSerializar.Click += (s, e) => Serializar();
            btnDeserializar.Click += (s, e) => Deserializar();

            AplicarTextos();
            ActualizarVisibilidadEdad();

            // Igual que FormReservas_68SA: la primera carga va en Shown, con el handle creado, para que los íconos de la grilla se pinten
            Shown += (s, e) =>
            {
                DistribuirFiltros();
                CargarLista();
            };
        }


        #region Idioma

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

            toolTipCrud.SetToolTip(btnLimpiar, t.GetTexto("Huespedes_ayudaLimpiar"));
            toolTipCrud.SetToolTip(btnSerializar, t.GetTexto("Huespedes_ayudaSerializar"));
            toolTipCrud.SetToolTip(btnDeserializar, t.GetTexto("Huespedes_ayudaDeserializar"));

            ReconstruirComboTipoDocumento();
            ReconstruirComboOperadorEdad();

            AplicarEncabezados();
            AjustarAnchoColumnas(); // los encabezados traducidos pueden ser más largos o más cortos

            if (lblAvisoLimite.Visible)
                lblAvisoLimite.Text = string.Format(t.GetTexto("Huespedes_msgLimite"), HuespedBLL_68SA.LimiteListado);

            if (lblOrigenXml.Visible)
                lblOrigenXml.Text = t.GetTexto("Huespedes_avisoOrigenXml");
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

        #endregion

        #region Filtros

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

        private void ReconstruirComboOperadorEdad()
        {
            var t = TraductorManager_08YS.Instance;
            int? seleccion = ItemCombo_68SA.IdSeleccionado(cmbOperadorEdad);

            cmbOperadorEdad.Items.Clear();
            cmbOperadorEdad.Items.Add(new ItemCombo_68SA((int)OperadorEdad.Cualquiera, t.GetTexto("Huespedes_edadCualquiera")));
            cmbOperadorEdad.Items.Add(new ItemCombo_68SA((int)OperadorEdad.MayorA, t.GetTexto("Huespedes_edadMayorA")));
            cmbOperadorEdad.Items.Add(new ItemCombo_68SA((int)OperadorEdad.MenorA, t.GetTexto("Huespedes_edadMenorA")));
            cmbOperadorEdad.Items.Add(new ItemCombo_68SA((int)OperadorEdad.Entre, t.GetTexto("Huespedes_edadEntre")));

            // Sin selección previa queda en el primero (Cualquiera)
            ItemCombo_68SA.Seleccionar(cmbOperadorEdad, seleccion);
            ActualizarVisibilidadEdad();
        }

        // Igual que el costo en FormReservas_68SA: según el operador se muestran uno o dos valores.
        // Las columnas del TableLayoutPanel son AutoSize, así que las que se ocultan colapsan solas y el combo usa el espacio.
        private void ActualizarVisibilidadEdad()
        {
            var operador = OperadorEdadSeleccionado();
            nudEdadDesde.Visible = operador != OperadorEdad.Cualquiera;
            lblGuionEdad.Visible = operador == OperadorEdad.Entre;
            nudEdadHasta.Visible = operador == OperadorEdad.Entre;
        }

        private OperadorEdad OperadorEdadSeleccionado() =>
            (OperadorEdad)(ItemCombo_68SA.IdSeleccionado(cmbOperadorEdad) ?? (int)OperadorEdad.Cualquiera);

        private void LimpiarFiltros()
        {
            foreach (var txt in new[] { txtNombreFiltro, txtApellidoFiltro, txtDocumentoFiltro, txtNacionalidadFiltro, txtEmailFiltro, txtTelefonoFiltro })
                txt.Text = string.Empty;

            ItemCombo_68SA.Seleccionar(cmbTipoDocFiltro, null);

            ItemCombo_68SA.Seleccionar(cmbOperadorEdad, (int)OperadorEdad.Cualquiera);
            nudEdadDesde.Value = 0;
            nudEdadHasta.Value = 0;
            ActualizarVisibilidadEdad();

            CargarLista();
        }

        private static string Texto(IconPlaceholderTextBox txt) =>
            string.IsNullOrWhiteSpace(txt.RealText) ? null : txt.RealText.Trim();

        private HuespedFiltro_68SA ConstruirFiltro()
        {
            // Misma lógica que el costo en FormReservas_68SA: "Menor a" usa el primer valor
            int? edadDesde = null, edadHasta = null;
            switch (OperadorEdadSeleccionado())
            {
                case OperadorEdad.MayorA:
                    edadDesde = (int)nudEdadDesde.Value;
                    break;
                case OperadorEdad.MenorA:
                    edadHasta = (int)nudEdadDesde.Value;
                    break;
                case OperadorEdad.Entre:
                    edadDesde = (int)nudEdadDesde.Value;
                    edadHasta = (int)nudEdadHasta.Value;
                    break;
            }

            return new HuespedFiltro_68SA
            {
                Nombre = Texto(txtNombreFiltro),
                Apellido = Texto(txtApellidoFiltro),
                Documento = Texto(txtDocumentoFiltro),
                Nacionalidad = Texto(txtNacionalidadFiltro),
                Email = Texto(txtEmailFiltro),
                Telefono = Texto(txtTelefonoFiltro),
                TipoDocumento = (TipoDocumento?)ItemCombo_68SA.IdSeleccionado(cmbTipoDocFiltro),
                EdadDesde = edadDesde,
                EdadHasta = edadHasta
            };
        }

        #endregion

        #region FrontEnd

        // FlowLayoutPanel solo sabe hacer wrap: si el siguiente filtro no entra, pasa a la línea de abajo y deja
        // espacio vacío al final de la fila. Acá se agrupan los filtros en filas con la misma regla de wrap
        // (MinimumSize + Margin) y el sobrante de cada fila se reparte entre sus filtros.
        private void DistribuirFiltros()
        {
            int disponible = flpFiltros.ClientSize.Width - flpFiltros.Padding.Horizontal;
            if (disponible <= 0 || disponible == _anchoFiltrosAplicado) return;
            _anchoFiltrosAplicado = disponible;

            var filtros = flpFiltros.Controls.Cast<Control>().Where(c => c.Visible).ToList();
            var fila = new List<Control>();
            int usado = 0;

            flpFiltros.SuspendLayout();

            foreach (var filtro in filtros)
            {
                int necesita = filtro.MinimumSize.Width + filtro.Margin.Horizontal;

                if (fila.Count > 0 && usado + necesita > disponible)
                {
                    EstirarFila(fila, usado, disponible);
                    fila.Clear();
                    usado = 0;
                }

                fila.Add(filtro);
                usado += necesita;
            }

            EstirarFila(fila, usado, disponible); // última fila (también se estira)

            flpFiltros.ResumeLayout(true);
        }

        private static void EstirarFila(List<Control> fila, int usado, int disponible)
        {
            if (fila.Count == 0) return;

            int sobrante = Math.Max(0, disponible - usado);
            int porFiltro = sobrante / fila.Count;
            int resto = sobrante % fila.Count; // los píxeles que sobran de la división van de a uno a los primeros

            for (int i = 0; i < fila.Count; i++)
                fila[i].Width = fila[i].MinimumSize.Width + porFiltro + (i < resto ? 1 : 0);
        }

        #endregion

        #region Carga de datos

        private void CargarLista()
        {
            var lista = _huespedBLL.GetListado(ConstruirFiltro());
            MostrarEnGrilla(lista, esSnapshotXml: false);

            // Si se llegó al tope, se avisa que hay más resultados y que conviene acotar con filtros
            lblAvisoLimite.Visible = lista.Count >= HuespedBLL_68SA.LimiteListado;
            if (lblAvisoLimite.Visible)
                lblAvisoLimite.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("Huespedes_msgLimite"), HuespedBLL_68SA.LimiteListado);
        }

        // Deja en pantalla la lista dada, venga de la base (CargarLista) o de un XML deserializado.
        // Nunca escribe en la base de datos.
        private void MostrarEnGrilla(List<Huesped_68SA> lista, bool esSnapshotXml)
        {
            _listadoActual = lista ?? new List<Huesped_68SA>();

            dgvHuespedes.DataSource = null;
            dgvHuespedes.DataSource = _listadoActual;
            AplicarColumnas();

            lblOrigenXml.Visible = esSnapshotXml;
            if (esSnapshotXml)
            {
                lblAvisoLimite.Visible = false; // el aviso de tope de filas no aplica a un archivo XML
                lblOrigenXml.Text = TraductorManager_08YS.Instance.GetTexto("Huespedes_avisoOrigenXml");
            }
        }

        #endregion

        #region Grid

        private void AplicarColumnas()
        {
            if (dgvHuespedes.Columns["Id"] != null) dgvHuespedes.Columns["Id"].Visible = false;
            if (dgvHuespedes.Columns["CantidadReservas"] != null) dgvHuespedes.Columns["CantidadReservas"].Visible = false;

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

            AplicarEncabezados();  // primero el texto traducido, para que se mida el encabezado real
            AjustarAnchoColumnas();

            dgvHuespedes.Invalidate();
        }

        // Cada columna se mide contra su contenido (encabezado incluido) y ese ancho pasa a ser su mínimo y su peso:
        //  - si sobra lugar, la grilla ocupa todo el ancho y las columnas crecen en proporción (modo Fill, como antes);
        //  - si no alcanza, ninguna se achica por debajo de lo que necesita: no se corta texto y aparece el scroll.
        // Solo afecta a esta grilla: no se toca el helper compartido CrudEstilo_68SA.AplicarGrilla.
        private void AjustarAnchoColumnas()
        {
            for (int i = 0; i < ColumnasDatos.Length; i++)
            {
                var col = dgvHuespedes.Columns[ColumnasDatos[i]];
                if (col == null) continue;

                col.DisplayIndex = i;

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.MinimumWidth = 20; // se libera el mínimo anterior para poder medir de nuevo (idioma o datos distintos)
                dgvHuespedes.AutoResizeColumn(col.Index, DataGridViewAutoSizeColumnMode.AllCells);

                int ancho = col.Width;
                col.MinimumWidth = ancho;
                col.FillWeight = ancho;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        #endregion

        #region Serialización XML

        private void Serializar()
        {
            var t = TraductorManager_08YS.Instance;

            if (_listadoActual == null || _listadoActual.Count == 0)
            {
                MessageBox.Show(t.GetTexto("Huespedes_msgNadaParaSerializar"), t.GetTexto("Comun_tituloErrorValidacion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialogo = new SaveFileDialog
            {
                Filter = t.GetTexto("Huespedes_filtroXml"),
                DefaultExt = "xml",
                AddExtension = true,
                FileName = "Huespedes.xml",
                Title = t.GetTexto("Huespedes_tituloGuardarXml")
            })
            {
                // El propio diálogo cubre "elegir ubicación" + "elegir nombre" en un solo paso
                if (dialogo.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    var serializador = new XmlSerializer(typeof(List<Huesped_68SA>));
                    using (var writer = new StreamWriter(dialogo.FileName, false, Encoding.UTF8))
                    {
                        serializador.Serialize(writer, _listadoActual);
                    }

                    MessageBox.Show(t.GetTexto("Huespedes_msgSerializado"), t.GetTexto("Huespedes_titulo"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(t.GetTexto("Huespedes_msgErrorSerializar"), ex.Message),
                        t.GetTexto("Comun_tituloErrorValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Deserializar()
        {
            var t = TraductorManager_08YS.Instance;

            using (var dialogo = new OpenFileDialog
            {
                Filter = t.GetTexto("Huespedes_filtroXml"),
                Title = t.GetTexto("Huespedes_tituloAbrirXml")
            })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    var serializador = new XmlSerializer(typeof(List<Huesped_68SA>));
                    List<Huesped_68SA> lista;
                    using (var reader = new StreamReader(dialogo.FileName, Encoding.UTF8))
                    {
                        lista = (List<Huesped_68SA>)serializador.Deserialize(reader);
                    }

                    // Se sube a la misma grilla del Maestro de Huéspedes; no toca la base de datos
                    MostrarEnGrilla(lista, esSnapshotXml: true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(t.GetTexto("Huespedes_msgErrorDeserializar"), ex.Message),
                        t.GetTexto("Comun_tituloErrorValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region CRUD

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

        #endregion
    }
}