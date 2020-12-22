using System.Collections.Generic;
using Pathfinding.Shared;

namespace Pathfinding.Algorithm
{
  public interface IAlgorithmService
  {
    List<GridNode> GetNeighbors(GridNode[][] grid, (int row, int col) point);

    List<GridNode> GetNeighborsDiagonal(GridNode[][] grid, (int row, int col) point);
  }
}