using BLL_08YS.Negocio;
using DAL_08YS;
using DAL_08YS.Interfaces_Repositories;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using DAL_08YS.Interfaces_Repositories.Negocio.habitacion;
using DAL_08YS.SQL;
using DAL_08YS.SQL.Negocio;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS
{
    public static class BLLFactory_08YS
    {
        #region sistema

        public static UserBLL_08YS CreateUserBLL()
        {
            IDbFactory_08YS factory = new SqlDbFactory_08YS();
            IUserRepository_08YS repo = new SqlUserRepository_08YS(factory);
            IRolRepository_08YS rolRepo = new SqlRolRepository_08YS(factory);
            BitacoraBLL_08YS bitacoraBll = CreateBitacoraBLL();
            return new UserBLL_08YS(repo, rolRepo, bitacoraBll);
        }

        public static BitacoraBLL_08YS CreateBitacoraBLL()
        {
            IDbFactory_08YS factory = new SqlDbFactory_08YS();
            IBitacoraRepository_08YS repo = new SqlBitacoraRepository_08YS(factory);
            return new BitacoraBLL_08YS(repo);
        }

        public static FamiliaBLL_08YS CreateFamiliaBLL()
        {
            IDbFactory_08YS factory = new SqlDbFactory_08YS();
            return new FamiliaBLL_08YS(
                       new SqlFamiliaRepository_08YS(factory),
                       new SqlRolRepository_08YS(factory),
                       new SqlPermisoRepository_08YS(factory),
                       CreateBitacoraBLL()
                   );
        }

        public static RolBLL_08YS CreateRolBLL()
        {
            IDbFactory_08YS factory = new SqlDbFactory_08YS();
            IRolRepository_08YS rolRepo = new SqlRolRepository_08YS(factory);
            IFamiliaRepository_08YS familiaRepo = new SqlFamiliaRepository_08YS(factory);
            IPermisoRepository_08YS permisoRepo = new SqlPermisoRepository_08YS(factory);
            BitacoraBLL_08YS bitacoraBll = CreateBitacoraBLL();
            return new RolBLL_08YS(rolRepo, familiaRepo, permisoRepo, bitacoraBll);
        }

        public static DvBLL_08YS CreateDvBLL()
        {
            IDbFactory_08YS factory = new SqlDbFactory_08YS();
            return new DvBLL_08YS(new SqlDvRepository_08YS(factory));
        }

        public static BackupBLL_08YS CreateBackupBLL()
        {
            IDbFactory_08YS factory = new SqlDbFactory_08YS();
            IBackupRepository_08YS backupRepo = new SqlBackupRepository_08YS(factory);
            return new BackupBLL_08YS(backupRepo);
        }
        #endregion

        #region Negocio

        public static PisoBLL_68SA CreatePisoBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IPisoRepository_68SA pisoRepo = new SqlPisoRepository_68SA(factory);
            return new PisoBLL_68SA(pisoRepo, CreateBitacoraBLL());
        }

        public static TipoHabitacionBLL_68SA CreateTipoHabitacionBLL()
        {
            var factory = new SqlDbFactory_08YS();
            ITipoHabitacionRepository_68SA tipoRepo = new SqlTipoHabitacionRepository_68SA(factory);
            return new TipoHabitacionBLL_68SA(tipoRepo, CreateBitacoraBLL());
        }

        public static HabitacionBLL_68SA CreateHabitacionBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IHabitacionRepository_68SA habitacionRepo = new SqlHabitacionRepository_68SA(factory);
            return new HabitacionBLL_68SA(habitacionRepo, CreateBitacoraBLL());
        }

        public static HuespedBLL_68SA CreateHuespedBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IHuespedRepository_68SA huespedRepo = new SqlHuespedRepository_68SA(factory);
            return new HuespedBLL_68SA(huespedRepo, CreateBitacoraBLL());
        }

        public static ReservaBLL_68SA CreateReservaBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IReservaRepository_68SA reservaRepo = new SqlReservaRepository_68SA(factory);
            return new ReservaBLL_68SA(reservaRepo, CreateHuespedBLL(), CreateBitacoraBLL());
        }

        public static PagoBLL_68SA CreatePagoBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IPagoRepository_68SA pagoRepo = new SqlPagoRepository_68SA(factory);
            return new PagoBLL_68SA(pagoRepo, CreateBitacoraBLL());
        }

        public static ConfiguracionHotelBLL_68SA CreateConfiguracionHotelBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IConfiguracionHotelRepository_68SA repo = new SqlConfiguracionHotelRepository_68SA(factory);
            return new ConfiguracionHotelBLL_68SA(repo, CreateBitacoraBLL());
        }

        public static MetricasBLL_68SA CreateMetricasBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IMetricasRepository_68SA repo = new SqlMetricasRepository_68SA(factory);
            return new MetricasBLL_68SA(repo);
        }

        #endregion

        #region Cargos adicionales (PN2)

        public static CambioHabitacionBLL_68SA CreateCambioHabitacionBLL()
        {
            var factory = new SqlDbFactory_08YS();
            ICambioHabitacionRepository_68SA cambioRepo = new SqlCambioHabitacionRepository_68SA(factory);
            IReservaRepository_68SA reservaRepo = new SqlReservaRepository_68SA(factory);
            IHabitacionRepository_68SA habitacionRepo = new SqlHabitacionRepository_68SA(factory);

            // Una política por motivo: agregar un motivo nuevo es sumar una clase a esta lista
            var politicas = new IPoliticaCambioHabitacion_68SA[]
            {
                new PoliticaUpgrade_68SA(),
                new PoliticaReubicacionAdministrativa_68SA()
            };
            return new CambioHabitacionBLL_68SA(cambioRepo, reservaRepo, habitacionRepo, CreateBitacoraBLL(), politicas);
        }

        public static RoomServiceBLL_68SA CreateRoomServiceBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IPedidoRoomServiceRepository_68SA pedidoRepo = new SqlPedidoRoomServiceRepository_68SA(factory);
            ICatalogoRoomServiceRepository_68SA catalogoRepo = new SqlCatalogoRoomServiceRepository_68SA(factory);
            IReservaRepository_68SA reservaRepo = new SqlReservaRepository_68SA(factory);
            return new RoomServiceBLL_68SA(pedidoRepo, catalogoRepo, reservaRepo, CreateBitacoraBLL());
        }

        public static CatalogoRoomServiceBLL_68SA CreateCatalogoRoomServiceBLL()
        {
            var factory = new SqlDbFactory_08YS();
            ICatalogoRoomServiceRepository_68SA repo = new SqlCatalogoRoomServiceRepository_68SA(factory);
            return new CatalogoRoomServiceBLL_68SA(repo, CreateBitacoraBLL());
        }

        public static CuentaEstadiaBLL_68SA CreateCuentaEstadiaBLL()
        {
            var factory = new SqlDbFactory_08YS();
            ICargoAdicionalRepository_68SA cargoRepo = new SqlCargoAdicionalRepository_68SA(factory);
            IPagoRepository_68SA pagoRepo = new SqlPagoRepository_68SA(factory);
            IReservaRepository_68SA reservaRepo = new SqlReservaRepository_68SA(factory);
            return new CuentaEstadiaBLL_68SA(cargoRepo, pagoRepo, reservaRepo);
        }

        public static BitacoraCambioHabitacionBLL_68SA CreateBitacoraCambioHabitacionBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IBitacoraCambioHabitacionRepository_68SA repo = new SqlBitacoraCambioHabitacionRepository_68SA(factory);
            return new BitacoraCambioHabitacionBLL_68SA(repo);
        }

        public static ReporteServiciosBLL_68SA CreateReporteServiciosBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IReporteServiciosRepository_68SA repo = new SqlReporteServiciosRepository_68SA(factory);
            return new ReporteServiciosBLL_68SA(repo);
        }

        public static AnalisisDemandaBLL_68SA CreateAnalisisDemandaBLL()
        {
            var factory = new SqlDbFactory_08YS();
            IAnalisisDemandaRepository_68SA repo = new SqlAnalisisDemandaRepository_68SA(factory);
            return new AnalisisDemandaBLL_68SA(repo);
        }

        #endregion
    }
}
