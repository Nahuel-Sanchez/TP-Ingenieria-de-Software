using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlPedidoRoomServiceRepository_68SA : Connection_08YS, IPedidoRoomServiceRepository_68SA
    {
        #region Campos

        // El pedido se ve junto con su cargo (reserva, fecha, monto), la habitación y el titular de la reserva
        private const string Select = @"
            SELECT p.PedidoID, p.CargoID, p.Estado, p.FechaHoraEntrega,
                   c.ReservaID, c.Descripcion, c.FechaHora, c.Monto,
                   hab.NroHabitacion,
                   hu.Nombre AS NombreTitular, hu.Apellido AS ApellidoTitular,
                   u.Nombre AS NombreUsuario, u.Apellido AS ApellidoUsuario
            FROM PedidosRoomService p
            INNER JOIN CargosAdicionales c ON c.CargoID = p.CargoID
            INNER JOIN Reservas r          ON r.ReservaID = c.ReservaID
            INNER JOIN Habitaciones hab    ON hab.HabitacionID = r.HabitacionID
            INNER JOIN Huespedes hu        ON hu.HuespedID = r.HuespedTitularID
            LEFT  JOIN Users u             ON u.DNI = c.UsuarioRegistroDNI";

        #endregion

        #region Constructor

        public SqlPedidoRoomServiceRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        #endregion

        #region Consultas

        public List<PedidoRoomService_68SA> GetPedidos(PedidoRoomServiceFiltro_68SA filtro)
        {
            filtro = filtro ?? new PedidoRoomServiceFiltro_68SA();
            var where = new List<string>();
            var parametros = new List<IDbDataParameter>();

            if (filtro.Estado.HasValue)
            {
                where.Add("p.Estado = @Estado");
                parametros.Add(Param("@Estado", (int)filtro.Estado.Value));
            }
            if (filtro.ReservaId.HasValue)
            {
                where.Add("c.ReservaID = @ReservaID");
                parametros.Add(Param("@ReservaID", filtro.ReservaId.Value));
            }
            if (!string.IsNullOrWhiteSpace(filtro.NroHabitacion))
            {
                where.Add("hab.NroHabitacion LIKE @NroHabitacion");
                parametros.Add(Param("@NroHabitacion", $"%{filtro.NroHabitacion.Trim()}%"));
            }
            if (filtro.FechaDesde.HasValue)
            {
                where.Add("c.FechaHora >= @FechaDesde");
                parametros.Add(Param("@FechaDesde", filtro.FechaDesde.Value.Date));
            }
            if (filtro.FechaHasta.HasValue)
            {
                where.Add("c.FechaHora < @FechaHasta");
                parametros.Add(Param("@FechaHasta", filtro.FechaHasta.Value.Date.AddDays(1)));
            }
            if (filtro.EntregadosDesde.HasValue)
            {
                where.Add("p.FechaHoraEntrega >= @EntregadosDesde");
                parametros.Add(Param("@EntregadosDesde", filtro.EntregadosDesde.Value.Date));
            }

            // Pendientes primero (el más antiguo arriba: es el que hay que llevar antes); después, lo último entregado
            string query = Select
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY p.Estado, CASE WHEN p.Estado = 0 THEN c.FechaHora END ASC, p.FechaHoraEntrega DESC";

            return PedidoRoomServiceMapper_68SA.FromDataTable(GetDataTable(query, parametros.ToArray()));
        }

        public List<DetallePedidoRoomService_68SA> GetDetalle(int pedidoId)
        {
            DataTable dt = GetDataTable(@"
                SELECT d.ItemID, i.Nombre, d.Cantidad, d.PrecioUnitario
                FROM DetallePedidoRoomService d
                INNER JOIN ItemsRoomService i ON i.ItemID = d.ItemID
                WHERE d.PedidoID = @PedidoID
                ORDER BY i.Nombre",
                new[] { Param("@PedidoID", pedidoId) });

            return PedidoRoomServiceMapper_68SA.DetalleFromDataTable(dt);
        }

        #endregion

        #region Operaciones

        public ResultadoPedidoRoomService Registrar(SolicitudPedidoRoomService_68SA solicitud, string descripcion,
                                                    int? usuarioDni, out int pedidoId)
        {
            var items = new DataTable();
            items.Columns.Add("ItemID", typeof(int));
            items.Columns.Add("Cantidad", typeof(int));
            foreach (var renglon in solicitud.CantidadesPorItem)
                items.Rows.Add(renglon.Key, renglon.Value);

            var pedidoOutput = ParamOutput("@PedidoID");
            var resultadoOutput = ParamOutput("@Resultado");

            ExecuteNonQuery("sp_RegistrarPedidoRoomService",
                new[]
                {
                    Param("@ReservaID", solicitud.ReservaId),
                    ParamTVP("@Items", items, "dbo.ItemCantidadList"),
                    Param("@Descripcion", descripcion),
                    Param("@UsuarioDNI", (object)usuarioDni ?? DBNull.Value),
                    pedidoOutput,
                    resultadoOutput
                },
                storedProcedure: true);

            pedidoId = pedidoOutput.Value == DBNull.Value ? 0 : Convert.ToInt32(pedidoOutput.Value);
            return (ResultadoPedidoRoomService)Convert.ToInt32(resultadoOutput.Value);
        }

        public bool ConfirmarEntrega(int pedidoId)
        {
            var resultado = ParamOutput("@Resultado");
            ExecuteNonQuery("sp_ConfirmarEntregaPedido",
                new[] { Param("@PedidoID", pedidoId), resultado },
                storedProcedure: true);
            return Convert.ToBoolean(resultado.Value);
        }

        #endregion
    }
}
