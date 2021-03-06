﻿using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services;
using Backend.Services.Services.Interfaces;
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

namespace ContractHolder.WebAPI
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
                options.Filters.Add(new CorsAuthorizationFilterFactory("ContractHolderPermission"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("ContractHolderPermission",
                builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials());
            });

            // Services
            services.AddScoped<IService<ContractHolderDomain>, ContractHolderService>();

            // Repositories
            services.AddScoped<IRepository<BeneficiaryAddress>, BeneficiaryAddressRepository>();

            services.AddScoped<IRepository<ContractHolderDomain>, ContractHolderRepository>();

            services.AddScoped<IRepository<ContractEntity>, ContractRepository>();

            services.AddScoped<IRepository<TelephoneEntity>, TelephoneRepository>();

            services.AddScoped<IRepository<IndividualEntity>, IndividualRepository>();

            services.AddScoped<IRepository<IndividualTelephone>, IndividualTelephoneRepository>();
            
            services.AddScoped<IRepository<RealtyEntity>, RealtyRepository>();

            services.AddScoped<IRepository<AddressEntity>, AddressRepository>();
            
            services.AddScoped<IRepository<SignedContractEntity>, SignedContractRepository>();

            // Validators
            services.AddScoped<IContractHolderValidator, ContractHolderValidator>();

            services.AddScoped<IDateValidator, DateValidator>();

            services.AddScoped<IIndividualValidator, IndividualValidator>();

            services.AddScoped<INumberValidator, NumberValidator>();

            services.AddScoped<ITelephoneValidator, TelephoneValidator>();

            services.AddScoped<IAddressValidator, AddressValidator>();

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

            app.UseCors("ContractHolderPermission");

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
