using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_08YS;
using System.Security.Authentication;
using Service_08YS;
using Service;
using BLL;

namespace BLL_08YS
{
    public class UserBLL_08YS
    {
        private readonly IUserRepository_08YS userRepository;
        private readonly BLLBitacora_08YS _bitacoraBll;
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
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Inicio de sesión exitoso", Criticidad.Bajo);

            return user;
        }

        public void BloquearUsuario(string username)
        {
            userRepository.BloquearUsuario(username);
        }
    }
}
