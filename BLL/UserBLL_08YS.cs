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
            new User("admin01", 12345678, UserRole.Admin, "Juan", "Perez", "juan@test.com", "h", "s", false,true),
            new User("userBloqueado", 99999999, UserRole.Basico, "Marta", "Gomez", "marta@test.com", "h", "s",  true,true), // BLOQUEADO
            new User("userDeshabilitado", 99999999, UserRole.Basico, "Alfredo", "Ortigoza", "Alfredo@test.com", "h", "s",  true,false)
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

            User nuevo = new User(username, dni, rol, nombre, apellido, email, hash, salt, false,true);

            userRepository.Create(nuevo);
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Usuario creado", Criticidad.Alto);
        }

        #region Login
        private readonly Dictionary<string, LoginAttempt_08YS> _loginAttempts = new Dictionary<string, LoginAttempt_08YS>();

        public User Login(string username, string password)
        {
            var user = userRepository.GetByUsername(username) ?? throw new UserNoRegistradoException_08YS();

            if (user.Bloqueado)
                throw new UserBloqueadoException_08YS();

            if ( ! Encriptador.Verificar(password, user.Hash, user.Salt) )
            {
                RegistrarIntentoFallido(username);
                throw new AuthenticationException("Ha ingresado una contraseña incorrecta.");
            }
            _loginAttempts.Remove(username);

            SessionManager.Instance.SetCurrentUser(user);
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Inicio de sesión exitoso", Criticidad.Medio);

            return user;
        }

        private void RegistrarIntentoFallido(string username)
        {
            if (!_loginAttempts.ContainsKey(username))
            {
                _loginAttempts[username] = new LoginAttempt_08YS
                {
                    Attempts = 1,
                    LastAttempt = DateTime.Now
                };
                return;
            }

            var attempt = _loginAttempts[username];

            if ( (DateTime.Now - attempt.LastAttempt).TotalHours >= 2)
                attempt.Attempts = 1;
            
            else attempt.Attempts++;
            

            attempt.LastAttempt = DateTime.Now;

            if (attempt.Attempts >= 3)
            {
                UserLockOut(username);
                throw new AuthenticationException("Ha ingresado una contraseña incorrecta. Su cuenta ha sido bloqueada.");
            }
        }

        #endregion

        public void UserLockOut(string username)
        {
            //userRepository.LockOut(username);
            //_bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Bloqueo de usuario", Criticidad.Alto);
        }

        public void DesbloquearUsuario(string username)
        {
            //userRepository.Unlock(username);

            //_bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Desbloqueo de usuario", Criticidad.Alto);
            var user = _usuariosLocal.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            //var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            // 2. Aplicar los cambios (Solo Email y Rol)
            user.Bloqueado = false;

        }

        public void ModificarUsuario(string username, string nuevoEmail, UserRole nuevoRol)
        {
            // 1. Buscar al usuario existente
            var user = _usuariosLocal.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            //var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            // 2. Aplicar los cambios (Solo Email y Rol)
            user.Email = nuevoEmail;
            user.Rol = nuevoRol;

            // 3. Registrar en Bitácora
            //_bitacoraBll.RegistrarEvento(Modulo.Usuarios, "Modificación de perfil (Email/Rol)",Criticidad.Alto);
        }

        public void AlternarEstadoActivo(string username)
        {
            var user = _usuariosLocal.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            //var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            bool nuevoEstado = !user.Activo;
            //userRepository.UpdateState(username, nuevoEstado);
            
            user.Activo = nuevoEstado;
            // string accion = user.Activo ? "Activación" : "Desactivación";
            // _bitacoraBll.Registrar(username, $"{accion}");
        }
        public void CambiarContraseña(string username, string passwordActual, string passwordNueva)
        {
            // 1. Buscar al usuario en la lista local (o BD)
            var user = _usuariosLocal.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null)
                throw new Exception("El usuario no existe en el sistema.");

            // 2. VALIDACIÓN DE SEGURIDAD: Verificar si la contraseña actual ingresada es correcta
            // Usamos el método Verificar de tu clase Encriptador
            bool esValida = Encriptador.Verificar(passwordActual, user.Hash, user.Salt);

            if (!esValida)
                throw new Exception("La contraseña actual ingresada es incorrecta.");

            // 3. REGLA DE NEGOCIO (Opcional pero recomendada): Que la nueva no sea igual a la anterior
            if (passwordActual == passwordNueva)
                throw new Exception("La nueva contraseña no puede ser igual a la actual.");

            // 4. ENCRIPTACIÓN: Generar un nuevo Hash y Salt para la nueva contraseña
            string nuevoHash, nuevoSalt;
            Encriptador.CrearHash(passwordNueva, out nuevoHash, out nuevoSalt);

            // 5. PERSISTENCIA: Actualizamos el objeto
            user.Hash = nuevoHash;
            user.Salt = nuevoSalt;

            // Si usaras Base de Datos SQL, aquí llamarías a la DAL:
            // _userDal.ActualizarPassword(user.Username, nuevoHash, nuevoSalt);

            // 6. Registro en Bitácora
            // _bitacoraBll.Registrar(username, "Cambio de contraseña por el usuario");
        }

        public List<User> GetAll() => userRepository.GetAll();
    }
}
