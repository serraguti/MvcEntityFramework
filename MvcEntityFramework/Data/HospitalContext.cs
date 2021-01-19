using Microsoft.EntityFrameworkCore;
using MvcEntityFramework.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDIMIENTOS ALMACENADOS
//create procedure MostrarDoctores
//as
//	select * from doctor
//go
//create procedure cambiarespecialidad
//(@iddoctor int, @especialidad nvarchar(30))
//as
//update doctor set especialidad = @especialidad
//where doctor_no = @iddoctor
//go
//create procedure doctoresespecialidad
//(@especialidad nvarchar(30))
//as
//    select* from doctor

//    where especialidad = @especialidad
//go
//create procedure updatesalariodoctorespecialidad
//(@especialidad nvarchar(30), @incremento int)
//as
//    update doctor set salario += @incremento
//	where ESPECIALIDAD = @especialidad
//go
//create procedure mostrarespecialidades
//as
//	select distinct especialidad from doctor
//go
//create procedure procempleadoshospital
//(@hospitalcod int, @suma int out
//, @media int out)
//as
//    select* from empleadoshospital
//    where hospital_cod = @hospitalcod
//            select @suma = sum(salario), @media = avg(salario)
//    from empleadoshospital
//    where hospital_cod = @hospitalcod
//go
//create view empleadoshospital
//as
//	select isnull(empleado_no, 0) as IdEmpleado
//	, apellido, funcion, salario, hospital_cod
//    from plantilla
//	union
//	select doctor_no, apellido, especialidad, salario
//    , hospital_cod
//	from doctor
//go
#endregion

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

        public DbSet<Doctor> Doctores { get; set; }

        public DbSet<EmpleadoHospital> EmpleadosHospital { get; set; }

        //CREAMOS EL PRIMER PROCEDIMIENTO DE ACCION
        public void ModificarEspecialidad(int iddoctor, String espe)
        {
            String sql = "cambiarespecialidad @iddoctor, @especialidad";
            //NECESITAMOS PARAMETROS PARA ENVIAR LOS DATOS
            //AL PROCEDIMIENTO
            SqlParameter pamid = new SqlParameter("@iddoctor", iddoctor);
            SqlParameter pamespe =
                new SqlParameter("@especialidad", espe);
            //EL OBJETO CONTEXT CONTIENE UNA PROPIEDAD DATABASE
            //QUE ES LA ENCARGADA DE EJECUTAR LAS CONSULTAS DE 
            //ACCION SOBRE LA BBDD
            this.Database.ExecuteSqlRaw(sql, pamid, pamespe);
        }
    }
}
