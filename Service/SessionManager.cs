using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SessionManager
    {
        private static readonly Lazy<SessionManager> lazy =
            new Lazy<SessionManager>(() => new SessionManager());

        public static SessionManager Instance { get { return lazy.Value; } }

        private SessionManager()
        {
        }
    }
}
