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
        Task<List<ExperienciaLaborales>> ListarExperienciaLaboral(string token);
        Task<bool> InsertarExperienciaLaboral(ExperienciaLaborales experienciaLaboral, string token);
        Task<bool> EliminarExperienciaLaboral(int id, string token);
        Task<bool> ModificarExperienciaLaboral(ExperienciaLaborales experienciaLaboral, int id, string token);
    }
}
