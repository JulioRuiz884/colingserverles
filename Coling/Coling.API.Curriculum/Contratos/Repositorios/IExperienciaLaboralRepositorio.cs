using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IExperienciaLaboralRepositorio
    {
        public Task<bool> Insertar(ExperienciaLaboral experienciaLaboral);
        public Task<bool> Modificar(ExperienciaLaboral experienciaLaboral);
        public Task<bool> Eliminar(string partitiokey, string rowkey, string etag);
        public Task<List<ExperienciaLaboral>> ObtenerTodo();
        public Task<ExperienciaLaboral> ObtenerById(string id);
    }
}
