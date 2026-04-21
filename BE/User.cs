using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BE.Usuarios
{

    public abstract class User
    {
        private int _ID;
        private string _nombre;
        private string _apellido;
        private string _hash;
        private string _salt;
		private int _dni;
		private string _email;


        public User(int id, string nombre, string ape, int dni, string email, string hash, string salt)
        {
			this._ID = id;
            this._nombre = nombre;
            this._apellido = ape;
            this._hash = hash;
			this._salt = salt;
            this._dni = dni;
            this._email = email;
        }

		public User(string nombre, string ape, int dni, string email, string hash, string salt)
        {
		    this._nombre = nombre;
            this._apellido = ape;
            this._hash = hash;
			this._salt = salt;
            this._dni = dni;
            this._email = email;
        }

        public User() { }

        public override bool Equals(object obj)
        {
            if (!(obj is User other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ID == other.ID;
        }

        public override int GetHashCode() => ID.GetHashCode();

        public static bool operator ==(User a, User b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(User a, User b) => !(a == b);

        [Browsable(false)]
        public int ID
		{
			get { return _ID; }
			set { _ID = value; }
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

		public int DNI
		{
			get { return _dni; }
			set { _dni = value; }
		}
		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

    }
}
