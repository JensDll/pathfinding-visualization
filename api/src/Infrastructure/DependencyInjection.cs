using Application.Interfaces;
using Infrastructure.Mapping;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            // services
            services.AddSingleton<IPathfindingService, PathfindingService>();

            // mapping
            services.AddSingleton<IPathfindingRequestMapper, PathfindingRequestMapper>();
            services.AddSingleton<IPathfindingResponseMapper, PathfindingResponseMapper>();
        }
    }
}
