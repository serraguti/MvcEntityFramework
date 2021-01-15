using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcEntityFramework.Models;
using MvcEntityFramework.Repositories;

namespace MvcEntityFramework.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;
        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IncrementarSalarios()
        {
            List<String> oficios = this.repo.GetOficios();
            ViewData["OFICIOS"] = oficios;
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        [HttpPost]
        public IActionResult IncrementarSalarios(String oficio
            , int incremento)
        {
            List<String> oficios = this.repo.GetOficios();
            ViewData["OFICIOS"] = oficios;
            List<Empleado> empleados =
                this.repo.GetEmpleadosOficio(oficio);
            this.repo.IncrementarSalariosOficios(oficio, incremento);
            return View(empleados);
        }

        public IActionResult EmpleadosDepartamentoLambda()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmpleadosDepartamentoLambda(int departamento)
        {
            ResumenDepartamento model =
                this.repo.GetResumenDepartamento(departamento);
            return View(model);
        }
    }
}
