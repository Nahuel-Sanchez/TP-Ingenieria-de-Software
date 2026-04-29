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
        List<User> GetAll();

        User GetByUsername(string username);

        void LockOut(string username);

        void Unlock(string username);

        void Create(User user);

        bool Exists(int DNI);

        void Modify(User user, string login);
    }
}
