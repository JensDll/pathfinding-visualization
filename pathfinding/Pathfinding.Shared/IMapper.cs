using System.Threading.Tasks;
using Pathfinding.Shared.Domain;

namespace Pathfinding.Shared
{
  public interface IMapper
  {
    bool TryTransformGrid(
      GridNodeDto[][] grid,
      out (int row, int col) start,
      out (int row, int col) finish,
      out GridNode[][] transformedGrid);

    PathfindingResponseDto MapPathfindingResult(PathfindingResult result);
  }
}