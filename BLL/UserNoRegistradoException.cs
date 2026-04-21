using System;
using System.Runtime.Serialization;

namespace BLL
{
    public class UserNoRegistradoException : Exception
    {
        public UserNoRegistradoException()
        {
        }

        public UserNoRegistradoException(string message) : base(message)
        {
        }

        public UserNoRegistradoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNoRegistradoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}