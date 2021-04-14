using Domain.Entities;
using Domain.Pathfinding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Pathfinding.Implementation
{
    public class PathfindingTestData : TheoryData
    {
        public PathfindingTestData(params (string[] stringGrid, Position[] expectedPositions)[] testData)
        {
            foreach (var (stringGrid, expectedPositions) in testData)
            {
                var (grid, start, _) = GridFactory.Produce(stringGrid);
                AddRow(grid, start, expectedPositions);
            }
        }
    }
}
