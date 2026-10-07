namespace GUI_08YS.Admin
{
    partial class FormGestionRespaldos_08YS
    {
        /// <summary>Variable del diseñador necesaria.</summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.pnlHistorial = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.dgvRespaldos = new System.Windows.Forms.DataGridView();
            this.pnlHistorialEncabezado = new System.Windows.Forms.Panel();
            this.btnActualizarHistorial = new FontAwesome.Sharp.IconButton();
            this.btnAbrirCarpeta = new FontAwesome.Sharp.IconButton();
            this.lblHistorialResumen = new System.Windows.Forms.Label();
            this.lblHistorialTitulo = new System.Windows.Forms.Label();
            this.tlpTarjetas = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBackup = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.picIconoBackup = new FontAwesome.Sharp.IconPictureBox();
            this.lblBackupTitulo = new System.Windows.Forms.Label();
            this.lblBackupDesc = new System.Windows.Forms.Label();
            this.lblCarpetaEtiqueta = new System.Windows.Forms.Label();
            this.txtCarpetaDestino = new CustomControls.IconPlaceholderTextBox();
            this.btnSeleccionarCarpetaBackup = new FontAwesome.Sharp.IconButton();
            this.lblEstadoCarpeta = new System.Windows.Forms.Label();
            this.picEstadoCarpeta = new FontAwesome.Sharp.IconPictureBox();
            this.pnlNombreArchivo = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblNombreArchivo = new System.Windows.Forms.Label();
            this.lblNombreLabel = new System.Windows.Forms.Label();
            this.btnRealizarBackUp = new FontAwesome.Sharp.IconButton();
            this.pnlResultadoBackup = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblResultadoBackup = new System.Windows.Forms.Label();
            this.picResultadoBackup = new FontAwesome.Sharp.IconPictureBox();
            this.pnlRestore = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.picIconoRestore = new FontAwesome.Sharp.IconPictureBox();
            this.lblRestoreTitulo = new System.Windows.Forms.Label();
            this.lblRestoreDesc = new System.Windows.Forms.Label();
            this.pnlWarning = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblWarning = new System.Windows.Forms.Label();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.lblRestoreLabel = new System.Windows.Forms.Label();
            this.txtArchivoRestore = new CustomControls.IconPlaceholderTextBox();
            this.btnSeleccionarArchivoRestore = new FontAwesome.Sharp.IconButton();
            this.pnlInfoArchivo = new System.Windows.Forms.Panel();
            this.lblInfoArchivo = new System.Windows.Forms.Label();
            this.iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            this.btnRealizarRestore = new FontAwesome.Sharp.IconButton();
            this.pnlResultadoRestore = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblResultadoRestore = new System.Windows.Forms.Label();
            this.picResultadoRestore = new FontAwesome.Sharp.IconPictureBox();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRespaldos)).BeginInit();
            this.pnlHistorialEncabezado.SuspendLayout();
            this.tlpTarjetas.SuspendLayout();
            this.pnlBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconoBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEstadoCarpeta)).BeginInit();
            this.pnlNombreArchivo.SuspendLayout();
            this.pnlResultadoBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResultadoBackup)).BeginInit();
            this.pnlRestore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconoRestore)).BeginInit();
            this.pnlWarning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            this.pnlInfoArchivo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox3)).BeginInit();
            this.pnlResultadoRestore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResultadoRestore)).BeginInit();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHistorial
            // 
            this.pnlHistorial.Controls.Add(this.dgvRespaldos);
            this.pnlHistorial.Controls.Add(this.pnlHistorialEncabezado);
            this.pnlHistorial.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlHistorial.CornerRadius = 14;
            this.pnlHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistorial.Padding = new System.Windows.Forms.Padding(20, 14, 20, 16);
            this.pnlHistorial.Name = "pnlHistorial";
            // 
            // dgvRespaldos
            // 
            this.dgvRespaldos.BackgroundColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.dgvRespaldos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRespaldos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRespaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRespaldos.Name = "dgvRespaldos";
            // 
            // pnlHistorialEncabezado
            // 
            this.pnlHistorialEncabezado.Controls.Add(this.btnActualizarHistorial);
            this.pnlHistorialEncabezado.Controls.Add(this.btnAbrirCarpeta);
            this.pnlHistorialEncabezado.Controls.Add(this.lblHistorialResumen);
            this.pnlHistorialEncabezado.Controls.Add(this.lblHistorialTitulo);
            this.pnlHistorialEncabezado.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlHistorialEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHistorialEncabezado.Size = new System.Drawing.Size(1374, 62);
            this.pnlHistorialEncabezado.Name = "pnlHistorialEncabezado";
            // 
            // btnActualizarHistorial
            // 
            this.btnActualizarHistorial.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnActualizarHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarHistorial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarHistorial.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnActualizarHistorial.IconChar = FontAwesome.Sharp.IconChar.ArrowsRotate;
            this.btnActualizarHistorial.IconColor = System.Drawing.Color.Gold;
            this.btnActualizarHistorial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnActualizarHistorial.IconSize = 22;
            this.btnActualizarHistorial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizarHistorial.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnActualizarHistorial.Size = new System.Drawing.Size(150, 40);
            this.btnActualizarHistorial.Tag = "Backups_btnActualizar";
            this.btnActualizarHistorial.Text = "Actualizar";
            this.btnActualizarHistorial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnActualizarHistorial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnActualizarHistorial.UseVisualStyleBackColor = false;
            this.btnActualizarHistorial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarHistorial.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnActualizarHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnActualizarHistorial.Location = new System.Drawing.Point(1224, 4);
            this.btnActualizarHistorial.Name = "btnActualizarHistorial";
            // 
            // btnAbrirCarpeta
            // 
            this.btnAbrirCarpeta.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnAbrirCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirCarpeta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirCarpeta.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnAbrirCarpeta.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            this.btnAbrirCarpeta.IconColor = System.Drawing.Color.Gold;
            this.btnAbrirCarpeta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAbrirCarpeta.IconSize = 22;
            this.btnAbrirCarpeta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirCarpeta.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnAbrirCarpeta.Size = new System.Drawing.Size(180, 40);
            this.btnAbrirCarpeta.Tag = "Backups_btnAbrirCarpeta";
            this.btnAbrirCarpeta.Text = "Abrir carpeta";
            this.btnAbrirCarpeta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAbrirCarpeta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbrirCarpeta.UseVisualStyleBackColor = false;
            this.btnAbrirCarpeta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirCarpeta.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnAbrirCarpeta.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnAbrirCarpeta.Location = new System.Drawing.Point(1032, 4);
            this.btnAbrirCarpeta.Name = "btnAbrirCarpeta";
            // 
            // lblHistorialResumen
            // 
            this.lblHistorialResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblHistorialResumen.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblHistorialResumen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistorialResumen.Location = new System.Drawing.Point(2, 34);
            this.lblHistorialResumen.Size = new System.Drawing.Size(1000, 22);
            this.lblHistorialResumen.Text = "-";
            this.lblHistorialResumen.Name = "lblHistorialResumen";
            // 
            // lblHistorialTitulo
            // 
            this.lblHistorialTitulo.AutoSize = true;
            this.lblHistorialTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistorialTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblHistorialTitulo.Location = new System.Drawing.Point(0, 4);
            this.lblHistorialTitulo.Tag = "Backups_lblHistorial";
            this.lblHistorialTitulo.Text = "Respaldos guardados";
            this.lblHistorialTitulo.Name = "lblHistorialTitulo";
            // 
            // tlpTarjetas
            // 
            this.tlpTarjetas.Controls.Add(this.pnlBackup, 0, 0);
            this.tlpTarjetas.Controls.Add(this.pnlRestore, 1, 0);
            this.tlpTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTarjetas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTarjetas.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpTarjetas.ColumnCount = 2;
            this.tlpTarjetas.RowCount = 1;
            this.tlpTarjetas.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTarjetas.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.tlpTarjetas.Size = new System.Drawing.Size(1414, 466);
            this.tlpTarjetas.Name = "tlpTarjetas";
            // 
            // pnlBackup
            // 
            this.pnlBackup.Controls.Add(this.picIconoBackup);
            this.pnlBackup.Controls.Add(this.lblBackupTitulo);
            this.pnlBackup.Controls.Add(this.lblBackupDesc);
            this.pnlBackup.Controls.Add(this.lblCarpetaEtiqueta);
            this.pnlBackup.Controls.Add(this.txtCarpetaDestino);
            this.pnlBackup.Controls.Add(this.btnSeleccionarCarpetaBackup);
            this.pnlBackup.Controls.Add(this.lblEstadoCarpeta);
            this.pnlBackup.Controls.Add(this.picEstadoCarpeta);
            this.pnlBackup.Controls.Add(this.pnlNombreArchivo);
            this.pnlBackup.Controls.Add(this.btnRealizarBackUp);
            this.pnlBackup.Controls.Add(this.pnlResultadoBackup);
            this.pnlBackup.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlBackup.CornerRadius = 14;
            this.pnlBackup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackup.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlBackup.Size = new System.Drawing.Size(695, 450);
            this.pnlBackup.Name = "pnlBackup";
            // 
            // picIconoBackup
            // 
            this.picIconoBackup.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.picIconoBackup.ForeColor = System.Drawing.Color.Goldenrod;
            this.picIconoBackup.IconChar = FontAwesome.Sharp.IconChar.Database;
            this.picIconoBackup.IconColor = System.Drawing.Color.Goldenrod;
            this.picIconoBackup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picIconoBackup.IconSize = 36;
            this.picIconoBackup.Location = new System.Drawing.Point(24, 22);
            this.picIconoBackup.Size = new System.Drawing.Size(36, 36);
            this.picIconoBackup.TabStop = false;
            this.picIconoBackup.Name = "picIconoBackup";
            // 
            // lblBackupTitulo
            // 
            this.lblBackupTitulo.AutoSize = true;
            this.lblBackupTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackupTitulo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblBackupTitulo.Location = new System.Drawing.Point(70, 18);
            this.lblBackupTitulo.Tag = "Backups_backup_crear_titulo";
            this.lblBackupTitulo.Text = "Crear respaldo";
            this.lblBackupTitulo.Name = "lblBackupTitulo";
            // 
            // lblBackupDesc
            // 
            this.lblBackupDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblBackupDesc.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblBackupDesc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackupDesc.Location = new System.Drawing.Point(72, 52);
            this.lblBackupDesc.Size = new System.Drawing.Size(599, 40);
            this.lblBackupDesc.Tag = "Backups_backup_crear_desc";
            this.lblBackupDesc.Text = "Seleccioná la carpeta donde se guardará el archivo de backup";
            this.lblBackupDesc.Name = "lblBackupDesc";
            // 
            // lblCarpetaEtiqueta
            // 
            this.lblCarpetaEtiqueta.AutoSize = true;
            this.lblCarpetaEtiqueta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblCarpetaEtiqueta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarpetaEtiqueta.Tag = "Backups_lblCarpeta";
            this.lblCarpetaEtiqueta.Text = "Carpeta de destino";
            this.lblCarpetaEtiqueta.Location = new System.Drawing.Point(26, 100);
            this.lblCarpetaEtiqueta.Name = "lblCarpetaEtiqueta";
            // 
            // txtCarpetaDestino
            // 
            this.txtCarpetaDestino.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.txtCarpetaDestino.BorderColor = System.Drawing.Color.FromArgb(70, 82, 120);
            this.txtCarpetaDestino.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtCarpetaDestino.BorderWidth = 2;
            this.txtCarpetaDestino.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCarpetaDestino.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCarpetaDestino.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            this.txtCarpetaDestino.IconColor = System.Drawing.Color.Goldenrod;
            this.txtCarpetaDestino.IconColorRight = System.Drawing.Color.DimGray;
            this.txtCarpetaDestino.IconPadding = 4;
            this.txtCarpetaDestino.IconSize = 20;
            this.txtCarpetaDestino.MaxLength = 260;
            this.txtCarpetaDestino.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtCarpetaDestino.PlaceholderText = "C:\\HorizonHotel\\Backups";
            this.txtCarpetaDestino.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCarpetaDestino.Size = new System.Drawing.Size(465, 46);
            this.txtCarpetaDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtCarpetaDestino.CornerRadius = 8;
            this.txtCarpetaDestino.Location = new System.Drawing.Point(24, 122);
            this.txtCarpetaDestino.ReadOnly = true;
            this.txtCarpetaDestino.TabStop = false;
            this.txtCarpetaDestino.Name = "txtCarpetaDestino";
            // 
            // btnSeleccionarCarpetaBackup
            // 
            this.btnSeleccionarCarpetaBackup.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnSeleccionarCarpetaBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarCarpetaBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarCarpetaBackup.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnSeleccionarCarpetaBackup.IconChar = FontAwesome.Sharp.IconChar.FolderTree;
            this.btnSeleccionarCarpetaBackup.IconColor = System.Drawing.Color.Gold;
            this.btnSeleccionarCarpetaBackup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSeleccionarCarpetaBackup.IconSize = 22;
            this.btnSeleccionarCarpetaBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeleccionarCarpetaBackup.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSeleccionarCarpetaBackup.Size = new System.Drawing.Size(170, 46);
            this.btnSeleccionarCarpetaBackup.Tag = "Backups_btnCambiarCarpeta";
            this.btnSeleccionarCarpetaBackup.Text = "Cambiar";
            this.btnSeleccionarCarpetaBackup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSeleccionarCarpetaBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSeleccionarCarpetaBackup.UseVisualStyleBackColor = false;
            this.btnSeleccionarCarpetaBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeleccionarCarpetaBackup.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnSeleccionarCarpetaBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnSeleccionarCarpetaBackup.Location = new System.Drawing.Point(501, 122);
            this.btnSeleccionarCarpetaBackup.Name = "btnSeleccionarCarpetaBackup";
            // 
            // lblEstadoCarpeta
            // 
            this.lblEstadoCarpeta.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblEstadoCarpeta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEstadoCarpeta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoCarpeta.Location = new System.Drawing.Point(52, 178);
            this.lblEstadoCarpeta.Size = new System.Drawing.Size(619, 40);
            this.lblEstadoCarpeta.Text = "-";
            this.lblEstadoCarpeta.Name = "lblEstadoCarpeta";
            // 
            // picEstadoCarpeta
            // 
            this.picEstadoCarpeta.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.picEstadoCarpeta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.picEstadoCarpeta.IconChar = FontAwesome.Sharp.IconChar.CircleNotch;
            this.picEstadoCarpeta.IconColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.picEstadoCarpeta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picEstadoCarpeta.IconSize = 20;
            this.picEstadoCarpeta.Location = new System.Drawing.Point(26, 178);
            this.picEstadoCarpeta.Size = new System.Drawing.Size(20, 20);
            this.picEstadoCarpeta.TabStop = false;
            this.picEstadoCarpeta.Name = "picEstadoCarpeta";
            // 
            // pnlNombreArchivo
            // 
            this.pnlNombreArchivo.Controls.Add(this.lblNombreArchivo);
            this.pnlNombreArchivo.Controls.Add(this.lblNombreLabel);
            this.pnlNombreArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlNombreArchivo.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.pnlNombreArchivo.CornerRadius = 10;
            this.pnlNombreArchivo.Location = new System.Drawing.Point(24, 226);
            this.pnlNombreArchivo.Size = new System.Drawing.Size(647, 64);
            this.pnlNombreArchivo.Name = "pnlNombreArchivo";
            // 
            // lblNombreArchivo
            // 
            this.lblNombreArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblNombreArchivo.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreArchivo.ForeColor = System.Drawing.Color.Gold;
            this.lblNombreArchivo.Location = new System.Drawing.Point(16, 30);
            this.lblNombreArchivo.Size = new System.Drawing.Size(615, 24);
            this.lblNombreArchivo.Text = "TP_Ing_Soft_00000000_000000.bak";
            this.lblNombreArchivo.Name = "lblNombreArchivo";
            // 
            // lblNombreLabel
            // 
            this.lblNombreLabel.AutoSize = true;
            this.lblNombreLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblNombreLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreLabel.Location = new System.Drawing.Point(16, 9);
            this.lblNombreLabel.Tag = "Backups_backup_nombre_label";
            this.lblNombreLabel.Text = "El nombre del archivo será:";
            this.lblNombreLabel.Name = "lblNombreLabel";
            // 
            // btnRealizarBackUp
            // 
            this.btnRealizarBackUp.BackColor = System.Drawing.Color.Goldenrod;
            this.btnRealizarBackUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarBackUp.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarBackUp.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnRealizarBackUp.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnRealizarBackUp.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnRealizarBackUp.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRealizarBackUp.IconSize = 22;
            this.btnRealizarBackUp.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRealizarBackUp.Padding = new System.Windows.Forms.Padding(0);
            this.btnRealizarBackUp.Size = new System.Drawing.Size(647, 52);
            this.btnRealizarBackUp.Tag = "Backups_backup_btn_realizar";
            this.btnRealizarBackUp.Text = "Crear respaldo";
            this.btnRealizarBackUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRealizarBackUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRealizarBackUp.UseVisualStyleBackColor = false;
            this.btnRealizarBackUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRealizarBackUp.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnRealizarBackUp.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.btnRealizarBackUp.Location = new System.Drawing.Point(24, 306);
            this.btnRealizarBackUp.FlatAppearance.BorderSize = 0;
            this.btnRealizarBackUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnRealizarBackUp.Name = "btnRealizarBackUp";
            // 
            // pnlResultadoBackup
            // 
            this.pnlResultadoBackup.Controls.Add(this.lblResultadoBackup);
            this.pnlResultadoBackup.Controls.Add(this.picResultadoBackup);
            this.pnlResultadoBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlResultadoBackup.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.pnlResultadoBackup.CornerRadius = 10;
            this.pnlResultadoBackup.Location = new System.Drawing.Point(24, 374);
            this.pnlResultadoBackup.Size = new System.Drawing.Size(647, 56);
            this.pnlResultadoBackup.Visible = false;
            this.pnlResultadoBackup.Name = "pnlResultadoBackup";
            // 
            // lblResultadoBackup
            // 
            this.lblResultadoBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom));
            this.lblResultadoBackup.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblResultadoBackup.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultadoBackup.Location = new System.Drawing.Point(50, 4);
            this.lblResultadoBackup.Size = new System.Drawing.Size(585, 48);
            this.lblResultadoBackup.Text = "-";
            this.lblResultadoBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblResultadoBackup.Name = "lblResultadoBackup";
            // 
            // picResultadoBackup
            // 
            this.picResultadoBackup.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.picResultadoBackup.ForeColor = System.Drawing.Color.LimeGreen;
            this.picResultadoBackup.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.picResultadoBackup.IconColor = System.Drawing.Color.LimeGreen;
            this.picResultadoBackup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picResultadoBackup.IconSize = 26;
            this.picResultadoBackup.Location = new System.Drawing.Point(14, 15);
            this.picResultadoBackup.Size = new System.Drawing.Size(26, 26);
            this.picResultadoBackup.TabStop = false;
            this.picResultadoBackup.Name = "picResultadoBackup";
            // 
            // pnlRestore
            // 
            this.pnlRestore.Controls.Add(this.picIconoRestore);
            this.pnlRestore.Controls.Add(this.lblRestoreTitulo);
            this.pnlRestore.Controls.Add(this.lblRestoreDesc);
            this.pnlRestore.Controls.Add(this.pnlWarning);
            this.pnlRestore.Controls.Add(this.lblRestoreLabel);
            this.pnlRestore.Controls.Add(this.txtArchivoRestore);
            this.pnlRestore.Controls.Add(this.btnSeleccionarArchivoRestore);
            this.pnlRestore.Controls.Add(this.pnlInfoArchivo);
            this.pnlRestore.Controls.Add(this.btnRealizarRestore);
            this.pnlRestore.Controls.Add(this.pnlResultadoRestore);
            this.pnlRestore.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlRestore.CornerRadius = 14;
            this.pnlRestore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRestore.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pnlRestore.Size = new System.Drawing.Size(695, 450);
            this.pnlRestore.Name = "pnlRestore";
            // 
            // picIconoRestore
            // 
            this.picIconoRestore.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.picIconoRestore.ForeColor = System.Drawing.Color.Goldenrod;
            this.picIconoRestore.IconChar = FontAwesome.Sharp.IconChar.ClockRotateLeft;
            this.picIconoRestore.IconColor = System.Drawing.Color.Goldenrod;
            this.picIconoRestore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picIconoRestore.IconSize = 36;
            this.picIconoRestore.Location = new System.Drawing.Point(24, 22);
            this.picIconoRestore.Size = new System.Drawing.Size(36, 36);
            this.picIconoRestore.TabStop = false;
            this.picIconoRestore.Name = "picIconoRestore";
            // 
            // lblRestoreTitulo
            // 
            this.lblRestoreTitulo.AutoSize = true;
            this.lblRestoreTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestoreTitulo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblRestoreTitulo.Location = new System.Drawing.Point(70, 18);
            this.lblRestoreTitulo.Tag = "Backups_backup_restore_titulo";
            this.lblRestoreTitulo.Text = "Restaurar respaldo";
            this.lblRestoreTitulo.Name = "lblRestoreTitulo";
            // 
            // lblRestoreDesc
            // 
            this.lblRestoreDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblRestoreDesc.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblRestoreDesc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestoreDesc.Location = new System.Drawing.Point(72, 52);
            this.lblRestoreDesc.Size = new System.Drawing.Size(599, 40);
            this.lblRestoreDesc.Tag = "Backups_lblRestoreDesc";
            this.lblRestoreDesc.Text = "Volvé la base de datos al estado de un respaldo anterior.";
            this.lblRestoreDesc.Name = "lblRestoreDesc";
            // 
            // pnlWarning
            // 
            this.pnlWarning.Controls.Add(this.lblWarning);
            this.pnlWarning.Controls.Add(this.iconPictureBox2);
            this.pnlWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlWarning.BackColor = System.Drawing.Color.FromArgb(48, 30, 14);
            this.pnlWarning.CornerRadius = 10;
            this.pnlWarning.Location = new System.Drawing.Point(24, 90);
            this.pnlWarning.Size = new System.Drawing.Size(647, 74);
            this.pnlWarning.Name = "pnlWarning";
            // 
            // lblWarning
            // 
            this.lblWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom));
            this.lblWarning.ForeColor = System.Drawing.Color.FromArgb(240, 214, 170);
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(58, 6);
            this.lblWarning.Size = new System.Drawing.Size(577, 62);
            this.lblWarning.Tag = "Backups_backup_warning";
            this.lblWarning.Text = "Restaurar reemplaza toda la base de datos actual por el contenido del respaldo. No se puede deshacer y el sistema se reinicia al terminar.";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWarning.Name = "lblWarning";
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.BackColor = System.Drawing.Color.FromArgb(48, 30, 14);
            this.iconPictureBox2.ForeColor = System.Drawing.Color.FromArgb(240, 170, 60);
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.TriangleExclamation;
            this.iconPictureBox2.IconColor = System.Drawing.Color.FromArgb(240, 170, 60);
            this.iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox2.IconSize = 30;
            this.iconPictureBox2.Location = new System.Drawing.Point(16, 22);
            this.iconPictureBox2.Size = new System.Drawing.Size(30, 30);
            this.iconPictureBox2.TabStop = false;
            this.iconPictureBox2.Name = "iconPictureBox2";
            // 
            // lblRestoreLabel
            // 
            this.lblRestoreLabel.AutoSize = true;
            this.lblRestoreLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblRestoreLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestoreLabel.Tag = "Backups_backup_restore_label";
            this.lblRestoreLabel.Text = "Seleccioná el archivo de backup (.bak)";
            this.lblRestoreLabel.Location = new System.Drawing.Point(26, 180);
            this.lblRestoreLabel.Name = "lblRestoreLabel";
            // 
            // txtArchivoRestore
            // 
            this.txtArchivoRestore.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.txtArchivoRestore.BorderColor = System.Drawing.Color.FromArgb(70, 82, 120);
            this.txtArchivoRestore.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtArchivoRestore.BorderWidth = 2;
            this.txtArchivoRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArchivoRestore.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtArchivoRestore.IconChar = FontAwesome.Sharp.IconChar.FileZipper;
            this.txtArchivoRestore.IconColor = System.Drawing.Color.Goldenrod;
            this.txtArchivoRestore.IconColorRight = System.Drawing.Color.DimGray;
            this.txtArchivoRestore.IconPadding = 4;
            this.txtArchivoRestore.IconSize = 20;
            this.txtArchivoRestore.MaxLength = 260;
            this.txtArchivoRestore.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtArchivoRestore.PlaceholderText = "-";
            this.txtArchivoRestore.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtArchivoRestore.Size = new System.Drawing.Size(465, 46);
            this.txtArchivoRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtArchivoRestore.CornerRadius = 8;
            this.txtArchivoRestore.Location = new System.Drawing.Point(24, 202);
            this.txtArchivoRestore.ReadOnly = true;
            this.txtArchivoRestore.TabStop = false;
            this.txtArchivoRestore.Name = "txtArchivoRestore";
            // 
            // btnSeleccionarArchivoRestore
            // 
            this.btnSeleccionarArchivoRestore.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnSeleccionarArchivoRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarArchivoRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarArchivoRestore.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnSeleccionarArchivoRestore.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnSeleccionarArchivoRestore.IconColor = System.Drawing.Color.Gold;
            this.btnSeleccionarArchivoRestore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSeleccionarArchivoRestore.IconSize = 22;
            this.btnSeleccionarArchivoRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeleccionarArchivoRestore.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSeleccionarArchivoRestore.Size = new System.Drawing.Size(170, 46);
            this.btnSeleccionarArchivoRestore.Tag = "Backups_btnExaminar";
            this.btnSeleccionarArchivoRestore.Text = "Examinar";
            this.btnSeleccionarArchivoRestore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSeleccionarArchivoRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSeleccionarArchivoRestore.UseVisualStyleBackColor = false;
            this.btnSeleccionarArchivoRestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeleccionarArchivoRestore.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnSeleccionarArchivoRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnSeleccionarArchivoRestore.Location = new System.Drawing.Point(501, 202);
            this.btnSeleccionarArchivoRestore.Name = "btnSeleccionarArchivoRestore";
            // 
            // pnlInfoArchivo
            // 
            this.pnlInfoArchivo.Controls.Add(this.lblInfoArchivo);
            this.pnlInfoArchivo.Controls.Add(this.iconPictureBox3);
            this.pnlInfoArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlInfoArchivo.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlInfoArchivo.Location = new System.Drawing.Point(24, 256);
            this.pnlInfoArchivo.Size = new System.Drawing.Size(647, 40);
            this.pnlInfoArchivo.Visible = false;
            this.pnlInfoArchivo.Name = "pnlInfoArchivo";
            // 
            // lblInfoArchivo
            // 
            this.lblInfoArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblInfoArchivo.ForeColor = System.Drawing.Color.FromArgb(200, 205, 220);
            this.lblInfoArchivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoArchivo.Location = new System.Drawing.Point(28, 0);
            this.lblInfoArchivo.Size = new System.Drawing.Size(619, 40);
            this.lblInfoArchivo.Text = "-";
            this.lblInfoArchivo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfoArchivo.Name = "lblInfoArchivo";
            // 
            // iconPictureBox3
            // 
            this.iconPictureBox3.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.iconPictureBox3.ForeColor = System.Drawing.Color.Goldenrod;
            this.iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.iconPictureBox3.IconColor = System.Drawing.Color.Goldenrod;
            this.iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox3.IconSize = 18;
            this.iconPictureBox3.Location = new System.Drawing.Point(2, 11);
            this.iconPictureBox3.Size = new System.Drawing.Size(18, 18);
            this.iconPictureBox3.TabStop = false;
            this.iconPictureBox3.Name = "iconPictureBox3";
            // 
            // btnRealizarRestore
            // 
            this.btnRealizarRestore.BackColor = System.Drawing.Color.FromArgb(60, 18, 18);
            this.btnRealizarRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarRestore.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarRestore.ForeColor = System.Drawing.Color.FromArgb(255, 140, 125);
            this.btnRealizarRestore.IconChar = FontAwesome.Sharp.IconChar.ClockRotateLeft;
            this.btnRealizarRestore.IconColor = System.Drawing.Color.FromArgb(255, 140, 125);
            this.btnRealizarRestore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRealizarRestore.IconSize = 22;
            this.btnRealizarRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRealizarRestore.Padding = new System.Windows.Forms.Padding(0);
            this.btnRealizarRestore.Size = new System.Drawing.Size(647, 52);
            this.btnRealizarRestore.Tag = "Backups_backup_btn_restaurar";
            this.btnRealizarRestore.Text = "Restaurar este respaldo";
            this.btnRealizarRestore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRealizarRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRealizarRestore.UseVisualStyleBackColor = false;
            this.btnRealizarRestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRealizarRestore.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 70, 60);
            this.btnRealizarRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.btnRealizarRestore.Enabled = false;
            this.btnRealizarRestore.Location = new System.Drawing.Point(24, 306);
            this.btnRealizarRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(110, 26, 26);
            this.btnRealizarRestore.Name = "btnRealizarRestore";
            // 
            // pnlResultadoRestore
            // 
            this.pnlResultadoRestore.Controls.Add(this.lblResultadoRestore);
            this.pnlResultadoRestore.Controls.Add(this.picResultadoRestore);
            this.pnlResultadoRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlResultadoRestore.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.pnlResultadoRestore.CornerRadius = 10;
            this.pnlResultadoRestore.Location = new System.Drawing.Point(24, 374);
            this.pnlResultadoRestore.Size = new System.Drawing.Size(647, 56);
            this.pnlResultadoRestore.Visible = false;
            this.pnlResultadoRestore.Name = "pnlResultadoRestore";
            // 
            // lblResultadoRestore
            // 
            this.lblResultadoRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom));
            this.lblResultadoRestore.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblResultadoRestore.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultadoRestore.Location = new System.Drawing.Point(50, 4);
            this.lblResultadoRestore.Size = new System.Drawing.Size(585, 48);
            this.lblResultadoRestore.Text = "-";
            this.lblResultadoRestore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblResultadoRestore.Name = "lblResultadoRestore";
            // 
            // picResultadoRestore
            // 
            this.picResultadoRestore.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.picResultadoRestore.ForeColor = System.Drawing.Color.LimeGreen;
            this.picResultadoRestore.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.picResultadoRestore.IconColor = System.Drawing.Color.LimeGreen;
            this.picResultadoRestore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picResultadoRestore.IconSize = 26;
            this.picResultadoRestore.Location = new System.Drawing.Point(14, 15);
            this.picResultadoRestore.Size = new System.Drawing.Size(26, 26);
            this.picResultadoRestore.TabStop = false;
            this.picResultadoRestore.Name = "picResultadoRestore";
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Size = new System.Drawing.Size(1414, 78);
            this.pnlEncabezado.Name = "pnlEncabezado";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.Tag = "Backups_lblSubtitulo";
            this.lblSubtitulo.Text = "Copias de seguridad de la base de datos del hotel.";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSubtitulo.Name = "lblSubtitulo";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Size = new System.Drawing.Size(1414, 38);
            this.lblTitulo.Tag = "Backups_backup_titulo";
            this.lblTitulo.Text = "Gestión de Respaldos";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Name = "lblTitulo";
            // 
            // FormGestionRespaldos_08YS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1470, 920);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Padding = new System.Windows.Forms.Padding(28, 18, 28, 20);
            this.Text = "Gestión de Respaldos";
            this.Controls.Add(this.pnlHistorial);
            this.Controls.Add(this.tlpTarjetas);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormGestionRespaldos_08YS";
            this.Load += new System.EventHandler(this.FormGestionRespaldos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picResultadoRestore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIconoRestore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResultadoBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEstadoCarpeta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIconoBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRespaldos)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlResultadoRestore.ResumeLayout(false);
            this.pnlInfoArchivo.ResumeLayout(false);
            this.pnlWarning.ResumeLayout(false);
            this.pnlRestore.ResumeLayout(false);
            this.pnlRestore.PerformLayout();
            this.pnlResultadoBackup.ResumeLayout(false);
            this.pnlNombreArchivo.ResumeLayout(false);
            this.pnlNombreArchivo.PerformLayout();
            this.pnlBackup.ResumeLayout(false);
            this.pnlBackup.PerformLayout();
            this.tlpTarjetas.ResumeLayout(false);
            this.tlpTarjetas.PerformLayout();
            this.pnlHistorialEncabezado.ResumeLayout(false);
            this.pnlHistorialEncabezado.PerformLayout();
            this.pnlHistorial.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GUI_08YS.UserControls.RoundedPanel_68SA pnlHistorial;
        private System.Windows.Forms.DataGridView dgvRespaldos;
        private System.Windows.Forms.Panel pnlHistorialEncabezado;
        private FontAwesome.Sharp.IconButton btnActualizarHistorial;
        private FontAwesome.Sharp.IconButton btnAbrirCarpeta;
        private System.Windows.Forms.Label lblHistorialResumen;
        private System.Windows.Forms.Label lblHistorialTitulo;
        private System.Windows.Forms.TableLayoutPanel tlpTarjetas;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlBackup;
        private FontAwesome.Sharp.IconPictureBox picIconoBackup;
        private System.Windows.Forms.Label lblBackupTitulo;
        private System.Windows.Forms.Label lblBackupDesc;
        private System.Windows.Forms.Label lblCarpetaEtiqueta;
        private CustomControls.IconPlaceholderTextBox txtCarpetaDestino;
        private FontAwesome.Sharp.IconButton btnSeleccionarCarpetaBackup;
        private System.Windows.Forms.Label lblEstadoCarpeta;
        private FontAwesome.Sharp.IconPictureBox picEstadoCarpeta;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlNombreArchivo;
        private System.Windows.Forms.Label lblNombreArchivo;
        private System.Windows.Forms.Label lblNombreLabel;
        private FontAwesome.Sharp.IconButton btnRealizarBackUp;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlResultadoBackup;
        private System.Windows.Forms.Label lblResultadoBackup;
        private FontAwesome.Sharp.IconPictureBox picResultadoBackup;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlRestore;
        private FontAwesome.Sharp.IconPictureBox picIconoRestore;
        private System.Windows.Forms.Label lblRestoreTitulo;
        private System.Windows.Forms.Label lblRestoreDesc;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlWarning;
        private System.Windows.Forms.Label lblWarning;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private System.Windows.Forms.Label lblRestoreLabel;
        private CustomControls.IconPlaceholderTextBox txtArchivoRestore;
        private FontAwesome.Sharp.IconButton btnSeleccionarArchivoRestore;
        private System.Windows.Forms.Panel pnlInfoArchivo;
        private System.Windows.Forms.Label lblInfoArchivo;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private FontAwesome.Sharp.IconButton btnRealizarRestore;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlResultadoRestore;
        private System.Windows.Forms.Label lblResultadoRestore;
        private FontAwesome.Sharp.IconPictureBox picResultadoRestore;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
    }
}
