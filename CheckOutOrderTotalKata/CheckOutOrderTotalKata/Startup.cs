using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Services;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace CheckOutOrderTotalKata
{
    /// <summary>
    /// Sartup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services. This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Registering Memory Cache to persist data
            services.AddMemoryCache();

            // Registering services
            services.AddSingleton<BaseCacheService<CartItem>, CartService>();
            services.AddSingleton<BaseCacheService<StoreItem>, StoreService>();
            services.AddSingleton<BaseCacheService<MultiplesPromotion>, MultiplesPromotionService>();
            services.AddSingleton<BaseCacheService<MarkdownPromotion>, MarkdownPromotionService>();
            services.AddSingleton<BaseCacheService<BogoPromotion>, BogoPromotionService>();

            // Register Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CheckOutOrderKata API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures the specified application. This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //Global Exception Handling middleware
            app.ConfigureExceptionHandler();

            //Swagger setup
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CheckOutOrderKata API V1");
                c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root
            });

            app.UseMvc();
        }
    }
}
