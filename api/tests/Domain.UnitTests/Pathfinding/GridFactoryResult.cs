using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Pathfinding
{
    public record GridFactoryResult(GridNode[][] Grid, Position Start, Position Finish);
}
