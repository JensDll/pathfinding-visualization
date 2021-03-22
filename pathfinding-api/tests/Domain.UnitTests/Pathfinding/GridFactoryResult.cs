using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Pathfinding
{
    public record GridFactoryResult(GridNode[][] grid, Position start, Position finish);
}
