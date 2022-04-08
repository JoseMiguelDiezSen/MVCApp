using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MVCApp.Controllers;
using MVCApp.Models;

namespace MVCApp
{
    public class Startup
    {
        // Con esta variable podremos acceder a los recursos de appSettings
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("ConexionSQL")));
            // Para agregar este tipo de arquitectura
            services.AddMvc();
            // Crea un servicio Singleton cuando se solicita por primera vez. Una vez que se instancia una vez no se vuelve a instanciar
            //services.AddSingleton<IAmigoAlmacen, MockAmigoRepositorio>();
            services.AddScoped<IAmigoAlmacen, SQLAmigoRepsitorio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Si el entorno es de desarrollo 
            if (env.IsDevelopment())
            {   // utilizaremos la pagina deexceppcion por defecto
                app.UseDeveloperExceptionPage();
            }

            // Para poder usar ficheros estaticos, deben ir en este orden
            app.UseStaticFiles();
 
            // Definimos que utilice la ruta por defecto
            app.UseMvcWithDefaultRoute();



            // *DELEGADOS DE SOLICITUDES*
            // Metodo Use
            app.Use(async (context,next) =>
            {
                await next.Invoke();

            });

            // Metodo Run
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            
            //app.Map("/map1", HandleMapTest1);
            //app.Map("/map2", HandleMapTest2);
            
        }
    }
}
