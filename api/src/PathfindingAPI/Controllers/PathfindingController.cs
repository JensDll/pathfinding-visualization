using Application.Interfaces;
using Contracts;
using Contracts.Request;
using Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using PathfindingAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class PathfindingController : ControllerBase
    {
        private readonly IPathfindingService _pathfindingService;
        private readonly IPathfindingRequestMapper _requestMapper;
        private readonly IPathfindingResponseMapper _responseMapper;

        public PathfindingController(IPathfindingService pathfindingService,
            IPathfindingRequestMapper requestMapper,
            IPathfindingResponseMapper responseMapper)
        {
            _pathfindingService = pathfindingService;
            _requestMapper = requestMapper;
            _responseMapper = responseMapper;
        }

        [HttpPost(ApiRoutes.PathfindingRoutes.BreadthFirstSearch)]
        [ProducesResponseType(typeof(PathfindingResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationErrorResponseDto), 400)]
        public IActionResult BreadthFirstSearch(PathfindingRequestDto pathfindingRequestDto)
        {
            return GenerateResponse(PathfindingAlgorithm.BreadthFirstSearch, pathfindingRequestDto);
        }

        [HttpPost(ApiRoutes.PathfindingRoutes.Dijkstra)]
        [ProducesResponseType(typeof(PathfindingResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationErrorResponseDto), 400)]
        public IActionResult Dijkstra(PathfindingRequestDto pathfindingRequestDto)
        {
            return GenerateResponse(PathfindingAlgorithm.Dijkstra, pathfindingRequestDto);
        }

        [HttpPost(ApiRoutes.PathfindingRoutes.AStar)]
        [ProducesResponseType(typeof(PathfindingResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationErrorResponseDto), 400)]
        public IActionResult AStar(PathfindingRequestDto pathfindingRequestDto)
        {
            return GenerateResponse(PathfindingAlgorithm.AStar, pathfindingRequestDto);
        }

        private IActionResult GenerateResponse(PathfindingAlgorithm algorithm, PathfindingRequestDto pathfindingRequestDto)
        {
            var errors = ValidateRequest(pathfindingRequestDto);

            if (errors.Any())
            {
                return BadRequest(new ValidationErrorResponseDto
                {
                    ErrorMessages = errors
                });
            }

            var grid = _requestMapper.MapPathfindingRequestDto(pathfindingRequestDto);

            var pathfindingResult = algorithm switch
            {
                PathfindingAlgorithm.BreadthFirstSearch => _pathfindingService.BreadthFirstSearch(grid),
                PathfindingAlgorithm.Dijkstra => _pathfindingService.Dijkstra(grid),
                PathfindingAlgorithm.AStar => _pathfindingService.AStar(grid),
                _ => throw new InvalidEnumArgumentException()
            };

            return Ok(_responseMapper.MapPathfindingResult(pathfindingResult));
        }

        private static IEnumerable<string> ValidateRequest(PathfindingRequestDto pathfindingRequestDto)
        {
            var validator = new PathfindingRequestDtoValidator();
            var validationResult = validator.Validate(pathfindingRequestDto);

            return validationResult.Errors.Select(error => error.ErrorMessage);
        }
    }
}
