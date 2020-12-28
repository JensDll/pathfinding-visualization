import { useFetch } from '../composables/useFetch';
import { GridNode, Position } from '../store/modules/gridModule/gridModule';

export type PathfindingResponse = {
  visitedPositions: Position[];
  shortestPath: Position[];
};

const baseUri = 'http://localhost:5000/api';

export const pathfindingService = {
  breadthFirstSearch() {
    const { json, exec } = useFetch<PathfindingResponse>();

    return {
      bfsResponse: json,
      execBfs: (grid: GridNode[][]) =>
        exec(
          new Request(`${baseUri}/algorithm/breadth-first-search`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(grid)
          })
        )
    };
  },
  dijkstra() {
    const { json, exec } = useFetch<PathfindingResponse>();

    return {
      dijkstraResponse: json,
      execDijkstra: (grid: GridNode[][]) =>
        exec(
          new Request(`${baseUri}/algorithm/dijkstra`, {
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
