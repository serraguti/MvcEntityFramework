using MvcEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace MvcEntityFramework.Data
{
    public class DepartamentosContextMySql : IDepartamentosContext
    {
        private MySqlDataAdapter addept;
        private DataTable tabladept;

        public DepartamentosContextMySql(String cadena)
        {
            this.addept = new MySqlDataAdapter("SELECT * FROM DEPT", cadena);
            this.tabladept = new DataTable();
            this.addept.Fill(this.tabladept);
        }

        public List<Departamento> GetDepartamentos()
        {
            List<Departamento> departamentos = new List<Departamento>();
            var consulta = from datos in this.tabladept.AsEnumerable()
                           select datos;
            foreach (var fila in consulta)
            {
                Departamento dept = new Departamento();
                dept.Numero = fila.Field<int>("DEPT_NO");
                dept.Nombre = fila.Field<String>("DNOMBRE");
                dept.Localidad = fila.Field<String>("LOC");
                departamentos.Add(dept);
            }
            return departamentos;
        }
    }
}
