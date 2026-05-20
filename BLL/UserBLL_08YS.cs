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
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, Evento.UsuarioCreado, Criticidad.Alto);
        }

        #region Login
        private readonly Dictionary<string, LoginAttempt_08YS> _loginAttempts = new Dictionary<string, LoginAttempt_08YS>();

        public User Login(string username, string password)
        {
            var user = userRepository.GetByUsername(username) ?? throw new UserNoRegistradoException_08YS();
            if (SessionManager.Instance.IsLogged)
            {
                throw new InvalidOperationException("Ya hay un usuario logueado. Cierre la sesión antes de iniciar otra.");
            }
            if (user.Bloqueado)
                throw new UserBloqueadoException_08YS();
            if (!user.Activo)
                throw new UserInactivoException_08YS();

            if ( ! Encriptador.Verificar(password, user.Hash, user.Salt) )
            {
                RegistrarIntentoFallido(username);
                throw new AuthenticationException("Ha ingresado una contraseña incorrecta.");
            }
            _loginAttempts.Remove(username);

            SessionManager.Instance.SetCurrentUser(user);
            _bitacoraBll.RegistrarEvento(Modulo.Login, Evento.LoginExitoso, Criticidad.Medio);

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
        #region Bloqueos
        public void UserLockOut(string username)
        {
            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");
            userRepository.LockOut(user.Username);
            _bitacoraBll.RegistrarEvento(username, Modulo.Usuarios, Evento.UsuarioBloqueado, Criticidad.Alto);
        }

        public void DesbloquearUsuario(string username)
        {
            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            userRepository.Unlock(user.Username);
            string passwordDefault = user.DNI.ToString() + user.Apellido;

           
            Encriptador.CrearHash(passwordDefault, out string nuevoHash, out string nuevoSalt);

          
            userRepository.UpdatePassword(user.Username, nuevoHash, nuevoSalt);


            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, Evento.UsuarioDesbloqueado, Criticidad.Alto);
            
        }
        #endregion
        public void ModificarUsuario(string username, string nuevoEmail, UserRole nuevoRol)
        {
         
            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

           
            user.Email = nuevoEmail;
            user.Rol = nuevoRol;
            userRepository.Modify(user,user.Username);
            

       
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios,Evento.UsuarioModificado, Criticidad.Alto);
        }

        public void AlternarEstadoActivo(string username)
        {
           
            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            bool nuevoEstado = !user.Activo;
            userRepository.UpdateState(username, nuevoEstado);

            user.Activo = nuevoEstado;
            Evento accion = user.Activo ? Evento.UsuarioHabilitado : Evento.UsuarioDeshabilitado;
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, accion, Criticidad.Alto);
        }
        public void CambiarContraseña(string passwordActual, string passwordNueva)
        {
            if(!Encriptador.Verificar(passwordActual, SessionManager.Instance.Current.Hash, SessionManager.Instance.Current.Salt))
                throw new Exception("La contraseña actual ingresada es incorrecta.");

            Encriptador.CrearHash(passwordNueva, out string hashNuevo, out string saltNuevo);
            userRepository.UpdatePassword(SessionManager.Instance.Current.Username, hashNuevo, saltNuevo);
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, Evento.CambioContraseña, Criticidad.Alto);
        }
      
        public List<User> GetAll() => userRepository.GetAll();

    }
}
