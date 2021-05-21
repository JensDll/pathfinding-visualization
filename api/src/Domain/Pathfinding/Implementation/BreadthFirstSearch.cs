
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
    public class BreadthFirstSearch : IPathfindingAlgorithm
    {
        private readonly Grid _grid;

        public BreadthFirstSearch(Grid grid)
        {
            _grid = grid;
        }

        public PathfindingResult ShortestPath()
        {
            _grid.Start.Visited = true;

            var visitedNodes = new List<GridNode> { _grid.Start };
            var queue = new Queue<GridNode>(new[] { _grid.Start });

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                currentNode.Visited = true;

                var neighbors = _grid.GetNeighbors(currentNode);

                foreach (var neighbor in neighbors)
                {
                    neighbor.Visited = true;
                    neighbor.PreviousGridNode = currentNode;

                    visitedNodes.Add(neighbor);
                    queue.Enqueue(neighbor);

                    if (neighbor.Type == GridNodeType.Finish)
                    {
                        return new PathfindingResult
                        {
                            VisitedNodes = visitedNodes,
                            ShortestPath = neighbor.ConstructShortestPath()
                        };
                    }
                }
            }

            return new PathfindingResult
            {
                VisitedNodes = visitedNodes,
                ShortestPath = new List<GridNode>()
            };
        }
    }
}
