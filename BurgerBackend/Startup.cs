using BurgerBackend.Data;
using BurgerBackend.DTO;
using BurgerBackend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BurgerBackend
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
            services.AddEntityFrameworkNpgsql().AddDbContext<BurgerDBContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("BurgerConnection")));
            services.AddScoped<IBurgerRepository, BurgerRepository>();
            services.AddScoped<IMappers, Mappers>();
            services.AddScoped<IGeoService>(g => new GoogleGeoService(""));
            //services.AddScoped<IImageSerce>(x => new ImageService(@"C:\Users\TamasVeingartner\Pictures\Saved Pictures"));
            services.AddScoped<IImageSerce>(x => new AWSs3ImageService("", "", "burger-backend"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BurgerBackend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BurgerBackend v1"));

            app.UseStaticFiles();
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
