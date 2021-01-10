using System.Linq;
using Pathfinding.Shared.Domain;

namespace Pathfinding.Shared
{
  public class Mapper : IMapper
  {
    public (GridNode[][] grid, (int row, int col) startPos, (int row, int col) finishPos) TransformGrid(GridDto gridDto)
    {
      (int row, int col) startPos = (0, 0);
      (int row, int col) finishPos = (0, 0);

      var grid = gridDto.Grid.Select((row, rowIndex) => row.Select((node, colIndex) =>
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

      return (grid, startPos, finishPos);
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