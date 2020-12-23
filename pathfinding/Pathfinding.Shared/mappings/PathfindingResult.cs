using System.Collections.Generic;

namespace Pathfinding.Shared
{
  public class PathfindingResult
  {
    public IEnumerable<GridNode> VisitedNodes { get; set; }

    public IEnumerable<GridNode> ShortestPath { get; set; }
  }
}