using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pathfinding.Interfaces
{
    public interface IGetNeighbors
    {
        List<GridNode> GetNeighbors(GridNode[][] grid, Position position);
    }
}
