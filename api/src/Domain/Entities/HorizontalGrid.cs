using Domain.Extensions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HorizontalGrid : Grid
    {
        public HorizontalGrid(GridNode[][] grid) : base(grid)
        { }

        public override List<GridNode> GetNeighbors(GridNode node)
        {
            var neighbors = new List<GridNode>();
            var position = node.Position;
            var (row, col) = position;
            int lastRow = Rows - 1;
            int lastCol = Cols - 1;

            if (col > 0)
            {
                node = this[position.Left];
                if (node.NotVisitedNotWall())
                {
                    neighbors.Add(node);
                }
            }

            if (row > 0)
            {
                node = this[position.Top];
                if (node.NotVisitedNotWall())
                {
                    neighbors.Add(node);
                }
            }

            if (col < lastCol)
            {
                node = this[position.Right];
                if (node.NotVisitedNotWall())
                {
                    neighbors.Add(node);
                }
            }

            if (row < lastRow)
            {
                node = this[position.Bottom];
                if (node.NotVisitedNotWall())
                {
                    neighbors.Add(node);
                }
            }

            return neighbors;
        }
    }
}
