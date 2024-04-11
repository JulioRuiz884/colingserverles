using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados.Contratos
{
    public interface ITelefonoService
    {
        public Task<bool> InsertarTelefono(Telefono telefono, string token);
        public Task<bool> ModificarTelefono(Telefono telefono, int id, string token);
        public Task<bool> EliminarTelefono(int id, string token);
        public Task<Telefono> ObtenerTelefonoById(int id, string token);
        public Task<List<Telefono>> ListarTelefonos(string token);
    }
}
