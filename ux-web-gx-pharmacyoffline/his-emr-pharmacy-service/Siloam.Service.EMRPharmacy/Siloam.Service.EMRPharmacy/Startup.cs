using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration;
using Siloam.System;
using Siloam.Service.EMRPharmacy.Commons;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;
using Siloam.Service.EMRPharmacy.Hub;
using Siloam.Service.EMRPharmacy.Models;

namespace Siloam.Service.EMRPharmacy
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory factory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddCloudFoundry()
                .AddEnvironmentVariables();

            Configuration = builder.Build();

        }


        public IConfigurationRoot Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            SetApplicationSettings();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(ApplicationSetting.ConnectionString));
            services.AddDiscoveryClient(Configuration);
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v2.0", new Info
                {

                    Title = "Siloam Service for EMR Pharmacy",
                    Version = "v1.0",
                    Description = "Build on the .NET Core 2.0 and SignalR - GTN"

                });

            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddCors();
            services.AddMvcCore().AddApiExplorer();
            services.AddMvc();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddOptions();
            services.AddSignalR();

            //mapper digunakan untuk mapping antar model agar properti dari model dapat digunakan di dalam function di repository untuk model lainnya.
            AutoMapper.Mapper.Initialize(mapper =>
            {

                //mapper.CreateMap<Models.Users, Models.ViewModels.UserMasters>().ReverseMap();
                //mapper.CreateMap<Models.UserContacts, Models.ViewModels.UserDetails>().ReverseMap();
                //mapper.CreateMap<Models.UserApplications, Models.ViewModels.UserApplicationDetails>().ReverseMap();

            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {

                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    // Custom
                    "image/svg+xml"
                };

            });

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDiscoveryClient();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseSwagger();
            app.UseResponseCompression();
            app.UseMvc();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "Siloam Service for EMR Pharmacy API");
            });
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("messagehub");
            });

        }


        private void SetApplicationSettings()
        {
            if (Configuration["Data:keyregistry"].ToString() == "1")
            {
                ApplicationSetting.ConnectionString = Siloam.Config.Configuration.Functions.GetValue("DB_EMRPharmacy").ToString();

                Configuration["Data:ApiUrl"] = Siloam.Config.Configuration.Functions.GetValue("urlPharmacy").ToString();
                Configuration["Data:ApiName"] = Siloam.Config.Configuration.Functions.GetValue("APINamePharmacy").ToString();
                Configuration["Data:UrlSync"] = Siloam.Config.Configuration.Functions.GetValue("urlExtension").ToString();
                Configuration["Data:CompoundItem"] = Siloam.Config.Configuration.Functions.GetValue("CompoundItem").ToString();
                Configuration["Data:AidoUrlSync"] = Siloam.Config.Configuration.Functions.GetValue("AidoUrlSync").ToString();
                Configuration["Data:AidoSecretKey"] = Siloam.Config.Configuration.Functions.GetValue("AidoSecretKey").ToString();
                Configuration["Data:AidoPharmacyId"] = Siloam.Config.Configuration.Functions.GetValue("AidoPharmacyId").ToString();
                Configuration["Data:MySiloamUrlSync"] = Siloam.Config.Configuration.Functions.GetValue("BaseURL_MySiloam_MobileInternal").ToString();
                Configuration["Data:MySiloaURLQueueEngine"] = Siloam.Config.Configuration.Functions.GetValue("BaseURL_MySiloam_QueueEngine").ToString();
                Configuration["Data:UrlDeliveryFee"] = Siloam.Config.Configuration.Functions.GetValue("UrlDeliveryFee").ToString();
                Configuration["Data:AidoUrlSyncNew"] = Siloam.Config.Configuration.Functions.GetValue("AidoUrlSyncNew").ToString();
                Configuration["Data:AidoUrlSyncDrug"] = Siloam.Config.Configuration.Functions.GetValue("AidoUrlSyncDrug").ToString();
                Configuration["Data:AidoSecretKeyV2"] = Siloam.Config.Configuration.Functions.GetValue("AidoSecretKeyV2").ToString();
                Configuration["Data:IntegrationConnectionString"] = Siloam.Config.Configuration.Functions.GetValue("DB_Integration").ToString();

                ValueStorage.SlackUrl = Configuration["Data:SlackUrl"].ToString();
                ValueStorage.ApiUrl = Configuration["Data:ApiUrl"].ToString();
                ValueStorage.ApiName = Configuration["Data:ApiName"].ToString();
                ValueStorage.UrlSync = Configuration["Data:UrlSync"].ToString();
                ValueStorage.CompoundItem = Configuration["Data:CompoundItem"].ToString();
                ValueStorage.AidoUrlSync = Configuration["Data:AidoUrlSync"].ToString();
                ValueStorage.AidoSecret = Configuration["Data:AidoSecretKey"].ToString();
                ValueStorage.AidoPharmacyId = Configuration["Data:AidoPharmacyId"].ToString();
                ValueStorage.MySiloamUrlSync = Configuration["Data:MySiloamUrlSync"].ToString();
                ValueStorage.MySiloamUrlQueueEngine = Configuration["Data:MySiloaURLQueueEngine"].ToString();
                ValueStorage.UrlDeliveryFee = Configuration["Data:UrlDeliveryFee"].ToString();
                ValueStorage.AidoUrlSyncNew = Configuration["Data:AidoUrlSyncNew"].ToString();
                ValueStorage.AidoUrlSyncDrug = Configuration["Data:AidoUrlSyncDrug"].ToString();
                ValueStorage.AidoSecretV2 = Configuration["Data:AidoSecretKeyV2"].ToString();
                ValueStorage.IntegrationConnectionString = Configuration["Data:IntegrationConnectionString"].ToString();

            }
            else
            {
                ApplicationSetting.ConnectionString = Configuration["Data:ConnectionString"];

                ValueStorage.SlackUrl = Configuration["Data:SlackUrl"].ToString();
                ValueStorage.ApiUrl = Configuration["Data:ApiUrl"].ToString();
                ValueStorage.ApiName = Configuration["Data:ApiName"].ToString();
                ValueStorage.UrlSync = Configuration["Data:UrlSync"].ToString();
                ValueStorage.CompoundItem = Configuration["Data:CompoundItem"].ToString();
                ValueStorage.AidoUrlSync = Configuration["Data:AidoUrlSync"].ToString();
                ValueStorage.AidoSecret = Configuration["Data:AidoSecretKey"].ToString();
                ValueStorage.AidoSecret = Configuration["Data:AidoSecretKey"].ToString();
                ValueStorage.AidoPharmacyId = Configuration["Data:AidoPharmacyId"].ToString();
                ValueStorage.MySiloamUrlSync = Configuration["Data:MySiloamUrlSync"].ToString();
                ValueStorage.MySiloamUrlQueueEngine = Configuration["Data:MySiloaURLQueueEngine"].ToString();
                ValueStorage.UrlDeliveryFee = Configuration["Data:UrlDeliveryFee"].ToString();
                ValueStorage.AidoUrlSyncNew = Configuration["Data:AidoUrlSyncNew"].ToString();
                ValueStorage.AidoUrlSyncDrug = Configuration["Data:AidoUrlSyncDrug"].ToString();
                ValueStorage.AidoSecretV2 = Configuration["Data:AidoSecretKeyV2"].ToString();
                ValueStorage.IntegrationConnectionString = Configuration["Data:IntegrationConnectionString"].ToString();
            }
            
        }

    }
}
