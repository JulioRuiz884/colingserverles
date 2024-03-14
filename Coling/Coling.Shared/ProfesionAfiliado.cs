using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class ProfesionAfiliado
    {
        public int Id { get; set; }
        public int IdAfiliado { get; set; }
        public int IdProfesion { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int NroSellosSib { get; set; }
        public string? Estado { get; set; }
    }
}
