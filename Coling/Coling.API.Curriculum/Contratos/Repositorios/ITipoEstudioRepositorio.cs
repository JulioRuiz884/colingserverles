using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface ITipoEstudioRepositorio
    {
        public Task<bool> Insertar(TipoEstudio tipoEstudio);
        public Task<bool> Modificar(TipoEstudio tipoEstudio);
        public Task<bool> Eliminar(string partitiokey, string rowkey, string etag);
        public Task<List<TipoEstudio>> ObtenerTodo();
        public Task<TipoEstudio> ObtenerById(string id);
    }
}
