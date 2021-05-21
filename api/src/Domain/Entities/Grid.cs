using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Grid
    {
        private readonly GridNode[][] _grid;

        protected int Rows => _grid.Length;
        protected int Cols => _grid[0].Length;

        public GridNode Start { get; init; }
        public GridNode Finish { get; init; }

        public Grid(GridNode[][] grid)
        {
            _grid = grid;
        }

        public GridNode this[int row, int col]
        {
            get => _grid[row][col];
            set => _grid[row][col] = value;
        }

        public GridNode this[Position position]
        {
            get => _grid[position.Row][position.Col];
            set => _grid[position.Row][position.Col] = value;
        }

        public abstract List<GridNode> GetNeighbors(GridNode node);

        protected static bool IsValidNode(GridNode node) =>
            !node.Visited && node.Type != GridNodeType.Wall;
    }
}
