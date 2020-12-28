using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pathfinding.Algorithm;
using Pathfinding.Shared;

namespace Pathfinding
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
      services.AddSingleton<IAlgorithmService, AlgorithmService>();
      services.AddSingleton<IBreadthFirstSearch, BreadthFirstSearch>();
      services.AddSingleton<IDijkstra, Dijkstra>();
      services.AddSingleton<IMapper, Mapper>();

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        });
      });

      services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.Converters.Add(
          new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.CamelCase));
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pathfinding", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pathfinding v1"));
      }

      // app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}