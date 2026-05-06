using System;
using System.Runtime.Serialization;

namespace BLL_08YS
{
    public class UserNoRegistradoException_08YS : Exception
    {
        public UserNoRegistradoException_08YS() : base("Usuario no encontrado. Por favor, verifique sus credenciales.")
        {
        }

        public UserNoRegistradoException_08YS(string message) : base(message)
        {
        }

        public UserNoRegistradoException_08YS(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}