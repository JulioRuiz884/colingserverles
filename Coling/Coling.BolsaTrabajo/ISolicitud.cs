using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coling.BolsaTrabajo
{
    public interface ISolicitud
    {
        public string? NombreCompleto { get; set; }
        public int PretencionSalarial { get; set; }
        public int IdAfiliado { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string? Acercade { get; set; }
        public int IdOferta { get; set; }
    }
}
