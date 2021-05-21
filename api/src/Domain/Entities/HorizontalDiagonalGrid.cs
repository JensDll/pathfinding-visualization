using Domain.Extensions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HorizontalDiagonalGrid : HorizontalGrid
    {
        public HorizontalDiagonalGrid(GridNode[][] grid) : base(grid)
        { }

        public override List<GridNode> GetNeighbors(GridNode node)
        {
            var neighbors = base.GetNeighbors(node);
            var position = node.Position;
            var (row, col) = position;
            int lastRow = Rows - 1;
            int lastCol = Cols - 1;

            if (row > 0 && col > 0)
            {
                node = this[position.TopLeft];
                if (node.NotVisitedNotWall())
                {
                    neighbors.Add(node);
                }
            }

            if (row > 0 && col < lastCol)
            {
                node = this[position.TopRight];
                if (node.NotVisitedNotWall())
                {
                    neighbors.Add(node);
                }
            }

            if (row < lastRow && col < lastCol)
            {
                node = this[position.BottomRight];
                if (node.NotVisitedNotWall())
                {
                    neighbors.Add(node);
                }
            }

            if (row < lastRow && col > 0)
            {
                node = this[position.BottomLeft];
                if (node.NotVisitedNotWall())
                {
                    neighbors.Add(node);
                }
            }

            return neighbors;
        }
    }
}
