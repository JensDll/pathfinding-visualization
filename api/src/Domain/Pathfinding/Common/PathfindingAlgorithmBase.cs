using Domain.Pathfinding.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pathfinding.Common
{
    public abstract class PathfindingAlgorithmBase
    {
        private readonly IGetNeighbors _getNeighbors;

        protected PathfindingAlgorithmBase(IGetNeighbors getNeighbors)
        {
            _getNeighbors = getNeighbors;
        }

        protected List<GridNode> GetNeighbors(GridNode[][] grid, Position position) =>
            _getNeighbors.GetNeighbors(grid, position);

        protected void ConstructShortestPath(in GridNode node, in List<GridNode> shortestPath)
        {
            if (node == null) return;
            ConstructShortestPath(node.PreviousGridNode, shortestPath);
            shortestPath.Add(node);
        }
    }
}
