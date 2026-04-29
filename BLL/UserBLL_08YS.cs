using DAL_08YS;
using DAL_08YS.Repositories_Interfaces;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

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
        public void CrearUsuario(int dni, string nombre, string apellido, string email, UserRole rol)
        {
            if (_usuariosLocal.Any(u => u.DNI == dni))
                throw new Exception("El DNI ya existe.");

            // 2. Composición de Login y Password
            string login = dni.ToString() + nombre.Trim();
            string passwordDefault = dni.ToString() + apellido.Trim();

            // 3. SEGURIDAD: Aquí es donde corresponde el Hash y Salt
            string hash, salt;
            Encriptador.CrearHash(passwordDefault, out hash, out salt);

            // 4. Armado del objeto
            User nuevo = new User(
                login,
                dni,
                rol,
                nombre,
                apellido,
                email,
                hash,
                salt,
                "", // celular
                "", // direccion
                false // bloqueado
            );

           
            // 5. Persistencia (a la lista local)
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
            var user = _usuariosLocal.FirstOrDefault(u => u.Username==username);
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
        public void ModificarUsuario(string username, string nuevoEmail, UserRole nuevoRol)
        {
            // 1. Buscar al usuario existente
            var user = _usuariosLocal.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            // 2. Aplicar los cambios (Solo Email y Rol)
            user.Email = nuevoEmail;
            user.Rol = nuevoRol;

            // 3. Registrar en Bitácora
            //_bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Modificación de perfil (Email/Rol)",Criticidad.Alto);
        }
        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }
    }
}
