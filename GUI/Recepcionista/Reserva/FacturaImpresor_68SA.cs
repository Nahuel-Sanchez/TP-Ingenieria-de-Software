using BE_08YS;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Punto de entrada anterior para imprimir la factura de una reserva. Ahora abre el visor de documentos
    /// (factura A4 profesional + comprobante), desde donde se imprime, se guarda en PDF o se envía por mail.
    /// </summary>
    public class FacturaImpresor_68SA
    {
        private readonly Reserva_68SA _reserva;

        public FacturaImpresor_68SA(Reserva_68SA reserva, List<Pago_68SA> pagos)
        {
            _reserva = reserva;
        }

        public void Imprimir(IWin32Window propietario = null)
        {
            if (_reserva == null) return;
            FormDocumentosReserva_68SA.Mostrar(propietario, _reserva.Id, indiceInicial: 1);
        }
    }
}
