using Pathfinding.Shared;

namespace Pathfinding.Algorithm
{
  public interface IDijkstra
  {
    PathfindingResult ShortestPath((int row, int col) start, GridNode[][] grid);
  }
}