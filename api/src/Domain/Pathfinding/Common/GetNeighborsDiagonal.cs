using Domain.Pathfinding.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System.Collections.Generic;
using Domain.ValueObjects;

namespace Domain.Pathfinding.Common
{
    public class GetNeighborsDiagonal : IGetNeighbors
    {
        private readonly GetNeighborsHorizontal _getNeighborsHorizontal = new();

        public List<GridNode> GetNeighbors(GridNode[][] grid, Position position)
        {
            (int row, int col) = position;
            int lastRow = grid.Length - 1;
            int lastCol = grid[0].Length - 1;

            var neighbors = _getNeighborsHorizontal.GetNeighbors(grid, position);

            // Top Left 
            if (row > 0 && col > 0)
                AddNodeIfValid(grid[row - 1][col - 1], neighbors);

            // Top Right
            if (row > 0 && col < lastCol)
                AddNodeIfValid(grid[row - 1][col + 1], neighbors);

            // Bottom Right
            if (row < lastRow && col < lastCol)
                AddNodeIfValid(grid[row + 1][col + 1], neighbors);

            // Bottom Left
            if (row < lastRow && col > 0)
                AddNodeIfValid(grid[row + 1][col - 1], neighbors);

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
