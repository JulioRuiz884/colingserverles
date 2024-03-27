using Coling.API.Bolsatrabajo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Contratos
{
    public interface ISolicitudRepositorio
    {
        public Task<bool> InsertarSolicitud(Solicitud solicitud);
        public Task<List<Solicitud>> ListarSolicitudes();
        public Task<bool> ModificarSolicitud(Solicitud solicitud, string id);
        public Task<bool> EliminarSolicitud(string id);
        public Task<Solicitud> ObtenerById(string id);
    }
}
