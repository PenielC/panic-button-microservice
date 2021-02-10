using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database;
using Helper.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository;
using Services;
using Swagger_Dot_Net_Core;

namespace AccountMicroservice
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
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            SwaggerServiceModule.Register(services, new SwaggerIntegrationHelper
            {
                Description = "Account Microservice Api",
                Email = "peniel.chingombe@gmail.com",
                JWTISSUER = Configuration["AppConfig:JWTISSUER"],
                JWTKEY = Configuration["AppConfig:JWTKEY"],
                Title = "Api",
                Version = "V1",
                xmlPath = xmlPath
            });
            var DbConfiguration = Configuration.GetConnectionString("Dev");
            services.Configure<AppCustomConfig>(Configuration.GetSection("AppConfig"));
            DatabaseModule.Register(services, DbConfiguration);
            RepositoryModule.Register(services);
            ServiceModule.Register(services);
            services.AddControllers();
            services.AddApiVersioning(
              options =>
              {
                   // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                   options.ReportApiVersions = true;
              });
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthorization();
            app.UseAuthentication();
            SwaggerConfigModule.Register(app, true, provider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
