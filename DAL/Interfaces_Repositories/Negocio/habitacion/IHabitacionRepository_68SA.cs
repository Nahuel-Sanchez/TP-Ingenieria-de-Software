using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio.habitacion
{
    public interface IHabitacionRepository_68SA
    {
        #region Consultas

        List<Habitacion_68SA> GetAll(int? idTipoHabitacion = null);

        List<Habitacion_68SA> GetDisponibles(DateTime fechaIngreso, DateTime fechaEgreso,
                                              int? idTipoHabitacion = null, int? capacidadMinima = null);

        Habitacion_68SA GetById(int habitacionId);

        List<Habitacion_68SA> GetListado(HabitacionFiltro_68SA filtro); 
        bool ExisteNumero(string nroHabitacion, int? excluirHabitacionId = null);
        bool TieneReservasActivas(int habitacionId);

        #endregion

        #region Operaciones

        void CambiarEstado(int habitacionId, EstadoHabitacion nuevoEstado);
        int Crear(Habitacion_68SA habitacion);
        void Modificar(Habitacion_68SA habitacion);
        bool Eliminar(int habitacionId);    // false si no existe o tiene reservas

        #endregion
    }
}
