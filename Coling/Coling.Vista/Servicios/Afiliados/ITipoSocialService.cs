using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface ITipoSocialService
    {
        Task<List<TipoSocial>> ListarTipoSocial();
        Task<bool> InsertarTipoSocial(TipoSocial tipoSocial);
        Task<bool> EliminarTipoSocial(int id);
        Task<bool> ModificarTipoSocial(TipoSocial tipoSocial, int id);
    }
}
