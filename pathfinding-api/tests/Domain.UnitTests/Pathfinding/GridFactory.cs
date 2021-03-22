using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Pathfinding
{
    public static class GridFactory
    {
        public static GridFactoryResult Produce(string[] stringGrid)
        {
            // Ensure all strings have the same length
            Contract.ForAll(stringGrid, x => x.Length == stringGrid[0].Length);

            int rows = stringGrid.Length;
            int cols = stringGrid[0].Length;

            var grid = new GridNode[rows][];
            Position start = null;
            Position finish = null;

            for (int row = 0; row < rows; row++)
            {
                grid[row] = new GridNode[cols];

                for (int col = 0; col < cols; col++)
                {
                    char c = stringGrid[row][col];
                    var position = new Position(row, col);

                    if (c == 'S') start = position;
                    if (c == 'F') finish = position;

                    grid[row][col] = new GridNode
                    {
                        Type = c switch
                        {
                            'W' => GridNodeType.Wall,
                            'S' => GridNodeType.Start,
                            'F' => GridNodeType.Finish,
                            _ => GridNodeType.Default
                        },
                        Visited = false,
                        Weight = int.TryParse(c.ToString(), out int weight) ? weight : 0,
                        Position = position
                    };
                }
            }

            // Ensure a start position has been marked with 'S'
            Contract.Assert(start is not null, "No position was marked with 'S'!");
            // Ensure a finish position has been marked with 'F'
            Contract.Assert(finish is not null, "No position was marked with 'F'!");

            return new GridFactoryResult(grid, start, finish);
        }

        public static List<GridFactoryResult> ProduceMultiple(string[][] stringGrids) =>
          stringGrids.Select(Produce).ToList();
    }
}
