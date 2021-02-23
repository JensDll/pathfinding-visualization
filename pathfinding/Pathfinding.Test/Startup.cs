using Microsoft.Extensions.DependencyInjection;
using Pathfinding.Algorithm;
// GitHub action test
namespace Pathfinding.Test
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<IAlgorithmService, AlgorithmService>();
      services.AddTransient<IBreadthFirstSearch, BreadthFirstSearch>();
    }
  }
}