using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio.habitacion
{
    public interface ITipoHabitacionRepository_68SA
    {
        List<TipoHabitacion> GetAll();
        List<TipoHabitacion> GetAll(TipoHabitacionFiltro_68SA filtro);
        TipoHabitacion GetById(int tipoHabitacionId);
        int Crear(TipoHabitacion tipo);
        void Modificar(TipoHabitacion tipo);
        bool Eliminar(int tipoHabitacionId);    // false si no existe o tiene habitaciones
        bool ExisteNombre(string nombre, int? excluirTipoId = null);
    }
}
