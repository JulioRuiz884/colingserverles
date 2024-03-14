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
    public class TelefonoLogic : ITelefonoLogic
    {
        private readonly Contexto contexto;

        public TelefonoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarTelefono(int id)
        {
            bool sw = false;
            Telefono TelefonoEliminar = await contexto.Telefono.FirstOrDefaultAsync(x => x.Id == id);
            if (TelefonoEliminar != null)
            {
                contexto.Remove(TelefonoEliminar);

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<bool> InsertarTelefono(Telefono telefono)
        {
            bool sw = false;
            contexto.Telefono.Add(telefono);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Telefono>> ListarTelefonoTodos()
        {
            return await contexto.Telefono.ToListAsync();
        }

        public async Task<bool> ModificarTelefono(Telefono telefono, int id)
        {
            bool sw = false;
            Telefono telefonomod = await contexto.Telefono.FirstOrDefaultAsync(x => x.Id == id);
            if (telefonomod != null)
            {
                telefonomod.NroTelefono = telefono.NroTelefono;
                telefonomod.Estado = telefono.Estado;

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<Telefono> ObtenerTelefonoById(int id)
        {
            return await contexto.Telefono.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
