namespace Pathfinding.Shared.Domain
{
  public enum GridNodeType
  {
    Default,
    Wall,
    Start,
    Finish,
  }

  public class GridNodeDomain
  {
    public GridNodeType Type { get; set; }
  }
}