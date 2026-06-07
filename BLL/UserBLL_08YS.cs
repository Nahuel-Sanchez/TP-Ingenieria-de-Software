using DAL_08YS;
using DAL_08YS.Interfaces_Repositories;
using DAL_08YS.Repositories_Interfaces;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using Service_08YS.Entities.Bitacora;
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
        private readonly IRolRepository_08YS _rolRepo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        public UserBLL_08YS(IUserRepository_08YS userRepository, IRolRepository_08YS rolRepo, BitacoraBLL_08YS bitacoraBll)
        {
            this.userRepository = userRepository;
            _rolRepo = rolRepo;
            _bitacoraBll = bitacoraBll;
        }

        #region Login

        public User_08YS Login(string username, string password, out bool passwordDefault)
        {
            var user = userRepository.GetByUsername(username) ?? throw new UserNoRegistradoException_08YS();

            if (SessionManager_08YS.Instance.IsLogged)
                throw new InvalidOperationException("Ya hay un usuario logueado. Cierre la sesión antes de iniciar otra.");

            if (user.Bloqueado) throw new UserBloqueadoException_08YS();
            if (!user.Activo)   throw new UserInactivoException_08YS();

            if ( ! Encriptador_08YS.Verificar(password, user.Hash, user.Salt) )
            {
                RegistrarIntentoFallido(username);
                throw new AuthenticationException("Ha ingresado una contraseña incorrecta. Después de 3 intentos fallidos, su cuenta será bloqueada.");
            }

            user.Rol = _rolRepo.GetById(user.Rol.RolID);
            SessionManager_08YS.Instance.SetCurrentUser(user);

            //TraductorManager_08YS.Instance.CambiarIdioma(user.Idioma);
            _bitacoraBll.RegistrarEvento(Evento.LoginExitoso);

            passwordDefault = Encriptador_08YS.Verificar(user.DNI.ToString() + user.Apellido, user.Hash, user.Salt);

            return user;
        }

        private void RegistrarIntentoFallido(string username)
        {
            _bitacoraBll.RegistrarEvento(Evento.LoginFallido, username: username);

            int intentos = _bitacoraBll.ContarIntentosFallidos(username, ventanaHoras: 2);

            if (intentos >= 3)
            {
                UserLockOut(username);
                _bitacoraBll.RegistrarEvento(Evento.UsuarioBloqueado, username: username);
            }
        }

        #endregion

        public void UserLockOut(string username)
        {
            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");
            userRepository.LockOut(user.Username);
            _bitacoraBll.RegistrarEvento(Evento.UsuarioBloqueado, username: username);
            throw new UserBloqueadoException_08YS("Su cuenta ha sido bloqueada debido a múltiples intentos fallidos de inicio de sesión. Por favor, contacte al administrador para desbloquear su cuenta.");
        }

        #region GestionUsuario

        public void DesbloquearUsuario(string username)
        {
            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            userRepository.Unlock(user.Username);
            string passwordDefault = user.DNI.ToString() + user.Apellido;

           
            Encriptador_08YS.CrearHash(passwordDefault, out string nuevoHash, out string nuevoSalt);

            userRepository.UpdatePassword(user.Username, nuevoHash, nuevoSalt);

            _bitacoraBll.RegistrarEvento(Evento.UsuarioDesbloqueado, targetUsername: username);
        }

        public void CrearUsuario(int dni, string nombre, string apellido, string email, Rol_08YS rol)
        {
            if (userRepository.Exists(dni))
                throw new InvalidOperationException("Ya existe un usuario registrado con ese DNI.");

            string username = dni.ToString() + nombre;
            string passwordDefault = dni.ToString() + apellido;
            Encriptador_08YS.CrearHash(passwordDefault, out string hash, out string salt);

            User_08YS nuevo = new User_08YS(username, dni, rol, nombre, apellido, email, hash, salt, false, true);

            userRepository.Create(nuevo);
            _bitacoraBll.RegistrarEvento(Evento.UsuarioCreado, targetUsername: username);
        }

        public void ModificarUsuario(string username, string nuevoEmail, Rol_08YS nuevoRol)
        {
            if(username == SessionManager_08YS.Instance.Current.Username)
                throw new InvalidOperationException("No puede modificar su propio rol o email.");

            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            user.Email = nuevoEmail;
            user.Rol = nuevoRol;

            userRepository.Modify(user,user.Username);
            _bitacoraBll.RegistrarEvento(Evento.UsuarioModificado, targetUsername: username);
        }

        public void AlternarEstadoActivo(string username)
        {
            if(username == SessionManager_08YS.Instance.Current.Username)
                throw new InvalidOperationException("No puede modificar su propio estado activo.");

            var user = userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("No se encontró el usuario para modificar.");

            bool nuevoEstado = !user.Activo;
            userRepository.UpdateState(username, nuevoEstado);

            user.Activo = nuevoEstado;
            Evento accion = user.Activo ? Evento.UsuarioHabilitado : Evento.UsuarioDeshabilitado;
            _bitacoraBll.RegistrarEvento(accion, targetUsername: username);
        }

        #endregion

        public void CambiarContraseña(string passwordActual, string passwordNueva)
        {
            if(!Encriptador_08YS.Verificar(passwordActual, SessionManager_08YS.Instance.Current.Hash, SessionManager_08YS.Instance.Current.Salt))
                throw new Exception("La contraseña actual ingresada es incorrecta.");

            Encriptador_08YS.CrearHash(passwordNueva, out string hashNuevo, out string saltNuevo);
            userRepository.UpdatePassword(SessionManager_08YS.Instance.Current.Username, hashNuevo, saltNuevo);
            _bitacoraBll.RegistrarEvento(Evento.CambioContraseña);
        }
        public void CambiarIdiomaUsuario(User_08YS usuario, string nuevoIdioma)
        {
            if (usuario == null) return;

            // 1. Guardamos en la base de datos a través de la DAL
            userRepository.UpdateLanguage(usuario.Username, nuevoIdioma);

            // 2. Modificamos el objeto local en memoria de la sesión actual
            usuario.Idioma = nuevoIdioma;
        }
        public List<User_08YS> GetAll() => userRepository.GetAll();

    }
}
