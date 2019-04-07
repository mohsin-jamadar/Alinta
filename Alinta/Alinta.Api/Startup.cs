using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Alinta.Entity.UnitofWork;
using Alinta.Entity.Context;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Alinta.Core.UnitofWork;
using Alinta.Core.Service.Abstract;
using Alinta.Core.Service;
using Alinta.Core.Service.Generic;
using TestCore.Domain.Mapping;

namespace Alinta.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //db service
            if (Configuration["ConnectionStrings:UseInMemoryDatabase"] == "True")
                services.AddDbContext<AlintaContext>(opt => opt.UseInMemoryDatabase("TestDB-" + Guid.NewGuid().ToString()), ServiceLifetime.Singleton);
            else
                services.AddDbContext<AlintaContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:AlintaDB"]));

            //mvc service
            services.AddMvc();
            

            #region "DI code"

            //general unitofwork injections
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //services injections

            services.AddTransient(typeof(ICustomerService<,>), typeof(CustomerService<,>));
            services.AddTransient(typeof(ICustomerServiceAsync<,>), typeof(CustomerServiceAsync<,>));

            services.AddTransient(typeof(IService<,>), typeof(GenericService<,>));
            services.AddTransient(typeof(IServiceAsync<,>), typeof(GenericServiceAsync<,>));

            #endregion

            //data mapper profiler setting
            
            Mapper.Initialize((config) =>
            {
                config.AddProfile<MappingProfile>();
            });
            
            
            //Swagger API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Alinta API", Version = "v1" });
            });

           
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseMiddleware<ExceptionHandler>();

            app.UseMvc();


            //Swagger API documentation
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alinta API V1");
            });

            //migrations and seeds from json files
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (Configuration["ConnectionStrings:UseInMemoryDatabase"] == "False" && !serviceScope.ServiceProvider.GetService<AlintaContext>().AllMigrationsApplied())
                {
                    if (Configuration["ConnectionStrings:UseMigrationService"] == "True")
                        serviceScope.ServiceProvider.GetService<AlintaContext>().Database.Migrate();
                }
                //it will seed tables on aservice run from json files if tables empty
                if (Configuration["ConnectionStrings:UseSeedService"] == "True")
                    serviceScope.ServiceProvider.GetService<AlintaContext>().EnsureSeeded();
            }
        }


    }
}





