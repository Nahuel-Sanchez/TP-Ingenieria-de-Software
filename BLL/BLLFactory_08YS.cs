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
            
    }
}
