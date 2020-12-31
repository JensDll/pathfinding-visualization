import { PathfindingResponse } from '../../../services/pathfindingService';
import { GridModule, Position } from './gridModule';
import {
  GRID_MUTATIONS,
  HELPER_MUTATIONS,
  NODE_MUTATIONS,
  WEIGHT_MUTATIONS
} from './mutations';

export const gridActions: GridModule['actions'] = {
  updateRows({ commit, state, dispatch }, rows: number) {
    if (state.startPosition.row >= rows || state.finishPosition.row >= rows) {
      dispatch('moveStart', { row: 0, col: 0 });
      dispatch('moveFinish', { row: 0, col: 1 });
    }

    commit(GRID_MUTATIONS.UPDATE_ROWS, rows);
  },
  updateCols({ commit, state, dispatch }, cols: number) {
    if (state.startPosition.col >= cols || state.finishPosition.col >= cols) {
      dispatch('moveStart', { row: 0, col: 0 });
      dispatch('moveFinish', { row: 0, col: 1 });
    }

    commit(GRID_MUTATIONS.UPDATE_COLS, cols);
  },
  randomWeights({ commit }) {
    commit(GRID_MUTATIONS.RANDOM_WEIGHTS);
  },
  resetGridClassnames({ commit }) {
    commit(GRID_MUTATIONS.RESET_GRID_CLASSNAMES);
  },
  resetGridWeights({ commit }) {
    commit(GRID_MUTATIONS.RESET_GRID_WEIGHTS);
  },
  resetGridAll({ commit }) {
    commit(GRID_MUTATIONS.RESET_GRID_ALL);
  }
};

export const weightActions: GridModule['actions'] = {
  toggleWeightActive({ commit }) {
    commit(WEIGHT_MUTATIONS.TOGGLE_WEIGHT_ACTIVE);
  },
  toggleWeightHidden({ commit }) {
    commit(WEIGHT_MUTATIONS.TOGGLE_WEIGHT_HIDDEN);
  },
  updateWeight({ commit }, weight: number) {
    commit(WEIGHT_MUTATIONS.UPDATE_WEIGHT, weight);
  }
};

export const helperActions: GridModule['actions'] = {
  moveStart({ commit, state }, position: Position) {
    commit(NODE_MUTATIONS.SET_TYPE, state.lastEnteredStart);
    commit(HELPER_MUTATIONS.UPDATE_LAST_ENTERED_START, position);
    commit(NODE_MUTATIONS.SET_START, position);
  },
  moveFinish({ commit, state }, position: Position) {
    commit(NODE_MUTATIONS.SET_TYPE, state.lastEnteredFinish);
    commit(HELPER_MUTATIONS.UPDATE_LAST_ENTERED_FINISH, position);
    commit(NODE_MUTATIONS.SET_FINISH, position);
  }
};

export const nodeActions: GridModule['actions'] = {
  nodeOnClick({ commit, state }, position: Position) {
    commit(HELPER_MUTATIONS.UPDATE_LAST_CLICKED, position);

    if (state.weight.active) {
      commit(NODE_MUTATIONS.SET_WEIGHT, position);
    } else {
      commit(NODE_MUTATIONS.SET_WALL, position);
    }
  },
  nodeOnMouseEnter({ commit, dispatch, state }, position: Position) {
    const type = state.grid[position.row][position.col].type;

    if (type === 'start' || type === 'finish' || !window.mouseIsDown) {
      return;
    }

    switch (state.lastClicked.type) {
      case 'start':
        dispatch('moveStart', position);
        break;
      case 'finish':
        dispatch('moveFinish', position);
        break;
      default:
        if (state.weight.active) {
          commit(NODE_MUTATIONS.SET_WEIGHT, position);
        } else {
          commit(NODE_MUTATIONS.SET_WALL, position);
        }
    }
  },
  async animate(
    { commit },
    { visitedPositions, shortestPath }: PathfindingResponse
  ) {
    for (const position of visitedPositions) {
      await new Promise<void>(r => setTimeout(r, 0));
      commit(NODE_MUTATIONS.SET_CLASS_VISITED, position);
    }
    for (const position of shortestPath) {
      await new Promise<void>(r => setTimeout(r, 0));
      commit(NODE_MUTATIONS.SET_CLASS_PATH, position);
    }
  }
};
