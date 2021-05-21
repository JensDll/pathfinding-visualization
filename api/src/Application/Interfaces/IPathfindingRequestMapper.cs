using Contracts.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPathfindingRequestMapper
    {
        Grid MapPathfindingRequestDto(PathfindingRequestDto pathfindingRequestDto);
    }
}
