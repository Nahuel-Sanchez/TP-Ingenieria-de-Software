using Service_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS
{
    public interface IUserRepository_08YS
    {
        User GetByUsername(string username);
        void BloquearUsuario(string username);
    }
}
