using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using Service_08YS.Entities.Bitacora;
using System;
using System.Collections.Generic;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// ABM de la carta de servicio a la habitación. No hay baja física: un ítem que ya se pidió
    /// queda en el detalle de esos pedidos, así que se desactiva (deja de ofrecerse).
    /// </summary>
    public class CatalogoRoomServiceBLL_68SA
    {
        #region Campos

        public const decimal PrecioMaximo = 99999999.99m;

        private readonly ICatalogoRoomServiceRepository_68SA _repo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        #endregion

        #region Constructor

        public CatalogoRoomServiceBLL_68SA(ICatalogoRoomServiceRepository_68SA repo, BitacoraBLL_08YS bitacoraBll)
        {
            _repo = repo;
            _bitacoraBll = bitacoraBll;
        }

        #endregion

        #region Consultas

        public List<ItemRoomService_68SA> GetItems(bool incluirInactivos = true) => _repo.GetItems(incluirInactivos);

        #endregion

        #region Operaciones

        public int Crear(ItemRoomService_68SA item)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.GestionarItemsRoomService);
            Validar(item);
            if (_repo.ExisteNombre(item.Nombre))
                throw new ItemRoomServiceDuplicadoException_68SA();

            item.Id = _repo.Crear(item);
            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.ItemRoomServiceCreado, targetUsername: item.Id.ToString());
            return item.Id;
        }

        public void Modificar(ItemRoomService_68SA item)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.GestionarItemsRoomService);
            Validar(item);
            if (_repo.GetById(item.Id) == null)
                throw new ItemRoomServiceNoEncontradoException_68SA();
            if (_repo.ExisteNombre(item.Nombre, item.Id))
                throw new ItemRoomServiceDuplicadoException_68SA();

            _repo.Modificar(item);
            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.ItemRoomServiceModificado, targetUsername: item.Id.ToString());
        }

        /// <summary>Activa o desactiva el ítem (sale de la carta sin perder los pedidos que ya lo incluyen).</summary>
        public void CambiarActivo(int itemId, bool activo)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.GestionarItemsRoomService);
            var item = _repo.GetById(itemId) ?? throw new ItemRoomServiceNoEncontradoException_68SA();
            if (item.Activo == activo) return;

            item.Activo = activo;
            _repo.Modificar(item);
            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.ItemRoomServiceModificado, targetUsername: item.Id.ToString());
        }

        #endregion

        #region Validaciones

        private static void Validar(ItemRoomService_68SA item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            item.Nombre = item.Nombre?.Trim();
            item.Descripcion = string.IsNullOrWhiteSpace(item.Descripcion) ? null : item.Descripcion.Trim();

            if (string.IsNullOrEmpty(item.Nombre))
                throw new DatosInvalidosException_68SA("El nombre del ítem es obligatorio.");
            if (item.Nombre.Length > 100)
                throw new DatosInvalidosException_68SA("El nombre no puede superar los 100 caracteres.");
            if (item.Descripcion != null && item.Descripcion.Length > 200)
                throw new DatosInvalidosException_68SA("La descripción no puede superar los 200 caracteres.");
            if (item.PrecioUnitario <= 0 || item.PrecioUnitario > PrecioMaximo)
                throw new DatosInvalidosException_68SA("El precio debe ser mayor a cero.");
        }

        #endregion
    }
}
