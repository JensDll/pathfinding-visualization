using Domain.Entities;
using Domain.Enums;
using Domain.Pathfinding.Interfaces;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain.Pathfinding.Common
{
    public class GetNeighborsHorizontal : IGetNeighbors
    {
        public List<GridNode> GetNeighbors(GridNode[][] grid, Position position)
        {
            (int row, int col) = position;
            int lastRow = grid.Length - 1;
            int lastCol = grid[0].Length - 1;

            var neighbors = new List<GridNode>();

            // Left
            if (col > 0)
                AddNodeIfValid(grid[row][col - 1], neighbors);

            // Top
            if (row > 0)
                AddNodeIfValid(grid[row - 1][col], neighbors);

            // Right
            if (col < lastCol)
                AddNodeIfValid(grid[row][col + 1], neighbors);

            // Bottom
            if (row < lastRow)
                AddNodeIfValid(grid[row + 1][col], neighbors);

            return neighbors;
        }

        private static void AddNodeIfValid(GridNode node, List<GridNode> neighbors)
        {
            if (!node.Visited && node.Type is not GridNodeType.Wall)
            {
                neighbors.Add(node);
            }
        }
    }
}
