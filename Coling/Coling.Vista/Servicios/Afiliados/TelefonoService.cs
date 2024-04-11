using Coling.Vista.Modelos;
using Coling.Vista.Servicios.Afiliados.Contratos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class TelefonoService:ITelefonoService
	{
		string url = "http://localhost:7169";
		string endPoint = "";
		HttpClient client = new HttpClient();


		public async Task<bool> InsertarTelefono(Telefono telefono, string token)
		{
			bool sw = false;
			endPoint = url + "/api/insertarTelefono";
			string jsonBody = JsonConvert.SerializeObject(telefono);
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
			HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
			if (respuesta.IsSuccessStatusCode)
			{
				sw = true;
			}
			return sw;

		}
		public async Task<bool> ModificarTelefono(Telefono telefono, int id, string token)
		{
			bool sw = false;
			endPoint = url + "/api/modificarTelefono/" + id;
			string jsonBody = JsonConvert.SerializeObject(telefono);
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
			HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
			if (respuesta.IsSuccessStatusCode)
			{
				sw = true;
			}
			return sw;

		}
		public async Task<bool> EliminarTelefono(int id, string token)
		{
			bool sw = false;
			endPoint = url + "/api/eliminarTelefono/" + id;
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
			if (respuesta.IsSuccessStatusCode)
			{
				sw = true;
			}
			return sw;

		}
		public async Task<Telefono> ObtenerTelefonoById(int id, string token)
		{
			endPoint = "/api/obtenerTelefonobyid/" + id;
			if (client.BaseAddress == null)
			{
				client.BaseAddress = new Uri(url);
			}
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage respuesta = await client.GetAsync(endPoint);
			Telefono telefono = new Telefono();
			if (respuesta.IsSuccessStatusCode)
			{
				string respuestaCuerpo = await respuesta.Content.ReadAsStringAsync();
				telefono = JsonConvert.DeserializeObject<Telefono>(respuestaCuerpo)!;
			}
			return telefono;

		}
		public async Task<List<Telefono>> ListarTelefonos(string token)
		{
			endPoint = "/api/listarTelefono";
			if (client.BaseAddress == null)
			{
				client.BaseAddress = new Uri(url);
			}
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage response = await client.GetAsync(endPoint);
			List<Telefono> result = new List<Telefono>();
			if (response.IsSuccessStatusCode)
			{
				string respuestaCuerpo = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<List<Telefono>>(respuestaCuerpo)!;
			}

			return result;
		}


	}
}
