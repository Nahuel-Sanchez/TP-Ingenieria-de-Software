using BLL_08YS;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using Service_08YS.Entities;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Admin
{
    /// <summary>
    /// Respaldos de la base de datos: crear un .bak en una carpeta validada, restaurar uno elegido
    /// (con doble confirmación) y ver los respaldos que ya hay en la carpeta.
    /// </summary>
    public partial class FormGestionRespaldos_08YS : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private static readonly Dictionary<string, Permisos> _mapaPermisos =
            new Dictionary<string, Permisos>
            {
                { nameof(btnSeleccionarCarpetaBackup),  Permisos.RealizarBackup },
                { nameof(btnRealizarBackUp),            Permisos.RealizarBackup },
                { nameof(btnSeleccionarArchivoRestore), Permisos.RealizarRestore },
                { nameof(btnRealizarRestore),           Permisos.RealizarRestore },
            };

        private static readonly Color ColorExito = Color.FromArgb(76, 217, 100);
        private static readonly Color ColorAdvertencia = Color.FromArgb(240, 170, 60);
        private static readonly Color ColorError = Color.FromArgb(240, 95, 85);
        private static readonly Color TextoSuave = Color.FromArgb(160, 165, 180);

        private readonly BackupBLL_08YS _bll;
        private bool _carpetaValida;

        #endregion

        #region Constructor

        public FormGestionRespaldos_08YS()
        {
            InitializeComponent();

            PermissionFilter_08YS.Aplicar(this, _mapaPermisos);
            _bll = BLLFactory_08YS.CreateBackupBLL();

            CrudEstilo_68SA.AplicarGrilla(dgvRespaldos, 40);
            dgvRespaldos.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) UsarRespaldoSeleccionado(); };
            dgvRespaldos.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { UsarRespaldoSeleccionado(); e.Handled = true; } };

            btnAbrirCarpeta.Click += (s, e) => AbrirCarpeta();
            btnActualizarHistorial.Click += (s, e) => CargarHistorial();
            btnSeleccionarCarpetaBackup.Click += btnSeleccionarCarpetaBackup_Click;
            btnRealizarBackUp.Click += btnRealizarBackUp_Click;
            btnSeleccionarArchivoRestore.Click += btnSeleccionarArchivoRestore_Click;
            btnRealizarRestore.Click += btnRealizarRestore_Click;

            // El MDI suscribe esta pantalla a los cambios de idioma al abrirla
            UpdateIdioma();
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            TraducirControles(this);
            if (IsHandleCreated) CargarHistorial();
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
                    c.Text = T(tag);

                if (c.HasChildren)
                    TraducirControles(c);
            }
        }

        #endregion

        #region Crear respaldo

        private void ActualizarPreviewNombre()
        {
            lblNombreArchivo.Text = _bll.GenerarNombreArchivo();
        }

        private void btnSeleccionarCarpetaBackup_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = T("Backups_backup_seleccionar_carpeta");
                dialog.ShowNewFolderButton = true;
                dialog.SelectedPath = txtCarpetaDestino.Text;

                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                // Se valida al elegirla, antes de que el usuario intente el backup
                ValidarYMostrarCarpeta(dialog.SelectedPath);
            }
        }

        private void ValidarYMostrarCarpeta(string carpeta)
        {
            txtCarpetaDestino.Text = carpeta;
            ActualizarPreviewNombre();
            pnlResultadoBackup.Visible = false;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnRealizarBackUp.Enabled = false;

                var resultado = _bll.ValidarCarpeta(carpeta);
                switch (resultado.Tipo)
                {
                    case ResultadoValidacion_08YS.TipoResultado.Ok:
                        MostrarEstadoCarpeta(IconChar.CircleCheck, ColorExito, T("Backups_backup_carpeta_valida"));
                        _carpetaValida = true;
                        break;

                    case ResultadoValidacion_08YS.TipoResultado.Advertencia:
                        // Puede continuar, pero con riesgo
                        MostrarEstadoCarpeta(IconChar.TriangleExclamation, ColorAdvertencia, resultado.Mensaje);
                        _carpetaValida = true;
                        break;

                    default:
                        MostrarEstadoCarpeta(IconChar.CircleXmark, ColorError, resultado.Mensaje);
                        _carpetaValida = false;
                        break;
                }
            }
            finally
            {
                btnRealizarBackUp.Enabled = _carpetaValida && PuedeRespaldar();
                Cursor = Cursors.Default;
            }

            CargarHistorial();
        }

        private void MostrarEstadoCarpeta(IconChar icono, Color color, string mensaje)
        {
            picEstadoCarpeta.IconChar = icono;
            picEstadoCarpeta.IconColor = color;
            lblEstadoCarpeta.ForeColor = color;
            lblEstadoCarpeta.Text = mensaje;
        }

        private void btnRealizarBackUp_Click(object sender, EventArgs e)
        {
            string carpeta = txtCarpetaDestino.Text.Trim();
            if (string.IsNullOrWhiteSpace(carpeta)) return;

            ActualizarPreviewNombre();
            try
            {
                Cursor = Cursors.WaitCursor;
                btnRealizarBackUp.Enabled = false;

                string rutaCreada = _bll.RealizarBackup(carpeta);

                MostrarResultado(pnlResultadoBackup, picResultadoBackup, lblResultadoBackup, true,
                    $"{T("Backups_backup_ok")} {Path.GetFileName(rutaCreada)}");
                CargarHistorial(Path.GetFileName(rutaCreada));
            }
            catch (InvalidOperationException ex)
            {
                MostrarResultado(pnlResultadoBackup, picResultadoBackup, lblResultadoBackup, false, ex.Message);
            }
            catch (Exception ex)
            {
                MostrarResultado(pnlResultadoBackup, picResultadoBackup, lblResultadoBackup, false,
                    T("Backups_backup_error") + " " + ex.Message);
            }
            finally
            {
                btnRealizarBackUp.Enabled = _carpetaValida && PuedeRespaldar();
                ActualizarPreviewNombre();
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Restaurar respaldo

        private void btnSeleccionarArchivoRestore_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = T("Comun_restore_seleccionar_archivo");
                dialog.Filter = T("Backups_filtroArchivos") + " (*.bak)|*.bak";
                dialog.FilterIndex = 1;
                dialog.InitialDirectory = Directory.Exists(txtCarpetaDestino.Text) ? txtCarpetaDestino.Text : BackupBLL_08YS.CarpetaDefault;

                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                ElegirArchivoRestore(dialog.FileName);
            }
        }

        private void ElegirArchivoRestore(string ruta)
        {
            if (!File.Exists(ruta)) return;

            txtArchivoRestore.Text = ruta;
            btnRealizarRestore.Enabled = PuedeRestaurar();
            pnlResultadoRestore.Visible = false;

            var info = new FileInfo(ruta);
            lblInfoArchivo.Text = string.Format(T("Backups_restore_detalle_archivo"),
                info.Name, Tamano(info.Length), info.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));
            pnlInfoArchivo.Visible = true;
        }

        private void btnRealizarRestore_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RealizarRestore);

            string rutaBak = txtArchivoRestore.Text.Trim();
            if (string.IsNullOrWhiteSpace(rutaBak)) return;

            // Confirmación con "No" por defecto, por la gravedad de la operación
            var confirmacion = MessageBox.Show(this,
                string.Format(T("Backups_restore_confirm_detalle"), Path.GetFileName(rutaBak)),
                T("Backups_restore_confirm_titulo"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnRealizarRestore.Enabled = false;

                _bll.RestaurarBackup(rutaBak);

                MessageBox.Show(this, T("Backups_restore_ok"), T("Backups_backup_restore_titulo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            catch (InvalidOperationException ex)
            {
                btnRealizarRestore.Enabled = PuedeRestaurar();
                MostrarResultado(pnlResultadoRestore, picResultadoRestore, lblResultadoRestore, false, ex.Message);
            }
            catch (Exception ex)
            {
                btnRealizarRestore.Enabled = PuedeRestaurar();
                MostrarResultado(pnlResultadoRestore, picResultadoRestore, lblResultadoRestore, false,
                    T("Backups_restore_error") + " " + ex.Message);
            }
            finally { Cursor = Cursors.Default; }
        }

        #endregion

        #region Respaldos guardados

        private void CargarHistorial(string seleccionar = null)
        {
            string carpeta = txtCarpetaDestino.Text.Trim();
            List<ArchivoRespaldo_08YS> archivos;
            try
            {
                archivos = _bll.ListarRespaldos(carpeta);
            }
            catch (UnauthorizedAccessException)
            {
                archivos = new List<ArchivoRespaldo_08YS>();
            }

            var filas = archivos.Select(a => new FilaRespaldo_08YS
            {
                Fecha = a.Fecha,
                Archivo = a.Nombre,
                Tamano = Tamano(a.Bytes),
                Antiguedad = Antiguedad(a.Fecha),
                Ruta = a.Ruta
            }).ToList();

            dgvRespaldos.DataSource = null;
            dgvRespaldos.DataSource = filas;
            ConfigurarColumnas();

            lblHistorialResumen.Text = filas.Count == 0
                ? string.Format(T("Backups_txtSinRespaldos"), carpeta)
                : string.Format(T("Backups_txtHistorialResumen"), filas.Count, Tamano(archivos.Sum(a => a.Bytes)), carpeta);

            if (seleccionar != null)
            {
                foreach (DataGridViewRow fila in dgvRespaldos.Rows)
                    if (((FilaRespaldo_08YS)fila.DataBoundItem).Archivo == seleccionar)
                    {
                        fila.Selected = true;
                        dgvRespaldos.CurrentCell = fila.Cells[0];
                        break;
                    }
            }
            else
            {
                dgvRespaldos.ClearSelection();
            }
        }

        private void ConfigurarColumnas()
        {
            var col = dgvRespaldos.Columns;
            if (col.Count == 0) return;

            col[nameof(FilaRespaldo_08YS.Fecha)].HeaderText = T("Backups_colFecha");
            col[nameof(FilaRespaldo_08YS.Fecha)].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            col[nameof(FilaRespaldo_08YS.Fecha)].FillWeight = 22;
            col[nameof(FilaRespaldo_08YS.Archivo)].HeaderText = T("Backups_colArchivo");
            col[nameof(FilaRespaldo_08YS.Archivo)].FillWeight = 46;
            col[nameof(FilaRespaldo_08YS.Tamano)].HeaderText = T("Backups_colTamano");
            col[nameof(FilaRespaldo_08YS.Tamano)].FillWeight = 14;
            col[nameof(FilaRespaldo_08YS.Tamano)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col[nameof(FilaRespaldo_08YS.Antiguedad)].HeaderText = T("Backups_colAntiguedad");
            col[nameof(FilaRespaldo_08YS.Antiguedad)].FillWeight = 18;
        }

        private void UsarRespaldoSeleccionado()
        {
            if (!PuedeRestaurar()) return;
            if (dgvRespaldos.CurrentRow?.DataBoundItem is FilaRespaldo_08YS fila)
                ElegirArchivoRestore(fila.Ruta);
        }

        private void AbrirCarpeta()
        {
            string carpeta = txtCarpetaDestino.Text.Trim();
            if (!Directory.Exists(carpeta)) return;
            try
            {
                Process.Start(new ProcessStartInfo("explorer.exe", "\"" + carpeta + "\"") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, T("Backups_btnAbrirCarpeta"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string Antiguedad(DateTime fecha)
        {
            int dias = (DateTime.Today - fecha.Date).Days;
            if (dias <= 0) return T("Backups_txtHoy");
            if (dias == 1) return T("Backups_txtAyer");
            return string.Format(T("Backups_txtHaceDias"), dias);
        }

        private static string Tamano(long bytes)
        {
            double mb = bytes / (1024.0 * 1024);
            return mb >= 1024 ? $"{mb / 1024:0.0} GB" : $"{mb:0.0} MB";
        }

        #endregion

        #region Estilo

        private static bool PuedeRespaldar() => SessionManager_08YS.Instance.HasPermission(Permisos.RealizarBackup);
        private static bool PuedeRestaurar() => SessionManager_08YS.Instance.HasPermission(Permisos.RealizarRestore);

        private static void MostrarResultado(Control panel, IconPictureBox icono, Label etiqueta, bool exito, string mensaje)
        {
            icono.IconChar = exito ? IconChar.CircleCheck : IconChar.CircleXmark;
            icono.IconColor = exito ? ColorExito : ColorError;
            etiqueta.ForeColor = exito ? ColorExito : ColorError;
            etiqueta.Text = mensaje;
            panel.Visible = true;
        }

        /// <summary>Los botones secundarios se iluminan en dorado al pasar el mouse.</summary>
        private void ConfigurarBotones()
        {
            var fondo = Color.FromArgb(5, 15, 45);
            var oscuro = Color.FromArgb(10, 15, 35);
            foreach (var btn in new[] { btnSeleccionarCarpetaBackup, btnSeleccionarArchivoRestore, btnAbrirCarpeta, btnActualizarHistorial })
            {
                btn.MouseEnter += (s, _) =>
                {
                    var b = (IconButton)s;
                    if (!b.Enabled) return;
                    b.BackColor = Color.Goldenrod;
                    b.ForeColor = oscuro;
                    b.IconColor = oscuro;
                };
                btn.MouseLeave += (s, _) =>
                {
                    var b = (IconButton)s;
                    b.BackColor = fondo;
                    b.ForeColor = Color.Goldenrod;
                    b.IconColor = Color.Gold;
                };
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion

        #region Eventos de controles

        private void FormGestionRespaldos_Load(object sender, EventArgs e)
        {
            ConfigurarBotones();
            txtArchivoRestore.Text = string.Empty;
            ValidarYMostrarCarpeta(BackupBLL_08YS.CarpetaDefault);
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        #endregion

        #region Tipos anidados

        /// <summary>Fila de la grilla de respaldos guardados.</summary>
        private class FilaRespaldo_08YS
        {
            public DateTime Fecha { get; set; }
            public string Archivo { get; set; }
            public string Tamano { get; set; }
            public string Antiguedad { get; set; }
            internal string Ruta;
        }

        #endregion
    }
}
