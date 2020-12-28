using System.Linq;
using System.Threading.Tasks;
using Pathfinding.Shared.Domain;

namespace Pathfinding.Shared
{
  public class Mapper : IMapper
  {
    public bool TryTransformGrid(
      GridNodeDto[][] grid,
      out (int row, int col) start,
      out (int row, int col) finish,
      out GridNode[][] transformedGrid)
    {
      (int row, int col) startPos = (-1, -1);
      (int row, int col) finishPos = (-1, -1);

      start = startPos;
      finish = finishPos;

      transformedGrid = grid.Select((row, rowIndex) => row.Select((node, colIndex) =>
      {
        if (node.Type == GridNodeTypeDto.Start)
        {
          startPos = (row: rowIndex, col: colIndex);
        }

        if (node.Type == GridNodeTypeDto.Finish)
        {
          finishPos = (row: rowIndex, col: colIndex);
        }

        return new GridNode
        {
          Type = (GridNodeType)node.Type,
          Visited = false,
          Weight = node.Weight,
          TotalWeight = 0,
          Position = (row: rowIndex, col: colIndex),
          PreviousGridNode = null
        };
      }).ToArray()).ToArray();

      if (startPos == (-1, -1) || finishPos == (-1, -1))
      {
        return false;
      }

      start = startPos;
      finish = finishPos;

      return true;
    }

    public PathfindingResponseDto MapPathfindingResult(PathfindingResult result) =>
      new PathfindingResponseDto
      {
        VisitedPositions = result.VisitedNodes.Select(node => new PositionDto
        {
          Row = node.Position.row,
          Col = node.Position.col
        }),
        ShortestPath = result.ShortestPath.Select(node => new PositionDto
        {
          Row = node.Position.row,
          Col = node.Position.col
        })
      };
  }
}