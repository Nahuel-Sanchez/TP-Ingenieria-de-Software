namespace GUI_08YS.Recepcionista
{
    partial class FormDisponibilidad
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
            this.tlpCuerpo = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCalendario = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.calRangoReservas = new CustomControls.CustomDateRangePanel();
            this.flpRapidos = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAyudaCalendario = new System.Windows.Forms.Label();
            this.lblCalendarioTitulo = new System.Windows.Forms.Label();
            this.pnlResumen = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.flpDisponibilidad = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAviso = new System.Windows.Forms.Label();
            this.btnContinuar = new FontAwesome.Sharp.IconButton();
            this.lblDisponibilidadTitulo = new System.Windows.Forms.Label();
            this.pnlHuespedes = new System.Windows.Forms.Panel();
            this.nudAdultos = new CustomControls.IconNumericUpDown();
            this.nudNinos = new CustomControls.IconNumericUpDown();
            this.lblNinos = new System.Windows.Forms.Label();
            this.lblAdultos = new System.Windows.Forms.Label();
            this.lblHuespedesTitulo = new System.Windows.Forms.Label();
            this.pnlFechas = new System.Windows.Forms.Panel();
            this.pnlLlegada = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblLlegadaValor = new System.Windows.Forms.Label();
            this.lblLlegadaEtiqueta = new System.Windows.Forms.Label();
            this.pnlSalida = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblSalidaValor = new System.Windows.Forms.Label();
            this.lblSalidaEtiqueta = new System.Windows.Forms.Label();
            this.pnlNoches = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblNochesDetalle = new System.Windows.Forms.Label();
            this.lblNoches = new System.Windows.Forms.Label();
            this.lblResumenTitulo = new System.Windows.Forms.Label();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pasosReserva = new GUI_08YS.UserControls.PasosReserva_68SA();
            this.tlpCuerpo.SuspendLayout();
            this.pnlCalendario.SuspendLayout();
            this.calRangoReservas.SuspendLayout();
            this.flpRapidos.SuspendLayout();
            this.pnlResumen.SuspendLayout();
            this.flpDisponibilidad.SuspendLayout();
            this.pnlHuespedes.SuspendLayout();
            this.pnlFechas.SuspendLayout();
            this.pnlLlegada.SuspendLayout();
            this.pnlSalida.SuspendLayout();
            this.pnlNoches.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpCuerpo
            // 
            this.tlpCuerpo.Controls.Add(this.pnlCalendario, 0, 0);
            this.tlpCuerpo.Controls.Add(this.pnlResumen, 1, 0);
            this.tlpCuerpo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCuerpo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.tlpCuerpo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCuerpo.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpCuerpo.ColumnCount = 2;
            this.tlpCuerpo.RowCount = 1;
            this.tlpCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCuerpo.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.tlpCuerpo.Size = new System.Drawing.Size(1464, 788);
            this.tlpCuerpo.Name = "tlpCuerpo";
            // 
            // pnlCalendario
            // 
            this.pnlCalendario.Controls.Add(this.calRangoReservas);
            this.pnlCalendario.Controls.Add(this.flpRapidos);
            this.pnlCalendario.Controls.Add(this.lblAyudaCalendario);
            this.pnlCalendario.Controls.Add(this.lblCalendarioTitulo);
            this.pnlCalendario.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlCalendario.CornerRadius = 14;
            this.pnlCalendario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCalendario.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlCalendario.Padding = new System.Windows.Forms.Padding(22, 18, 22, 16);
            this.pnlCalendario.Size = new System.Drawing.Size(1028, 776);
            this.pnlCalendario.Name = "pnlCalendario";
            // 
            // calRangoReservas
            // 
            this.calRangoReservas.RangeChanged += new System.EventHandler(this.calRangoReservas_RangeChanged);
            this.calRangoReservas.ArrowForeColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.ArrowHoverBackColor = System.Drawing.Color.FromArgb(25, 40, 80);
            this.calRangoReservas.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.calRangoReservas.BlockedBackColor = System.Drawing.Color.FromArgb(70, 25, 25);
            this.calRangoReservas.BlockedForeColor = System.Drawing.Color.FromArgb(220, 110, 110);
            this.calRangoReservas.ClearButtonColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.ClearButtonText = "Limpiar selección";
            this.calRangoReservas.DayForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.calRangoReservas.DayNameForeColor = System.Drawing.Color.FromArgb(170, 160, 150);
            this.calRangoReservas.DisabledForeColor = System.Drawing.Color.FromArgb(80, 85, 100);
            this.calRangoReservas.DisablePastDates = true;
            this.calRangoReservas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calRangoReservas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calRangoReservas.FooterForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.calRangoReservas.HoverBackColor = System.Drawing.Color.FromArgb(25, 40, 75);
            this.calRangoReservas.HoverForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.calRangoReservas.InRangeBackColor = System.Drawing.Color.FromArgb(90, 218, 165, 32);
            this.calRangoReservas.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.calRangoReservas.MaxNights = null;
            this.calRangoReservas.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.calRangoReservas.MinimumSize = new System.Drawing.Size(620, 249);
            this.calRangoReservas.MonthsVisible = 2;
            this.calRangoReservas.PreviewBackColor = System.Drawing.Color.FromArgb(50, 218, 165, 32);
            this.calRangoReservas.SelectedBackColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.SelectedForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.calRangoReservas.SeparatorColor = System.Drawing.Color.FromArgb(60, 70, 105);
            this.calRangoReservas.ShowTodayHighlight = true;
            this.calRangoReservas.Size = new System.Drawing.Size(984, 600);
            this.calRangoReservas.TitleBackColor = System.Drawing.Color.FromArgb(14, 24, 62);
            this.calRangoReservas.TitleForeColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.TodayHighlightColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.TrailingForeColor = System.Drawing.Color.FromArgb(90, 90, 100);
            this.calRangoReservas.Name = "calRangoReservas";
            // 
            // flpRapidos
            // 
            this.flpRapidos.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.flpRapidos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpRapidos.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.flpRapidos.Size = new System.Drawing.Size(984, 54);
            this.flpRapidos.WrapContents = false;
            this.flpRapidos.Name = "flpRapidos";
            // 
            // lblAyudaCalendario
            // 
            this.lblAyudaCalendario.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAyudaCalendario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyudaCalendario.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblAyudaCalendario.Size = new System.Drawing.Size(984, 30);
            this.lblAyudaCalendario.Tag = "Disponibilidad_lblAyudaCalendario";
            this.lblAyudaCalendario.Text = "Hacé clic en el día de llegada y después en el día de salida.";
            this.lblAyudaCalendario.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblAyudaCalendario.Name = "lblAyudaCalendario";
            // 
            // lblCalendarioTitulo
            // 
            this.lblCalendarioTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCalendarioTitulo.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalendarioTitulo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCalendarioTitulo.Size = new System.Drawing.Size(984, 30);
            this.lblCalendarioTitulo.Tag = "Disponibilidad_lblCalendarioTitulo";
            this.lblCalendarioTitulo.Text = "Fechas de la estadía";
            this.lblCalendarioTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCalendarioTitulo.Name = "lblCalendarioTitulo";
            // 
            // pnlResumen
            // 
            this.pnlResumen.Controls.Add(this.flpDisponibilidad);
            this.pnlResumen.Controls.Add(this.lblAviso);
            this.pnlResumen.Controls.Add(this.btnContinuar);
            this.pnlResumen.Controls.Add(this.lblDisponibilidadTitulo);
            this.pnlResumen.Controls.Add(this.pnlHuespedes);
            this.pnlResumen.Controls.Add(this.lblHuespedesTitulo);
            this.pnlResumen.Controls.Add(this.pnlFechas);
            this.pnlResumen.Controls.Add(this.lblResumenTitulo);
            this.pnlResumen.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlResumen.CornerRadius = 14;
            this.pnlResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResumen.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResumen.Padding = new System.Windows.Forms.Padding(22, 18, 22, 20);
            this.pnlResumen.Size = new System.Drawing.Size(420, 776);
            this.pnlResumen.Name = "pnlResumen";
            // 
            // flpDisponibilidad
            // 
            this.flpDisponibilidad.AutoScroll = true;
            this.flpDisponibilidad.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.flpDisponibilidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDisponibilidad.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpDisponibilidad.Size = new System.Drawing.Size(376, 300);
            this.flpDisponibilidad.WrapContents = false;
            this.flpDisponibilidad.Name = "flpDisponibilidad";
            // 
            // lblAviso
            // 
            this.lblAviso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAviso.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAviso.ForeColor = System.Drawing.Color.FromArgb(240, 200, 130);
            this.lblAviso.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.lblAviso.Size = new System.Drawing.Size(376, 54);
            this.lblAviso.Text = "-";
            this.lblAviso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAviso.Name = "lblAviso";
            // 
            // btnContinuar
            // 
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            this.btnContinuar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuar.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinuar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnContinuar.IconChar = FontAwesome.Sharp.IconChar.ArrowRight;
            this.btnContinuar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnContinuar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnContinuar.IconSize = 22;
            this.btnContinuar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnContinuar.Padding = new System.Windows.Forms.Padding(0, 0, 18, 0);
            this.btnContinuar.Size = new System.Drawing.Size(376, 52);
            this.btnContinuar.Tag = "Disponibilidad_btnContinuarDisponibilidad";
            this.btnContinuar.Text = "Elegir habitación";
            this.btnContinuar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnContinuar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnContinuar.UseVisualStyleBackColor = false;
            this.btnContinuar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinuar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnContinuar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnContinuar.Enabled = false;
            this.btnContinuar.FlatAppearance.BorderSize = 0;
            this.btnContinuar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnContinuar.Name = "btnContinuar";
            // 
            // lblDisponibilidadTitulo
            // 
            this.lblDisponibilidadTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDisponibilidadTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisponibilidadTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblDisponibilidadTitulo.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblDisponibilidadTitulo.Size = new System.Drawing.Size(376, 40);
            this.lblDisponibilidadTitulo.Tag = "Disponibilidad_lblDisponibilidad";
            this.lblDisponibilidadTitulo.Text = "Disponibilidad";
            this.lblDisponibilidadTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDisponibilidadTitulo.Name = "lblDisponibilidadTitulo";
            // 
            // pnlHuespedes
            // 
            this.pnlHuespedes.Controls.Add(this.nudAdultos);
            this.pnlHuespedes.Controls.Add(this.nudNinos);
            this.pnlHuespedes.Controls.Add(this.lblNinos);
            this.pnlHuespedes.Controls.Add(this.lblAdultos);
            this.pnlHuespedes.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlHuespedes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHuespedes.Size = new System.Drawing.Size(376, 80);
            this.pnlHuespedes.Name = "pnlHuespedes";
            // 
            // nudAdultos
            // 
            this.nudAdultos.ValueChanged += new System.EventHandler(this.nudHuespedes_ValueChanged);
            this.nudAdultos.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudAdultos.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.BorderWidth = 2;
            this.nudAdultos.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAdultos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudAdultos.IconChar = FontAwesome.Sharp.IconChar.User;
            this.nudAdultos.IconColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.IconPadding = 4;
            this.nudAdultos.IconSize = 20;
            this.nudAdultos.Location = new System.Drawing.Point(0, 26);
            this.nudAdultos.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudAdultos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudAdultos.Size = new System.Drawing.Size(182, 46);
            this.nudAdultos.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudAdultos.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudAdultos.Value = new decimal(new int[] { 2, 0, 0, 0 });
            this.nudAdultos.Name = "nudAdultos";
            // 
            // nudNinos
            // 
            this.nudNinos.ValueChanged += new System.EventHandler(this.nudHuespedes_ValueChanged);
            this.nudNinos.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudNinos.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.BorderWidth = 2;
            this.nudNinos.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudNinos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudNinos.IconChar = FontAwesome.Sharp.IconChar.Child;
            this.nudNinos.IconColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.IconPadding = 4;
            this.nudNinos.IconSize = 20;
            this.nudNinos.Location = new System.Drawing.Point(194, 26);
            this.nudNinos.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudNinos.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudNinos.Size = new System.Drawing.Size(182, 46);
            this.nudNinos.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudNinos.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudNinos.Value = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudNinos.Name = "nudNinos";
            // 
            // lblNinos
            // 
            this.lblNinos.AutoSize = true;
            this.lblNinos.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblNinos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNinos.Tag = "Disponibilidad_lblNinos";
            this.lblNinos.Text = "Niños";
            this.lblNinos.Location = new System.Drawing.Point(196, 4);
            this.lblNinos.Name = "lblNinos";
            // 
            // lblAdultos
            // 
            this.lblAdultos.AutoSize = true;
            this.lblAdultos.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblAdultos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdultos.Tag = "Disponibilidad_lblAdultos";
            this.lblAdultos.Text = "Adultos";
            this.lblAdultos.Location = new System.Drawing.Point(2, 4);
            this.lblAdultos.Name = "lblAdultos";
            // 
            // lblHuespedesTitulo
            // 
            this.lblHuespedesTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHuespedesTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHuespedesTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblHuespedesTitulo.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.lblHuespedesTitulo.Size = new System.Drawing.Size(376, 42);
            this.lblHuespedesTitulo.Tag = "Disponibilidad_lblHuespedes";
            this.lblHuespedesTitulo.Text = "Huéspedes";
            this.lblHuespedesTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHuespedesTitulo.Name = "lblHuespedesTitulo";
            // 
            // pnlFechas
            // 
            this.pnlFechas.Controls.Add(this.pnlLlegada);
            this.pnlFechas.Controls.Add(this.pnlSalida);
            this.pnlFechas.Controls.Add(this.pnlNoches);
            this.pnlFechas.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlFechas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFechas.Size = new System.Drawing.Size(376, 150);
            this.pnlFechas.Name = "pnlFechas";
            // 
            // pnlLlegada
            // 
            this.pnlLlegada.Controls.Add(this.lblLlegadaValor);
            this.pnlLlegada.Controls.Add(this.lblLlegadaEtiqueta);
            this.pnlLlegada.BackColor = System.Drawing.Color.FromArgb(14, 24, 62);
            this.pnlLlegada.CornerRadius = 10;
            this.pnlLlegada.Location = new System.Drawing.Point(0, 0);
            this.pnlLlegada.Size = new System.Drawing.Size(182, 66);
            this.pnlLlegada.Name = "pnlLlegada";
            // 
            // lblLlegadaValor
            // 
            this.lblLlegadaValor.AutoEllipsis = true;
            this.lblLlegadaValor.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLlegadaValor.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblLlegadaValor.Location = new System.Drawing.Point(12, 30);
            this.lblLlegadaValor.Size = new System.Drawing.Size(162, 26);
            this.lblLlegadaValor.Text = "-";
            this.lblLlegadaValor.UseMnemonic = false;
            this.lblLlegadaValor.Name = "lblLlegadaValor";
            // 
            // lblLlegadaEtiqueta
            // 
            this.lblLlegadaEtiqueta.AutoSize = true;
            this.lblLlegadaEtiqueta.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLlegadaEtiqueta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblLlegadaEtiqueta.Location = new System.Drawing.Point(12, 9);
            this.lblLlegadaEtiqueta.Tag = "Disponibilidad_lblLlegada";
            this.lblLlegadaEtiqueta.Text = "LLEGADA";
            this.lblLlegadaEtiqueta.Name = "lblLlegadaEtiqueta";
            // 
            // pnlSalida
            // 
            this.pnlSalida.Controls.Add(this.lblSalidaValor);
            this.pnlSalida.Controls.Add(this.lblSalidaEtiqueta);
            this.pnlSalida.BackColor = System.Drawing.Color.FromArgb(14, 24, 62);
            this.pnlSalida.CornerRadius = 10;
            this.pnlSalida.Location = new System.Drawing.Point(194, 0);
            this.pnlSalida.Size = new System.Drawing.Size(182, 66);
            this.pnlSalida.Name = "pnlSalida";
            // 
            // lblSalidaValor
            // 
            this.lblSalidaValor.AutoEllipsis = true;
            this.lblSalidaValor.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalidaValor.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblSalidaValor.Location = new System.Drawing.Point(12, 30);
            this.lblSalidaValor.Size = new System.Drawing.Size(162, 26);
            this.lblSalidaValor.Text = "-";
            this.lblSalidaValor.UseMnemonic = false;
            this.lblSalidaValor.Name = "lblSalidaValor";
            // 
            // lblSalidaEtiqueta
            // 
            this.lblSalidaEtiqueta.AutoSize = true;
            this.lblSalidaEtiqueta.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalidaEtiqueta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSalidaEtiqueta.Location = new System.Drawing.Point(12, 9);
            this.lblSalidaEtiqueta.Tag = "Disponibilidad_lblSalida";
            this.lblSalidaEtiqueta.Text = "SALIDA";
            this.lblSalidaEtiqueta.Name = "lblSalidaEtiqueta";
            // 
            // pnlNoches
            // 
            this.pnlNoches.Controls.Add(this.lblNochesDetalle);
            this.pnlNoches.Controls.Add(this.lblNoches);
            this.pnlNoches.BackColor = System.Drawing.Color.FromArgb(14, 24, 62);
            this.pnlNoches.CornerRadius = 10;
            this.pnlNoches.Location = new System.Drawing.Point(0, 78);
            this.pnlNoches.Size = new System.Drawing.Size(376, 64);
            this.pnlNoches.Name = "pnlNoches";
            // 
            // lblNochesDetalle
            // 
            this.lblNochesDetalle.AutoEllipsis = true;
            this.lblNochesDetalle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNochesDetalle.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblNochesDetalle.Location = new System.Drawing.Point(132, 12);
            this.lblNochesDetalle.Size = new System.Drawing.Size(232, 40);
            this.lblNochesDetalle.Text = "-";
            this.lblNochesDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNochesDetalle.UseMnemonic = false;
            this.lblNochesDetalle.Name = "lblNochesDetalle";
            // 
            // lblNoches
            // 
            this.lblNoches.AutoEllipsis = true;
            this.lblNoches.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoches.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblNoches.Location = new System.Drawing.Point(10, 12);
            this.lblNoches.Size = new System.Drawing.Size(120, 40);
            this.lblNoches.Text = "—";
            this.lblNoches.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNoches.UseMnemonic = false;
            this.lblNoches.Name = "lblNoches";
            // 
            // lblResumenTitulo
            // 
            this.lblResumenTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResumenTitulo.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumenTitulo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblResumenTitulo.Size = new System.Drawing.Size(376, 40);
            this.lblResumenTitulo.Tag = "Disponibilidad_lblResumen";
            this.lblResumenTitulo.Text = "Tu estadía";
            this.lblResumenTitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblResumenTitulo.Name = "lblResumenTitulo";
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.pasosReserva);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Size = new System.Drawing.Size(1464, 72);
            this.pnlEncabezado.Name = "pnlEncabezado";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSubtitulo.Tag = "Disponibilidad_lblSubtitulo";
            this.lblSubtitulo.Text = "Elegí las fechas y cuántas personas se alojan; después vas a elegir la habitación.";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSubtitulo.Name = "lblSubtitulo";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Size = new System.Drawing.Size(944, 38);
            this.lblTitulo.Tag = "Disponibilidad_lblTituloDisponibilidad";
            this.lblTitulo.Text = "Nueva reserva";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Name = "lblTitulo";
            // 
            // pasosReserva
            // 
            this.pasosReserva.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pasosReserva.Dock = System.Windows.Forms.DockStyle.Right;
            this.pasosReserva.Size = new System.Drawing.Size(520, 72);
            this.pasosReserva.Name = "pasosReserva";
            // 
            // FormDisponibilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1520, 910);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Padding = new System.Windows.Forms.Padding(28, 18, 28, 20);
            this.Text = "Nueva reserva";
            this.Controls.Add(this.tlpCuerpo);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormDisponibilidad";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlNoches.ResumeLayout(false);
            this.pnlSalida.ResumeLayout(false);
            this.pnlSalida.PerformLayout();
            this.pnlLlegada.ResumeLayout(false);
            this.pnlLlegada.PerformLayout();
            this.pnlFechas.ResumeLayout(false);
            this.pnlHuespedes.ResumeLayout(false);
            this.pnlHuespedes.PerformLayout();
            this.flpDisponibilidad.ResumeLayout(false);
            this.flpDisponibilidad.PerformLayout();
            this.pnlResumen.ResumeLayout(false);
            this.flpRapidos.ResumeLayout(false);
            this.flpRapidos.PerformLayout();
            this.calRangoReservas.ResumeLayout(false);
            this.pnlCalendario.ResumeLayout(false);
            this.tlpCuerpo.ResumeLayout(false);
            this.tlpCuerpo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpCuerpo;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlCalendario;
        private CustomControls.CustomDateRangePanel calRangoReservas;
        private System.Windows.Forms.FlowLayoutPanel flpRapidos;
        private System.Windows.Forms.Label lblAyudaCalendario;
        private System.Windows.Forms.Label lblCalendarioTitulo;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlResumen;
        private System.Windows.Forms.FlowLayoutPanel flpDisponibilidad;
        private System.Windows.Forms.Label lblAviso;
        private FontAwesome.Sharp.IconButton btnContinuar;
        private System.Windows.Forms.Label lblDisponibilidadTitulo;
        private System.Windows.Forms.Panel pnlHuespedes;
        private CustomControls.IconNumericUpDown nudAdultos;
        private CustomControls.IconNumericUpDown nudNinos;
        private System.Windows.Forms.Label lblNinos;
        private System.Windows.Forms.Label lblAdultos;
        private System.Windows.Forms.Label lblHuespedesTitulo;
        private System.Windows.Forms.Panel pnlFechas;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlLlegada;
        private System.Windows.Forms.Label lblLlegadaValor;
        private System.Windows.Forms.Label lblLlegadaEtiqueta;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlSalida;
        private System.Windows.Forms.Label lblSalidaValor;
        private System.Windows.Forms.Label lblSalidaEtiqueta;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlNoches;
        private System.Windows.Forms.Label lblNochesDetalle;
        private System.Windows.Forms.Label lblNoches;
        private System.Windows.Forms.Label lblResumenTitulo;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
        private GUI_08YS.UserControls.PasosReserva_68SA pasosReserva;
    }
}
