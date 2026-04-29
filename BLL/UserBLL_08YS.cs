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
            if (userRepository.Exists(dni))
                throw new InvalidOperationException("Ya existe un usuario registrado con ese DNI.");

            string username = dni.ToString() + nombre;
            string passwordDefault = dni.ToString() + apellido;
            Encriptador.CrearHash(passwordDefault, out string hash, out string salt);

            User nuevo = new User(username, dni, rol, nombre, apellido, email, hash, salt, "celular", "Direccion", false);

            userRepository.Create(nuevo);
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Usuario creado", Criticidad.Alto);
        }

        public User Login(string username, string password)
        {
            User user = userRepository.GetByUsername(username) ?? throw new UserNoRegistradoException_08YS();

            if (user.Bloqueado)
                throw new UserBloqueadoException_08YS();
            
            bool valido = Encriptador.Verificar(password, user.Hash, user.Salt);
            if (!valido)
                throw new InvalidCredentialException();
            
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Inicio de sesión exitoso", Criticidad.Medio);

            return user;
        }

        public void BloquearUsuario(string username)
        {
            userRepository.LockOut(username);
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Bloqueo de usuario", Criticidad.Alto);
        }

        public void DesbloquearUsuario(string username)
        {
            userRepository.Unlock(username);
            
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Desbloqueo de usuario", Criticidad.Alto);


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
