using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcEntityFramework.Models;
using MvcEntityFramework.Repositories;

namespace MvcEntityFramework.Controllers
{
    public class EnfermosController : Controller
    {
        private RepositoryEnfermos repo;
        public EnfermosController(RepositoryEnfermos repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult EliminarEnfermo(int inscripcion)
        {
            this.repo.EliminarEnfermo(inscripcion);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int idinscripcion)
        {
            Enfermo enfermo = this.repo.BuscarEnfermo(idinscripcion);
            return View(enfermo);
        }

        public IActionResult EnfermosGenero(String genero)
        {
            //DEBEMOS ENVIAR DOS CARACTERISTICAS
            //ENVIAMOS LOS GENEROS
            //AL RECIBIR UN GENERO, ENVIAMOS LOS ENFERMOS
            List<Genero> generos = this.repo.GetGeneros();
            ViewData["GENEROS"] = generos;
            if (genero != null) {
                List<Enfermo> enfermos = this.repo.GetEnfermosGenero(genero);
                return View(enfermos);
            }
            else
            {
                return View();
            }
        }

        public IActionResult InsertarEnfermo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertarEnfermo(Enfermo enfermo)
        {
            this.repo.InsertarEnfermo(enfermo.Inscripcion
                , enfermo.Apellido, enfermo.Direccion
                , enfermo.FechaNacimiento, enfermo.Sexo
                , enfermo.SeguridadSocial);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateEnfermo(int inscripcion)
        {
            Enfermo enfermo = this.repo.BuscarEnfermo(inscripcion);
            return View(enfermo);
        }

        [HttpPost]
        public IActionResult UpdateEnfermo(Enfermo enfermo)
        {
            this.repo.ModificarEnfermo(enfermo.Inscripcion
                , enfermo.Apellido, enfermo.Direccion
                , enfermo.FechaNacimiento, enfermo.Sexo
                , enfermo.SeguridadSocial);
            return RedirectToAction("Index");
        }
    }
}
