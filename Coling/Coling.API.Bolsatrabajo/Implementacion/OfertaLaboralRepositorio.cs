using Coling.API.Bolsatrabajo.Contratos;
using Coling.API.Bolsatrabajo.Modelo;
using Coling.BolsaTrabajo;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Implementacion
{
    public class OfertaLaboralRepositorio : IOfertaLaboralRepositorio
    {
        private readonly string? cadenaconexion;
        private readonly string? tablanombre;
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<OfertaLaboral> collection;

        public OfertaLaboralRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaconexion = configuration.GetSection("cadena").Value;
            var client = new MongoClient(cadenaconexion);
            var database = client.GetDatabase("BolsaTrabajo");
            collection = database.GetCollection<OfertaLaboral>("OfertaLaborales");
        }
        public async Task<bool> EliminarOfertaLaboral(string id)
        {
            try
            {

                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var result = await collection.DeleteOneAsync(Builders<OfertaLaboral>.Filter.Eq("_id", objectId));
                    return result.DeletedCount == 1;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> InsertarOfertaLaboral(OfertaLaboral ofertaLaboral)
        {
            try
            {
                await collection.InsertOneAsync(ofertaLaboral);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> InsertarOfertaLaboral(Solicitud solicitud)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OfertaLaboral>> ListarOfertaLaboral()
        {
            List<OfertaLaboral> lista = new List<OfertaLaboral>();

            lista = await collection.Find(d => true).ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarOfertaLaboral(OfertaLaboral ofertaLaboral, string id)
        {
            try
            {
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    bool sw = false;
                    OfertaLaboral modificar = await collection.Find(Builders<OfertaLaboral>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
                    if (modificar != null)
                    {
                        modificar.IdInstitucion = ofertaLaboral.IdInstitucion;
                        modificar.FechaOferta = ofertaLaboral.FechaOferta;
                        modificar.FechaLimite = ofertaLaboral.FechaLimite;
                        modificar.Descripcion = ofertaLaboral.Descripcion;
                        modificar.TituloCargo = ofertaLaboral.TituloCargo;
                        modificar.TipoContrato = ofertaLaboral.TipoContrato;
                        modificar.TipoTrabajo = ofertaLaboral.TipoTrabajo;
                        modificar.Area = ofertaLaboral.Area;
                        modificar.Caracteristicas = ofertaLaboral.Caracteristicas;
                        modificar.Estado = ofertaLaboral.Estado;

                        await collection.ReplaceOneAsync(Builders<OfertaLaboral>.Filter.Eq("_id", objectId), modificar);
                        sw = true;
                    }
                    return sw;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<OfertaLaboral> ObtenerById(string id)
        {
            try
            {
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var cursor = await collection.Find(Builders<OfertaLaboral>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();

                    return cursor;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
