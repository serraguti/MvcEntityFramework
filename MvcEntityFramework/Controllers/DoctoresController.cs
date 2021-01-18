using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcEntityFramework.Models;
using MvcEntityFramework.Repositories;

namespace MvcEntityFramework.Controllers
{
    public class DoctoresController : Controller
    {
        RepositoryDoctores repo;
        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateDoctoresEspecialidad()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        [HttpPost]
        public IActionResult UpdateDoctoresEspecialidad
            (int iddoctor, String especialidad)
        {
            this.repo.UpdateEspecialidad(iddoctor, especialidad);
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult ModificarSalarioDoctoresEspecialidad()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            List<String> especialidades = this.repo.GetEspecialidades();
            ViewData["ESPECIALIDADES"] = especialidades;
            return View(doctores);
        }

        [HttpPost]
        public IActionResult ModificarSalarioDoctoresEspecialidad
            (String especialidad, int incremento)
        {
            List<string> especialidades = this.repo.GetEspecialidades();
            ViewData["ESPECIALIDADES"] = especialidades;
            this.repo.UpdateSalarioDoctoresEspecialidad
                (especialidad, incremento);
            List<Doctor> doctores =
                this.repo.GetDoctoresEspecialidad(especialidad);
            return View(doctores);
        }
    }
}
