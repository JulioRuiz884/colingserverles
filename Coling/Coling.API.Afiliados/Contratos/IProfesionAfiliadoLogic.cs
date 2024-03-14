using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IProfesionAfiliadoLogic
    {
        public Task<bool> InsertarProfesionAfiliado(ProfesionAfiliado profesionAfiliado);
        public Task<bool> ModificarProfesionAfiliado(ProfesionAfiliado profesionAfiliado, int id);
        public Task<bool> EliminarProfesionAfiliado(int id);
        public Task<List<ProfesionAfiliado>> ListarProfesionAfiliadoTodos();
        public Task<ProfesionAfiliado> ObtenerProfesionAfiliadoById(int id);
    }
}
