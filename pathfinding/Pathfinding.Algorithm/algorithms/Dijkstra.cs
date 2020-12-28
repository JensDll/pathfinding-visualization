using System.Collections.Generic;
using Pathfinding.Shared;

namespace Pathfinding.Algorithm
{
  public class Dijkstra : IDijkstra
  {
    private readonly IAlgorithmService algorithmService;

    public Dijkstra(IAlgorithmService algorithmService)
    {
      this.algorithmService = algorithmService;
    }

    public PathfindingResult ShortestPath((int row, int col) start, GridNode[][] grid)
    {
      var startNode = grid[start.row][start.col];
      startNode.Visited = true;

      var visitedNodes = new List<GridNode>();
      var shortestPath = new List<GridNode>();
      var queue = new List<GridNode> { startNode }; // TODO: Replace with Min-Heap

      while (queue.Count != 0)
      {
        var currentNode = queue[0];
        queue.RemoveAt(0);

        visitedNodes.Add(currentNode);

        if (currentNode.Type == GridNodeType.Finish)
        {
          algorithmService.ConstructShortestPath(currentNode, shortestPath);

          return new PathfindingResult
          {
            VisitedNodes = visitedNodes,
            ShortestPath = shortestPath
          };
        }

        currentNode.Visited = true;

        var neighbors = algorithmService.GetNeighbors(grid, currentNode.Position);

        foreach (var neighbor in neighbors)
        {
          if (!neighbor.Visited && neighbor.Type != GridNodeType.Wall)
          {
            queue.Add(neighbor);
            neighbor.Visited = true;
            neighbor.TotalWeight += neighbor.Weight + currentNode.TotalWeight;
            neighbor.PreviousGridNode = currentNode;
          }
        }

        queue.Sort((n1, n2) => n1.TotalWeight - n2.TotalWeight);
      }

      return new PathfindingResult
      {
        VisitedNodes = visitedNodes,
        ShortestPath = shortestPath
      };
    }
  }
}