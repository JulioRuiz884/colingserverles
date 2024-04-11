using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Modelos
{
    public class Telefono
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int NroTelefono { get; set; }
        public string? Estado { get; set; }
    }
}
