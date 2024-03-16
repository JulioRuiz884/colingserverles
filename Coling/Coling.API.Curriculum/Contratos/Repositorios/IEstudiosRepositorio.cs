using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IEstudiosRepositorio
    {
        public Task<bool> Insertar(Estudios estudios);
        public Task<bool> Modificar(Estudios estudios);
        public Task<bool> Eliminar(string partitiokey, string rowkey, string etag);
        public Task<List<Estudios>> ObtenerTodo();
        public Task<Estudios> ObtenerById(string id);
    }
}
