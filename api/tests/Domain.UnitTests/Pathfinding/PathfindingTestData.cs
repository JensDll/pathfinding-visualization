using Domain.Entities;
using Domain.Pathfinding.Interfaces;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Pathfinding
{
    public class PathfindingTestData : TheoryData
    {
        public PathfindingTestData(params (string[] stringGrid, GridType gridType, Position[] expectedPositions)[] testData)
        {
            foreach (var (stringGrid, gridType, expectedPositions) in testData)
            {
                var grid = GridFactory.Produce(stringGrid, gridType);
                AddRow(grid, expectedPositions);
            }
        }
    }
}
