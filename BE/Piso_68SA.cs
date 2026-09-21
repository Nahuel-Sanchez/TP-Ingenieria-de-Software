using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_08YS
{
    public class Piso_68SA
    {
        public int PisoId { get; set; }
        public int Numero { get; set; }
        public string Nombre { get; set; }

        //Para listado
        public int CantidadHabitaciones { get; set; }
    }

    public class PisoFiltro_68SA
    {
        public string Numero { get; set; }
        public string Nombre { get; set; }
    }
}

