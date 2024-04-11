using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IPersonaService
    {
        Task<List<Persona>> ListarPersonas();
        Task<bool> InsertarPersonas(Persona persona);
        Task<bool> EliminarPersonas(int id);
        Task<bool> ModificarPersonas(Persona persona, int id);
    }
}
