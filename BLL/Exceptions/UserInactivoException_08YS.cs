using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS
{
    public class UserInactivoException_08YS:Exception
    {
        public UserInactivoException_08YS()
        : base("Su usuario se encuentra deshabilitado. Por favor, comuníquese con un administrador.")
        {
        }

        public UserInactivoException_08YS(string message) : base(message) { }

        public UserInactivoException_08YS(string message, Exception innerException) : base(message, innerException) { }
    }
}
