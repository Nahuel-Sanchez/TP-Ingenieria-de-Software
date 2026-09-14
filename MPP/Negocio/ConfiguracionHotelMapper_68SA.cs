using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_08YS.Negocio
{
    public static class ConfiguracionHotelMapper_68SA
    {
        public static ConfiguracionHotel_68SA FromDataRow(DataRow row)
        {
            return new ConfiguracionHotel_68SA
            {
                Id = Convert.ToInt32(row["ConfiguracionID"]),
                HoraCheckIn = (TimeSpan)row["HoraCheckIn"],
                HoraCheckOut = (TimeSpan)row["HoraCheckOut"],
                GraciaNoShowHoras = Convert.ToDecimal(row["GraciaNoShowHoras"])
            };
        }
    }
}
