using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coling.BolsaTrabajo
{
    [BsonIgnoreExtraElements]
    public class Solicitud
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        [BsonElement("nombre")]
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; }
        [BsonElement("pretencionSalarial")]
        [JsonPropertyName("pretencionSalarial")]
        public int PretencionSalarial { get; set; }
    }
}
