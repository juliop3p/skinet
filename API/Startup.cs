using System.IO;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace API
{
  public class Startup
  {
    private readonly IConfiguration _config;
    public Startup(IConfiguration config)
    {
      _config = config;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // DB Connection App
      services.AddDbContext<StoreContext>(x =>
        x.UseNpgsql(_config.GetConnectionString("DefaultConnection")));

      // DB Connection Identity
      services.AddDbContext<AppIdentityDbContext>(x =>
      {
        x.UseNpgsql(_config.GetConnectionString("IdentityConnection"));
      });

      //Setting up Redis
      services.AddSingleton<IConnectionMultiplexer>(c =>
      {
        var configuration = ConfigurationOptions.Parse(
          _config.GetConnectionString("Redis"),
          true
        );

        return ConnectionMultiplexer.Connect(configuration);
      });

      // Injecting AutoMapper
      services.AddAutoMapper(typeof(MappingProfiles));

      services.AddControllers();

      // Extensions Methods
      services.AddApplicationServices();
      services.AddSwaggerDocumentation();
      services.AddIdentityServices(_config);

      // Cors Configuration
      services.AddCors(opt =>
      {
        opt.AddPolicy("CorsPolicy", policy =>
        {
          policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // Error Handling
      app.UseMiddleware<ExceptionMiddleware>();

      app.UseStatusCodePagesWithReExecute("/errors/{0}");

      app.UseHttpsRedirection();

      app.UseRouting();

      // Serve Statis Files Config
      app.UseStaticFiles();
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), "Content")
        ), RequestPath = "/content"
      });

      // Config JWT Bearer ** must be after UseAuthorization() **
      app.UseAuthentication();

      app.UseAuthorization();

      // Cors Configuration
      app.UseCors("CorsPolicy");

      app.UseSwaggerDocumentation();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapFallbackToController("Index", "Fallback");
      });
    }
  }
}
