using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public record Position(int Row, int Col)
    {
        public static Position operator +(Position p1, Position p2) =>
            new (p1.Row + p2.Row, p1.Col + p2.Col);

        public static Position operator -(Position p1, Position p2) =>
            new(p1.Row - p2.Row, p1.Col - p2.Col);

        public Position TopLeft => this + new Position(-1, -1);

        public Position Top => this + new Position(-1, 0);

        public Position TopRight => this + new Position(-1, 1);

        public Position Right => this + new Position(0, 1);

        public Position BottomRight => this + new Position(1, 1);

        public Position Bottom => this + new Position(1, 0);

        public Position BottomLeft => this + new Position(1, -1);

        public Position Left => this + new Position(0, -1);

        public Position Absolute => new (Math.Abs(Row), Math.Abs(Col));

        public int ManhattanDistance(Position p)
        {
            var v = (this - p).Absolute;
            return v.Row + v.Col;
        }

        public override string ToString()
        {
            return $"({Row}, {Col})";
        }
    }
};
