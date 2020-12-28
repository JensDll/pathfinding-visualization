namespace Pathfinding.Shared.Domain
{
  public static class ApiRoutes
  {
    public const string Base = "api";

    public static class GridRoutes
    {
      public const string BreadthFirstSearch = Base + "/algorithm/breadth-first-search";

      public const string Dijkstra = Base + "/algorithm/dijkstra";
    }
  }
}