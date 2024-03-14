using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IAfiliadoIdiomaLogic
    {
        public Task<bool> InsertarAfiliadoIdioma(AfiliadoIdioma afiliadoIdioma);
        public Task<bool> ModificarAfiliadoIdioma(AfiliadoIdioma afiliadoIdioma, int id);
        public Task<bool> EliminarAfiliadoIdioma(int id);
        public Task<List<AfiliadoIdioma>> ListarAfiliadoIdiomaTodos();
        public Task<AfiliadoIdioma> ObtenerAfiliadoIdiomaById(int id);
    }
}
