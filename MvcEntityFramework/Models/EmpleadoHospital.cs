using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEntityFramework.Models
{
    [Table("EMPLEADOSHOSPITAL")]
    public class EmpleadoHospital
    {
        [Key]
        [Column("IDEMPLEADO")]
        public int IdEmpleado { get; set; }
        [Column("APELLIDO")]
        public String Apellido { get; set; }
        [Column("FUNCION")]
        public String Funcion { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
        [Column("HOSPITAL_COD")]
        public int Hospital { get; set; }
    }
}
