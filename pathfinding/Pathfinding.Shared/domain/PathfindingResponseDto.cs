using System.Collections.Generic;

namespace Pathfinding.Shared.Domain
{
  public class PathfindingResponseDto
  {
    public IEnumerable<PositionDto> VisitedPositions { get; set; }

    public IEnumerable<PositionDto> ShortestPath { get; set; }
  }
}