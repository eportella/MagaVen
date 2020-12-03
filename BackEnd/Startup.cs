using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackEnd", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
            });
            services.AddSingleton(s=> new MongoClient(Configuration["MongoDb:ConnectionString"]));
            services.AddScoped<Catalogo.Service>(
                service => new Catalogo.MongoDb.Service(service.GetService<MongoClient>(), "magaven")
            );
            services.AddScoped<Cliente.Service>(
                service => new Cliente.MongoDb.Service(service.GetService<MongoClient>(), "magaven")
            );
            services.AddScoped<Armazem.Service>(
                service => new Armazem.MongoDb.Service(service.GetService<MongoClient>(), "magaven")
            );
            services.AddScoped<Produto.Service>(
                service => new Produto.MongoDb.Service(service.GetService<MongoClient>(), "magaven")
            );
            services.AddScoped<SeuCliente.Service>(
                service => new SeuCliente.MongoDb.Service(service.GetService<MongoClient>(), "magaven")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
