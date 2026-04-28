using Service_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Repositories_Interfaces
{
    public interface IUserRepository_08YS
    {
        User GetByUsername(string username);
        void BloquearUsuario(string username);

        List<User> GetAll();

        void DesbloquearUsuario(string username);

        void AddUser(User user);
    }
}
