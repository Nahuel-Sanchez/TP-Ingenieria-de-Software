using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS;
using DAL_08YS;
using System.Security.Authentication;

namespace BLL_08YS
{
    public class UserBLL_08YS
    {
        private readonly IUserRepository_08YS userRepository;

        public UserBLL_08YS(IUserRepository_08YS userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Login(string username, string password)
        {
            User user = userRepository.GetByUsername(username) ?? throw new UserNoRegistradoException_08YS();

            if (user.Bloqueado)
            {

                throw new UserBloqueadoException_08YS();
            }

            bool valido = Encriptador.Verificar(password, user.Hash, user.Salt);
            if (!valido)
            {

                throw new InvalidCredentialException();
            }

            return user;
        }

        public void BloquearUsuario(string username)
        {
            userRepository.BloquearUsuario(username);
        }
    }
}
