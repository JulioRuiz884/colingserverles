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
    public class AfiliadoIdiomaLogic : IAfiliadoIdiomaLogic
    {
        private readonly Contexto contexto;

        public AfiliadoIdiomaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarAfiliadoIdioma(int id)
        {
            bool sw = false;
            AfiliadoIdioma AfiliadoIdiomaEliminar = await contexto.AfiliadoIdioma.FirstOrDefaultAsync(x => x.Id == id);
            if (AfiliadoIdiomaEliminar != null)
            {
                contexto.Remove(AfiliadoIdiomaEliminar);

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<bool> InsertarAfiliadoIdioma(AfiliadoIdioma afiliadoIdioma)
        {
            bool sw = false;
            contexto.AfiliadoIdioma.Add(afiliadoIdioma);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<AfiliadoIdioma>> ListarAfiliadoIdiomaTodos()
        {
            return await contexto.AfiliadoIdioma.ToListAsync();
        }

        public async Task<bool> ModificarAfiliadoIdioma(AfiliadoIdioma afiliadoIdioma, int id)
        {
            bool sw = false;
            AfiliadoIdioma afiliadoidiomamod = await contexto.AfiliadoIdioma.FirstOrDefaultAsync(x => x.Id == id);
            if (afiliadoidiomamod != null)
            {
                afiliadoidiomamod.IdIdioma = afiliadoIdioma.IdIdioma;
                afiliadoidiomamod.IdAfiliado = afiliadoIdioma.IdAfiliado;

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<AfiliadoIdioma> ObtenerAfiliadoIdiomaById(int id)
        {
            return await contexto.AfiliadoIdioma.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
