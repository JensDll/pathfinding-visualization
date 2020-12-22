using Microsoft.Extensions.DependencyInjection;
using Pathfinding.Algorithm;

namespace Pathfinding.Test
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<IAlgorithmService, AlgorithmService>();
    }
  }
}