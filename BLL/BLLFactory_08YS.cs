using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL_08YS;

namespace BLL_08YS
{
    public static class BLLFactory_08YS
    {
        public static UserBLL_08YS CreateUserBLL()
        {
            IDbFactory_08YS factory = new SqlDbFactory_08YS();
            IUserRepository_08YS repo = new SqlUserRepository_08YS(factory);
            return new UserBLL_08YS(repo);
        }


        public static UserBLL_08YS CreateUserBLLMock()
        {
            var factory = new MockDbFactory_08YS
            {
                MockData = BuildMockUserTable() // tabla de prueba
            };
            IUserRepository_08YS repo = new SqlUserRepository_08YS(factory);
            // Mismo repositorio, distinto factory. No se usa MockUserRepository
            return new UserBLL_08YS(repo);
        }

        private static DataTable BuildMockUserTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("Username"); dt.Columns.Add("DNI");
            dt.Columns.Add("Nombre"); dt.Columns.Add("Apellido");
            dt.Columns.Add("Hash"); dt.Columns.Add("Salt");
            dt.Columns.Add("Email"); dt.Columns.Add("Celular");
            dt.Columns.Add("Direccion"); dt.Columns.Add("Bloqueado", typeof(bool));
            return dt;
        }
    }
}
