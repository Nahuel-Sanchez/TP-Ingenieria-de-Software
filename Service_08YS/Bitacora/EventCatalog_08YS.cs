using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Bitacora
{
    public class EventoMetadata_08YS
    {
        public Modulo Modulo { get; }
        public Criticidad Criticidad { get; }

        public EventoMetadata_08YS(Modulo modulo, Criticidad criticidad)
        {
            Modulo = modulo;
            Criticidad = criticidad;
        }
    }

    public static class EventCatalog_08YS
    {
        private static readonly Dictionary<Evento, EventoMetadata_08YS> _catalogo =
            new Dictionary<Evento, EventoMetadata_08YS>
            {
            { Evento.LoginExitoso,           new EventoMetadata_08YS(Modulo.Login,    Criticidad.Bajo)    },
            { Evento.LoginFallido,           new EventoMetadata_08YS(Modulo.Login,    Criticidad.Medio)    },
            { Evento.UsuarioBloqueado,       new EventoMetadata_08YS(Modulo.Login,    Criticidad.Critico) },
            { Evento.UsuarioCreado,          new EventoMetadata_08YS(Modulo.Usuarios, Criticidad.Medio)   },
            { Evento.UsuarioDesbloqueado,    new EventoMetadata_08YS(Modulo.Usuarios, Criticidad.Medio)   },
            { Evento.UsuarioDeshabilitado,   new EventoMetadata_08YS(Modulo.Usuarios, Criticidad.Alto)   },
            { Evento.UsuarioHabilitado,      new EventoMetadata_08YS(Modulo.Usuarios, Criticidad.Alto)    },
            { Evento.UsuarioModificado,      new EventoMetadata_08YS(Modulo.Usuarios, Criticidad.Bajo)    },
            { Evento.CambioContraseña,       new EventoMetadata_08YS(Modulo.Usuarios, Criticidad.Medio)   },
            };

        public static EventoMetadata_08YS GetMetadata(Evento evento) => _catalogo[evento];

        public static List<Evento> GetEventsByModule(Modulo modulo)
        {
            return _catalogo
                .Where(kvp => kvp.Value.Modulo == modulo)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        public static List<Evento> GetEventsByCriticidad(Criticidad value)
        {
            return _catalogo
                .Where(kvp => kvp.Value.Criticidad == value)
                .Select(kvp => kvp.Key)
                .ToList();
        }
    }
}
