using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_08YS
{
    public enum TipoDocumento
    {
        DNI,
        Pasaporte,
        Cedula,
        Otro
    }

    public class Huesped_68SA
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string Nacionalidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public int Edad
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        //para listado
        public int CantidadReservas { get; set; }
    }

    public class HuespedFiltro_68SA
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public TipoDocumento? TipoDocumento { get; set; }
        public string Nacionalidad { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime? NacimientoDesde { get; set; }
        public DateTime? NacimientoHasta { get; set; }
        public int Maximo { get; set; } // tope de filas: lo fija el BLL (HuespedBLL_68SA.LimiteListado)
    }
}
