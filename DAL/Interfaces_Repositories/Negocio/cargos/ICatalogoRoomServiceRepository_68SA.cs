using BE_08YS;
using System.Collections.Generic;

namespace DAL_08YS.Interfaces_Repositories.Negocio.cargos
{
    /// <summary>Carta de servicio a la habitación (ABM con baja lógica).</summary>
    public interface ICatalogoRoomServiceRepository_68SA
    {
        #region Consultas

        List<ItemRoomService_68SA> GetItems(bool incluirInactivos);
        ItemRoomService_68SA GetById(int itemId);
        bool ExisteNombre(string nombre, int? excluirItemId = null);

        #endregion

        #region Operaciones

        int Crear(ItemRoomService_68SA item);
        void Modificar(ItemRoomService_68SA item);

        #endregion
    }
}
