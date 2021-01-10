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
    public IActionResult BreadthFirstSearch(GridDto gridDto)
    {
      if (TryGetValidationErrors(gridDto, out var errors))
      {
        return BadRequest(errors);
      }

      var (grid, start, _) = mapper.TransformGrid(gridDto);

      var result = bfs.ShortestPath(start, grid);

      return Ok(mapper.MapPathfindingResult(result));
    }


    [HttpPost(ApiRoutes.GridRoutes.Dijkstra)]
    [ProducesResponseType(typeof(PathfindingResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public IActionResult Dijkstra(GridDto gridDto)
    {
      if (TryGetValidationErrors(gridDto, out var errors))
      {
        return BadRequest(errors);
      }

      var (grid, start, _) = mapper.TransformGrid(gridDto);

      var result = dijkstra.ShortestPath(start, grid);

      return Ok(mapper.MapPathfindingResult(result));
    }

    private bool TryGetValidationErrors(GridDto gridDto, out ValidationErrorDto validationErrors)
    {
      var validator = new GridDtoValidator();
      var validationResult = validator.Validate(gridDto);

      validationErrors = new();

      if (!validationResult.IsValid)
      {
        validationErrors = new ValidationErrorDto
        {
          ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage)
        };

        return true;
      }

      return false;
    }
  }
}