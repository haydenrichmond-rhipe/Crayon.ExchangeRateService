using Crayon.ExchangeRateHost.Core;
using Crayon.ExchangeRates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Crayon.ExchangeRateService
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crayon.ExchangeRateService", Version = "v1" });
            });

            services.Scan(scan => scan
                .FromAssemblyOf<CrayonExchangeRateHostCoreReference>()
                .AddClasses(classes => classes.InExactNamespaces("Crayon.ExchangeRateHost.Core.Services"))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .FromAssemblyOf<CrayonExchangeRatesReference>()
                .AddClasses(classes => classes.InExactNamespaces("Crayon.ExchangeRates.Services"))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .FromAssemblyOf<CrayonExchangeRatesReference>()
                .AddClasses(classes => classes.InExactNamespaces("Crayon.ExchangeRates.Validators"))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crayon.ExchangeRateService v1"));
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
