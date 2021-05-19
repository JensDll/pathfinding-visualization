using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Response
{
    public record PathfindingResponseDto
    {
        public IEnumerable<PositionDto> VisitedPositions { get; init; }

        public IEnumerable<PositionDto> ShortestPath { get; init; }
    }
}
