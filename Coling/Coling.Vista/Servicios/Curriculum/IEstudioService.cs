using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IEstudioService
    {
        Task<List<Estudio>> ListarEstudio(string token);
        Task<bool> InsertarEstudio(Estudio estudio, string token);
        Task<bool> EliminarEstudio(int id, string token);
        Task<bool> ModificarEstudio(Estudio estudio, int id, string token);
    }
}
