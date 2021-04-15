using Application.Interfaces;
using Contracts.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    internal class PathfindingResponseMapper : IPathfindingResponseMapper
    {
        public PathfindingResponseDto MapPathfindingResult(PathfindingResult pathfindingResult)
        {
            return new PathfindingResponseDto
            {
                ShortestPath = pathfindingResult.ShortestPath.Select(node => new PositionDto
                {
                    Row = node.Position.Row,
                    Col = node.Position.Col
                }),
                VisitedPositions = pathfindingResult.VisitedNodes.Select(node => new PositionDto
                {
                    Row = node.Position.Row,
                    Col = node.Position.Col
                })
            };
        }
    }
}
