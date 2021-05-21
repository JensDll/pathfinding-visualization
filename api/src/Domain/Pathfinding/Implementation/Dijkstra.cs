using Domain.Pathfinding.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Pathfinding.Implementation
{
    public class Dijkstra : IPathfindingAlgorithm
    {
        private readonly Grid _grid;

        public Dijkstra(Grid grid)
        {
            _grid = grid;
        }

        public PathfindingResult ShortestPath()
        {
            _grid.Start.Visited = true;

            var visitedNodes = new List<GridNode>();
            var minHeap = new Heap<GridNode>((n1, n2) => n1.TotalWeight - n2.TotalWeight).Add(_grid.Start);

            while (minHeap.Count > 0)
            {
                var currentNode = minHeap.RemoveTop();

                visitedNodes.Add(currentNode);

                if (currentNode.Type == GridNodeType.Finish)
                {
                    return new PathfindingResult
                    {
                        VisitedNodes = visitedNodes,
                        ShortestPath = currentNode.ConstructShortestPath()
                    };
                }

                currentNode.Visited = true;

                var neighbors = _grid.GetNeighbors(currentNode);

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
                ShortestPath = new List<GridNode>()
            };
        }
    }
}
