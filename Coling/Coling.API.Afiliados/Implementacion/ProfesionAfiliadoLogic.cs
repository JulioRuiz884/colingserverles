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
    public class ProfesionAfiliadoLogic : IProfesionAfiliadoLogic
    {
        private readonly Contexto contexto;

        public ProfesionAfiliadoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarProfesionAfiliado(int id)
        {
            bool sw = false;
            ProfesionAfiliado profesionAfiliadoEliminar = await contexto.ProfesionAfiliado.FirstOrDefaultAsync(x => x.Id == id);
            if (profesionAfiliadoEliminar != null)
            {
                contexto.Remove(profesionAfiliadoEliminar);

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<bool> InsertarProfesionAfiliado(ProfesionAfiliado profesionAfiliado)
        {
            bool sw = false;
            contexto.ProfesionAfiliado.Add(profesionAfiliado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<ProfesionAfiliado>> ListarProfesionAfiliadoTodos()
        {
            return await contexto.ProfesionAfiliado.ToListAsync();
        }

        public async Task<bool> ModificarProfesionAfiliado(ProfesionAfiliado profesionAfiliado, int id)
        {
            bool sw = false;
            ProfesionAfiliado profesionafiliadomod = await contexto.ProfesionAfiliado.FirstOrDefaultAsync(x => x.Id == id);
            if (profesionafiliadomod != null)
            {
                profesionafiliadomod.FechaAsignacion = profesionAfiliado.FechaAsignacion;
                profesionafiliadomod.NroSellosSib = profesionAfiliado.NroSellosSib;
                profesionafiliadomod.Estado = profesionAfiliado.Estado;

                await contexto.SaveChangesAsync();
                return sw = true;
            }
            else { return sw; }
        }

        public async Task<ProfesionAfiliado> ObtenerProfesionAfiliadoById(int id)
        {
            return await contexto.ProfesionAfiliado.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
