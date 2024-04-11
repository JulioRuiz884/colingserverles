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
    public class IdiomaLogic: IIdiomaLogic
    {
        private readonly Contexto contexto;

        public IdiomaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarIdioma(int id)
        {
            bool sw = false;
            Idioma idiomaEliminar = await contexto.Idioma.FirstOrDefaultAsync(x => x.Id == id);
            if (idiomaEliminar != null)
            {
                contexto.Remove(idiomaEliminar);

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<bool> InsertarIdioma(Idioma idioma)
        {
            bool sw = false;
            contexto.Idioma.Add(idioma);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Idioma>> ListarIdiomaTodos()
        {
            return await contexto.Idioma.ToListAsync();
        }

        public async Task<bool> ModificarIdioma(Idioma idioma, int id)
        {
            bool sw = false;
            Idioma idiomamod = await contexto.Idioma.FirstOrDefaultAsync(x => x.Id == id);
            if (idiomamod != null)
            {
                idiomamod.NombreIdioma = idioma.NombreIdioma;
                idiomamod.Estado = idioma.Estado;

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<Idioma> ObtenerIdiomaById(int id)
        {
            return await contexto.Idioma.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
