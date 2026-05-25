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

        public User Login(string username, string password, out bool passwordDefault)
        {
            var user = userRepository.GetByUsername(username) ?? throw new UserNoRegistradoException_08YS();

            if (SessionManager.Instance.IsLogged)
                throw new InvalidOperationException("Ya hay un usuario logueado. Cierre la sesión antes de iniciar otra.");

            if (user.Bloqueado) throw new UserBloqueadoException_08YS();
            if (!user.Activo)   throw new UserInactivoException_08YS();

            if ( ! Encriptador.Verificar(password, user.Hash, user.Salt) )
            {
                RegistrarIntentoFallido(username);
                throw new AuthenticationException("Ha ingresado una contraseña incorrecta.");
            }

            SessionManager.Instance.SetCurrentUser(user);
            _bitacoraBll.RegistrarEvento(Modulo.Login, Evento.LoginExitoso, Criticidad.Bajo);

            passwordDefault = Encriptador.Verificar(user.DNI.ToString() + user.Apellido, user.Hash, user.Salt);

            return user;
        }

        private void RegistrarIntentoFallido(string username)
        {
            _bitacoraBll.RegistrarEvento(Modulo.Login, Evento.LoginFallido, Criticidad.Medio, username);

            int intentos = _bitacoraBll.ContarIntentosFallidos(username, ventanaHoras: 2);

            if (intentos >= 3)
            {
                UserLockOut(username);
                _bitacoraBll.RegistrarEvento(Modulo.Login, Evento.UsuarioBloqueado, Criticidad.Alto, username);
            }
            else throw new AuthenticationException("Ha ingresado una contraseña incorrecta. Después de 3 intentos fallidos, su cuenta será bloqueada.");
        }

        #endregion
        #region Bloqueos
        public void UserLockOut(string username)
        {
            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");
            userRepository.LockOut(user.Username);
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, Evento.UsuarioBloqueado, Criticidad.Alto, username);
            throw new UserBloqueadoException_08YS("Su cuenta ha sido bloqueada debido a múltiples intentos fallidos de inicio de sesión. Por favor, contacte al administrador para desbloquear su cuenta.");
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

            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, Evento.AdminDesbloqueaUsuario, Criticidad.Alto);
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios, Evento.UsuarioDesbloqueado, Criticidad.Alto, username);
        }
        #endregion

        public void ModificarUsuario(string username, string nuevoEmail, UserRole nuevoRol)
        {
            if(username == SessionManager.Instance.Current.Username)
                throw new InvalidOperationException("No puede modificar su propio rol o email.");

            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

           
            user.Email = nuevoEmail;
            user.Rol = nuevoRol;
            userRepository.Modify(user,user.Username);
            

       
            _bitacoraBll.RegistrarEvento(Modulo.Usuarios,Evento.UsuarioModificado, Criticidad.Medio);
        }

        public void AlternarEstadoActivo(string username)
        {
            if(username == SessionManager.Instance.Current.Username)
                throw new InvalidOperationException("No puede modificar su propio estado activo.");

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
