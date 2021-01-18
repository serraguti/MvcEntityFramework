using Microsoft.EntityFrameworkCore;
using MvcEntityFramework.Data;
using MvcEntityFramework.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;

namespace MvcEntityFramework.Repositories
{
    public class RepositoryDoctores
    {
        HospitalContext context;
        public RepositoryDoctores(HospitalContext context)
        {
            this.context = context;
        }

        public void UpdateEspecialidad(int iddoctor, String espe)
        {
            this.context.ModificarEspecialidad(iddoctor, espe);
        }

        //PROCEDIMIENTO ALMACENADO CON SELECT
        public List<Doctor> GetDoctores()
        {
            //EN ESTE CASO NO TIENE PARAMETROS, PERO LA CONSULTA
            //ES IGUAL CON PARAMETROS
            String sql = "mostrardoctores";
            //LA CONSULTA SE EJECUTA A PARTIR DE UN DbSet DE COLECCION
            //Y AUTOMATICAMENTE MAPEARA LOS CAMPOS DE LOS DATOS
            //QUE DEVUELVA EL PROCEDURE
            List<Doctor> doctores = 
                this.context.Doctores.FromSqlRaw(sql).ToList();
            return doctores;
        }

        public List<Doctor> GetDoctoresEspecialidad
            (String espe)
        {
            String sql = "doctoresespecialidad @especialidad";
            SqlParameter pamespe =
                new SqlParameter("@especialidad", espe);
            List<Doctor> doctores =
                this.context.Doctores.FromSqlRaw(sql, pamespe).ToList();
            return doctores;
        }

        public void UpdateSalarioDoctoresEspecialidad
            (String espe, int incremento)
        {
            String sql = "updatesalariodoctorespecialidad "
                + " @especialidad, @incremento";
            SqlParameter pamespe =
                new SqlParameter("@especialidad", espe);
            SqlParameter paminc =
                new SqlParameter("@incremento", incremento);
            this.context.Database.ExecuteSqlRaw(sql, pamespe, paminc);
        }

        public List<String> GetEspecialidades()
        {
            //PARA LLAMAR A PROCEDIMIENTOS SELECT
            //QUE NO TENEMOS MAPEADOS MEDIANTE DbSet
            //NECESITAMOS HACERLO A LA "ANTIGUA"
            //Y MAPEAR NOSOTROS MANUALMENTE LA RESPUESTA
            //SE UTILIZAN OBJEROS STANDAR DE ADO
            //PERO DE CORE
            //TAMBIEN SE DEBE UTILIZAR LA CONEXION DE LINQ
            using (DbCommand com =
                this.context.Database.GetDbConnection().CreateCommand())
            {
                String sql = "mostrarespecialidades";
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();
                List<string> especialidades = new List<string>();
                while (reader.Read())
                {
                    especialidades.Add(reader["ESPECIALIDAD"].ToString());
                }
                reader.Close();
                com.Connection.Close();
                return especialidades;
            }
        }
    }
}
