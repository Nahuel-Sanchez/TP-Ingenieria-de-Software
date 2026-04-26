using System;
using System.Runtime.Serialization;

namespace BLL_08YS
{
    public class UserBloqueadoException_08YS : Exception
    {
        public UserBloqueadoException_08YS()
        {
        }

        public UserBloqueadoException_08YS(string message) : base(message)
        {
        }

        public UserBloqueadoException_08YS(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}