using System.Collections.Generic;
using Pathfinding.Shared;
using Pathfinding.Shared.Domain;

namespace Pathfinding.Algorithm
{
  public class BreadthFirstSearch : IBreadthFirstSearch
  {
    private readonly IAlgorithmService algorithmService;

    public BreadthFirstSearch(IAlgorithmService algorithmService)
    {
      this.algorithmService = algorithmService;
    }

    public PathfindingResult ShortestPath((int row, int col) start, GridNode[][] grid)
    {
      var startNode = grid[start.row][start.col];
      startNode.Visited = true;

      var visitedNodes = new List<GridNode> { startNode };
      var shortestPath = new List<GridNode>();
      var queue = new Queue<GridNode>(new[] { startNode });

      while (queue.Count != 0)
      {
        var currentNode = queue.Dequeue();

        currentNode.Visited = true;

        var neighbors = algorithmService.GetNeighbors(grid, currentNode.Position);

        foreach (var neighbor in neighbors)
        {
          if (!neighbor.Visited && neighbor.Type != GridNodeType.Wall)
          {
            neighbor.Visited = true;
            neighbor.PreviousGridNode = currentNode;
            visitedNodes.Add(neighbor);
            queue.Enqueue(neighbor);

            if (neighbor.Type == GridNodeType.Finish)
            {
              algorithmService.ConstructShortestPath(neighbor, shortestPath);

              return new PathfindingResult
              {
                VisitedNodes = visitedNodes,
                ShortestPath = shortestPath
              };
            }
          }
        }
      }

      return new PathfindingResult
      {
        VisitedNodes = visitedNodes,
        ShortestPath = shortestPath
      };
    }
  }
}
