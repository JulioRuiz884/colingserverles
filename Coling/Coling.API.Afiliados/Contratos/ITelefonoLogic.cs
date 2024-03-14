using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface ITelefonoLogic
    {
        public Task<bool> InsertarTelefono(Telefono telefono);
        public Task<bool> ModificarTelefono(Telefono telefono, int id);
        public Task<bool> EliminarTelefono(int id);
        public Task<List<Telefono>> ListarTelefonoTodos();
        public Task<Telefono> ObtenerTelefonoById(int id);
    }
}
