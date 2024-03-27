using Coling.API.Bolsatrabajo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Contratos
{
    public interface IOfertaLaboralRepositorio
    {
        public Task<bool> InsertarOfertaLaboral(OfertaLaboral ofertaLaboral);
        public Task<List<OfertaLaboral>> ListarOfertaLaboral();
        public Task<bool> ModificarOfertaLaboral(OfertaLaboral ofertaLaboral, string id);
        public Task<bool> EliminarOfertaLaboral(string id);
        public Task<OfertaLaboral> ObtenerById(string id);
    }
}
