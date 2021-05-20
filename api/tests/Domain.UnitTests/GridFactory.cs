using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests
{
    public static class GridFactory
    {
        public static Grid Produce(string[] stringGrid, GridType gridType)
        {
            var _stringGrid = stringGrid.Select(row => row.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

            // Ensure all rows have the same length
            Assert.True(Contract.ForAll(_stringGrid, row => row.Length == _stringGrid[0].Length), "Bad grid dimensions!");

            int rows = _stringGrid.Length;
            int cols = _stringGrid[0].Length;

            var grid = new GridNode[rows][];
            GridNode start = null;
            GridNode finish = null;

            for (int row = 0; row < rows; row++)
            {
                grid[row] = new GridNode[cols];

                for (int col = 0; col < cols; col++)
                {
                    string type = _stringGrid[row][col];
                    var position = new Position(row, col);
                    var node = new GridNode
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

                    if (node.Type == GridNodeType.Start)
                        start = node;

                    if (node.Type == GridNodeType.Finish)
                        finish = node;

                    grid[row][col] = node;
                }
            }

            // Ensure a start position has been marked with 'S'
            Assert.True(start is not null, "No position was marked with 'S'!");
            // Ensure a finish position has been marked with 'F'
            Assert.True(finish is not null, "No position was marked with 'F'!");

            return gridType switch
            {
                GridType.Horizontal => new HorizontalGrid(grid)
                {
                    Start = start,
                    Finish = finish
                },
                GridType.Diagonal => new DiagonalGrid(grid)
                {
                    Start = start,
                    Finish = finish,
                },
                _ => throw new ArgumentException("Invalid grid type!")
            };
        }
    }
}
