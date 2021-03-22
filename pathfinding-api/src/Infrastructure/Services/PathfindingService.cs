using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Pathfinding.Common;
using Domain.Pathfinding.Implementation;
using Domain.Pathfinding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal class PathfindingService : IPathfindingService
    {
        public PathfindingResult BreadthFirstSearch(GridNode[][] grid, Position startPosition, bool searchDiagonal = false)
        {
            return new BreadthFirstSearch(GetSearchType(searchDiagonal)).ShortestPath(grid, startPosition);
        }

        public PathfindingResult Dijkstra(GridNode[][] grid, Position startPosition, bool searchDiagonal = false)
        {
            return new Dijkstra(GetSearchType(searchDiagonal)).ShortestPath(grid, startPosition);
        }

        private static IGetNeighbors GetSearchType(bool searchDiagonal) =>
            searchDiagonal
                ? new GetNeighborsDiagonal()
                : new GetNeighborsHorizontal();
    }
}
