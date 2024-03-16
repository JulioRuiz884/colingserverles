using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IInstitucion
    {
        string Nombre { get; set; }
        string Tipo { get; set; }
        string Direccion { get; set; }
        string Estado { get; set; }
    }
}
