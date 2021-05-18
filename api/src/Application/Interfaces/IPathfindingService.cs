using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPathfindingService
    {
        PathfindingResult BreadthFirstSearch(GridNode[][] grid, Position startPosition, bool searchDiagonal);

        PathfindingResult Dijkstra(GridNode[][] grid, Position startPosition, bool searchDiagonal);
    }
}
