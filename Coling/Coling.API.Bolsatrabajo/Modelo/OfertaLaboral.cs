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
    public class OfertaLaboral : IOfertaLaboral
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("IdInstitucion")]
        [JsonPropertyName("IdInstitucion")]
        public int IdInstitucion { get; set; }

        [BsonElement("FechaOferta")]
        [JsonPropertyName("FechaOferta")]
        public DateTime FechaOferta { get; set; }

        [BsonElement("FechaLimite")]
        [JsonPropertyName("FechaLimite")]
        public DateTime FechaLimite { get; set; }

        [BsonElement("Descripcion")]
        [JsonPropertyName("Descripcion")]
        public string? Descripcion { get; set; }

        [BsonElement("TituloCargo")]
        [JsonPropertyName("TituloCargo")]
        public string? TituloCargo { get; set; }

        [BsonElement("TipoContrato")]
        [JsonPropertyName("TipoContrato")]
        public string? TipoContrato { get; set; }

        [BsonElement("TipoTrabajo")]
        [JsonPropertyName("TipoTrabajo")]
        public string? TipoTrabajo { get; set; }

        [BsonElement("Area")]
        [JsonPropertyName("Area")]
        public string? Area { get; set; }

        [BsonElement("Caracteristicas")]
        [JsonPropertyName("Caracteristicas")]
        public string? Caracteristicas { get; set; }

        [BsonElement("Estado")]
        [JsonPropertyName("Estado")]
        public string? Estado { get; set; }
    }
}
