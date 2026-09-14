using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP_08YS.Negocio;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlConfiguracionHotelRepository_68SA : Connection_08YS, IConfiguracionHotelRepository_68SA
    {
        public SqlConfiguracionHotelRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public ConfiguracionHotel_68SA GetConfiguracion()
        {
            DataTable dt = GetDataTable(
                "SELECT TOP 1 ConfiguracionID, HoraCheckIn, HoraCheckOut, GraciaNoShowHoras FROM ConfiguracionHotel");

            return dt.Rows.Count > 0 ? ConfiguracionHotelMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public void Actualizar(ConfiguracionHotel_68SA configuracion)
        {
            ExecuteNonQuery(
                "UPDATE ConfiguracionHotel SET HoraCheckIn = @HoraCheckIn, HoraCheckOut = @HoraCheckOut, GraciaNoShowHoras = @Gracia WHERE ConfiguracionID = @Id",
                new[]
                {
                    Param("@HoraCheckIn", configuracion.HoraCheckIn),
                    Param("@HoraCheckOut", configuracion.HoraCheckOut),
                    Param("@Gracia", configuracion.GraciaNoShowHoras),
                    Param("@Id", configuracion.Id)
                });
        }
    }
}
