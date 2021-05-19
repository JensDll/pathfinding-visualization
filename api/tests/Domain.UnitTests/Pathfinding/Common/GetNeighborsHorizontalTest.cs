using Domain.Entities;
using Domain.Pathfinding.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Pathfinding.Common
{
    public class GetNeighborsHorizontalTest
    {
        public static IEnumerable<object[]> GetNeighborsHorizontalData()
        {
            var (grid, _, _) = GridFactory.Produce(new[]
            {
                "S 1 1",
                "1 W 1",
                "1 1 F"
            });

            yield return new object[] { grid, new Position(0, 0), new Position[] { new(0, 1), new(1, 0) } };
            yield return new object[] { grid, new Position(0, 1), new Position[] { new(0, 0), new(0, 2) } };
            yield return new object[] { grid, new Position(0, 2), new Position[] { new(0, 1), new(1, 2) } };

            yield return new object[] { grid, new Position(1, 0), new Position[] { new(0, 0), new(2, 0) } };
            yield return new object[] { grid, new Position(1, 1), new Position[] { new(0, 1), new(1, 2), new(1, 0), new(2, 1) } };
            yield return new object[] { grid, new Position(1, 2), new Position[] { new(0, 2), new(2, 2) } };

            yield return new object[] { grid, new Position(2, 0), new Position[] { new(1, 0), new(2, 1) } };
            yield return new object[] { grid, new Position(2, 1), new Position[] { new(2, 0), new(2, 2) } };
            yield return new object[] { grid, new Position(2, 2), new Position[] { new(2, 1), new(1, 2) } };
        }

        [Theory]
        [MemberData(nameof(GetNeighborsHorizontalData))]
        public void GetNeighborsHorizontal_ShouldReturnHorizontalNeighbors(GridNode[][] grid, Position start, Position[] expectedNeighbors)
        {
            var sut = new GetNeighborsHorizontal();
            var neighbors = sut.GetNeighbors(grid, start);

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
