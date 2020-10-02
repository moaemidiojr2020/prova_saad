using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using UI.Web.API.Extensions.Swagger;
using UI.Web.IoC;

namespace UI.API.Web
{
    public class Startup
    {

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);


            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        const string PT_BR = "pt-BR";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowAnyOrigin();
                });
            });


            services.AddControllers()
            .AddNewtonsoftJson(opt =>
            {
	            opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
	            opt.SerializerSettings.Culture = new CultureInfo(PT_BR);
	            opt.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
	            opt.SerializerSettings.DateFormatString = "dd'/'MM'/'yyyy";

            })
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.IgnoreNullValues = true;
                opt.JsonSerializerOptions.WriteIndented = true;
            });
         
            services.RegisterServices(Configuration);
            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<HeaderParameters>();

                c.DescribeAllEnumsAsStrings();

                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1.0.0",
                    Title = $"Prova SAAD API",
                    Description = "Prova SAAD API v1",
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }   

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prova SAAD API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
