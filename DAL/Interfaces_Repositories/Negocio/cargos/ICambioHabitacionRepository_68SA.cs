using BE_08YS;
using System.Collections.Generic;

namespace DAL_08YS.Interfaces_Repositories.Negocio.cargos
{
    /// <summary>Cambio de habitación de una estadía en curso (upgrade o reubicación administrativa).</summary>
    public interface ICambioHabitacionRepository_68SA
    {
        #region Consultas

        /// <summary>Tarifa del último upgrade o, si no hubo, la tarifa con la que se reservó (dbo.fn_TarifaVigenteReserva).</summary>
        decimal GetTarifaVigente(int reservaId);

        /// <summary>Habitaciones libres hasta el egreso y con capacidad suficiente; con soloUpgrade, además de tarifa mayor.</summary>
        List<Habitacion_68SA> GetHabitacionesParaCambio(int reservaId, bool soloUpgrade);

        List<CambioHabitacion_68SA> GetByReserva(int reservaId);

        #endregion

        #region Operaciones

        /// <summary>Registra el cambio en una transacción (sp_RegistrarCambioHabitacion) y devuelve su código de resultado.</summary>
        ResultadoRegistroCambio_68SA Registrar(SolicitudCambioHabitacion_68SA solicitud, string descripcionCargo,
                                               int? usuarioDni, string usuarioApp);

        #endregion
    }
}
