import { GridNode, Position } from '../store/modules/gridModule/gridModule';
import { apiClient } from './apiClient';

export type PathfindingRequest = {
  grid: GridNode[][];
  searchDiagonal: boolean;
};

export type PathfindingResponse = {
  visitedPositions: Position[];
  shortestPath: Position[];
};

export const pathfindingService = {
  breadthFirstSearch(request: PathfindingRequest) {
    return apiClient
      .useFetch<PathfindingResponse>()
      .execute('/pathfinding/breadth-first-search')
      .post(JSON.stringify(request))
      .json().promise;
  },
  dijkstra(request: PathfindingRequest) {
    return apiClient
      .useFetch<PathfindingResponse>()
      .execute('/pathfinding/dijkstra')
      .post(JSON.stringify(request))
      .json().promise;
  }
};
