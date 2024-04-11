using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados.Contratos
{
    public interface IAfiliadoService
    {
        public Task<bool> InsertarAfiliado(Afiliado afiliado, string token);
        public Task<bool> InsertarAfiliadoCompleto(AfiliadoDTO afiliado, string token);
        public Task<bool> ModificarAfiliado(Afiliado afiliado, int id, string token);
        public Task<bool> EliminarAfiliado(int id, string token);
        public Task<Afiliado> ObtenerAfiliadoById(int id, string token);
        public Task<List<Afiliado>> ListarAfiliados(string token);
    }
}
