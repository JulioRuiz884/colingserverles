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
    public class PersonaTipoSocialLogic : IPersonaTipoSocialLogic
    {
        private readonly Contexto contexto;

        public PersonaTipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarPersonaTipoSocial(int id)
        {
            bool sw = false;
            PersonaTipoSocial personaTipoSocialEliminar = await contexto.PersonaTipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            if (personaTipoSocialEliminar != null)
            {
                contexto.Remove(personaTipoSocialEliminar);

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial personaTipoSocial)
        {
            bool sw = false;
            contexto.PersonaTipoSocial.Add(personaTipoSocial);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<PersonaTipoSocial>> ListarPersonaTipoSocialTodos()
        {
            return await contexto.PersonaTipoSocial.ToListAsync();
        }

        public async Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial personaTipoSocial, int id)
        {
            bool sw = false;
            PersonaTipoSocial personatiposocialmod = await contexto.PersonaTipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            if (personatiposocialmod != null)
            {
                personatiposocialmod.IdTipoSocial = personaTipoSocial.IdTipoSocial;
                personatiposocialmod.IdPersona = personaTipoSocial.IdPersona;
                personatiposocialmod.Estado = personaTipoSocial.Estado;

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<PersonaTipoSocial> ObtenerPersonaTipoSocialById(int id)
        {
            return await contexto.PersonaTipoSocial.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
