using System.Collections.Generic;

namespace Pathfinding.Shared.Domain
{
  public class ValidationErrorDto
  {
    public IEnumerable<string> ErrorMessages { get; set; }
  }
}