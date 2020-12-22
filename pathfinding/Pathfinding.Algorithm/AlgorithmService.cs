using System.Collections.Generic;
using Pathfinding.Shared;

namespace Pathfinding.Algorithm
{
  public class AlgorithmService : IAlgorithmService
  {
    public List<GridNode> GetNeighbors(GridNode[][] grid, (int row, int col) point)
    {
      (int row, int col) = point;
      int lastRow = grid.Length - 1;
      int lastCol = grid[0].Length - 1;

      var neighbors = new List<GridNode>();

      // Left
      if (col > 0) neighbors.Add(grid[row][col - 1]);

      // Top
      if (row > 0) neighbors.Add(grid[row - 1][col]);

      // Right
      if (col < lastCol) neighbors.Add(grid[row][col + 1]);

      // Bottom
      if (row < lastRow) neighbors.Add(grid[row + 1][col]);

      return neighbors;
    }

    public List<GridNode> GetNeighborsDiagonal(GridNode[][] grid, (int row, int col) point)
    {
      (int row, int col) = point;
      int lastRow = grid.Length - 1;
      int lastCol = grid[0].Length - 1;

      var neighbors = new List<GridNode>(GetNeighbors(grid, point));

      // Top Left 
      if (row > 0 && col > 0) neighbors.Add(grid[row - 1][col - 1]);

      // Top Right
      if (row > 0 && col < lastCol) neighbors.Add(grid[row - 1][col + 1]);

      // Bottom Right
      if (row < lastRow && col < lastCol) neighbors.Add(grid[row + 1][col + 1]);

      // Bottom Left
      if (row < lastRow && col > 0) neighbors.Add(grid[row + 1][col - 1]);

      return neighbors;
    }
  }
}