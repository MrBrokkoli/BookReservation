using Loading.DataAccess.Repositories;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;
using BookReservation.DataAccess.Repositories.Base;
using BookReservation.DataAccess;
using Microsoft.EntityFrameworkCore;
using BookReservation.Services.Base.Interfaces;
using System.Security.AccessControl;

namespace BookReservation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BaseDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));

            var listServiceTypes = typeof(IService).Assembly.GetTypes().AsEnumerable()
                .Where(t => typeof(IService).IsAssignableFrom(t) && t != typeof(IService))
                .Where(x => x.IsClass)
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var serviceType in listServiceTypes)
            {
                var interfaceName = $"I{serviceType.Name}";
                var interfaceType = serviceType.GetInterfaces().First(x => x.Name == interfaceName);
                services.AddScoped(interfaceType, serviceType);
            }
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BaseDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
