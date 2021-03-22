using Application.Interfaces;
using Contracts.Request;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    internal class RequestMapper : IRequestMapper
    {
        public (GridNode[][] grid, Position startPosition) MapPathfindingRequestDto(PathfindingRequestDto pathfindingRequestDto)
        {
            Position startPosition = null;

            GridNode[][] grid = pathfindingRequestDto.Grid.Select((row, rowIndex) =>
                row.Select((nodeDto, colIndex) =>
                {
                    var postion = new Position(rowIndex, colIndex);

                    if (nodeDto.Type is GridNodeTypeDto.Start)
                        startPosition = postion;

                    return new GridNode
                    {
                        Type = (GridNodeType)nodeDto.Type,
                        Visited = false,
                        Weight = 0,
                        TotalWeight = 0,
                        Position = postion,
                        PreviousGridNode = null
                    };
                }).ToArray()
            ).ToArray();

            return (grid, startPosition);
        }
    }
}
