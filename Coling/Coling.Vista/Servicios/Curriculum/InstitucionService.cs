using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class InstitucionService: IInstitucionService
    {
        string url = "http://localhost:7214";
        string endPoint = "";
        HttpClient client = new HttpClient();

        public InstitucionService(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri(url);
        }
        public async Task<List<Institucion>> ListarInstituciones(string token)
        {
            endPoint = "api/ListarInstitucion";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Institucion> result = new List<Institucion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Institucion>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<bool> InsertarInstitucion(Institucion institucion, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarInstitucion";
            string jsonBody = JsonConvert.SerializeObject(institucion);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
        public async Task<bool> EliminarInstitucion(int id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/EliminarInstitucion/" + id;
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
        public async Task<bool> ModificarInstitucion(Institucion institucion, int id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/ModificarInstitucion/" + id;
            string jsonBody = JsonConvert.SerializeObject(institucion);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
    }
}
