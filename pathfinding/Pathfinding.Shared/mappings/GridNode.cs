using Pathfinding.Shared.Domain;

namespace Pathfinding.Shared
{
  public class GridNode
  {
    public GridNodeType Type { get; set; }

    public bool Visited { get; set; }

    public int Weight { get; set; }

    public (int row, int col) Position { get; set; }
  }
}