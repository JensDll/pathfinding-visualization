using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PathfindingResult
    {
        public IEnumerable<GridNode> VisitedNodes { get; init; }

        public IEnumerable<GridNode> ShortestPath { get; init; }
    }
}
