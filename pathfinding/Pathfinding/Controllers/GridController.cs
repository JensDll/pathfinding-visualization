using Microsoft.AspNetCore.Mvc;
using Pathfinding.Shared.Domain;
using Pathfinding.Algorithm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.Controllers
{
  [ApiController]
  public class GridController : ControllerBase
  {
    [HttpPost(ApiRoutes.GridRoutes.BreadthFirstSearch)]
    public IActionResult BreadthFirstSearch(GridNodeDomain[][] grid)
    {
      return Ok(grid);
    }

    [HttpPost(ApiRoutes.GridRoutes.Dijkstra)]
    public IActionResult Dijkstra(GridNodeDomain[][] grid)
    {
      return Ok(grid);
    }
  }
}