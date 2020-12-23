using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Pathfinding.Algorithm;
using Xunit;
using Pathfinding.Shared;

namespace Pathfinding.Test
{
  public class AlgorithmServiceTest
  {
    public static string[][] testGrids = new string[][]
    {
      new string[] { "Saa", "aaa", "aaF" },
      new string[] { "aSa", "aaa", "aaF" },
      new string[] { "aaS", "aaa", "aaF" },
      new string[] { "aaa", "Saa", "aaF" },
      new string[] { "aaa", "aSa", "aaF" },
      new string[] { "aaa", "aaS", "aaF" },
      new string[] { "aaa", "aaa", "SaF" },
      new string[] { "aaa", "aaa", "aSF" },
      new string[] { "aaa", "aaa", "aFS" },
    };

    public static (int row, int col)[][] expectedNeighbors = new (int row, int col)[][]
    {
      new (int row, int col)[] { (0, 1), (1, 0) },
      new (int row, int col)[] { (0, 0), (0, 2), (1, 1) },
      new (int row, int col)[] { (0, 1), (1, 2) },

      new (int row, int col)[] { (1, 1), (0, 0), (2, 0) },
      new (int row, int col)[] { (1, 0), (1, 2), (0, 1), (2, 1) },
      new (int row, int col)[] { (1, 1), (0, 2), (2, 2) },

      new (int row, int col)[] { (2, 1), (1, 0) },
      new (int row, int col)[] { (2, 0), (2, 2), (1, 1) },
      new (int row, int col)[] { (2, 1), (1, 2) },
    };

    public static (int row, int col)[][] expectedNeighborsDiagonal = new (int row, int col)[][]
    {
      new (int row, int col)[] { (1, 0), (1, 1), (0, 1) },
      new (int row, int col)[] { (0, 0), (1, 0), (1, 1), (1, 2), (0, 2) },
      new (int row, int col)[] { (0, 1), (1, 1), (1, 2) },

      new (int row, int col)[] { (0, 0), (0, 1), (1, 1), (2, 1), (2, 0) },
      new (int row, int col)[] { (0, 0), (0, 1), (0, 2), (1, 0), (1, 2), (2, 0), (2, 1), (2, 2) },
      new (int row, int col)[] { (0, 2), (0, 1), (1, 1), (2, 1), (2, 2) },

      new (int row, int col)[] { (1, 0), (1, 1), (2, 1) },
      new (int row, int col)[] { (2, 0), (1, 0), (1, 1), (1, 2), (2, 2) },
      new (int row, int col)[] { (2, 1), (1, 1), (1, 2) },
    };

    public static GetNeighborsTheoryData getNeighbors_testData = new(testGrids, expectedNeighbors);
    public static GetNeighborsTheoryData getNeighborsDiagonal_testData = new(testGrids, expectedNeighborsDiagonal);


    private readonly IAlgorithmService sut;

    public AlgorithmServiceTest(IAlgorithmService sut)
    {
      this.sut = sut;
    }

    [Theory]
    [MemberData(nameof(getNeighbors_testData))]
    public void GetNeighborsShouldReturnCorrectNeigbors(GridNode[][] grid, (int row, int col) point, (int row, int col)[] expectedNeighbors)
    {
      var neighbors = sut.GetNeighbors(grid, point);

      Assert.Equal(neighbors.Count, expectedNeighbors.Length);
      Assert.All(neighbors, n =>
      {
        Assert.True(
          expectedNeighbors.Contains(n.Position),
          $"The position {n.Position} is not in the list of expected neighbors!");
      });
    }

    [Theory]
    [MemberData(nameof(getNeighborsDiagonal_testData))]
    public void GetNeighborsDiagonalShouldReturnCorrectNeigbors(GridNode[][] grid, (int row, int col) point, (int row, int col)[] expectedNeighbors)
    {
      var neighbors = sut.GetNeighborsDiagonal(grid, point);

      Assert.Equal(expectedNeighbors.Length, neighbors.Count);
      Assert.All(neighbors, n =>
      {
        Assert.True(
          expectedNeighbors.Contains(n.Position),
          $"The position {n.Position} is not in the list of expected neighbors!");
      });
    }
  }

  public class GetNeighborsTheoryData : TheoryData
  {
    public GetNeighborsTheoryData(string[][] stringGrids, (int row, int col)[][] expectedNeighbors)
    {
      var data = GridFactory.ProduceGrids(stringGrids);

      for (int i = 0; i < data.Count; i++)
      {
        AddRow(data[i].grid, data[i].start, expectedNeighbors[i]);
      }
    }
  }
}

