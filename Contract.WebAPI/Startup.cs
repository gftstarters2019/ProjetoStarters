using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services;
using Backend.Services.Validators;
using Backend.Services.Validators.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Contract.WebAPI
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
                options.Filters.Add(new CorsAuthorizationFilterFactory("ContractPermission"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("ContractPermission",
                builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials());
            });

            // Services
            services.AddScoped<Backend.Services.Services.Interfaces.IService<Backend.Core.Domains.CompleteContractDomain>, CompleteContractService>();

            // Repositories
            services.AddScoped<IRepository<Backend.Core.Domains.CompleteContractDomain>, CompleteContractRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.ContractEntity>, ContractRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.SignedContractEntity>, SignedContractRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.IndividualEntity>, IndividualRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.PetEntity>, PetRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.MobileDeviceEntity>, MobileDeviceRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.RealtyEntity>, RealtyRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.VehicleEntity>, VehicleRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.ContractBeneficiary>, ContractBeneficiaryRepository>();
            
            // Validators
            services.AddScoped<IAddressValidator, AddressValidator>();

            services.AddScoped<IDateValidator, DateValidator>();

            services.AddScoped<IIndividualValidator, IndividualValidator>();

            services.AddScoped<IMobileDeviceValidator, MobileDeviceValidator>();

            services.AddScoped<INumberValidator, NumberValidator>();

            services.AddScoped<IPetValidator, PetValidator>();

            services.AddScoped<IRealtyValidator, RealtyValidator>();

            services.AddScoped<IRepository<Backend.Core.Models.AddressEntity>, AddressRepository>();

            services.AddScoped<IVehicleValidator, VehicleValidator>();

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

            app.UseCors("ContractPermission");

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
