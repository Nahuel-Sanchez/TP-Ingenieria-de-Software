using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS
{
    public enum UserRole
    {
        Admin,
        Basico
    }
    public class User
    {
        private string _username;
        private int _dni;
        private UserRole _rol;
        private string _nombre;
        private string _apellido;
        private string _hash;
        private string _salt;
        private string _email;
        private string _celular;
        private string _direccion;
        private bool _bloqueado;


        public User(string username, int dni, UserRole rol, string nombre, string ape, string email, string hash, string salt, string celular, string direccion, bool bloqueado = false)
        {
            this._username = username;
            this._dni = dni;
            this._rol = rol;
            this._nombre = nombre;
            this._apellido = ape;
            this._hash = hash;  
            this._salt = salt;
            this._email = email;
            this._celular = celular;
            this._direccion = direccion;
            this._bloqueado = bloqueado;
        }

        public User() { }

        public override bool Equals(object obj)
        {
            if (!(obj is User other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DNI == other.DNI;
        }

        public override int GetHashCode() => DNI.GetHashCode();

        public static bool operator ==(User a, User b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(User a, User b) => !(a == b);

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public int DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        public UserRole Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        [Browsable(false)]
        public string Hash
        {
            get { return _hash; }
            set { _hash = value; }
        }

        [Browsable(false)]
        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public bool Bloqueado
        {
            get { return _bloqueado; }
            set { _bloqueado = value; }
        }

    }
}
