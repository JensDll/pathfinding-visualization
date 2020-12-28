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
  public class AlgorithmController : ControllerBase
  {
    private readonly IMapper mapper;
    private readonly IBreadthFirstSearch bfs;
    private readonly IDijkstra dijkstra;

    public AlgorithmController(IMapper mapper, IBreadthFirstSearch bfs, IDijkstra dijkstra)
    {
      this.mapper = mapper;
      this.bfs = bfs;
      this.dijkstra = dijkstra;
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
    [ProducesResponseType(typeof(PathfindingResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public IActionResult Dijkstra(GridNodeDto[][] grid)
    {
      if (mapper.TryTransformGrid(grid, out (int, int) start, out _, out var transformedGrid))
      {
        var result = dijkstra.ShortestPath(start, transformedGrid);

        return Ok(mapper.MapPathfindingResult(result));
      }

      return BadRequest(new ValidationErrorDto
      {
        Message = "No start or finish position found"
      });
    }
  }
}