using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IInstitucionRepositorio
    {
        public Task<bool> Insertar(Institucion institucion);
        public Task<bool> Modificar(Institucion institucion);
        public Task<bool> Eliminar(string partitiokey, string rowkey, string etag);
        public Task<List<Institucion>> ObtenerTodo();
        public Task<Institucion> ObtenerById(string id);
    }
}
