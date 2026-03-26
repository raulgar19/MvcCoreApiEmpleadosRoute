using Microsoft.AspNetCore.Mvc;
using MvcCoreApiEmpleadosRoute.Services;
using NugetApiModels.Models;

namespace MvcCoreApiEmpleadosRoute.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceEmpleados service;

        public EmpleadosController(ServiceEmpleados service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<string> oficios = await this.service.GetOficiosAsync();
            List<Empleado> empleados = await this.service.GetEmpleadosAsync();
            ViewData["OFICIOS"] = oficios;

            return View(empleados);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string oficio)
        {
            List<string> oficios = await this.service.GetOficiosAsync();
            List<Empleado> empleados = await this.service.GetEmpleadosOficioAsync(oficio);
            ViewData["OFICIOS"] = oficios;

            return View(empleados);
        }
    }
}