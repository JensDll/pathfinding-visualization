import { useFetch } from '../composables/useFetch';
import { GridNode, Position } from '../store/modules/gridModule/gridModule';

export type PathfindingResponse = {
  visitedPositions: Position[];
  shortestPath: Position[];
};

const baseUri = 'http://localhost:5000/api';

export const pathfindingService = {
  breadthFirstSearch() {
    const { json, loading, exec } = useFetch<PathfindingResponse>();

    return {
      pathfindingResponse: json,
      loading,
      shortestPath: (grid: GridNode[][]) =>
        exec(
          new Request(`${baseUri}/grid/breadth-first-search`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(grid)
          })
        )
    };
  }
};
