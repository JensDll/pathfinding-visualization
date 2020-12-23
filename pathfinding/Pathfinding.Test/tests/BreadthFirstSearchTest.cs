using System.Linq;
using Pathfinding.Algorithm;
using Pathfinding.Shared;
using Pathfinding.Shared.Domain;
using Xunit;

namespace Pathfinding.Test
{
  public class BreadthFirstSearchTest
  {
    public static string[][] testGrids_1 = new string[][]
    {
      new string[] { "SaaaaaF", "aaaaaaa" },
      new string[] { "SaaWaaF", "aaaaaaa" },
      new string[] { "SaaWaWF", "aaaaaaa" },
      new string[] { "SaaaWF", "WWWWWWW" },
    };

    public static (int row, int col)[][] shortestPaths = new (int row, int col)[][]
    {
      new (int row, int col)[] { (0, 0), (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6) },
      new (int row, int col)[] { (0, 0), (0, 1), (0, 2), (1, 2), (1, 3), (1, 4), (0, 4), (0, 5), (0, 6) },
      new (int row, int col)[] { (0, 0), (0, 1), (0, 2), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (0, 6) },
      new (int row, int col)[] {},
    };

    public static string[][] testGrids_2 = new string[][]
    {
      new string[] { "SaaaaWF", "WWWWWWW" },
      new string[] { "SaWF", "aaWW", "aaWW" },
      new string[] { "SaaWF", "WWaWW", "WWaWW", "WWaWW", "WWaWW", "WWaaa" },
    };

    public static (int row, int col)[][] visitedNodes = new (int row, int col)[][]
    {
      new (int row, int col)[] { (0, 0), (0, 1), (0, 2), (0, 3), (0, 4) },
      new (int row, int col)[] { (2, 0), (2, 1), (1, 0), (1, 1), (0, 0), (0, 1) },
      new (int row, int col)[] { (0, 0), (0, 1), (0, 2), (1, 2), (2, 2), (3, 2), (4, 2), (5, 2), (5, 3), (5, 4) },
    };

    public static BreadthFirstSearchTheoryData testData_1 = new(testGrids_1, shortestPaths);
    public static BreadthFirstSearchTheoryData testData_2 = new(testGrids_2, visitedNodes);


    private readonly IBreadthFirstSearch sut;

    public BreadthFirstSearchTest(IBreadthFirstSearch sut)
    {
      this.sut = sut;
    }

    [Theory]
    [MemberData(nameof(testData_1))]
    public void BFS_ShouldReturnTheShortestPath(GridNode[][] grid, (int row, int col) start, (int row, int col)[] expectedShortestPath)
    {
      var result = sut.ShortestPath(start, grid);
      var positions = result.ShortestPath.Select(node => node.Position);
      Assert.Equal(expectedShortestPath, positions);
    }

    [Theory]
    [MemberData(nameof(testData_2))]
    public void BFS_ShouldVisitAllNodes(GridNode[][] grid, (int row, int col) start, (int row, int col)[] expectedVisitedPositions)
    {
      var result = sut.ShortestPath(start, grid);
      var positions = result.VisitedNodes.Select(node => node.Position);

      Assert.All(expectedVisitedPositions, p =>
      {
        Assert.True(positions.Contains(p));
      });
    }
  }

  public class BreadthFirstSearchTheoryData : TheoryData
  {
    public BreadthFirstSearchTheoryData(string[][] testGrids, (int row, int col)[][] shortestPaths)
    {
      var data = GridFactory.ProduceGrids(testGrids);

      int i = 0;
      foreach (var (grid, start, _) in data)
      {
        AddRow(grid, start, shortestPaths[i++]);
      }
    }
  }
}