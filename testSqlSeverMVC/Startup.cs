using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using testSqlSeverMVC.Models;

namespace testSqlSeverMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<testSqlSeverMVCContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("testSqlSeverMVCContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

                
            app.UseMvc(routes =>
            {   
                //Define rutas para evitar escribir toda la ruta 
                routes.MapRoute(
                    //ruta por defecto, al poner la url
                    name: "default",

                    //El controlador es HelloWorldController, con acción(método) Index, el parámtro id es opcional
                    //  de esa manera se podría poner localhost/HelloWorld/Welcome/3?Anal
                    //  para que el id sea 3 y el nombre sea Anal
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
