using Coling.API.Afiliados.Contratos;
using Coling.API.Afiliados.DTO;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Implementacion
{
    public class AfiliadoLogic : IAfiliadoLogic
    {
        private readonly Contexto contexto;
        public AfiliadoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarAfiliado(int id)
        {
            bool sw = false;
            Afiliado existe = await contexto.Afiliados.FindAsync(id);
            if (existe != null)
            {
                contexto.Afiliados.Remove(existe);
                await contexto.SaveChangesAsync();
                sw = true;
            }
            return sw;
        }
    

        public async Task<bool> InsertarAfiliado(Afiliado afiliado)
        {
        bool sw = false;
        contexto.Afiliados.Add(afiliado);

        int response = await contexto.SaveChangesAsync();
        if (response == 1)
        {
            sw = true;
        }
        return sw;
    }

        public async Task<List<Afiliado>> ListarAfiliadoTodos()
        {
            var lista = await contexto.Afiliados.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarAfiliado(Afiliado afiliado, int id)
        {
            bool sw = false;
            Afiliado edit = await contexto.Afiliados.FindAsync(id);
            if (edit != null)
            {
                edit.IdPersona = afiliado.IdPersona;
                edit.FechaAfilacion=afiliado.FechaAfilacion;
                edit.CodigoAfiliado=afiliado.CodigoAfiliado;
                edit.NroTituloProvisional = afiliado.CodigoAfiliado;
                edit.Estado=afiliado.Estado;
                await contexto.SaveChangesAsync();
                sw = true;
            }
            return sw;
        }

        public async Task<Afiliado> ObtenerAfiliadoById(int id)
        {
            Afiliado afiliado = await contexto.Afiliados.FirstOrDefaultAsync(x => x.Id == id);
            return afiliado;
        }
        public async Task<bool> InsertarAfiliadoCompleto(AfiliadoDTO afiliado)
        {
            bool sw = false;
            Persona persona = new Persona();
            persona.Nombre = afiliado.Nombre;
            persona.Apellidos = afiliado.Apellidos;
            persona.FechaNacimiento = afiliado.FechaNacimiento;
            persona.Foto = afiliado.Foto;
            persona.Estado = afiliado.EstadoPersona;
            contexto.Personas.Add(persona);
            contexto.SaveChanges();

            Afiliado afil = new Afiliado();
            afil.IdPersona = persona.Id;
            afil.FechaAfilacion = afiliado.FechaAfilacion;
            afil.CodigoAfiliado = afiliado.CodigoAfiliado;
            afil.NroTituloProvisional = afiliado.NroTituloProvisional;
            afil.Estado = afiliado.Estado;
            contexto.Afiliados.Add(afil);
            contexto.SaveChanges();

            if (afiliado.idiomasLista != null)
            {
                foreach (string item in afiliado.idiomasLista)
                {
                    AfiliadoIdioma afiliadoidioma = new AfiliadoIdioma();
                    afiliadoidioma.IdAfiliado = afil.Id;
                    afiliadoidioma.IdIdioma = Convert.ToInt32(item);
                    contexto.AfiliadoIdioma.Add(afiliadoidioma);

                }
                contexto.SaveChanges();
            }
            if (afiliado.socialesLista != null)
            {
                foreach (string item in afiliado.socialesLista)
                {
                    PersonaTipoSocial personaTipoSocial = new PersonaTipoSocial();
                    personaTipoSocial.IdPersona = persona.Id;
                    personaTipoSocial.IdTipoSocial = Convert.ToInt32(item);
                    personaTipoSocial.Estado = "Activo";
                    contexto.PersonaTipoSocial.Add(personaTipoSocial);

                }
                contexto.SaveChanges();
            }

            Direccion direccion = new Direccion();
            direccion.Id = persona.Id;
            direccion.Descripcion = afiliado.Direccion;
            direccion.Estado = "Activo";
            contexto.Direccion.Add(direccion);

            Telefono telefono = new Telefono();
            telefono.IdPersona = persona.Id;
            telefono.NroTelefono = afiliado.NroTelefono;
            telefono.Estado = "Activo";
            contexto.Telefono.Add(telefono);


            int response = await contexto.SaveChangesAsync();

            if (response == 2)
            {
                sw = true;
            }

            return sw;
        }

    }
}
