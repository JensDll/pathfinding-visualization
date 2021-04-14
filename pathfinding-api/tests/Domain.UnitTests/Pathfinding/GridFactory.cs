using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Pathfinding
{
    public static class GridFactory
    {
        public static GridFactoryResult Produce(string[] stringGrid)
        {
            var grid = stringGrid.Select(row => row.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

            // Ensure all rows have the same length
            Assert.True(Contract.ForAll(grid, row => row.Length == grid[0].Length), "Bad grid dimensions!!!");

            int rows = grid.Length;
            int cols = grid[0].Length;

            var resultGrid = new GridNode[rows][];
            Position start = null;
            Position finish = null;

            for (int row = 0; row < rows; row++)
            {
                resultGrid[row] = new GridNode[cols];

                for (int col = 0; col < cols; col++)
                {
                    string type = grid[row][col];
                    var position = new Position(row, col);

                    if (type == "S") start = position;
                    if (type == "F") finish = position;

                    resultGrid[row][col] = new GridNode
                    {
                        Type = type switch
                        {
                            "W" => GridNodeType.Wall,
                            "S" => GridNodeType.Start,
                            "F" => GridNodeType.Finish,
                            _ => GridNodeType.Default
                        },
                        Visited = false,
                        Weight = int.TryParse(type, out int weight) ? weight : 0,
                        Position = position
                    };
                }
            }

            // Ensure a start position has been marked with 'S'
            Assert.True(start is not null, "No position was marked with 'S'!!!");
            // Ensure a finish position has been marked with 'F'
            Assert.True(finish is not null, "No position was marked with 'F'!!!");

            return new GridFactoryResult(resultGrid, start, finish);
        }

        public static List<GridFactoryResult> ProduceMultiple(string[][] stringGrids) =>
          stringGrids.Select(Produce).ToList();
    }
}
