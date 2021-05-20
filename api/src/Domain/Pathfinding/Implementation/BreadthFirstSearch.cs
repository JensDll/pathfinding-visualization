
using Domain.Pathfinding.Common;
using Domain.Pathfinding.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Pathfinding.Implementation
{
    public class BreadthFirstSearch : PathfindingAlgorithmBase, IPathfindingAlgorithm
    {
        private readonly IGetNeighbors _getNeighbors;

        public BreadthFirstSearch(IGetNeighbors getNeighbors)
        {
            _getNeighbors = getNeighbors;
        }

        public PathfindingResult ShortestPath(GridNode[][] grid, Position startPosition)
        {
            var startNode = grid[startPosition.Row][startPosition.Col];
            startNode.Visited = true;

            var visitedNodes = new List<GridNode> { startNode };
            var shortestPath = new List<GridNode>();
            var queue = new Queue<GridNode>(new[] { startNode });

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                currentNode.Visited = true;

                var neighbors = _getNeighbors.GetNeighbors(grid, currentNode.Position);

                foreach (var neighbor in neighbors)
                {
                    neighbor.Visited = true;
                    neighbor.PreviousGridNode = currentNode;

                    visitedNodes.Add(neighbor);
                    queue.Enqueue(neighbor);

                    if (neighbor.Type is GridNodeType.Finish)
                    {
                        ConstructShortestPath(neighbor, shortestPath);

                        return new PathfindingResult
                        {
                            VisitedNodes = visitedNodes,
                            ShortestPath = shortestPath
                        };
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
