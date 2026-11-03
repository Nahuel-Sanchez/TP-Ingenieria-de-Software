using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using Service_08YS.Entities.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS;
using Service_08YS.Entities.Acceso;

namespace BLL_08YS.Negocio
{
    public class FacturaBLL_68SA
    {
        #region Campos

        private readonly IFacturaRepository_68SA _facturaRepo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        #endregion

        #region Constructor

        public FacturaBLL_68SA(IFacturaRepository_68SA facturaRepo, BitacoraBLL_08YS bitacoraBll)
        {
            _facturaRepo = facturaRepo;
            _bitacoraBll = bitacoraBll;
        }

        #endregion

        #region Consultas

        public List<Factura_68SA> GetByReserva(int reservaId)
        {
            return _facturaRepo.GetByReserva(reservaId);
        }

        public decimal GetTotalPagado(int reservaId)
        {
            return _facturaRepo.GetTotalPagado(reservaId);
        }

        #endregion

        #region Operaciones

        public int Registrar(Factura_68SA factura)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.Cobrar);

            if (factura.Monto <= 0)
                throw new ArgumentException("El monto de la factura debe ser mayor a cero.");

            factura.FechaEmision = DateTime.Now;
            int nuevoId = _facturaRepo.Registrar(factura);

            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.FacturaRegistrada, targetUsername: factura.ReservaId.ToString());

            return nuevoId;
        }

        #endregion

        #region Validación de la tarjeta

        // Validaciones de los datos de la tarjeta con la que se cobra (antes estaban en la GUI, en TarjetaValidador_68SA).
        // No van a la base: solo revisan el formato antes de registrar la factura.

        public static string LimpiarNumero(string numero)
        {
            return new string((numero ?? "").Where(char.IsDigit).ToArray());
        }

        // Algoritmo de Luhn: recorriendo de derecha a izquierda, se duplica uno de cada dos dígitos;
        // si el doble supera 9, se le resta 9. La suma total tiene que ser múltiplo de 10.
        public static bool EsNumeroTarjetaValido(string numero)
        {
            string limpio = LimpiarNumero(numero);
            if (limpio.Length < 13 || limpio.Length > 19) return false;

            int suma = 0;
            bool duplicar = false;

            for (int i = limpio.Length - 1; i >= 0; i--)
            {
                int digito = limpio[i] - '0';

                if (duplicar)
                {
                    digito *= 2;
                    if (digito > 9) digito -= 9;
                }

                suma += digito;
                duplicar = !duplicar;
            }

            return suma % 10 == 0;
        }

        public static string DetectarMarcaTarjeta(string numero)
        {
            string limpio = LimpiarNumero(numero);
            if (limpio.Length == 0) return "-";

            if (limpio.StartsWith("4"))
                return "Visa";

            if (limpio.Length >= 2 && int.TryParse(limpio.Substring(0, 2), out int prefijo2) && prefijo2 >= 51 && prefijo2 <= 55)
                return "Mastercard";

            if (limpio.Length >= 4 && int.TryParse(limpio.Substring(0, 4), out int prefijo4) && prefijo4 >= 2221 && prefijo4 <= 2720)
                return "Mastercard";

            if (limpio.StartsWith("34") || limpio.StartsWith("37"))
                return "American Express";

            if (limpio.StartsWith("6011") || limpio.StartsWith("65"))
                return "Discover";

            return "Desconocida";
        }

        public static int LongitudCvvEsperada(string marca)
        {
            return marca == "American Express" ? 4 : 3;
        }

        public static bool EsCvvValido(string cvv, string marca)
        {
            string limpio = LimpiarNumero(cvv);
            return limpio.Length == LongitudCvvEsperada(marca);
        }

        public static bool EsVencimientoValido(int mes, int anioDosDigitos, out string error)
        {
            error = null;

            if (mes < 1 || mes > 12)
            {
                error = "vencMesInvalido";
                return false;
            }

            int anioCompleto = 2000 + anioDosDigitos;
            DateTime finDeMes = new DateTime(anioCompleto, mes, 1).AddMonths(1).AddDays(-1);

            if (finDeMes < DateTime.Today)
            {
                error = "vencExpirado";
                return false;
            }

            return true;
        }

        #endregion
    }
}