using System.Collections.Generic;
using Pathfinding.Shared;

namespace Pathfinding.Algorithm
{
  public interface IBreadthFirstSearch
  {
    void ShortestPath((int row, int col) start, (int row, int col) finish, GridNode[][] grid);
  }
}