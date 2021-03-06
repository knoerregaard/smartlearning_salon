using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartLearning_salon.Services.Person;
using SmartLearning_salon.Services.BlobStorage;
using Azure.Storage.Blobs;

namespace SmartLearning_salon
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
            services.AddControllersWithViews();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            // Dependencies registration https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1


            //services.AddScoped<IPersonService, PersonService>();
            services.AddSingleton<IPersonService>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDB")));
            services.AddSingleton<IBlobStorage>(InitializeBlobStorageAsync(Configuration.GetSection("BlobStorage")));

        }

        public static IBlobStorage InitializeBlobStorageAsync(IConfigurationSection configurationSection)
        {

            string Account = configurationSection.GetSection("Account").Value;
            string Key = configurationSection.GetSection("Key").Value;
            string ContainerId = configurationSection.GetSection("ContainerId").Value;
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
            + "AccountName=" + configurationSection.GetSection("Account").Value
            + ";AccountKey=" + configurationSection.GetSection("Key").Value 
            + ";EndpointSuffix=core.windows.net";

            //CosmosClient client = new CosmosClient(Account, Key);
            //IBlobStorage BlobStorage = new BlobStorage();


            BlobContainerClient bcc = new BlobContainerClient(storageConnectionString, ContainerId);

            IBlobStorage BlobStorageService = new BlobStorage(bcc, ContainerId);
            return BlobStorageService;
            //DatabaseResponse response = client.GetDatabase(DatabaseId);
        }


        public static IPersonService InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string DatabaseId = configurationSection.GetSection("DatabaseId").Value;
            string ContainerId = configurationSection.GetSection("ContainerId").Value;
            string Account = configurationSection.GetSection("Account").Value;
            string Key = configurationSection.GetSection("Key").Value;

            CosmosClient client = new CosmosClient(Account, Key);
            IPersonService personService = new PersonService(client, DatabaseId, ContainerId);
            return personService;
            //DatabaseResponse response = client.GetDatabase(DatabaseId);
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
