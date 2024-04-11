using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Coling.API.Afiliados
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Afiliado> Afiliados { get; set; }
        public virtual DbSet<Telefono> Telefono { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<PersonaTipoSocial> PersonaTipoSocial { get; set; }
        public virtual DbSet<ProfesionAfiliado> ProfesionAfiliado { get; set; }
        public virtual DbSet<AfiliadoIdioma> AfiliadoIdioma { get; set; }
        public virtual DbSet<Idioma> Idioma { get; set; }
        public virtual DbSet<TipoSocial> TipoSocial { get; set; }
    }
}