using FluentValidation;

namespace Pathfinding.Shared.Domain
{
  public class GridNodeDto
  {
    public GridNodeTypeDto Type { get; set; }

    public int Weight { get; set; }
  }

  public class GridNodeDtoValidator : AbstractValidator<GridNodeDto>
  {
    public GridNodeDtoValidator()
    {
      RuleFor(node => node.Weight)
        .GreaterThanOrEqualTo(0)
        .WithMessage("Weight can't be negative");
    }
  }
}