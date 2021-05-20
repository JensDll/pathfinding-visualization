using Application.Interfaces;
using Contracts.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    internal class PathfindingRequestMapper : IPathfindingRequestMapper
    {
        public Grid MapPathfindingRequestDto(PathfindingRequestDto pathfindingRequestDto)
        {
            GridNode start = null;
            GridNode finish = null;

            GridNode[][] grid = pathfindingRequestDto.Grid.Select((row, rowIndex) =>
                row.Select((nodeDto, colIndex) =>
                {
                    var node = new GridNode
                    {
                        Type = (GridNodeType)nodeDto.Type,
                        Visited = false,
                        Weight = nodeDto.Weight,
                        TotalWeight = 0,
                        Position = new Position(rowIndex, colIndex),
                        PreviousGridNode = null
                    };

                    if (node.Type == GridNodeType.Start)
                        start = node;

                    if (node.Type == GridNodeType.Finish)
                        finish = node;

                    return node;
                }).ToArray()
            ).ToArray();

            return pathfindingRequestDto.SearchDiagonal
                ? new DiagonalGrid(grid) { Start = start, Finish = finish }
                : new HorizontalGrid(grid) { Start = start, Finish = finish };
        }
    }
}
