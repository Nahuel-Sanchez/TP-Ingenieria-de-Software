using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlHuespedRepository_68SA : Connection_08YS, IHuespedRepository_68SA
    {
        private const string BaseSelect =
            "SELECT HuespedID, Nombre, Apellido, Documento, TipoDocumento, Nacionalidad, FechaNacimiento, Email, Telefono FROM Huespedes";

        public SqlHuespedRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public Huesped_68SA GetByDocumento(string documento)
        {
            DataTable dt = GetDataTable(
                BaseSelect + " WHERE Documento = @Documento",
                new[] { Param("@Documento", documento) });

            return dt.Rows.Count > 0 ? HuespedMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public bool Exists(string documento)
        {
            return ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Huespedes WHERE Documento = @Documento",
                new[] { Param("@Documento", documento) }) > 0;
        }

        public int Create(Huesped_68SA huesped)
        {
            return ExecuteScalar<int>(
                @"INSERT INTO Huespedes (Nombre, Apellido, Documento, TipoDocumento, Nacionalidad, FechaNacimiento, Email, Telefono)
                  VALUES (@Nombre, @Apellido, @Documento, @TipoDocumento, @Nacionalidad, @FechaNacimiento, @Email, @Telefono);
                  SELECT CAST(SCOPE_IDENTITY() AS int);",
                new[]
                {
                    Param("@Nombre",          huesped.Nombre),
                    Param("@Apellido",        huesped.Apellido),
                    Param("@Documento",       huesped.Documento),
                    Param("@TipoDocumento",   (int)huesped.TipoDocumento),
                    Param("@Nacionalidad",    huesped.Nacionalidad),
                    Param("@FechaNacimiento", huesped.FechaNacimiento.Date),
                    Param("@Email",           huesped.Email),
                    Param("@Telefono",        huesped.Telefono)
                });
        }
    }
}
