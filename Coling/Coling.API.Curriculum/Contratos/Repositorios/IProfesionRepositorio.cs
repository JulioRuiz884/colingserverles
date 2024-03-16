using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IProfesionRepositorio
    {
        public Task<bool> Insertar(Profesion profesion);
        public Task<bool> Modificar(Profesion profesion);
        public Task<bool> Eliminar(string partitiokey, string rowkey, string etag);
        public Task<List<Profesion>> ObtenerTodo();
        public Task<Profesion> ObtenerById(string id);
    }
}
