using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Pathfinding.Shared;

namespace Pathfinding.Test
{
  public static class GridFactory
  {
    public static (GridNode[][] grid, (int row, int col) start, (int row, int col) finish) ProduceGrid(string[] stringGrid)
    {
      // Ensure all strings have the same length
      Contract.ForAll(stringGrid, x => x.Length == stringGrid[0].Length);

      int rows = stringGrid.Length;
      int cols = stringGrid[0].Length;

      var grid = new GridNode[rows][];
      var startPos = (row: -1, col: -1);
      var finishPos = (row: -1, col: -1);

      for (int row = 0; row < rows; row++)
      {
        grid[row] = new GridNode[cols];

        for (int col = 0; col < cols; col++)
        {
          char c = stringGrid[row][col];

          if (c == 'S')
          {
            startPos = (row, col);
          }

          if (c == 'F')
          {
            finishPos = (row, col);
          }

          grid[row][col] = new GridNode
          {
            Type = c switch
            {
              'W' => GridNodeType.Wall,
              'S' => GridNodeType.Start,
              'F' => GridNodeType.Finish,
              _ => GridNodeType.Default
            },
            Visited = false,
            Weight = int.TryParse(c.ToString(), out int weight) ? weight : 0,
            Position = (row, col)
          };
        }
      }

      // Ensure a start position has been marked with 'S'
      Contract.Assert(startPos != (-1, -1), "No position was marked with 'S'!");
      // Ensure a finish position has been marked with 'F'
      Contract.Assert(finishPos != (-1, -1), "No position was marked with 'F'!");

      return (grid, start: startPos, finish: finishPos);
    }

    public static List<(GridNode[][] grid, (int row, int col) start, (int row, int col) finish)> ProduceGrids(string[][] stringGrids) =>
      stringGrids.Select(ProduceGrid).ToList();
  }
}
