using Domain.Entities;
using Domain.UnitTests;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Entities
{
    public class HorizontalGridTest
    {
        public static IEnumerable<object[]> TestData()
        {
            var grid = GridFactory.Produce(new[]
            {
                "S 1 1",
                "1 W 1",
                "1 1 F"
            }, GridType.Horizontal);

            yield return new object[] { grid, grid[0, 0], new Position[] { new(0, 1), new(1, 0) } };
            yield return new object[] { grid, grid[0, 1], new Position[] { new(0, 0), new(0, 2) } };
            yield return new object[] { grid, grid[0, 2], new Position[] { new(0, 1), new(1, 2) } };

            yield return new object[] { grid, grid[1, 0], new Position[] { new(0, 0), new(2, 0) } };
            yield return new object[] { grid, grid[1, 1], new Position[] { new(0, 1), new(1, 2), new(1, 0), new(2, 1) } };
            yield return new object[] { grid, grid[1, 2], new Position[] { new(0, 2), new(2, 2) } };

            yield return new object[] { grid, grid[2, 0], new Position[] { new(1, 0), new(2, 1) } };
            yield return new object[] { grid, grid[2, 1], new Position[] { new(2, 0), new(2, 2) } };
            yield return new object[] { grid, grid[2, 2], new Position[] { new(2, 1), new(1, 2) } };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void GetNeighbors_horizontal(Grid sut, GridNode target, Position[] expectedNeighbors)
        {
            var neighbors = sut.GetNeighbors(target);

            Assert.Equal(neighbors.Count, expectedNeighbors.Length);
            Assert.All(neighbors, neighbor =>
            {
                Assert.True(
                    expectedNeighbors.Contains(neighbor.Position),
                    $"The position {neighbor.Position} is not in the list of expected neighbors.");
            });
        }
    }
}
