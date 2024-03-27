using Coling.BolsaTrabajo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Modelo
{
    [BsonIgnoreExtraElements]
    public class Solicitud : ISolicitud
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombreCompleto")]
        [JsonPropertyName("nombreCompleto")]
        public string? NombreCompleto { get; set; }

        [BsonElement("pretencionSalarial")]
        [JsonPropertyName("pretencionSalarial")]
        public int PretencionSalarial { get; set; }

        [BsonElement("IdAfiliado")]
        [JsonPropertyName("IdAfiliado")]
        public int IdAfiliado { get; set; }

        [BsonElement("FechaPostulacion")]
        [JsonPropertyName("FechaPostulacion")]
        public DateTime FechaPostulacion { get; set; }

        [BsonElement("Acercade")]
        [JsonPropertyName("Acercade")]
        public string? Acercade { get; set; }

        [BsonElement("IdOferta")]
        [JsonPropertyName("IdOferta")]
        public int IdOferta { get; set; }
    }
}
