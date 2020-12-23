using System.Collections.Generic;

namespace Pathfinding.Shared.Domain
{
  public class PathfindingResponseDto
  {
    public List<PositionDto> VisitedPositions { get; set; }

    public List<PositionDto> ShortestPath { get; set; }
  }
}