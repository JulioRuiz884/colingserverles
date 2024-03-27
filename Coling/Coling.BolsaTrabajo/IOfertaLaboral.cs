using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.BolsaTrabajo
{
    public interface IOfertaLaboral
    {
        public int IdInstitucion { get; set; }
        public DateTime FechaOferta { get; set; }
        public DateTime FechaLimite { get; set; }
        public string? Descripcion { get; set; }
        public string? TituloCargo { get; set; }
        public string? TipoContrato { get; set; }
        public string? TipoTrabajo { get; set; }
        public string? Area { get; set; }
        public string? Caracteristicas { get; set; }
        public string? Estado { get; set; }
    }
}
