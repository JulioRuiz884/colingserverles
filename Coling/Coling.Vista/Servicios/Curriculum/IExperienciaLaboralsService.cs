using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IExperienciaLaboralsService
    {
        Task<List<ExperienciaLaboral>> ListarExperienciaLaboral(string token);
        Task<bool> InsertarExperienciaLaboral(ExperienciaLaboral experienciaLaboral, string token);
        Task<bool> EliminarExperienciaLaboral(string id, string token);
        Task<bool> ModificarExperienciaLaboral(ExperienciaLaboral experienciaLaboral, string id, string token);
    }
}
