using AutoMapper;
using DigitalMenuApi.Data;
using DigitalMenuApi.Repository;
using DigitalMenuApi.Repository.Implement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace DigitalMenuApi
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

            int apiVersion = Configuration.GetValue<int>("Version");
            //services.AddTransient<DbContext, DigitalMenuBoxContext>();
            services.AddDbContext<DigitalMenuBoxContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Digital_Menu_Box"));
            });

            //Json for Patch
            services.AddControllers().AddNewtonsoftJson(
                s => { s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); 
                s.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }
            );


            //Add AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Add DI for repository
            services.AddScoped<DbContext, DigitalMenuBoxContext>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            //(Configuration.GetConnectionString("Digital_Menu_Box")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v" + apiVersion, new OpenApiInfo { Title = "Digital Menu Api", Version = "v" + apiVersion });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            int apiVersion = Configuration.GetValue<int>("Version");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v" + apiVersion + "/swagger.json", "Feedback System API");
            });

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
