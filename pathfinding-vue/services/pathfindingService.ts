import { useFetch } from '../composables/useFetch';
import { Node, Point } from '../store/modules/gridModule';

export type PathfindingResponse = {
  visitedPositions: Point[];
  shortestPath: Point[];
};

const baseUri = 'http://localhost:5000/api';

export const pathfindingService = {
  breadthFirstSearch() {
    const { json, loading, exec } = useFetch<PathfindingResponse>();

    return {
      pathfindingResponse: json,
      loading,
      shortestPath: (grid: Node[][]) =>
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
