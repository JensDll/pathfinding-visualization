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
                neighbors.AddIf(this[position.Left], IsValidNode);
            }

            if (row > 0)
            {
                neighbors.AddIf(this[position.Top], IsValidNode);
            }

            if (col < lastCol)
            {
                neighbors.AddIf(this[position.Right], IsValidNode);
            }

            if (row < lastRow)
            {
                neighbors.AddIf(this[position.Bottom], IsValidNode);
            }

            return neighbors;
        }
    }
}
