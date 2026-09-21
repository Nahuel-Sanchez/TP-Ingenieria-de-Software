using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    public interface IPisoRepository_68SA
    {
        List<Piso_68SA> GetAll();
        List<Piso_68SA> GetAll(PisoFiltro_68SA filtro);
        Piso_68SA GetById(int pisoId);
        int Crear(Piso_68SA piso);
        void Modificar(Piso_68SA piso);
        bool Eliminar(int pisoId);              // false si no existe o tiene habitaciones asociadas
        bool ExisteNumero(int numero, int? excluirPisoId = null);
    }
}
