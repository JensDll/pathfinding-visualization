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
                neighbors.AddIf(this[position.TopLeft], IsValidNode);
            }

            if (row > 0 && col < lastCol)
            {
                neighbors.AddIf(this[position.TopRight], IsValidNode);
            }

            if (row < lastRow && col < lastCol)
            {
                neighbors.AddIf(this[position.BottomRight], IsValidNode);
            }

            if (row < lastRow && col > 0)
            {
                neighbors.AddIf(this[position.BottomLeft], IsValidNode);
            }

            return neighbors;
        }
    }
}
