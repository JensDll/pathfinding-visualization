using Contracts.Request;
using Contracts.Response;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRequestMapper
    {
        (GridNode[][] grid, Position startPosition) MapPathfindingRequestDto(PathfindingRequestDto pathfindingRequestDto);
    }
}
