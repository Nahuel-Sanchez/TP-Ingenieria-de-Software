using Service_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories
{
    public interface IUserRepository_08YS
    {
        #region Consultas

        List<User_08YS> GetAll();

        User_08YS GetByUsername(string username);

        bool Exists(int DNI);

        #endregion

        #region Operaciones

        void LockOut(string username);

        void Unlock(string username);

        void Create(User_08YS user);

        void Modify(User_08YS user, string login);

        void UpdateState(string username, bool nuevoEstado);

        void UpdatePassword(string username, string hashNuevo, string saltNuevo);

        void UpdateLanguage(string username, string language);

        #endregion
    }
}
