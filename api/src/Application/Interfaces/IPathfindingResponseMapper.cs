using Contracts.Response;
using Domain.Pathfinding;

namespace Application.Interfaces
{
    public interface IPathfindingResponseMapper
    {
        PathfindingResponseDto MapPathfindingResult(PathfindingResult pathfindingResult);
    }
}
