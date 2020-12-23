using Microsoft.AspNetCore.Mvc;
using Pathfinding.Shared.Domain;
using Pathfinding.Algorithm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinding.Shared;
using Microsoft.AspNetCore.Http;

namespace Pathfinding.Controllers
{
  [ApiController]
  [Produces("application/json")]
  public class GridController : ControllerBase
  {
    private readonly IMapper mapper;
    private readonly IBreadthFirstSearch bfs;

    public GridController(IMapper mapper, IBreadthFirstSearch bfs)
    {
      this.mapper = mapper;
      this.bfs = bfs;
    }

    [HttpPost(ApiRoutes.GridRoutes.BreadthFirstSearch)]
    [ProducesResponseType(typeof(PathfindingResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public IActionResult BreadthFirstSearch(GridNodeDto[][] grid)
    {
      if (mapper.TryTransformGrid(grid, out (int, int) start, out _, out var transformedGrid))
      {
        var result = bfs.ShortestPath(start, transformedGrid);

        return Ok(mapper.MapPathfindingResult(result));
      }

      return BadRequest(new ValidationErrorDto
      {
        Message = "No start or finish position found"
      });
    }


    [HttpPost(ApiRoutes.GridRoutes.Dijkstra)]
    public IActionResult Dijkstra(GridNodeDto[][] grid)
    {
      return Ok(new { row = 1 });
    }
  }
}