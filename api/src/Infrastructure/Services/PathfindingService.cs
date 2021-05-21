using Application.Interfaces;
using Domain.Entities;
using Domain.Pathfinding;
using Domain.Pathfinding.Implementation;

namespace Infrastructure.Services
{
    internal class PathfindingService : IPathfindingService
    {
        public PathfindingResult BreadthFirstSearch(Grid grid)
        {
            return new BreadthFirstSearch(grid).ShortestPath();
        }

        public PathfindingResult Dijkstra(Grid grid)
        {
            return new Dijkstra(grid).ShortestPath();
        }
    }
}
