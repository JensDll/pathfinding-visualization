using Pathfinding.Shared;

namespace Pathfinding.Algorithm
{
  public class BreadthFirstSearch : IBreadthFirstSearch
  {
    private readonly IAlgorithmService algorithmService;

    public BreadthFirstSearch(IAlgorithmService algorithmService)
    {
      this.algorithmService = algorithmService;
    }
    public void ShortestPath((int row, int col) start, (int row, int col) finish, GridNode[][] grid)
    {
      throw new System.NotImplementedException();
    }
  }
}
