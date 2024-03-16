using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorios
{
    public class GradoAcademicoRepositorio : IGradoAcademicoRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string? tableName;
        private readonly IConfiguration configuration;

        public GradoAcademicoRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaConexion").Value;
            tableName = "GradoAcademico";
        }

        public async Task<bool> Eliminar(string partitiokey, string rowkey, string etag)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.DeleteEntityAsync(partitiokey, rowkey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Insertar(GradoAcademico gradoAcademico)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.UpsertEntityAsync(gradoAcademico);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Modificar(GradoAcademico gradoAcademico)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.UpdateEntityAsync(gradoAcademico, gradoAcademico.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<GradoAcademico> ObtenerById(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tableName);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (GradoAcademico gradAc in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                return gradAc;
            }
            return null;
        }

        public async Task<List<GradoAcademico>> ObtenerTodo()
        {
            List<GradoAcademico> lista = new List<GradoAcademico>();
            var tablaCliente = new TableClient(cadenaConexion, tableName);
            var filtro = "$PartitionKey eq 'Educacion'";
            // Realizar la consulta y esperar el resultado
            await foreach (var gradAc in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                lista.Add(gradAc);
            }
            return lista;
        }
    }
}
