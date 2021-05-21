using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public static class ApiRoutes
    {
        private const string Base = "api";

        public static class PathfindingRoutes
        {
            public const string BreadthFirstSearch = Base + "/pathfinding/breadth-first-search";

            public const string Dijkstra = Base + "/pathfinding/dijkstra";

            public const string AStar = Base + "/pathfinding/a-star";
        }
    }
}
