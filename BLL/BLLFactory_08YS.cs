using DAL_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS
{
    public static class BLLFactory_08YS
    {
        public static UserBLL_08YS CreateUserBLL()
        {
            IUserRepository_08YS repo = new SqlUserRepository_08YS();
            return new UserBLL_08YS(repo);
        }
    }
}
