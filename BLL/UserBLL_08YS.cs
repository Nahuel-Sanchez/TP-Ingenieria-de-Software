using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_08YS;
using DAL_08YS.Repositories_Interfaces;
using System.Security.Authentication;
using Service_08YS;

namespace BLL_08YS
{
    public class UserBLL_08YS
    {
        private readonly IUserRepository_08YS userRepository;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        public static List<User> _usuariosLocal = new List<User>()
    {
        new User("admin01", 12345678, UserRole.Admin, "Juan", "Perez", "juan@test.com", "h", "s", "1122", "Calle 123", false),
        new User("user88", 99999999, UserRole.Basico, "Marta", "Gomez", "marta@test.com", "h", "s", "3344", "Av. Siempreviva", true) // BLOQUEADO
    };
        public UserBLL_08YS(IUserRepository_08YS userRepository, BitacoraBLL_08YS bitacoraBll)
        {
            this.userRepository = userRepository;
            _bitacoraBll = bitacoraBll;
        }
        public void CrearUsuario(User nuevo)
        {
            if (_usuariosLocal.Any(u => u.DNI == nuevo.DNI))
            {
                throw new Exception("Ya existe un usuario registrado con ese DNI.");
            }

            // 3. Persistencia (en tu caso, a la lista local)
            _usuariosLocal.Add(nuevo);

            //userRepository.AddUser(nuevo);
            //_bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Creacion exitosa", Criticidad.Alto);
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
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Inicio de sesión exitoso", Criticidad.Alto);

            return user;
        }

        public void BloquearUsuario(string username)
        {
            userRepository.BloquearUsuario(username);
        }

        public void DesbloquearUsuario(string username)
        {
            //userRepository.DesbloquearUsuario(dni);
            var user = _usuariosLocal.FirstOrDefault(u => u.Username==u.Username);
            if (user == null)
                throw new Exception("No se encontró el usuario con el login especificado.");
            string passwordDefault = user.DNI.ToString() + user.Apellido.Trim();

                // 3. Generar nuevas credenciales
                string nuevoHash, nuevoSalt;
                Encriptador.CrearHash(passwordDefault, out nuevoHash, out nuevoSalt);

                // 4. Actualizar el estado del usuario
                user.Hash = nuevoHash;
                user.Salt = nuevoSalt;
                user.Bloqueado = false;
            
            
            //_bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Desbloqueo exitoso", Criticidad.Alto);


        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }
    }
}
