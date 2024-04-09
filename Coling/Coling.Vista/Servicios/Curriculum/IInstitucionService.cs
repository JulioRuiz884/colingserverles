using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IInstitucionService
    {
        Task<List<Institucion>> ListarInstituciones(string token);
        Task<bool> InsertarInstitucion(Institucion institucion, string token);
        Task<bool> EliminarInstitucion(int id, string token);
        Task<bool> ModificarInstitucion(Institucion institucion, int id, string token);
    }
}
