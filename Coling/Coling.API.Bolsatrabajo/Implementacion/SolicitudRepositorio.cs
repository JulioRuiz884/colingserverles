using Coling.API.Bolsatrabajo.Contratos;
using Coling.API.Bolsatrabajo.Modelo;
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
    public class SolicitudRepositorio: ISolicitudRepositorio
    {
        private readonly string? cadenaconexion;
        private readonly string? tablanombre;
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Solicitud> collection;

        public SolicitudRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaconexion = configuration.GetSection("cadena").Value;
            var client = new MongoClient(cadenaconexion);
            var database = client.GetDatabase("BolsaTrabajo");
            collection = database.GetCollection<Solicitud>("Solicitudes");
        }

        public async Task<bool> EliminarSolicitud(string id)
        {
            try
            {
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var result = await collection.DeleteOneAsync(Builders<Solicitud>.Filter.Eq("_id", objectId));
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

        public async Task<bool> InsertarSolicitud(Solicitud solicitud)
        {
            try
            {
                await collection.InsertOneAsync(solicitud);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Solicitud>> ListarSolicitudes()
        {
            List<Solicitud> lista = new List<Solicitud>();

            lista = await collection.Find(d => true).ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarSolicitud(Solicitud solicitud, string id)
        {
            try
            {
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    bool sw = false;
                    Solicitud modificar = await collection.Find(Builders<Solicitud>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
                    if (modificar != null)
                    {
                        modificar.IdAfiliado = solicitud.IdAfiliado;
                        modificar.NombreCompleto = solicitud.NombreCompleto;
                        modificar.FechaPostulacion = solicitud.FechaPostulacion;
                        modificar.PretencionSalarial = solicitud.PretencionSalarial;
                        modificar.Acercade = solicitud.Acercade;
                        modificar.IdOferta = solicitud.IdOferta;

                        await collection.ReplaceOneAsync(Builders<Solicitud>.Filter.Eq("_id", objectId), modificar);
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

        public async Task<Solicitud> ObtenerById(string id)
        {
            try
            {
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var cursor = await collection.Find(Builders<Solicitud>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();

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
