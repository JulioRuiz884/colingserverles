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
    public class PersonaLogic : IPersonaLogic
    {
        private readonly Contexto contexto;

        public PersonaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public Task<bool> EliminarPersona(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertarPersona(Persona persona)
        {
            bool sw = false;
            contexto.Personas.Add(persona);
            int response = await contexto.SaveChangesAsync();
            if (response== 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Persona>> ListarPersonaTodos()
        {
            return await contexto.Personas.ToListAsync();
        }

        public Task<bool> ModificarPersona(Persona persona, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Persona> ObtenerPersonaById(int id)
        {
            return await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
