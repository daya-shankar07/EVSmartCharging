using Application.SmartCharging.BL;
using Application.SmartCharging.Common;
using Application.SmartCharging.Common.ConfigOptions;
using Application.SmartCharging.DL;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.SmartCharging
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

            services.AddControllers();
            services.AddSingleton<ITelemetryAdaptor, TelemetryAdaptor>();
            // can extract as extension method to separate class
            services.AddSingleton<IGroupRepository, GroupRepository>();
            services.AddSingleton<IConnectorRepository, ConnectorRepository>();
            services.AddSingleton<IChargeStationRepository, ChargeStationRepository>();
            services.AddSingleton<IGroupService, GroupService>();
            services.AddSingleton<IConnectorService, ConnectorService>();
            services.AddSingleton<IChargeStationService, ChargeStationService>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            //
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SmartChargingApi",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Email = "daya.shankar58@gmail.com",
                        Name = "Daya Shankar"
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application.SmartCharging v1"));
            }

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
