using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services;
using Backend.Services.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

            // Services
            services.AddScoped<IService<ContractHolderDomain>, ContractHolderService>();

            services.AddScoped<IService<CompleteContractDomain>, CompleteContractService>();

            // Repositories
            services.AddScoped<IRepository<ContractHolderDomain>, ContractHolderRepository>();

            services.AddScoped<IRepository<TelephoneEntity>, TelephoneRepository>();

            services.AddScoped<IRepository<IndividualEntity>, IndividualRepository>();

            services.AddScoped<IRepository<IndividualTelephone>, IndividualTelephoneRepository>();

            services.AddScoped<IRepository<AddressEntity>, AddressRepository>();

            services.AddScoped<IRepository<BeneficiaryAddress>, BeneficiaryAddressRepository>();

            services.AddScoped<IRepository<SignedContractEntity>, SignedContractRepository>();

            services.AddScoped<IRepository<CompleteContractDomain>, CompleteContractRepository>();

            services.AddScoped<IRepository<ContractEntity>, ContractRepository>();

            services.AddScoped<IRepository<PetEntity>, PetRepository>();

            services.AddScoped<IRepository<MobileDeviceEntity>, MobileDeviceRepository>();

            services.AddScoped<IRepository<RealtyEntity>, RealtyRepository>();

            services.AddScoped<IRepository<VehicleEntity>, VehicleRepository>();

            services.AddScoped<IRepository<ContractBeneficiary>, ContractBeneficiaryRepository>();

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
