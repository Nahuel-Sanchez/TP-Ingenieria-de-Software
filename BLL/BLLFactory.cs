using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class BLLFactory
    {
        public static UserBLL CreateUserBLL()
        {
            IUserRepository repo = new SqlUserRepository();
            return new UserBLL(repo);
        }
    }
}
