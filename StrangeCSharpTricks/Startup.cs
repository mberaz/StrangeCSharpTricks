using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StrangeCSharpTricks.DictionaryIsTheNewIf.FileStorage;
using StrangeCSharpTricks.DictionaryIsTheNewIf.Validators;
using System;
using System.Linq;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf
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
            //find all implementations of this interface

            RegisterImplementations(services, typeof(IAttributeValidator));
            RegisterImplementations(services, typeof(IFileStorageProxy));

            services.AddTransient<IEntityValidator, EntityValidator>();
            services.AddTransient<IFileStorageHandler, FileStorageHandler>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StrangeCSharpTricks", Version = "v1" });
            });
        }

        private void RegisterImplementations(IServiceCollection services, Type interfaceType)
        {
            var concreteTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && !p.IsInterface);

            foreach (var type in concreteTypes)
            {
                services.AddTransient(interfaceType, type);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StrangeCSharpTricks v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
