using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GridNode
    {
        public GridNodeType Type { get; set; }

        public bool Visited { get; set; }

        public int Weight { get; set; }

        public int TotalWeight { get; set; }

        public Position Position { get; set; }

        public GridNode PreviousGridNode { get; set; }

        public List<GridNode> ConstructShortestPath()
        {
            var shortestPath = new List<GridNode>();
            ConstructShortestPathImpl(this, shortestPath);
            return shortestPath;
        }

        private void ConstructShortestPathImpl(GridNode node, List<GridNode> shortestPath)
        {
            if (node == null) return;
            ConstructShortestPathImpl(node.PreviousGridNode, shortestPath);
            shortestPath.Add(node);
        }
    }
}
