using Domain.Pathfinding.Common;
using Domain.Pathfinding.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pathfinding.Implementation
{
    public class Dijkstra : PathfindingAlgorithmBase, IPathfindingAlgorithm
    {
        public Dijkstra(IGetNeighbors getNeighbors) : base(getNeighbors)
        { }

        public PathfindingResult ShortestPath(GridNode[][] grid, Position startPosition)
        {
            var startNode = grid[startPosition.Row][startPosition.Col];
            startNode.Visited = true;

            var visitedNodes = new List<GridNode>();
            var shortestPath = new List<GridNode>();
            var minHeap = new Heap<GridNode>((n1, n2) => n1.TotalWeight - n2.TotalWeight).Add(startNode);

            while (minHeap.Count > 0)
            {
                var currentNode = minHeap.RemoveTop();

                visitedNodes.Add(currentNode);

                if (currentNode.Type is GridNodeType.Finish)
                {
                    ConstructShortestPath(currentNode, shortestPath);

                    return new PathfindingResult
                    {
                        VisitedNodes = visitedNodes,
                        ShortestPath = shortestPath
                    };
                }

                currentNode.Visited = true;

                var neighbors = GetNeighbors(grid, currentNode.Position);

                foreach (var neighbor in neighbors)
                {
                    neighbor.Visited = true;
                    neighbor.TotalWeight += neighbor.Weight + currentNode.TotalWeight;
                    neighbor.PreviousGridNode = currentNode;
                    minHeap.Add(neighbor);
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
