    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class PersonaTipoSocial
    {
        public int Id { get; set; }
        public int IdTipoSocial { get; set; }
        public int IdPersona { get; set; }
        public string? Estado { get; set; }
    }
}
