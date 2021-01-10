using FluentValidation;

namespace Pathfinding.Shared.Domain
{
  public class GridDto
  {
    public GridNodeDto[][] Grid { get; set; }
  }

  public class GridDtoValidator : AbstractValidator<GridDto>
  {
    public GridDtoValidator()
    {
      RuleFor(x => x.Grid)
        .Must(ContainStartAndFinish)
        .WithMessage("No start and finish position found");

      RuleForEach(x => x.Grid).ForEach(nodeRule =>
      {
        nodeRule.SetValidator(new GridNodeDtoValidator());
      });
    }

    private bool ContainStartAndFinish(GridNodeDto[][] grid)
    {
      bool hasStart = false;
      bool hasFinish = false;

      foreach (var row in grid)
      {
        foreach (var node in row)
        {
          if (node.Type == GridNodeTypeDto.Start)
          {
            hasStart = true;
          }

          if (node.Type == GridNodeTypeDto.Finish)
          {
            hasFinish = true;
          }
        }
      }

      return hasStart && hasFinish;
    }
  }
}