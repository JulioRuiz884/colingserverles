using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IGradoAcademicoRepositorio
    {
        public Task<bool> Insertar(GradoAcademico gradoAcademico);
        public Task<bool> Modificar(GradoAcademico gradoAcademico);
        public Task<bool> Eliminar(string partitiokey, string rowkey, string etag);
        public Task<List<GradoAcademico>> ObtenerTodo();
        public Task<GradoAcademico> ObtenerById(string id);
    }
}
