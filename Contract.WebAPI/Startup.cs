using Backend.Application.ViewModels;
using Backend.Core;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories;
using Backend.Services.Services;
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

            services.AddScoped<Backend.Infrastructure.Repositories.Contracts.IRepository<Backend.Core.Models.Contract>, ContractRepository>();

            services.AddScoped<Backend.Infrastructure.Repositories.Contracts.IRepository<Backend.Core.Models.SignedContract>, SignedContractRepository>();

            services.AddScoped<Backend.Infrastructure.Repositories.Contracts.IRepository<ContractViewModel>, ContractViewModelRepository>();

            services.AddScoped<Backend.Services.Services.Contracts.IService<Backend.Core.Models.CompleteContract>, CompleteContractService>();

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
