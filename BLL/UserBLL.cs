using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using DAL;
using BE;
using BE.Usuarios;

namespace BLL
{
    public class UserBLL
    {
        private readonly IUserRepository userRepository;
        public User Login(string email, string password)
        {
            User user = userRepository.GetByEmail(email) ?? throw new UserNoRegistradoException();

            bool valido = Encriptador.Verificar(password, user.Hash, user.Salt);

            if (!valido)
                throw new Exception("La contraseña es incorrecta.");

            return user;
        }
    }
}
