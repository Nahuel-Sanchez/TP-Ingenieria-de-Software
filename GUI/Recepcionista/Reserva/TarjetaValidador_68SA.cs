using System;
using System.Linq;

namespace GUI_08YS.Recepcionista
{
    public static class TarjetaValidador_68SA
    {
        public static string LimpiarNumero(string numero)
        {
            return new string((numero ?? "").Where(char.IsDigit).ToArray());
        }

        // Algoritmo de Luhn: recorriendo de derecha a izquierda, se duplica uno de cada dos dígitos;
        // si el doble supera 9, se le resta 9. La suma total tiene que ser múltiplo de 10.
        public static bool EsNumeroValido(string numero)
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

        public static string DetectarMarca(string numero)
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
    }
}