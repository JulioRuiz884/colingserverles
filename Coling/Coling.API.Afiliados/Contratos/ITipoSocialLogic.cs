using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface ITipoSocialLogic
    {
        public Task<bool> InsertarTipoSocial(TipoSocial tipoSocial);
        public Task<bool> ModificarTipoSocial(TipoSocial tipoSocial, int id);
        public Task<bool> EliminarTipoSocial(int id);
        public Task<List<TipoSocial>> ListarTipoSocialTodos();
        public Task<TipoSocial> ObtenerTipoSocialById(int id);
    }
}
