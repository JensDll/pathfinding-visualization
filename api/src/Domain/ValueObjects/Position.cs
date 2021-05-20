using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public record Position(int Row, int Col)
    {
        public Position TopLeft => this with { Row = Row - 1, Col = Col - 1 };

        public Position Top => this with { Row = Row - 1 };

        public Position TopRight => this with { Row = Row - 1, Col = Col + 1 };

        public Position Right => this with { Col = Col + 1 };

        public Position BottomRight => this with { Row = Row + 1, Col = Col + 1 };

        public Position Bottom => this with { Row = Row + 1 };

        public Position BottomLeft => this with { Row = Row + 1, Col = Col - 1 };

        public Position Left => this with { Col = Col - 1 };

        public override string ToString()
        {
            return $"({Row}, {Col})";
        }
    }
};
