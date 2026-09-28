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
        List<TipoHabitacion_68SA> GetAll();
        List<TipoHabitacion_68SA> GetAll(TipoHabitacionFiltro_68SA filtro);
        TipoHabitacion_68SA GetById(int tipoHabitacionId);
        int Crear(TipoHabitacion_68SA tipo);
        void Modificar(TipoHabitacion_68SA tipo);
        bool Eliminar(int tipoHabitacionId);    // false si no existe o tiene habitaciones
        bool ExisteNombre(string nombre, int? excluirTipoId = null);
    }
}
