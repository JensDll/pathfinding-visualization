using Pathfinding.Shared;

namespace Pathfinding.Algorithm
{
  public interface IBreadthFirstSearch
  {
    PathfindingResult ShortestPath((int row, int col) start, GridNode[][] grid);
  }
}