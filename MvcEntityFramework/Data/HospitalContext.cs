using Microsoft.EntityFrameworkCore;
using MvcEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEntityFramework.Data
{
    public class HospitalContext: DbContext
    {
        //TENDRA UN CONSTRUCTOR OBLIGATORIO CON OPTIONS
        //PARA EL CONTEXT
        public HospitalContext(DbContextOptions options)
            :base(options)
        {}
        //DEBEMOS MAPEAR CON DbSet CADA ENTIDAD PARA QUE SEA
        //ACCESIBLE.  OBLIGATORIO PROPIEDADES
        public DbSet<Hospital> Hospitales { get; set; }
    }
}
