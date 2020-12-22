using System.Linq;
using System.Threading.Tasks;
using Pathfinding.Shared.Domain;

namespace Pathfinding.Shared
{
  public class Mapper : IMapper
  {
    public bool TransformGrid(GridNodeDomain[][] grid, out ((int row, int col) start, (int row, int col) finish, GridNode[][] grid) gridData)
    {
      (int row, int col) start = (-1, -1);
      (int row, int col) finish = (-1, -1);

      gridData = (start, finish, null);

      var transformedGrid = grid.Select((row, rowIndex) => row.Select((node, colIndex) =>
      {
        if (node.Type == GridNodeType.Start)
        {
          start = (row: rowIndex, col: colIndex);
        }

        if (node.Type == GridNodeType.Finish)
        {
          finish = (row: rowIndex, col: colIndex);
        }

        return new GridNode
        {
          Type = node.Type,
          Visited = false,
          Weight = 0,
          Position = (row: rowIndex, col: colIndex)
        };
      }).ToArray()).ToArray();

      if (start == (-1, -1) || finish == (-1, -1))
      {
        return false;
      }

      gridData = (start, finish, transformedGrid);

      return true;
    }
  }
}