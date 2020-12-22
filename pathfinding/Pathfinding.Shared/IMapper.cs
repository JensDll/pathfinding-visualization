using System.Threading.Tasks;
using Pathfinding.Shared.Domain;

namespace Pathfinding.Shared
{
  public interface IMapper
  {
    bool TransformGrid(GridNodeDomain[][] grid, out ((int row, int col) start, (int row, int col) finish, GridNode[][] grid) gridData);
  }
}