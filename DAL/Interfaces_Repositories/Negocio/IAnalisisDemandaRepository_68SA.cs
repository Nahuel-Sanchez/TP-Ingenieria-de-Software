using BE_08YS;
using System;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    /// <summary>Datos crudos para el análisis predictivo de ocupación y demanda.</summary>
    public interface IAnalisisDemandaRepository_68SA
    {
        /// <summary>Tipos de habitación, reservas, cargos y productos del período [desdeHistorial, hasta].</summary>
        DatosDemanda_68SA GetDatos(DateTime desdeHistorial, DateTime hasta);
    }
}
