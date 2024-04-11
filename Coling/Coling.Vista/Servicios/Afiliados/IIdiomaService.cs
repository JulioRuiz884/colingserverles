using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IIdiomaService
    {
        Task<List<Idioma>> ListarIdioma();
        Task<bool> InsertarIdioma(Idioma idioma);
        Task<bool> EliminarIdioma(int id);
        Task<bool> ModificarIdioma(Idioma idioma, int id);
    }
}
