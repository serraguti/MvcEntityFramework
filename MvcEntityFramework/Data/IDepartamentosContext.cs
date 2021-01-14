using MvcEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEntityFramework.Data
{
    public interface IDepartamentosContext
    {
        List<Departamento> GetDepartamentos();
    }
}
