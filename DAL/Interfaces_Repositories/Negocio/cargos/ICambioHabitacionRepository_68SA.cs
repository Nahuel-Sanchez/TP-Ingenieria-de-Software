using BE_08YS;
using System.Collections.Generic;

namespace DAL_08YS.Interfaces_Repositories.Negocio.cargos
{
    /// <summary>Cambio de habitación de una estadía en curso (upgrade o reubicación administrativa).</summary>
    public interface ICambioHabitacionRepository_68SA
    {
        #region Consultas

        /// <summary>Habitaciones libres hasta el egreso y con capacidad suficiente; con soloUpgrade, además de tarifa mayor.</summary>
        List<Habitacion_68SA> GetHabitacionesParaCambio(int reservaId, bool soloUpgrade);

        List<CambioHabitacion_68SA> GetByReserva(int reservaId);

        #endregion

        #region Operaciones

        /// <summary>
        /// Valida y registra el cambio en una sola transacción (sp_RegistrarCambioHabitacion): calcula el cargo del upgrade
        /// y arma su descripción. Devuelve el código de resultado, el cambio y el monto.
        /// </summary>
        ResultadoRegistroCambio_68SA Registrar(SolicitudCambioHabitacion_68SA solicitud);

        #endregion
    }
}
