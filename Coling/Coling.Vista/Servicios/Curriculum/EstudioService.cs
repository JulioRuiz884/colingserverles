using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class EstudioService: IEstudioService
    {
        string url = "http://localhost:7214";
        string endPoint = "";
        HttpClient client = new HttpClient();

        public EstudioService(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri(url);
        }
        public async Task<List<Estudio>> ListarEstudio(string token)
        {
            endPoint = "api/ListarEstudio";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Estudio> result = new List<Estudio>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Estudio>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> InsertarEstudio(Estudio estudio, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarEstudio";
            string jsonBody = JsonConvert.SerializeObject(estudio);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> EliminarEstudio(string id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/EliminarEstudio/" + id;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        } 

        public async Task<bool> ModificarEstudio(Estudio estudio, string id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/ModificarEstudio/" + id;
            string jsonBody = JsonConvert.SerializeObject(estudio);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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
