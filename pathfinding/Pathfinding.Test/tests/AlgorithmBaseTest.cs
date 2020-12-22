using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Pathfinding.Algorithm;
using Xunit;
using Pathfinding.Shared;
using Pathfinding.Shared.Domain;

namespace Pathfinding.Test
{
  /// <summary>
  /// Generate a matrix of grid nodes based on string array
  /// </summary>
  public class GetNeighborsTestData : TheoryData
  {
    public GetNeighborsTestData(string[][] stringGrids, (int row, int col)[][] expectedNeighbors)
    {
      // Ensure for every grid that there are expected neighbors
      Contract.Assert(
        stringGrids.Length == expectedNeighbors.Length,
        "Number of grids and expected neighbors don't match");

      for (int i = 0; i < stringGrids.Length; i++)
      {
        string[] stringGrid = stringGrids[i];

        // Ensure all strings have the same length
        Contract.ForAll(stringGrid, x => x.Length == stringGrid[0].Length);

        int rows = stringGrid.Length;
        int cols = stringGrid[0].Length;

        var grid = new GridNode[rows][];
        (int row, int col) point = (-1, -1);

        for (int row = 0; row < rows; row++)
        {
          grid[row] = new GridNode[cols];
          for (int col = 0; col < cols; col++)
          {
            char c = stringGrid[row][col];

            if (c == 'X')
            {
              point = (row, col);
            }

            grid[row][col] = new GridNode
            {
              Type = c switch
              {
                'W' => GridNodeType.Wall,
                'S' => GridNodeType.Start,
                'F' => GridNodeType.Finish,
                _ => GridNodeType.Default
              },
              Visited = false,
              Weight = 0,
              Position = (row, col)
            };
          }
        }

        // Ensure a target point is marked with 'X'
        Contract.Assert(point != (-1, -1), "No position was marked with X");

        AddRow(grid, point, expectedNeighbors[i]);
      }
    }
  }

  public class AlgorithmServiceTest
  {
    public static GetNeighborsTestData GetNeighbors_TestData = new(
      stringGrids: new string[][]
      {
        new string[] { "Xaa", "aaa", "aaa" },
        new string[] { "aXa", "aaa", "aaa" },
        new string[] { "aaX", "aaa", "aaa" },
        new string[] { "aaa", "Xaa", "aaa" },
        new string[] { "aaa", "aXa", "aaa" },
        new string[] { "aaa", "aaX", "aaa" },
        new string[] { "aaa", "aaa", "Xaa" },
        new string[] { "aaa", "aaa", "aXa" },
        new string[] { "aaa", "aaa", "aaX" },
      },
      expectedNeighbors: new (int row, int col)[][]
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
      }
    );

    public static GetNeighborsTestData GetNeighborsDiagonal_TestData = new(
      stringGrids: new string[][]
      {
        new string[] { "Xaa", "aaa", "aaa" },
        new string[] { "aXa", "aaa", "aaa" },
        new string[] { "aaX", "aaa", "aaa" },
        new string[] { "aaa", "Xaa", "aaa" },
        new string[] { "aaa", "aXa", "aaa" },
        new string[] { "aaa", "aaX", "aaa" },
        new string[] { "aaa", "aaa", "Xaa" },
        new string[] { "aaa", "aaa", "aXa" },
        new string[] { "aaa", "aaa", "aaX" },
      },
      expectedNeighbors: new (int row, int col)[][]
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
      }
    );

    private readonly IAlgorithmService sut;

    public AlgorithmServiceTest(IAlgorithmService sut)
    {
      this.sut = sut;
    }

    [Theory]
    [MemberData(nameof(GetNeighbors_TestData))]
    public void GetNeighborsShouldReturnCorrectNeigbors(GridNode[][] grid, (int row, int col) point, (int row, int col)[] expectedNeighbors)
    {
      var neighbors = sut.GetNeighbors(grid, point);

      Assert.Equal(neighbors.Count, expectedNeighbors.Length);
      Assert.All(neighbors, n =>
      {
        Assert.True(
          expectedNeighbors.Contains(n.Position),
          $"The position {n.Position} is not and expected neighbor.");
      });
    }

    [Theory]
    [MemberData(nameof(GetNeighborsDiagonal_TestData))]
    public void GetNeighborsDiagnoalShouldReturnCorrectNeigbors(GridNode[][] grid, (int row, int col) point, (int row, int col)[] expectedNeighbors)
    {
      var neighbors = sut.GetNeighborsDiagonal(grid, point);

      Assert.Equal(neighbors.Count, expectedNeighbors.Length);
      Assert.All(neighbors, n =>
      {
        Assert.True(
          expectedNeighbors.Contains(n.Position),
          $"The position {n.Position} is not and expected neighbor.");
      });
    }
  }
}

