using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class ExperienciaLaboralService : IExperienciaLaboralsService
    {
        string url = "http://localhost:7214";
        string endPoint = "";
        HttpClient client = new HttpClient();

        public ExperienciaLaboralService(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri(url);
        }

        public async Task<bool> EliminarExperienciaLaboral(string id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/EliminarExperienciaLaboral/" + id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarExperienciaLaboral(ExperienciaLaboral experienciaLaboral, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarExperienciaLaboral";
            string jsonBody = JsonConvert.SerializeObject(experienciaLaboral);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<ExperienciaLaboral>> ListarExperienciaLaboral(string token)
        {
            endPoint = "api/ListarExperienciaLaboral";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<ExperienciaLaboral> result = new List<ExperienciaLaboral>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ExperienciaLaboral>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> ModificarExperienciaLaboral(ExperienciaLaboral experienciaLaboral, string id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/ModificarExperienciaLaboral/" + id;
            string jsonBody = JsonConvert.SerializeObject(experienciaLaboral);
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
