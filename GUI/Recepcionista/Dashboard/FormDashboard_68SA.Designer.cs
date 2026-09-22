using GUI_08YS.Recepcionista.Dashboard;

namespace GUI_08YS.Recepcionista
{
    partial class FormDashboard_68SA
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblEncabezado = new System.Windows.Forms.Label();
            this.iconEncabezado = new FontAwesome.Sharp.IconPictureBox();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnTabHuespedes = new FontAwesome.Sharp.IconButton();
            this.btnTabOcupacion = new FontAwesome.Sharp.IconButton();
            this.btnTabGeneral = new FontAwesome.Sharp.IconButton();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.flpHuespedes = new System.Windows.Forms.FlowLayoutPanel();
            this.flpOcupacion = new System.Windows.Forms.FlowLayoutPanel();
            this.flpGeneral = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCardsOcupacion = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardDisponibles = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconDisponibles = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloDisponibles = new System.Windows.Forms.Label();
            this.lblValorDisponibles = new System.Windows.Forms.Label();
            this.lblSubDisponibles = new System.Windows.Forms.Label();
            this.pnlCardOcupadas = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconOcupadasDash = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloOcupadasDash = new System.Windows.Forms.Label();
            this.lblValorOcupadasDash = new System.Windows.Forms.Label();
            this.lblSubOcupadasDash = new System.Windows.Forms.Label();
            this.pnlCardLimpieza = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconLimpieza = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloLimpieza = new System.Windows.Forms.Label();
            this.lblValorLimpieza = new System.Windows.Forms.Label();
            this.lblSubLimpieza = new System.Windows.Forms.Label();
            this.pnlCardMantenimiento = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconMantenimiento = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloMantenimiento = new System.Windows.Forms.Label();
            this.lblValorMantenimiento = new System.Windows.Forms.Label();
            this.lblSubMantenimiento = new System.Windows.Forms.Label();
            this.pnlCardTasaOcupacion = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconTasaOcupacion = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloTasaOcupacion = new System.Windows.Forms.Label();
            this.lblValorTasaOcupacion = new System.Windows.Forms.Label();
            this.lblSubTasaOcupacion = new System.Windows.Forms.Label();
            this.pnlFiltroFechasGeneral = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.btnFiltroHistorico = new FontAwesome.Sharp.IconButton();
            this.btnFiltroEsteMes = new FontAwesome.Sharp.IconButton();
            this.btnFiltro30Dias = new FontAwesome.Sharp.IconButton();
            this.btnFiltro7Dias = new FontAwesome.Sharp.IconButton();
            this.btnFiltroHoy = new FontAwesome.Sharp.IconButton();
            this.btnActualizarGeneral = new FontAwesome.Sharp.IconButton();
            this.dtpHastaGeneral = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaHastaGeneral = new System.Windows.Forms.Label();
            this.dtpDesdeGeneral = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaDesdeGeneral = new System.Windows.Forms.Label();
            this.lblTituloFiltroGeneral = new System.Windows.Forms.Label();
            this.pnlCardsIngresos = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardIngresos = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconIngresos = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloIngresos = new System.Windows.Forms.Label();
            this.lblValorIngresos = new System.Windows.Forms.Label();
            this.pnlCardTicketPromedio = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconTicketPromedio = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloTicketPromedio = new System.Windows.Forms.Label();
            this.lblValorTicketPromedio = new System.Windows.Forms.Label();
            this.pnlCardAnuladas = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconAnuladas = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloAnuladas = new System.Windows.Forms.Label();
            this.lblValorAnuladas = new System.Windows.Forms.Label();
            this.pnlCardMontoAnulado = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.iconMontoAnulado = new FontAwesome.Sharp.IconPictureBox();
            this.lblTituloMontoAnulado = new System.Windows.Forms.Label();
            this.lblValorMontoAnulado = new System.Windows.Forms.Label();
            this.pnlOrigenGasto = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.flpLeyendaOrigenGasto = new System.Windows.Forms.FlowLayoutPanel();
            this.ucDonutOrigenGasto = new GUI_08YS.Recepcionista.Dashboard.UC_DonutChart_68SA();
            this.lblTituloOrigenGasto = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).BeginInit();
            this.pnlTabs.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.flpHuespedes.SuspendLayout();
            this.flpOcupacion.SuspendLayout();
            this.flpGeneral.SuspendLayout();
            this.pnlCardsOcupacion.SuspendLayout();
            this.pnlCardDisponibles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconDisponibles)).BeginInit();
            this.pnlCardOcupadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconOcupadasDash)).BeginInit();
            this.pnlCardLimpieza.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconLimpieza)).BeginInit();
            this.pnlCardMantenimiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMantenimiento)).BeginInit();
            this.pnlCardTasaOcupacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTasaOcupacion)).BeginInit();
            this.pnlFiltroFechasGeneral.SuspendLayout();
            this.pnlCardsIngresos.SuspendLayout();
            this.pnlCardIngresos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconIngresos)).BeginInit();
            this.pnlCardTicketPromedio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTicketPromedio)).BeginInit();
            this.pnlCardAnuladas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconAnuladas)).BeginInit();
            this.pnlCardMontoAnulado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMontoAnulado)).BeginInit();
            this.pnlOrigenGasto.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlEncabezado.Controls.Add(this.lblEncabezado);
            this.pnlEncabezado.Controls.Add(this.iconEncabezado);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1470, 76);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblEncabezado
            // 
            this.lblEncabezado.AutoSize = true;
            this.lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezado.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblEncabezado.Location = new System.Drawing.Point(72, 24);
            this.lblEncabezado.Name = "lblEncabezado";
            this.lblEncabezado.Size = new System.Drawing.Size(180, 30);
            this.lblEncabezado.TabIndex = 1;
            this.lblEncabezado.Text = "Panel de Control";
            // 
            // iconEncabezado
            // 
            this.iconEncabezado.IconChar = FontAwesome.Sharp.IconChar.Gauge;
            this.iconEncabezado.IconColor = System.Drawing.Color.Gold;
            this.iconEncabezado.IconSize = 40;
            this.iconEncabezado.Location = new System.Drawing.Point(20, 18);
            this.iconEncabezado.Name = "iconEncabezado";
            this.iconEncabezado.Size = new System.Drawing.Size(44, 44);
            this.iconEncabezado.TabIndex = 0;
            this.iconEncabezado.TabStop = false;
            // 
            // pnlTabs
            // 
            this.pnlTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlTabs.Controls.Add(this.btnTabHuespedes);
            this.pnlTabs.Controls.Add(this.btnTabOcupacion);
            this.pnlTabs.Controls.Add(this.btnTabGeneral);
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabs.Location = new System.Drawing.Point(0, 76);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(1470, 50);
            this.pnlTabs.TabIndex = 1;
            // 
            // btnTabGeneral
            // 
            this.btnTabGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.btnTabGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabGeneral.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTabGeneral.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnTabGeneral.IconChar = FontAwesome.Sharp.IconChar.TableCellsLarge;
            this.btnTabGeneral.IconColor = System.Drawing.Color.Goldenrod;
            this.btnTabGeneral.IconSize = 20;
            this.btnTabGeneral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabGeneral.Location = new System.Drawing.Point(20, 6);
            this.btnTabGeneral.Name = "btnTabGeneral";
            this.btnTabGeneral.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnTabGeneral.Size = new System.Drawing.Size(140, 38);
            this.btnTabGeneral.TabIndex = 0;
            this.btnTabGeneral.Text = "General";
            this.btnTabGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabGeneral.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTabGeneral.UseVisualStyleBackColor = false;
            // 
            // btnTabOcupacion
            // 
            this.btnTabOcupacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.btnTabOcupacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabOcupacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTabOcupacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.btnTabOcupacion.IconChar = FontAwesome.Sharp.IconChar.Bed;
            this.btnTabOcupacion.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.btnTabOcupacion.IconSize = 20;
            this.btnTabOcupacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabOcupacion.Location = new System.Drawing.Point(170, 6);
            this.btnTabOcupacion.Name = "btnTabOcupacion";
            this.btnTabOcupacion.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnTabOcupacion.Size = new System.Drawing.Size(160, 38);
            this.btnTabOcupacion.TabIndex = 1;
            this.btnTabOcupacion.Text = "Ocupación";
            this.btnTabOcupacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabOcupacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTabOcupacion.UseVisualStyleBackColor = false;
            // 
            // btnTabHuespedes
            // 
            this.btnTabHuespedes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.btnTabHuespedes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabHuespedes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTabHuespedes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.btnTabHuespedes.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.btnTabHuespedes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.btnTabHuespedes.IconSize = 20;
            this.btnTabHuespedes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabHuespedes.Location = new System.Drawing.Point(340, 6);
            this.btnTabHuespedes.Name = "btnTabHuespedes";
            this.btnTabHuespedes.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnTabHuespedes.Size = new System.Drawing.Size(160, 38);
            this.btnTabHuespedes.TabIndex = 2;
            this.btnTabHuespedes.Text = "Huéspedes";
            this.btnTabHuespedes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabHuespedes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTabHuespedes.UseVisualStyleBackColor = false;
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlContenido.Controls.Add(this.flpHuespedes);
            this.pnlContenido.Controls.Add(this.flpOcupacion);
            this.pnlContenido.Controls.Add(this.flpGeneral);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 126);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1470, 794);
            this.pnlContenido.TabIndex = 2;
            // 
            // flpGeneral
            // 
            this.flpGeneral.AutoScroll = true;
            this.flpGeneral.Controls.Add(this.pnlCardsOcupacion);
            this.flpGeneral.Controls.Add(this.pnlFiltroFechasGeneral);
            this.flpGeneral.Controls.Add(this.pnlCardsIngresos);
            this.flpGeneral.Controls.Add(this.pnlOrigenGasto);
            this.flpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpGeneral.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpGeneral.Location = new System.Drawing.Point(0, 0);
            this.flpGeneral.Name = "flpGeneral";
            this.flpGeneral.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.flpGeneral.Size = new System.Drawing.Size(1470, 794);
            this.flpGeneral.TabIndex = 0;
            this.flpGeneral.WrapContents = false;
            // 
            // pnlCardsOcupacion
            // 
            this.pnlCardsOcupacion.ColumnCount = 5;
            this.pnlCardsOcupacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlCardsOcupacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlCardsOcupacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlCardsOcupacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlCardsOcupacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlCardsOcupacion.Controls.Add(this.pnlCardDisponibles, 0, 0);
            this.pnlCardsOcupacion.Controls.Add(this.pnlCardOcupadas, 1, 0);
            this.pnlCardsOcupacion.Controls.Add(this.pnlCardLimpieza, 2, 0);
            this.pnlCardsOcupacion.Controls.Add(this.pnlCardMantenimiento, 3, 0);
            this.pnlCardsOcupacion.Controls.Add(this.pnlCardTasaOcupacion, 4, 0);
            this.pnlCardsOcupacion.Location = new System.Drawing.Point(3, 3);
            this.pnlCardsOcupacion.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.pnlCardsOcupacion.Name = "pnlCardsOcupacion";
            this.pnlCardsOcupacion.RowCount = 1;
            this.pnlCardsOcupacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCardsOcupacion.Size = new System.Drawing.Size(1424, 126);
            this.pnlCardsOcupacion.TabIndex = 0;
            // 
            // pnlCardDisponibles
            // 
            this.pnlCardDisponibles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardDisponibles.Controls.Add(this.iconDisponibles);
            this.pnlCardDisponibles.Controls.Add(this.lblTituloDisponibles);
            this.pnlCardDisponibles.Controls.Add(this.lblValorDisponibles);
            this.pnlCardDisponibles.Controls.Add(this.lblSubDisponibles);
            this.pnlCardDisponibles.CornerRadius = 12;
            this.pnlCardDisponibles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardDisponibles.Location = new System.Drawing.Point(3, 3);
            this.pnlCardDisponibles.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.pnlCardDisponibles.Name = "pnlCardDisponibles";
            this.pnlCardDisponibles.Size = new System.Drawing.Size(269, 120);
            this.pnlCardDisponibles.TabIndex = 0;
            // 
            // iconDisponibles
            // 
            this.iconDisponibles.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.iconDisponibles.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(217)))), ((int)(((byte)(100)))));
            this.iconDisponibles.IconSize = 30;
            this.iconDisponibles.Location = new System.Drawing.Point(212, 40);
            this.iconDisponibles.Name = "iconDisponibles";
            this.iconDisponibles.Size = new System.Drawing.Size(38, 38);
            this.iconDisponibles.TabIndex = 3;
            this.iconDisponibles.TabStop = false;
            // 
            // lblTituloDisponibles
            // 
            this.lblTituloDisponibles.AutoSize = true;
            this.lblTituloDisponibles.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloDisponibles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloDisponibles.Location = new System.Drawing.Point(14, 14);
            this.lblTituloDisponibles.Name = "lblTituloDisponibles";
            this.lblTituloDisponibles.Size = new System.Drawing.Size(120, 13);
            this.lblTituloDisponibles.TabIndex = 0;
            this.lblTituloDisponibles.Text = "DISPONIBLES";
            // 
            // lblValorDisponibles
            // 
            this.lblValorDisponibles.AutoSize = true;
            this.lblValorDisponibles.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorDisponibles.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblValorDisponibles.Location = new System.Drawing.Point(14, 34);
            this.lblValorDisponibles.Name = "lblValorDisponibles";
            this.lblValorDisponibles.Size = new System.Drawing.Size(40, 27);
            this.lblValorDisponibles.TabIndex = 1;
            this.lblValorDisponibles.Text = "0";
            // 
            // lblSubDisponibles
            // 
            this.lblSubDisponibles.AutoSize = true;
            this.lblSubDisponibles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblSubDisponibles.Location = new System.Drawing.Point(14, 76);
            this.lblSubDisponibles.Name = "lblSubDisponibles";
            this.lblSubDisponibles.Size = new System.Drawing.Size(90, 15);
            this.lblSubDisponibles.TabIndex = 2;
            this.lblSubDisponibles.Text = "de 0 hab.";
            // 
            // pnlCardOcupadas
            // 
            this.pnlCardOcupadas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardOcupadas.Controls.Add(this.iconOcupadasDash);
            this.pnlCardOcupadas.Controls.Add(this.lblTituloOcupadasDash);
            this.pnlCardOcupadas.Controls.Add(this.lblValorOcupadasDash);
            this.pnlCardOcupadas.Controls.Add(this.lblSubOcupadasDash);
            this.pnlCardOcupadas.CornerRadius = 12;
            this.pnlCardOcupadas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardOcupadas.Location = new System.Drawing.Point(287, 3);
            this.pnlCardOcupadas.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.pnlCardOcupadas.Name = "pnlCardOcupadas";
            this.pnlCardOcupadas.Size = new System.Drawing.Size(269, 120);
            this.pnlCardOcupadas.TabIndex = 1;
            // 
            // iconOcupadasDash
            // 
            this.iconOcupadasDash.IconChar = FontAwesome.Sharp.IconChar.Bed;
            this.iconOcupadasDash.IconColor = System.Drawing.Color.Goldenrod;
            this.iconOcupadasDash.IconSize = 30;
            this.iconOcupadasDash.Location = new System.Drawing.Point(212, 40);
            this.iconOcupadasDash.Name = "iconOcupadasDash";
            this.iconOcupadasDash.Size = new System.Drawing.Size(38, 38);
            this.iconOcupadasDash.TabIndex = 3;
            this.iconOcupadasDash.TabStop = false;
            // 
            // lblTituloOcupadasDash
            // 
            this.lblTituloOcupadasDash.AutoSize = true;
            this.lblTituloOcupadasDash.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloOcupadasDash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloOcupadasDash.Location = new System.Drawing.Point(14, 14);
            this.lblTituloOcupadasDash.Name = "lblTituloOcupadasDash";
            this.lblTituloOcupadasDash.Size = new System.Drawing.Size(110, 13);
            this.lblTituloOcupadasDash.TabIndex = 0;
            this.lblTituloOcupadasDash.Text = "OCUPADAS";
            // 
            // lblValorOcupadasDash
            // 
            this.lblValorOcupadasDash.AutoSize = true;
            this.lblValorOcupadasDash.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorOcupadasDash.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblValorOcupadasDash.Location = new System.Drawing.Point(14, 34);
            this.lblValorOcupadasDash.Name = "lblValorOcupadasDash";
            this.lblValorOcupadasDash.Size = new System.Drawing.Size(40, 27);
            this.lblValorOcupadasDash.TabIndex = 1;
            this.lblValorOcupadasDash.Text = "0";
            // 
            // lblSubOcupadasDash
            // 
            this.lblSubOcupadasDash.AutoSize = true;
            this.lblSubOcupadasDash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblSubOcupadasDash.Location = new System.Drawing.Point(14, 76);
            this.lblSubOcupadasDash.Name = "lblSubOcupadasDash";
            this.lblSubOcupadasDash.Size = new System.Drawing.Size(90, 15);
            this.lblSubOcupadasDash.TabIndex = 2;
            this.lblSubOcupadasDash.Text = "de 0 hab.";
            // 
            // pnlCardLimpieza
            // 
            this.pnlCardLimpieza.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardLimpieza.Controls.Add(this.iconLimpieza);
            this.pnlCardLimpieza.Controls.Add(this.lblTituloLimpieza);
            this.pnlCardLimpieza.Controls.Add(this.lblValorLimpieza);
            this.pnlCardLimpieza.Controls.Add(this.lblSubLimpieza);
            this.pnlCardLimpieza.CornerRadius = 12;
            this.pnlCardLimpieza.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardLimpieza.Location = new System.Drawing.Point(571, 3);
            this.pnlCardLimpieza.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.pnlCardLimpieza.Name = "pnlCardLimpieza";
            this.pnlCardLimpieza.Size = new System.Drawing.Size(269, 120);
            this.pnlCardLimpieza.TabIndex = 2;
            // 
            // iconLimpieza
            // 
            this.iconLimpieza.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.iconLimpieza.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(155)))), ((int)(((byte)(235)))));
            this.iconLimpieza.IconSize = 30;
            this.iconLimpieza.Location = new System.Drawing.Point(212, 40);
            this.iconLimpieza.Name = "iconLimpieza";
            this.iconLimpieza.Size = new System.Drawing.Size(38, 38);
            this.iconLimpieza.TabIndex = 3;
            this.iconLimpieza.TabStop = false;
            // 
            // lblTituloLimpieza
            // 
            this.lblTituloLimpieza.AutoSize = true;
            this.lblTituloLimpieza.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloLimpieza.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloLimpieza.Location = new System.Drawing.Point(14, 14);
            this.lblTituloLimpieza.Name = "lblTituloLimpieza";
            this.lblTituloLimpieza.Size = new System.Drawing.Size(90, 13);
            this.lblTituloLimpieza.TabIndex = 0;
            this.lblTituloLimpieza.Text = "POR LIMPIEZA";
            // 
            // lblValorLimpieza
            // 
            this.lblValorLimpieza.AutoSize = true;
            this.lblValorLimpieza.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorLimpieza.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblValorLimpieza.Location = new System.Drawing.Point(14, 34);
            this.lblValorLimpieza.Name = "lblValorLimpieza";
            this.lblValorLimpieza.Size = new System.Drawing.Size(40, 27);
            this.lblValorLimpieza.TabIndex = 1;
            this.lblValorLimpieza.Text = "0";
            // 
            // lblSubLimpieza
            // 
            this.lblSubLimpieza.AutoSize = true;
            this.lblSubLimpieza.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblSubLimpieza.Location = new System.Drawing.Point(14, 76);
            this.lblSubLimpieza.Name = "lblSubLimpieza";
            this.lblSubLimpieza.Size = new System.Drawing.Size(90, 15);
            this.lblSubLimpieza.TabIndex = 2;
            this.lblSubLimpieza.Text = "de 0 hab.";
            // 
            // pnlCardMantenimiento
            // 
            this.pnlCardMantenimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardMantenimiento.Controls.Add(this.iconMantenimiento);
            this.pnlCardMantenimiento.Controls.Add(this.lblTituloMantenimiento);
            this.pnlCardMantenimiento.Controls.Add(this.lblValorMantenimiento);
            this.pnlCardMantenimiento.Controls.Add(this.lblSubMantenimiento);
            this.pnlCardMantenimiento.CornerRadius = 12;
            this.pnlCardMantenimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardMantenimiento.Location = new System.Drawing.Point(855, 3);
            this.pnlCardMantenimiento.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.pnlCardMantenimiento.Name = "pnlCardMantenimiento";
            this.pnlCardMantenimiento.Size = new System.Drawing.Size(269, 120);
            this.pnlCardMantenimiento.TabIndex = 3;
            // 
            // iconMantenimiento
            // 
            this.iconMantenimiento.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            this.iconMantenimiento.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.iconMantenimiento.IconSize = 30;
            this.iconMantenimiento.Location = new System.Drawing.Point(212, 40);
            this.iconMantenimiento.Name = "iconMantenimiento";
            this.iconMantenimiento.Size = new System.Drawing.Size(38, 38);
            this.iconMantenimiento.TabIndex = 3;
            this.iconMantenimiento.TabStop = false;
            // 
            // lblTituloMantenimiento
            // 
            this.lblTituloMantenimiento.AutoSize = true;
            this.lblTituloMantenimiento.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloMantenimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloMantenimiento.Location = new System.Drawing.Point(14, 14);
            this.lblTituloMantenimiento.Name = "lblTituloMantenimiento";
            this.lblTituloMantenimiento.Size = new System.Drawing.Size(100, 13);
            this.lblTituloMantenimiento.TabIndex = 0;
            this.lblTituloMantenimiento.Text = "EN MANTENIMIENTO";
            // 
            // lblValorMantenimiento
            // 
            this.lblValorMantenimiento.AutoSize = true;
            this.lblValorMantenimiento.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorMantenimiento.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblValorMantenimiento.Location = new System.Drawing.Point(14, 34);
            this.lblValorMantenimiento.Name = "lblValorMantenimiento";
            this.lblValorMantenimiento.Size = new System.Drawing.Size(40, 27);
            this.lblValorMantenimiento.TabIndex = 1;
            this.lblValorMantenimiento.Text = "0";
            // 
            // lblSubMantenimiento
            // 
            this.lblSubMantenimiento.AutoSize = true;
            this.lblSubMantenimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblSubMantenimiento.Location = new System.Drawing.Point(14, 76);
            this.lblSubMantenimiento.Name = "lblSubMantenimiento";
            this.lblSubMantenimiento.Size = new System.Drawing.Size(90, 15);
            this.lblSubMantenimiento.TabIndex = 2;
            this.lblSubMantenimiento.Text = "de 0 hab.";
            // 
            // pnlCardTasaOcupacion
            // 
            this.pnlCardTasaOcupacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardTasaOcupacion.Controls.Add(this.iconTasaOcupacion);
            this.pnlCardTasaOcupacion.Controls.Add(this.lblTituloTasaOcupacion);
            this.pnlCardTasaOcupacion.Controls.Add(this.lblValorTasaOcupacion);
            this.pnlCardTasaOcupacion.Controls.Add(this.lblSubTasaOcupacion);
            this.pnlCardTasaOcupacion.CornerRadius = 12;
            this.pnlCardTasaOcupacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardTasaOcupacion.Location = new System.Drawing.Point(1139, 3);
            this.pnlCardTasaOcupacion.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pnlCardTasaOcupacion.Name = "pnlCardTasaOcupacion";
            this.pnlCardTasaOcupacion.Size = new System.Drawing.Size(282, 120);
            this.pnlCardTasaOcupacion.TabIndex = 4;
            // 
            // iconTasaOcupacion
            // 
            this.iconTasaOcupacion.IconChar = FontAwesome.Sharp.IconChar.Gauge;
            this.iconTasaOcupacion.IconColor = System.Drawing.Color.Gold;
            this.iconTasaOcupacion.IconSize = 30;
            this.iconTasaOcupacion.Location = new System.Drawing.Point(224, 40);
            this.iconTasaOcupacion.Name = "iconTasaOcupacion";
            this.iconTasaOcupacion.Size = new System.Drawing.Size(38, 38);
            this.iconTasaOcupacion.TabIndex = 3;
            this.iconTasaOcupacion.TabStop = false;
            // 
            // lblTituloTasaOcupacion
            // 
            this.lblTituloTasaOcupacion.AutoSize = true;
            this.lblTituloTasaOcupacion.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloTasaOcupacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloTasaOcupacion.Location = new System.Drawing.Point(14, 14);
            this.lblTituloTasaOcupacion.Name = "lblTituloTasaOcupacion";
            this.lblTituloTasaOcupacion.Size = new System.Drawing.Size(105, 13);
            this.lblTituloTasaOcupacion.TabIndex = 0;
            this.lblTituloTasaOcupacion.Text = "TASA DE OCUPACIÓN";
            // 
            // lblValorTasaOcupacion
            // 
            this.lblValorTasaOcupacion.AutoSize = true;
            this.lblValorTasaOcupacion.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTasaOcupacion.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblValorTasaOcupacion.Location = new System.Drawing.Point(14, 34);
            this.lblValorTasaOcupacion.Name = "lblValorTasaOcupacion";
            this.lblValorTasaOcupacion.Size = new System.Drawing.Size(60, 27);
            this.lblValorTasaOcupacion.TabIndex = 1;
            this.lblValorTasaOcupacion.Text = "0%";
            // 
            // lblSubTasaOcupacion
            // 
            this.lblSubTasaOcupacion.AutoSize = true;
            this.lblSubTasaOcupacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblSubTasaOcupacion.Location = new System.Drawing.Point(14, 76);
            this.lblSubTasaOcupacion.Name = "lblSubTasaOcupacion";
            this.lblSubTasaOcupacion.Size = new System.Drawing.Size(110, 15);
            this.lblSubTasaOcupacion.TabIndex = 2;
            this.lblSubTasaOcupacion.Text = "Capacidad activa";
            // 
            // pnlFiltroFechasGeneral
            // 
            this.pnlFiltroFechasGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlFiltroFechasGeneral.Controls.Add(this.btnFiltroHistorico);
            this.pnlFiltroFechasGeneral.Controls.Add(this.btnFiltroEsteMes);
            this.pnlFiltroFechasGeneral.Controls.Add(this.btnFiltro30Dias);
            this.pnlFiltroFechasGeneral.Controls.Add(this.btnFiltro7Dias);
            this.pnlFiltroFechasGeneral.Controls.Add(this.btnFiltroHoy);
            this.pnlFiltroFechasGeneral.Controls.Add(this.btnActualizarGeneral);
            this.pnlFiltroFechasGeneral.Controls.Add(this.dtpHastaGeneral);
            this.pnlFiltroFechasGeneral.Controls.Add(this.lblEtiquetaHastaGeneral);
            this.pnlFiltroFechasGeneral.Controls.Add(this.dtpDesdeGeneral);
            this.pnlFiltroFechasGeneral.Controls.Add(this.lblEtiquetaDesdeGeneral);
            this.pnlFiltroFechasGeneral.Controls.Add(this.lblTituloFiltroGeneral);
            this.pnlFiltroFechasGeneral.CornerRadius = 12;
            this.pnlFiltroFechasGeneral.Location = new System.Drawing.Point(3, 139);
            this.pnlFiltroFechasGeneral.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.pnlFiltroFechasGeneral.Name = "pnlFiltroFechasGeneral";
            this.pnlFiltroFechasGeneral.Size = new System.Drawing.Size(1424, 112);
            this.pnlFiltroFechasGeneral.TabIndex = 1;
            // 
            // lblTituloFiltroGeneral
            // 
            this.lblTituloFiltroGeneral.AutoSize = true;
            this.lblTituloFiltroGeneral.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloFiltroGeneral.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTituloFiltroGeneral.Location = new System.Drawing.Point(16, 10);
            this.lblTituloFiltroGeneral.Name = "lblTituloFiltroGeneral";
            this.lblTituloFiltroGeneral.Size = new System.Drawing.Size(150, 17);
            this.lblTituloFiltroGeneral.TabIndex = 0;
            this.lblTituloFiltroGeneral.Text = "INGRESOS DEL PERÍODO";
            // 
            // lblEtiquetaDesdeGeneral
            // 
            this.lblEtiquetaDesdeGeneral.AutoSize = true;
            this.lblEtiquetaDesdeGeneral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaDesdeGeneral.Location = new System.Drawing.Point(16, 70);
            this.lblEtiquetaDesdeGeneral.Name = "lblEtiquetaDesdeGeneral";
            this.lblEtiquetaDesdeGeneral.Size = new System.Drawing.Size(40, 15);
            this.lblEtiquetaDesdeGeneral.TabIndex = 1;
            this.lblEtiquetaDesdeGeneral.Text = "Desde";
            // 
            // dtpDesdeGeneral
            // 
            this.dtpDesdeGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpDesdeGeneral.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpDesdeGeneral.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpDesdeGeneral.BorderWidth = 2;
            this.dtpDesdeGeneral.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpDesdeGeneral.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpDesdeGeneral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDesdeGeneral.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesdeGeneral.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpDesdeGeneral.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpDesdeGeneral.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpDesdeGeneral.IconSize = 20;
            this.dtpDesdeGeneral.Location = new System.Drawing.Point(70, 58);
            this.dtpDesdeGeneral.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDesdeGeneral.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDesdeGeneral.Name = "dtpDesdeGeneral";
            this.dtpDesdeGeneral.Size = new System.Drawing.Size(220, 40);
            this.dtpDesdeGeneral.TabIndex = 2;
            this.dtpDesdeGeneral.Value = null;
            // 
            // lblEtiquetaHastaGeneral
            // 
            this.lblEtiquetaHastaGeneral.AutoSize = true;
            this.lblEtiquetaHastaGeneral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaHastaGeneral.Location = new System.Drawing.Point(310, 70);
            this.lblEtiquetaHastaGeneral.Name = "lblEtiquetaHastaGeneral";
            this.lblEtiquetaHastaGeneral.Size = new System.Drawing.Size(38, 15);
            this.lblEtiquetaHastaGeneral.TabIndex = 3;
            this.lblEtiquetaHastaGeneral.Text = "Hasta";
            // 
            // dtpHastaGeneral
            // 
            this.dtpHastaGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpHastaGeneral.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpHastaGeneral.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpHastaGeneral.BorderWidth = 2;
            this.dtpHastaGeneral.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpHastaGeneral.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpHastaGeneral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpHastaGeneral.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHastaGeneral.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpHastaGeneral.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpHastaGeneral.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpHastaGeneral.IconSize = 20;
            this.dtpHastaGeneral.Location = new System.Drawing.Point(364, 58);
            this.dtpHastaGeneral.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpHastaGeneral.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpHastaGeneral.Name = "dtpHastaGeneral";
            this.dtpHastaGeneral.Size = new System.Drawing.Size(220, 40);
            this.dtpHastaGeneral.TabIndex = 4;
            this.dtpHastaGeneral.Value = null;
            // 
            // btnActualizarGeneral
            // 
            this.btnActualizarGeneral.BackColor = System.Drawing.Color.Goldenrod;
            this.btnActualizarGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarGeneral.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarGeneral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnActualizarGeneral.IconChar = FontAwesome.Sharp.IconChar.ArrowsRotate;
            this.btnActualizarGeneral.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnActualizarGeneral.IconSize = 20;
            this.btnActualizarGeneral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizarGeneral.Location = new System.Drawing.Point(604, 56);
            this.btnActualizarGeneral.Name = "btnActualizarGeneral";
            this.btnActualizarGeneral.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnActualizarGeneral.Size = new System.Drawing.Size(170, 44);
            this.btnActualizarGeneral.TabIndex = 5;
            this.btnActualizarGeneral.Text = "Actualizar";
            this.btnActualizarGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizarGeneral.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnActualizarGeneral.UseVisualStyleBackColor = false;
            // 
            // btnFiltroHoy
            // 
            this.btnFiltroHoy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnFiltroHoy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltroHoy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltroHoy.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnFiltroHoy.Location = new System.Drawing.Point(678, 8);
            this.btnFiltroHoy.Name = "btnFiltroHoy";
            this.btnFiltroHoy.Size = new System.Drawing.Size(130, 36);
            this.btnFiltroHoy.TabIndex = 6;
            this.btnFiltroHoy.Text = "Hoy";
            this.btnFiltroHoy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFiltroHoy.UseVisualStyleBackColor = false;
            // 
            // btnFiltro7Dias
            // 
            this.btnFiltro7Dias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnFiltro7Dias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltro7Dias.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltro7Dias.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnFiltro7Dias.Location = new System.Drawing.Point(818, 8);
            this.btnFiltro7Dias.Name = "btnFiltro7Dias";
            this.btnFiltro7Dias.Size = new System.Drawing.Size(140, 36);
            this.btnFiltro7Dias.TabIndex = 7;
            this.btnFiltro7Dias.Text = "Últimos 7 días";
            this.btnFiltro7Dias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFiltro7Dias.UseVisualStyleBackColor = false;
            // 
            // btnFiltro30Dias
            // 
            this.btnFiltro30Dias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnFiltro30Dias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltro30Dias.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltro30Dias.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnFiltro30Dias.Location = new System.Drawing.Point(968, 8);
            this.btnFiltro30Dias.Name = "btnFiltro30Dias";
            this.btnFiltro30Dias.Size = new System.Drawing.Size(140, 36);
            this.btnFiltro30Dias.TabIndex = 8;
            this.btnFiltro30Dias.Text = "Últimos 30 días";
            this.btnFiltro30Dias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFiltro30Dias.UseVisualStyleBackColor = false;
            // 
            // btnFiltroEsteMes
            // 
            this.btnFiltroEsteMes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnFiltroEsteMes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltroEsteMes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltroEsteMes.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnFiltroEsteMes.Location = new System.Drawing.Point(1118, 8);
            this.btnFiltroEsteMes.Name = "btnFiltroEsteMes";
            this.btnFiltroEsteMes.Size = new System.Drawing.Size(130, 36);
            this.btnFiltroEsteMes.TabIndex = 9;
            this.btnFiltroEsteMes.Text = "Este Mes";
            this.btnFiltroEsteMes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFiltroEsteMes.UseVisualStyleBackColor = false;
            // 
            // btnFiltroHistorico
            // 
            this.btnFiltroHistorico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnFiltroHistorico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltroHistorico.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltroHistorico.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnFiltroHistorico.IconChar = FontAwesome.Sharp.IconChar.Infinity;
            this.btnFiltroHistorico.IconColor = System.Drawing.Color.Goldenrod;
            this.btnFiltroHistorico.IconSize = 16;
            this.btnFiltroHistorico.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltroHistorico.Location = new System.Drawing.Point(1258, 8);
            this.btnFiltroHistorico.Name = "btnFiltroHistorico";
            this.btnFiltroHistorico.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnFiltroHistorico.Size = new System.Drawing.Size(150, 36);
            this.btnFiltroHistorico.TabIndex = 10;
            this.btnFiltroHistorico.Text = "Histórico Total";
            this.btnFiltroHistorico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltroHistorico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltroHistorico.UseVisualStyleBackColor = false;
            // 
            // pnlCardsIngresos
            // 
            this.pnlCardsIngresos.ColumnCount = 4;
            this.pnlCardsIngresos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCardsIngresos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCardsIngresos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCardsIngresos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCardsIngresos.Controls.Add(this.pnlCardIngresos, 0, 0);
            this.pnlCardsIngresos.Controls.Add(this.pnlCardTicketPromedio, 1, 0);
            this.pnlCardsIngresos.Controls.Add(this.pnlCardAnuladas, 2, 0);
            this.pnlCardsIngresos.Controls.Add(this.pnlCardMontoAnulado, 3, 0);
            this.pnlCardsIngresos.Location = new System.Drawing.Point(3, 261);
            this.pnlCardsIngresos.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.pnlCardsIngresos.Name = "pnlCardsIngresos";
            this.pnlCardsIngresos.RowCount = 1;
            this.pnlCardsIngresos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCardsIngresos.Size = new System.Drawing.Size(1424, 110);
            this.pnlCardsIngresos.TabIndex = 2;
            // 
            // pnlCardIngresos
            // 
            this.pnlCardIngresos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardIngresos.Controls.Add(this.iconIngresos);
            this.pnlCardIngresos.Controls.Add(this.lblTituloIngresos);
            this.pnlCardIngresos.Controls.Add(this.lblValorIngresos);
            this.pnlCardIngresos.CornerRadius = 12;
            this.pnlCardIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardIngresos.Location = new System.Drawing.Point(3, 3);
            this.pnlCardIngresos.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.pnlCardIngresos.Name = "pnlCardIngresos";
            this.pnlCardIngresos.Size = new System.Drawing.Size(341, 104);
            this.pnlCardIngresos.TabIndex = 0;
            // 
            // iconIngresos
            // 
            this.iconIngresos.IconChar = FontAwesome.Sharp.IconChar.DollarSign;
            this.iconIngresos.IconColor = System.Drawing.Color.Goldenrod;
            this.iconIngresos.IconSize = 28;
            this.iconIngresos.Location = new System.Drawing.Point(288, 32);
            this.iconIngresos.Name = "iconIngresos";
            this.iconIngresos.Size = new System.Drawing.Size(36, 36);
            this.iconIngresos.TabIndex = 2;
            this.iconIngresos.TabStop = false;
            // 
            // lblTituloIngresos
            // 
            this.lblTituloIngresos.AutoSize = true;
            this.lblTituloIngresos.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloIngresos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloIngresos.Location = new System.Drawing.Point(16, 14);
            this.lblTituloIngresos.Name = "lblTituloIngresos";
            this.lblTituloIngresos.Size = new System.Drawing.Size(120, 13);
            this.lblTituloIngresos.TabIndex = 0;
            this.lblTituloIngresos.Text = "INGRESOS PERÍODO";
            // 
            // lblValorIngresos
            // 
            this.lblValorIngresos.AutoSize = true;
            this.lblValorIngresos.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorIngresos.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblValorIngresos.Location = new System.Drawing.Point(16, 32);
            this.lblValorIngresos.Name = "lblValorIngresos";
            this.lblValorIngresos.Size = new System.Drawing.Size(50, 27);
            this.lblValorIngresos.TabIndex = 1;
            this.lblValorIngresos.Text = "$0";
            // 
            // pnlCardTicketPromedio
            // 
            this.pnlCardTicketPromedio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardTicketPromedio.Controls.Add(this.iconTicketPromedio);
            this.pnlCardTicketPromedio.Controls.Add(this.lblTituloTicketPromedio);
            this.pnlCardTicketPromedio.Controls.Add(this.lblValorTicketPromedio);
            this.pnlCardTicketPromedio.CornerRadius = 12;
            this.pnlCardTicketPromedio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardTicketPromedio.Location = new System.Drawing.Point(359, 3);
            this.pnlCardTicketPromedio.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.pnlCardTicketPromedio.Name = "pnlCardTicketPromedio";
            this.pnlCardTicketPromedio.Size = new System.Drawing.Size(341, 104);
            this.pnlCardTicketPromedio.TabIndex = 1;
            // 
            // iconTicketPromedio
            // 
            this.iconTicketPromedio.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this.iconTicketPromedio.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(155)))), ((int)(((byte)(235)))));
            this.iconTicketPromedio.IconSize = 28;
            this.iconTicketPromedio.Location = new System.Drawing.Point(288, 32);
            this.iconTicketPromedio.Name = "iconTicketPromedio";
            this.iconTicketPromedio.Size = new System.Drawing.Size(36, 36);
            this.iconTicketPromedio.TabIndex = 2;
            this.iconTicketPromedio.TabStop = false;
            // 
            // lblTituloTicketPromedio
            // 
            this.lblTituloTicketPromedio.AutoSize = true;
            this.lblTituloTicketPromedio.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloTicketPromedio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloTicketPromedio.Location = new System.Drawing.Point(16, 14);
            this.lblTituloTicketPromedio.Name = "lblTituloTicketPromedio";
            this.lblTituloTicketPromedio.Size = new System.Drawing.Size(110, 13);
            this.lblTituloTicketPromedio.TabIndex = 0;
            this.lblTituloTicketPromedio.Text = "TICKET PROMEDIO";
            // 
            // lblValorTicketPromedio
            // 
            this.lblValorTicketPromedio.AutoSize = true;
            this.lblValorTicketPromedio.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTicketPromedio.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblValorTicketPromedio.Location = new System.Drawing.Point(16, 32);
            this.lblValorTicketPromedio.Name = "lblValorTicketPromedio";
            this.lblValorTicketPromedio.Size = new System.Drawing.Size(50, 27);
            this.lblValorTicketPromedio.TabIndex = 1;
            this.lblValorTicketPromedio.Text = "$0";
            // 
            // pnlCardAnuladas
            // 
            this.pnlCardAnuladas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardAnuladas.Controls.Add(this.iconAnuladas);
            this.pnlCardAnuladas.Controls.Add(this.lblTituloAnuladas);
            this.pnlCardAnuladas.Controls.Add(this.lblValorAnuladas);
            this.pnlCardAnuladas.CornerRadius = 12;
            this.pnlCardAnuladas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardAnuladas.Location = new System.Drawing.Point(715, 3);
            this.pnlCardAnuladas.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.pnlCardAnuladas.Name = "pnlCardAnuladas";
            this.pnlCardAnuladas.Size = new System.Drawing.Size(341, 104);
            this.pnlCardAnuladas.TabIndex = 2;
            // 
            // iconAnuladas
            // 
            this.iconAnuladas.IconChar = FontAwesome.Sharp.IconChar.Ban;
            this.iconAnuladas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.iconAnuladas.IconSize = 28;
            this.iconAnuladas.Location = new System.Drawing.Point(288, 32);
            this.iconAnuladas.Name = "iconAnuladas";
            this.iconAnuladas.Size = new System.Drawing.Size(36, 36);
            this.iconAnuladas.TabIndex = 2;
            this.iconAnuladas.TabStop = false;
            // 
            // lblTituloAnuladas
            // 
            this.lblTituloAnuladas.AutoSize = true;
            this.lblTituloAnuladas.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloAnuladas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloAnuladas.Location = new System.Drawing.Point(16, 14);
            this.lblTituloAnuladas.Name = "lblTituloAnuladas";
            this.lblTituloAnuladas.Size = new System.Drawing.Size(140, 13);
            this.lblTituloAnuladas.TabIndex = 0;
            this.lblTituloAnuladas.Text = "% RESERVAS ANULADAS";
            // 
            // lblValorAnuladas
            // 
            this.lblValorAnuladas.AutoSize = true;
            this.lblValorAnuladas.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorAnuladas.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblValorAnuladas.Location = new System.Drawing.Point(16, 32);
            this.lblValorAnuladas.Name = "lblValorAnuladas";
            this.lblValorAnuladas.Size = new System.Drawing.Size(50, 27);
            this.lblValorAnuladas.TabIndex = 1;
            this.lblValorAnuladas.Text = "0%";
            // 
            // pnlCardMontoAnulado
            // 
            this.pnlCardMontoAnulado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCardMontoAnulado.Controls.Add(this.iconMontoAnulado);
            this.pnlCardMontoAnulado.Controls.Add(this.lblTituloMontoAnulado);
            this.pnlCardMontoAnulado.Controls.Add(this.lblValorMontoAnulado);
            this.pnlCardMontoAnulado.CornerRadius = 12;
            this.pnlCardMontoAnulado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardMontoAnulado.Location = new System.Drawing.Point(1071, 3);
            this.pnlCardMontoAnulado.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pnlCardMontoAnulado.Name = "pnlCardMontoAnulado";
            this.pnlCardMontoAnulado.Size = new System.Drawing.Size(350, 104);
            this.pnlCardMontoAnulado.TabIndex = 3;
            // 
            // iconMontoAnulado
            // 
            this.iconMontoAnulado.IconChar = FontAwesome.Sharp.IconChar.CircleXmark;
            this.iconMontoAnulado.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.iconMontoAnulado.IconSize = 28;
            this.iconMontoAnulado.Location = new System.Drawing.Point(288, 32);
            this.iconMontoAnulado.Name = "iconMontoAnulado";
            this.iconMontoAnulado.Size = new System.Drawing.Size(36, 36);
            this.iconMontoAnulado.TabIndex = 2;
            this.iconMontoAnulado.TabStop = false;
            // 
            // lblTituloMontoAnulado
            // 
            this.lblTituloMontoAnulado.AutoSize = true;
            this.lblTituloMontoAnulado.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloMontoAnulado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloMontoAnulado.Location = new System.Drawing.Point(16, 14);
            this.lblTituloMontoAnulado.Name = "lblTituloMontoAnulado";
            this.lblTituloMontoAnulado.Size = new System.Drawing.Size(120, 13);
            this.lblTituloMontoAnulado.TabIndex = 0;
            this.lblTituloMontoAnulado.Text = "MONTO ANULADO";
            // 
            // lblValorMontoAnulado
            // 
            this.lblValorMontoAnulado.AutoSize = true;
            this.lblValorMontoAnulado.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorMontoAnulado.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblValorMontoAnulado.Location = new System.Drawing.Point(16, 32);
            this.lblValorMontoAnulado.Name = "lblValorMontoAnulado";
            this.lblValorMontoAnulado.Size = new System.Drawing.Size(50, 27);
            this.lblValorMontoAnulado.TabIndex = 1;
            this.lblValorMontoAnulado.Text = "$0";
            // 
            // pnlOrigenGasto
            // 
            this.pnlOrigenGasto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlOrigenGasto.Controls.Add(this.flpLeyendaOrigenGasto);
            this.pnlOrigenGasto.Controls.Add(this.ucDonutOrigenGasto);
            this.pnlOrigenGasto.Controls.Add(this.lblTituloOrigenGasto);
            this.pnlOrigenGasto.CornerRadius = 12;
            this.pnlOrigenGasto.Location = new System.Drawing.Point(3, 377);
            this.pnlOrigenGasto.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlOrigenGasto.Name = "pnlOrigenGasto";
            this.pnlOrigenGasto.Size = new System.Drawing.Size(1424, 300);
            this.pnlOrigenGasto.TabIndex = 3;
            // 
            // lblTituloOrigenGasto
            // 
            this.lblTituloOrigenGasto.AutoSize = true;
            this.lblTituloOrigenGasto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloOrigenGasto.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTituloOrigenGasto.Location = new System.Drawing.Point(16, 14);
            this.lblTituloOrigenGasto.Name = "lblTituloOrigenGasto";
            this.lblTituloOrigenGasto.Size = new System.Drawing.Size(140, 19);
            this.lblTituloOrigenGasto.TabIndex = 0;
            this.lblTituloOrigenGasto.Text = "ORIGEN DEL GASTO";
            // 
            // ucDonutOrigenGasto
            // 
            this.ucDonutOrigenGasto.BackColor = System.Drawing.Color.Transparent;
            this.ucDonutOrigenGasto.Location = new System.Drawing.Point(60, 44);
            this.ucDonutOrigenGasto.Name = "ucDonutOrigenGasto";
            this.ucDonutOrigenGasto.Size = new System.Drawing.Size(240, 240);
            this.ucDonutOrigenGasto.TabIndex = 1;
            // 
            // flpLeyendaOrigenGasto
            // 
            this.flpLeyendaOrigenGasto.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLeyendaOrigenGasto.Location = new System.Drawing.Point(360, 60);
            this.flpLeyendaOrigenGasto.Name = "flpLeyendaOrigenGasto";
            this.flpLeyendaOrigenGasto.Size = new System.Drawing.Size(400, 210);
            this.flpLeyendaOrigenGasto.TabIndex = 2;
            this.flpLeyendaOrigenGasto.WrapContents = false;
            // 
            // flpOcupacion
            // 
            this.flpOcupacion.AutoScroll = true;
            this.flpOcupacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOcupacion.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpOcupacion.Location = new System.Drawing.Point(0, 0);
            this.flpOcupacion.Name = "flpOcupacion";
            this.flpOcupacion.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.flpOcupacion.Size = new System.Drawing.Size(1470, 794);
            this.flpOcupacion.TabIndex = 1;
            this.flpOcupacion.Visible = false;
            this.flpOcupacion.WrapContents = false;
            // 
            // flpHuespedes
            // 
            this.flpHuespedes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpHuespedes.Location = new System.Drawing.Point(0, 0);
            this.flpHuespedes.Name = "flpHuespedes";
            this.flpHuespedes.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.flpHuespedes.Size = new System.Drawing.Size(1470, 794);
            this.flpHuespedes.TabIndex = 2;
            this.flpHuespedes.Visible = false;
            // 
            // FormDashboard_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1470, 920);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDashboard_68SA";
            this.Text = "FormDashboard_68SA";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).EndInit();
            this.pnlTabs.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.flpHuespedes.ResumeLayout(false);
            this.flpHuespedes.PerformLayout();
            this.flpOcupacion.ResumeLayout(false);
            this.flpOcupacion.PerformLayout();
            this.flpGeneral.ResumeLayout(false);
            this.pnlCardsOcupacion.ResumeLayout(false);
            this.pnlCardDisponibles.ResumeLayout(false);
            this.pnlCardDisponibles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconDisponibles)).EndInit();
            this.pnlCardOcupadas.ResumeLayout(false);
            this.pnlCardOcupadas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconOcupadasDash)).EndInit();
            this.pnlCardLimpieza.ResumeLayout(false);
            this.pnlCardLimpieza.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconLimpieza)).EndInit();
            this.pnlCardMantenimiento.ResumeLayout(false);
            this.pnlCardMantenimiento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMantenimiento)).EndInit();
            this.pnlCardTasaOcupacion.ResumeLayout(false);
            this.pnlCardTasaOcupacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTasaOcupacion)).EndInit();
            this.pnlFiltroFechasGeneral.ResumeLayout(false);
            this.pnlFiltroFechasGeneral.PerformLayout();
            this.pnlCardsIngresos.ResumeLayout(false);
            this.pnlCardIngresos.ResumeLayout(false);
            this.pnlCardIngresos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconIngresos)).EndInit();
            this.pnlCardTicketPromedio.ResumeLayout(false);
            this.pnlCardTicketPromedio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTicketPromedio)).EndInit();
            this.pnlCardAnuladas.ResumeLayout(false);
            this.pnlCardAnuladas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconAnuladas)).EndInit();
            this.pnlCardMontoAnulado.ResumeLayout(false);
            this.pnlCardMontoAnulado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMontoAnulado)).EndInit();
            this.pnlOrigenGasto.ResumeLayout(false);
            this.pnlOrigenGasto.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlEncabezado;
        private FontAwesome.Sharp.IconPictureBox iconEncabezado;
        private System.Windows.Forms.Label lblEncabezado;
        private System.Windows.Forms.Panel pnlTabs;
        private FontAwesome.Sharp.IconButton btnTabGeneral;
        private FontAwesome.Sharp.IconButton btnTabOcupacion;
        private FontAwesome.Sharp.IconButton btnTabHuespedes;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.FlowLayoutPanel flpGeneral;
        private System.Windows.Forms.TableLayoutPanel pnlCardsOcupacion;
        private UserControls.RoundedPanel_68SA pnlCardDisponibles;
        private FontAwesome.Sharp.IconPictureBox iconDisponibles;
        private System.Windows.Forms.Label lblTituloDisponibles;
        private System.Windows.Forms.Label lblValorDisponibles;
        private System.Windows.Forms.Label lblSubDisponibles;
        private UserControls.RoundedPanel_68SA pnlCardOcupadas;
        private FontAwesome.Sharp.IconPictureBox iconOcupadasDash;
        private System.Windows.Forms.Label lblTituloOcupadasDash;
        private System.Windows.Forms.Label lblValorOcupadasDash;
        private System.Windows.Forms.Label lblSubOcupadasDash;
        private UserControls.RoundedPanel_68SA pnlCardLimpieza;
        private FontAwesome.Sharp.IconPictureBox iconLimpieza;
        private System.Windows.Forms.Label lblTituloLimpieza;
        private System.Windows.Forms.Label lblValorLimpieza;
        private System.Windows.Forms.Label lblSubLimpieza;
        private UserControls.RoundedPanel_68SA pnlCardMantenimiento;
        private FontAwesome.Sharp.IconPictureBox iconMantenimiento;
        private System.Windows.Forms.Label lblTituloMantenimiento;
        private System.Windows.Forms.Label lblValorMantenimiento;
        private System.Windows.Forms.Label lblSubMantenimiento;
        private UserControls.RoundedPanel_68SA pnlCardTasaOcupacion;
        private FontAwesome.Sharp.IconPictureBox iconTasaOcupacion;
        private System.Windows.Forms.Label lblTituloTasaOcupacion;
        private System.Windows.Forms.Label lblValorTasaOcupacion;
        private System.Windows.Forms.Label lblSubTasaOcupacion;
        private UserControls.RoundedPanel_68SA pnlFiltroFechasGeneral;
        private System.Windows.Forms.Label lblTituloFiltroGeneral;
        private System.Windows.Forms.Label lblEtiquetaDesdeGeneral;
        private CustomControls.IconDateTimePicker dtpDesdeGeneral;
        private System.Windows.Forms.Label lblEtiquetaHastaGeneral;
        private CustomControls.IconDateTimePicker dtpHastaGeneral;
        private FontAwesome.Sharp.IconButton btnActualizarGeneral;
        private FontAwesome.Sharp.IconButton btnFiltroHoy;
        private FontAwesome.Sharp.IconButton btnFiltro7Dias;
        private FontAwesome.Sharp.IconButton btnFiltro30Dias;
        private FontAwesome.Sharp.IconButton btnFiltroEsteMes;
        private FontAwesome.Sharp.IconButton btnFiltroHistorico;
        private System.Windows.Forms.TableLayoutPanel pnlCardsIngresos;
        private UserControls.RoundedPanel_68SA pnlCardIngresos;
        private FontAwesome.Sharp.IconPictureBox iconIngresos;
        private System.Windows.Forms.Label lblTituloIngresos;
        private System.Windows.Forms.Label lblValorIngresos;
        private UserControls.RoundedPanel_68SA pnlCardTicketPromedio;
        private FontAwesome.Sharp.IconPictureBox iconTicketPromedio;
        private System.Windows.Forms.Label lblTituloTicketPromedio;
        private System.Windows.Forms.Label lblValorTicketPromedio;
        private UserControls.RoundedPanel_68SA pnlCardAnuladas;
        private FontAwesome.Sharp.IconPictureBox iconAnuladas;
        private System.Windows.Forms.Label lblTituloAnuladas;
        private System.Windows.Forms.Label lblValorAnuladas;
        private UserControls.RoundedPanel_68SA pnlCardMontoAnulado;
        private FontAwesome.Sharp.IconPictureBox iconMontoAnulado;
        private System.Windows.Forms.Label lblTituloMontoAnulado;
        private System.Windows.Forms.Label lblValorMontoAnulado;
        private UserControls.RoundedPanel_68SA pnlOrigenGasto;
        private System.Windows.Forms.Label lblTituloOrigenGasto;
        private UC_DonutChart_68SA ucDonutOrigenGasto;
        private System.Windows.Forms.FlowLayoutPanel flpLeyendaOrigenGasto;
        private System.Windows.Forms.FlowLayoutPanel flpOcupacion;
        private System.Windows.Forms.FlowLayoutPanel flpHuespedes;
    }
}