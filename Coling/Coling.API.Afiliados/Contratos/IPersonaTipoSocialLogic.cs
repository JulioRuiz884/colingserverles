using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IPersonaTipoSocialLogic
    {
        public Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial personaTipoSocial);
        public Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial personaTipoSocial, int id);
        public Task<bool> EliminarPersonaTipoSocial(int id);
        public Task<List<PersonaTipoSocial>> ListarPersonaTipoSocialTodos();
        public Task<PersonaTipoSocial> ObtenerPersonaTipoSocialById(int id);
    }
}
