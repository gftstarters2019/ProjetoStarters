﻿using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Backend.IntegrationTests
{
    public class StartupTest
    {
        public StartupTest(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IRepository<IndividualEntity>, ContractHolderRepository>();
            services.AddScoped<IRepository<TelephoneEntity>, TelephoneRepository>();
            services.AddScoped<IRepository<AddressEntity>, AddressRepository>();
            services.AddScoped<IRepository<SignedContractEntity>, SignedContractRepository>();

            services.AddScoped<IRepository<ContractHolderViewModel>, ContractHolderViewModelRepository>();

            services.AddScoped<IRepository<Backend.Core.Models.Contract>, ContractRepository>();

            services.AddScoped<IRepository<ContractViewModel>, ContractViewModelRepository>();

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            }).AddApplicationPart(Assembly.Load("ContractHolder.WebAPI"));

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            }).AddApplicationPart(Assembly.Load("Contract.WebAPI"));

            services.AddDbContext<ConfigurationContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
