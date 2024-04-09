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

        public async Task<bool> EliminarExperienciaLaboral(int id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/EliminarExperienciaLaboral/" + id;
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarExperienciaLaboral(ExperienciaLaborales experienciaLaboral, string token)
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

        public async Task<List<ExperienciaLaborales>> ListarExperienciaLaboral(string token)
        {
            endPoint = "api/ListarExperienciaLaboral";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<ExperienciaLaborales> result = new List<ExperienciaLaborales>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ExperienciaLaborales>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> ModificarExperienciaLaboral(ExperienciaLaborales experienciaLaboral, int id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/ModificarExperienciaLaboral/" + id;
            string jsonBody = JsonConvert.SerializeObject(experienciaLaboral);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        Task<List<ExperienciaLaborales>> IExperienciaLaboralsService.ListarExperienciaLaboral(string token)
        {
            throw new NotImplementedException();
        }
    }
}
