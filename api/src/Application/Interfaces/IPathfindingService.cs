using Domain.Entities;
using Domain.Pathfinding;

namespace Application.Interfaces
{
    public interface IPathfindingService
    {
        PathfindingResult BreadthFirstSearch(Grid grid);

        PathfindingResult Dijkstra(Grid grid);

        PathfindingResult AStar(Grid grid);
    }
}
