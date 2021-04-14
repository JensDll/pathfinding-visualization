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
        protected PathfindingAlgorithmBase(IGetNeighbors getNeighbors)
        {
            GetNeighbors = getNeighbors.GetNeighbors;
        }

        protected Func<GridNode[][], Position, List<GridNode>> GetNeighbors;

        protected void ConstructShortestPath(in GridNode node, in List<GridNode> shortestPath)
        {
            if (node == null) return;
            ConstructShortestPath(node.PreviousGridNode, shortestPath);
            shortestPath.Add(node);
        }
    }
}
