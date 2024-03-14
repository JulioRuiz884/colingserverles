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
    public class DireccionLogic : IDireccionLogic
    {
        private readonly Contexto contexto;

        public DireccionLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarDireccion(int id)
        {
            bool sw = false;
            Direccion DireccionEliminar = await contexto.Direccion.FirstOrDefaultAsync(x => x.Id == id);
            if (DireccionEliminar != null)
            {
                contexto.Remove(DireccionEliminar);

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<bool> InsertarDireccion(Direccion direccion)
        {
            bool sw = false;
            contexto.Direccion.Add(direccion);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Direccion>> ListarDireccionTodos()
        {
            return await contexto.Direccion.ToListAsync();
        }

        public async Task<bool> ModificarDireccion(Direccion direccion, int id)
        {
            bool sw = false;
            Direccion direccionmod = await contexto.Direccion.FirstOrDefaultAsync(x => x.Id == id);
            if (direccionmod != null)
            {
                direccionmod.Descripcion = direccion.Descripcion;
                direccionmod.Estado = direccion.Estado;

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<Direccion> ObtenerDireccionById(int id)
        {
            return await contexto.Direccion.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
