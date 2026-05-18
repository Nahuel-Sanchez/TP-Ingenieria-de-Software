using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS
{
    public class SessionManager
    {
        private static readonly Lazy<SessionManager> lazy =
            new Lazy<SessionManager>(() => new SessionManager());

        public static SessionManager Instance { get { return lazy.Value; } }

        private SessionManager() { }

        public User Current { get; private set; }

        public bool IsLogged => !(Current is null);

        public void SetCurrentUser(User user)
        {
            if (IsLogged)
                throw new InvalidOperationException("Ya hay un usuario logueado. Cierre la sesión antes de iniciar otra.");

            Current = user ?? throw new ArgumentNullException(nameof(user));
        }

        public void CerrarSesion() => Current = null;



    }
}
