using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Implementacion
{
    public class TipoSocialLogic: ITipoSocialLogic
    {
        private readonly Contexto contexto;

        public TipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarTipoSocial(int id)
        {
            bool sw = false;
            TipoSocial tiposocialEliminar = await contexto.TipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            if (tiposocialEliminar != null)
            {
                contexto.Remove(tiposocialEliminar);

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<bool> InsertarTipoSocial(TipoSocial tipoSocial)
        {
            bool sw = false;
            contexto.TipoSocial.Add(tipoSocial);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<TipoSocial>> ListarTipoSocialTodos()
        {
            return await contexto.TipoSocial.ToListAsync();
        }

        public async Task<bool> ModificarTipoSocial(TipoSocial tipoSocial, int id)
        {
            bool sw = false;
            TipoSocial tiposocialmod = await contexto.TipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            if (tiposocialmod != null)
            {
                tiposocialmod.NombreSocial = tipoSocial.NombreSocial;
                tiposocialmod.Estado = tipoSocial.Estado;

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<TipoSocial> ObtenerTipoSocialById(int id)
        {
            return await contexto.TipoSocial.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
