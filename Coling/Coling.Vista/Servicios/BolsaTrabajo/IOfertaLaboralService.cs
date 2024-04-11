using Coling.Vista.Modelos;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.BolsaTrabajo.Contratos
{
    public interface IOfertaLaboralService
    {
        Task<List<OfertaLaboral>> Listarofertas(string token);
        public Task<bool> EliminarOferta(string id, string token);
        public Task<bool> InsertarOferta(OfertaLaboral oLaboral, string token);
        public Task<bool> ModificarOferta(OfertaLaboral oLaboral, string id, string token);
       
        public Task<OfertaLaboral> ObtenerOfertaById(string id, string token);
    }
}
