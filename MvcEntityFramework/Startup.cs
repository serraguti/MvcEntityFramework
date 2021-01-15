using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcEntityFramework.Data;
using MvcEntityFramework.Models;
using MvcEntityFramework.Repositories;

namespace MvcEntityFramework
{
    public class Startup
    {
        //VARIABLE PARA PODER RECUPERAR EL OBJETO DE LA INYECCION
        IConfiguration Configuration { get; set; }
        //PARA PODER ACCEDER AL FICHERO APPSETTINGS.JSON
        //NECESITAMOS REALIZAR INYECCION DE DEPENDENCIAS
        //EN LA CLASE STARTUP DE LA INTERFACE IConfiguration
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            String cadena = 
                Configuration.GetConnectionString("casasqlhospital");
            services.AddTransient<RepositoryEmpleados>();
            services.AddDbContext<EmpleadosContext>(options =>
            options.UseSqlServer(cadena));

            services.AddTransient<RepositoryEnfermos>();
            services.AddDbContext<EnfermosContext>(options =>
            options.UseSqlServer(cadena));

            //RESOLVEMOS LA DEPENDENCIA PARA EL REPOSITORIO
            services.AddTransient<RepositoryHospital>();
            //PARA UTILIZA CONTEXTOS PUROS DbContext DE Entity Framework
            //DEBEMOS UTILIZAR UN METODO ESPECIAL PARA IoC QUE ES
            //.AddDbContext
            services.AddDbContext<HospitalContext>(options =>
            options.UseSqlServer(cadena));

            //String cadena = "Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            //String cadena = Configuration.GetConnectionString("casamysqlhospital");
            //services.AddSingleton<IDepartamentosContext>(context =>
            //new DepartamentosContextSQL(cadena));
            services.AddSingleton<IDepartamentosContext>(context =>
            new DepartamentosContextSQL(cadena));
            //LAS DEPENDENCIAS DE OBJETOS SE RESUELVEN EN LOS SERVICIOS DE LA APP
            //LA PRIMERA OPCION SERA UTILIZAR AddTransient<T>
            //QUE GENERA UN OBJETO POR CADA PETICION DE INYECCION
            //services.AddTransient<Coche>();
            //TAMBIEN TENEMOS LA OPCION DE CREAR UNA UNICA INSTANCIA
            //DE UN OBJETO PARA TODAS LAS PETICIONES DE INYECCION
            //SE REALIZA CON EL METODO .AddSingleton<T>
            //services.AddSingleton<ICoche, Deportivo>();
            services.AddSingleton<ICoche>(z => new Deportivo("Ferrari", "Testarrossa"
                , "ferrari.jpg", 290));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
