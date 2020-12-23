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
      var vistedNodes = new List<GridNode>();
      vistedNodes.Add(grid[start.row][start.col]);
      var shortestPath = new List<GridNode>();

      var queue = new Queue<GridNode>();
      queue.Enqueue(grid[start.row][start.col]);
      grid[start.row][start.col].Visited = true;

      while (queue.Count != 0)
      {
        var currentNode = queue.Dequeue();

        currentNode.Visited = true;

        if (currentNode.Type == GridNodeTypeDto.Finish)
        {
          algorithmService.ConstructShortestPath(currentNode, shortestPath);
        }

        var neighbors = algorithmService.GetNeighbors(grid, currentNode.Position);

        foreach (var neighbor in neighbors)
        {
          if (!neighbor.Visited && neighbor.Type != GridNodeTypeDto.Wall)
          {
            neighbor.Visited = true;
            neighbor.PreviousGridNode = currentNode;
            vistedNodes.Add(neighbor);
            queue.Enqueue(neighbor);
          }
        }
      }

      return new PathfindingResult
      {
        VisitedNodes = vistedNodes,
        ShortestPath = shortestPath
      };
    }
  }
}
