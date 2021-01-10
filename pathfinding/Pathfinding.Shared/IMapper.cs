using System.Threading.Tasks;
using Pathfinding.Shared.Domain;

namespace Pathfinding.Shared
{
  public interface IMapper
  {
    (GridNode[][] grid, (int row, int col) startPos, (int row, int col) finishPos) TransformGrid(GridDto gridDto);

    PathfindingResponseDto MapPathfindingResult(PathfindingResult result);
  }
}