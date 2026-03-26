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

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "api/empleados";

            List<Empleado> empleados = await this.CallApiAsync<List<Empleado>>(request);

            return empleados;
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            string request = "api/empleados/oficios";

            List<string> oficios = await this.CallApiAsync<List<string>>(request);

            return oficios;
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            string request = "/api/empleados/empleadosbyoficio/" + oficio;

            List<Empleado> empleados = await this.CallApiAsync<List<Empleado>>(request);

            return empleados;
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            string request = "/api/empleados/empleadobypk/" + idEmpleado;

            Empleado empleado = await this.CallApiAsync<Empleado>(request);

            return empleado;
        }
    }
}