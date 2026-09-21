using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    public interface IHuespedRepository_68SA
    {
        Huesped_68SA GetByDocumento(string documento);
        bool Exists(string documento);
        int Create(Huesped_68SA huesped);
        Huesped_68SA GetPorClaveCompleta(string documento, TipoDocumento tipoDocumento, string nacionalidad);
        Huesped_68SA GetById(int huespedId);
        List<Huesped_68SA> GetListado(HuespedFiltro_68SA filtro);
        void Modificar(Huesped_68SA huesped);
        bool Eliminar(int huespedId);   // false si no existe o figura en reservas
        bool ExisteOtroConClave(int excluirHuespedId, string documento, TipoDocumento tipoDocumento, string nacionalidad);
    }
}
