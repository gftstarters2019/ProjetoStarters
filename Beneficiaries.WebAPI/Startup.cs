using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Backend.Core;
using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services;
using Backend.Services.Services.Interfaces;
using Beneficiaries.WebAPI.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Beneficiaries.WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("BeneficiaryPermission"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("BeneficiaryPermission",
                builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials());
            });

            // Services
            services.AddScoped<IService<BeneficiaryDomain>, BeneficiaryService>();

            services.AddScoped<IService<IndividualDomain>, IndividualService>();

            services.AddScoped<IService<MobileDeviceDomain>, MobileDeviceService>();

            services.AddScoped<IService<RealtyDomain>, RealtyService>();

            services.AddScoped<IService<PetDomain>, PetService>();

            services.AddScoped<IService<VehicleDomain>, VehicleService>();
            
            // Repositories
            services.AddScoped<IRepository<Beneficiary>, BeneficiaryRepository>();

            services.AddScoped<IRepository<ContractBeneficiary>, ContractBeneficiaryRepository>();

            services.AddScoped<IRepository<IndividualEntity>, IndividualRepository>();

            services.AddScoped<IRepository<MobileDeviceEntity>, MobileDeviceRepository>();

            services.AddScoped<IRepository<RealtyEntity>, RealtyRepository>();

            services.AddScoped<IRepository<AddressEntity>, AddressRepository>();

            services.AddScoped<IRepository<BeneficiaryAddress>, BeneficiaryAddressRepository>();

            services.AddScoped<IRepository<PetEntity>, PetRepository>();

            services.AddScoped<IRepository<VehicleEntity>, VehicleRepository>();


            services.AddDbContext<ConfigurationContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            ConfigureSwagger(services);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("BeneficiaryPermission");

            app.UseHttpsRedirection();
            app.UseMvc();

            ConfigureSwaggerUi(app);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Insurance and Health Plans", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private static void ConfigureSwaggerUi(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Insurance and Health Plans");
            });
        }
    }
}
