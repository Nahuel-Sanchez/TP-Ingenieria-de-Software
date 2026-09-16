using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormReservas_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly Action<Form> _openChildForm;
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly PagoBLL_68SA _pagoBLL = BLLFactory_08YS.CreatePagoBLL();

        public FormReservas_68SA(Action<Form> openChildForm)
        {
            _openChildForm = openChildForm;
            InitializeComponent();

            cmbEstadoFiltro.Items.Add("Todos");
            cmbEstadoFiltro.Items.AddRange(Enum.GetValues(typeof(EstadoReserva)).Cast<object>().ToArray());
            cmbEstadoFiltro.SelectedIndex = 0;

            btnTabCalendario.Click += (s, e) => CambiarTab(false);
            btnTabLista.Click += (s, e) => CambiarTab(true);

            btnCrearReserva.Click += (s, e) => _openChildForm(new FormDisponibilidad(_openChildForm));

            dtpFechaDesde.ValueChanged += (s, e) => CargarLista();
            dtpFechaHasta.ValueChanged += (s, e) => CargarLista();
            cmbEstadoFiltro.SelectedIndexChanged += (s, e) => CargarLista();
            btnLimpiarFiltros.Click += (s, e) => LimpiarFiltros();

            CargarLista();
        }

        private void CambiarTab(bool lista)
        {
            pnlLista.Visible = lista;
            pnlCalendario.Visible = !lista;

            btnTabLista.BackColor = lista ? Color.FromArgb(10, 18, 50) : Color.FromArgb(5, 10, 40);
            btnTabLista.ForeColor = lista ? Color.Goldenrod : Color.FromArgb(160, 165, 180);
            btnTabLista.IconColor = btnTabLista.ForeColor;
            btnTabLista.Font = new Font("Segoe UI", 10F, lista ? FontStyle.Bold : FontStyle.Regular);

            btnTabCalendario.BackColor = !lista ? Color.FromArgb(10, 18, 50) : Color.FromArgb(5, 10, 40);
            btnTabCalendario.ForeColor = !lista ? Color.Goldenrod : Color.FromArgb(160, 165, 180);
            btnTabCalendario.IconColor = btnTabCalendario.ForeColor;
            btnTabCalendario.Font = new Font("Segoe UI", 10F, !lista ? FontStyle.Bold : FontStyle.Regular);
        }

        private void LimpiarFiltros()
        {
            dtpFechaDesde.Value = null;
            dtpFechaHasta.Value = null;
            cmbEstadoFiltro.SelectedIndex = 0;
            CargarLista();
        }

        private void CargarLista()
        {
            EstadoReserva? estadoFiltro = cmbEstadoFiltro.SelectedItem is EstadoReserva estado ? estado : (EstadoReserva?)null;

            var reservas = _reservaBLL.GetAll(dtpFechaDesde.Value, dtpFechaHasta.Value, estadoFiltro);

            ActualizarTarjetas(reservas);

            flpFilasReservas.Controls.Clear();
            foreach (var reserva in reservas)
            {
                decimal adelanto = _pagoBLL.GetTotalPagado(reserva.Id);
                flpFilasReservas.Controls.Add(CrearFilaReserva(reserva, adelanto));
            }
        }

        private void ActualizarTarjetas(List<Reserva_68SA> reservas)
        {
            var vigentes = reservas.Where(r => r.Estado == EstadoReserva.Confirmada || r.Estado == EstadoReserva.EnCurso).ToList();
            var pendientes = reservas.Where(r => r.Estado == EstadoReserva.Confirmada).ToList();
            var ocupadas = reservas.Where(r => r.Estado == EstadoReserva.EnCurso).ToList();
            var canceladas = reservas.Where(r => r.Estado == EstadoReserva.Cancelada).ToList();

            lblMontoVigentes.Text = $"${vigentes.Sum(r => r.MontoTotal):N2}";
            lblCantVigentes.Text = $"Cantidad: {vigentes.Count}";

            lblMontoPendientes.Text = $"${pendientes.Sum(r => r.MontoTotal):N2}";
            lblCantPendientes.Text = $"Cantidad: {pendientes.Count}";

            lblMontoOcupadas.Text = $"${ocupadas.Sum(r => r.MontoTotal):N2}";
            lblCantOcupadas.Text = $"Cantidad: {ocupadas.Count}";

            lblMontoCanceladas.Text = $"${canceladas.Sum(r => r.MontoTotal):N2}";
            lblCantCanceladas.Text = $"Cantidad: {canceladas.Count}";
        }

        private Panel CrearFilaReserva(Reserva_68SA reserva, decimal adelanto)
        {
            var fila = new RoundedPanel_68SA
            {
                Size = new Size(1240, 56),
                Margin = new Padding(0, 0, 0, 6),
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 8
            };

            AgregarLabel(fila, $"#{reserva.Id}", 10, 8, 50, Color.WhiteSmoke, bold: true);

            AgregarLabel(fila, $"{reserva.Titular.Nombre} {reserva.Titular.Apellido}", 70, 6, 170, Color.WhiteSmoke, bold: true);
            AgregarLabel(fila, reserva.Titular.Documento, 70, 26, 170, Color.FromArgb(160, 165, 180), pequena: true);

            AgregarLabel(fila, reserva.Habitacion.NroHabitacion, 250, 8, 60, Color.WhiteSmoke);

            AgregarLabel(fila, reserva.UsuarioRegistroNombre ?? "-", 320, 6, 150, Color.WhiteSmoke, pequena: true);
            AgregarLabel(fila, reserva.FechaRegistro == default ? "-" : reserva.FechaRegistro.ToString("dd/MM/yyyy HH:mm"), 320, 26, 150, Color.FromArgb(160, 165, 180), pequena: true);

            AgregarLabel(fila, reserva.FechaIngreso.ToString("dd/MM/yyyy"), 480, 8, 110, Color.WhiteSmoke, pequena: true);
            AgregarLabel(fila, reserva.FechaEgreso.ToString("dd/MM/yyyy"), 600, 8, 110, Color.WhiteSmoke, pequena: true);

            AgregarLabel(fila, $"${reserva.MontoTotal:N2}", 720, 8, 90, Color.WhiteSmoke, pequena: true);
            AgregarLabel(fila, $"${adelanto:N2}", 820, 8, 90, Color.FromArgb(76, 217, 100), pequena: true);

            decimal saldo = Math.Max(0, reserva.MontoTotal - adelanto);
            AgregarLabel(fila, $"${saldo:N2}", 920, 8, 90, saldo > 0 ? Color.FromArgb(235, 90, 90) : Color.FromArgb(160, 165, 180), pequena: true);

            var (textoEstado, colorEstado) = TextoYColorEstadoReserva(reserva.Estado);
            AgregarLabel(fila, textoEstado, 1020, 8, 100, colorEstado, bold: true);

            var btnEditar = CrearBotonAccion(IconChar.PenToSquare, 1130);
            btnEditar.Click += (s, e) => MessageBox.Show("Editar reserva — todavía no implementado.", "Pendiente",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            fila.Controls.Add(btnEditar);

            var btnImprimir = CrearBotonAccion(IconChar.Print, 1170);
            btnImprimir.Click += (s, e) => MessageBox.Show("Imprimir / generar comprobante — todavía no implementado.", "Pendiente",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            fila.Controls.Add(btnImprimir);

            if (reserva.Estado == EstadoReserva.Confirmada)
            {
                var btnCancelar = CrearBotonAccion(IconChar.Ban, 1210);
                btnCancelar.Click += (s, e) => CancelarReserva(reserva.Id);
                fila.Controls.Add(btnCancelar);
            }

            return fila;
        }

        private void CancelarReserva(int reservaId)
        {
            var confirmacion = MessageBox.Show("¿Cancelar esta reserva?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _reservaBLL.Cancelar(reservaId);
                CargarLista();
            }
            catch (EstadoReservaInvalidoException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo cancelar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void AgregarLabel(Control contenedor, string texto, int x, int y, int width, Color color, bool bold = false, bool pequena = false)
        {
            var label = new Label
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(width, 20),
                ForeColor = color,
                Font = new Font("Segoe UI", pequena ? 8F : 9F, bold ? FontStyle.Bold : FontStyle.Regular),
                AutoEllipsis = true
            };
            contenedor.Controls.Add(label);
        }

        private static IconButton CrearBotonAccion(IconChar icono, int x)
        {
            return new IconButton
            {
                IconChar = icono,
                IconColor = Color.Gold,
                IconSize = 18,
                BackColor = Color.FromArgb(5, 15, 45),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(x, 10),
                Size = new Size(34, 34),
                UseVisualStyleBackColor = false
            };
        }

        private static (string Texto, Color Color) TextoYColorEstadoReserva(EstadoReserva estado)
        {
            switch (estado)
            {
                case EstadoReserva.Confirmada: return ("Confirmada", Color.FromArgb(90, 155, 235));
                case EstadoReserva.EnCurso: return ("En curso", Color.FromArgb(76, 217, 100));
                case EstadoReserva.Finalizada: return ("Finalizada", Color.FromArgb(160, 165, 180));
                case EstadoReserva.Cancelada: return ("Cancelada", Color.FromArgb(235, 90, 90));
                default: return (estado.ToString(), Color.WhiteSmoke);
            }
        }

        public void UpdateIdioma()
        {
            // Pendiente junto con el resto de las traducciones de estos forms.
        }
    }
}