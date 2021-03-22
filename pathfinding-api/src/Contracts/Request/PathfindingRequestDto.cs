using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public record PathfindingRequestDto(GridNodeDto[][] Grid, bool SearchDiagonal);

    public class PathfindingRequestDtoValidator : AbstractValidator<PathfindingRequestDto>
    {
        public PathfindingRequestDtoValidator()
        {
            RuleForEach(request => request.Grid)
                .ForEach(gridNodeDtoRule =>
                    gridNodeDtoRule.SetValidator(new GridNodeDtoValidator()));

            RuleFor(request => request.Grid)
                .Must(ContainStart)
                .WithMessage("Grid must contain a start position")
                .Must(ContainFinish)
                .WithMessage("Grid must contain a finish position");
        }

        private bool ContainStart(GridNodeDto[][] grid) =>
            grid.SelectMany(row => row)
                .Count(gridNodeDto => gridNodeDto.Type is GridNodeTypeDto.Start) == 1;

        private bool ContainFinish(GridNodeDto[][] grid) =>
            grid.SelectMany(row => row)
                .Count(gridNodeDto => gridNodeDto.Type is GridNodeTypeDto.Finish) == 1;
    }
}
