using NugetApiModels.Models;
using System.Net.Http.Headers;

namespace MvcCoreApiEmpleadosRoute.Services
{
    public class ServiceEmpleados
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceEmpleados(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiEmpleados");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleados";

                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    List<Empleado> empleados = await response.Content.ReadAsAsync<List<Empleado>>();

                    return empleados;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleados/oficios";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    List<string> oficios = await response.Content.ReadAsAsync<List<string>>();
                    return oficios;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/empleados/empleadosbyoficio/" + oficio;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    List<Empleado> empleados = await response.Content.ReadAsAsync<List<Empleado>>();
                    return empleados;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}