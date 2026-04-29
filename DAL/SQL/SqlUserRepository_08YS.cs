using DAL_08YS.Repositories_Interfaces;
using MPP_08YS;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS
{
    public class SqlUserRepository_08YS : Connection_08YS, IUserRepository_08YS
    {
        // Recibe el factory por inyeccion de dependencia
        public SqlUserRepository_08YS(IDbFactory_08YS factory) : base(factory) { }
        private IDbDataParameter[] ToParameters(User user)
        {
            return new[]
            {
                Param("@Username",  user.Username),
                Param("@DNI",       user.DNI),
                Param("@Rol",       user.Rol.ToString()),
                Param("@Nombre",    user.Nombre),
                Param("@Apellido",  user.Apellido),
                Param("@Hash",      user.Hash),
                Param("@Salt",      user.Salt),
                Param("@Email",     user.Email),
                Param("@Celular",   (object)user.Celular   ?? DBNull.Value),
                Param("@Direccion", (object)user.Direccion ?? DBNull.Value),
                Param("@Bloqueado", user.Bloqueado)
        };
        }

        public List<User> GetAll()
        {
            DataTable dt = Leer("SELECT * FROM Users");
            return UserMapper_08YS.FromDataTable(dt);
        }

        public User GetByUsername(string username)
        {
            DataTable dt = Leer("SELECT * FROM Users WHERE Username = @Username",
                                    new[] { Param("@Username", username) });

            if (dt.Rows.Count == 0) return null;

            return dt.Rows.Count > 0 ? UserMapper_08YS.FromDataRow(dt.Rows[0]) : null;
        }

        public void LockOut(string username)
        {
            ExecuteNonQuery
            (
                "UPDATE Users SET Bloqueado = 1 WHERE Username = @Username",
                new[] { Param("@Username", username) }
            );
        }

        public void Unlock(string username)
        {
            ExecuteNonQuery
            (
                "UPDATE Users SET Bloqueado = 1 WHERE Username = @Username",
                new[] { Param("@Username", username) }
            );
        }

        public void Create(User user)
        {
            ExecuteNonQuery
            (
                "INSERT INTO Users (Username, DNI, Rol, Nombre, Apellido, Hash, Salt, Email, Celular, Direccion, Bloqueado) " +
                "VALUES (@Username, @DNI, @Rol, @Nombre, @Apellido, @Hash, @Salt, @Email, @Celular, @Direccion, @Bloqueado)",
                ToParameters(user)
            );
        }

        public bool Exists(int dni)
        {
            return ExecuteScalar<int>(   
                                        "SELECT COUNT(1) FROM Users WHERE DNI = @DNI",
                                        new[] { Param("@DNI", dni) }
                                     ) > 0;
        }

        public void Modify(User user, string login)
        {
            Escribir
            (
                "UPDATE Users SET Email = @email AND Rol=@rol WHERE Username = @Username",
                new[] { Param("@email", user.Email),
                        Param("@rol", user.Rol) ,
                        Param("@Username",user.Username)}
            );
        }
    }
}
