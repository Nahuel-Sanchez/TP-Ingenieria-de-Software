using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS
{
    public class SessionManager_08YS
    {
        private static readonly Lazy<SessionManager_08YS> lazy =
            new Lazy<SessionManager_08YS>(() => new SessionManager_08YS());

        public static SessionManager_08YS Instance { get { return lazy.Value; } }

        private SessionManager_08YS() { }

        public User_08YS Current { get; private set; }

        public bool IsLogged => !(Current is null);

        public void SetCurrentUser(User_08YS user)
        {
            if (IsLogged)
                throw new InvalidOperationException("Ya hay un usuario logueado. Cierre la sesión antes de iniciar otra.");

            Current = user ?? throw new ArgumentNullException(nameof(user));
        }

        public void CerrarSesion() => Current = null;



    }
}
