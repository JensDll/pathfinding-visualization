import { getDefaultNodes, GridModule, GridNode, Position } from './gridModule';

export enum GRID_MUTATIONS {
  UPDATE_ROWS = 'updateRows',
  UPDATE_COLS = 'updateCols',
  RESET_GRID_CLASSNAMES = 'resetGridClassnames',
  RESET_GRID_WEIGHTS = 'resetGridWeights',
  RESET_GRID_ALL = 'resetGridAll'
}

export enum WEIGHT_MUTATIONS {
  TOGGLE_WEIGHT_ACTIVE = 'toggleWeightActive',
  TOGGLE_WEIGHT_HIDDEN = 'toggleWeightHidden',
  UPDATE_WEIGHT = 'updateWeight'
}

export enum HELPER_MUTATIONS {
  UPDATE_LAST_CLICKED = 'updateLastClicked',
  UPDATE_LAST_ENTERED_START = 'updateLastEnteredStart',
  UPDATE_LAST_ENTERED_FINISH = 'updateLastEnteredFinish'
}

export enum NODE_MUTATIONS {
  SET_WEIGHT = 'setWeight',
  SET_DEFAULT = 'setDefault',
  SET_WALL = 'setWall',
  SET_START = 'setStart',
  SET_FINISH = 'setFinish',
  SET_TYPE = 'setType',
  SET_CLASS_VISITED = 'setVisited',
  SET_CLASS_PATH = 'setPath'
}

export const gridMutations: GridModule['mutations'] = {
  [GRID_MUTATIONS.UPDATE_ROWS](state, rows: number) {
    const oldRows = state.grid.length;
    const oldCols = state.grid[0].length;

    if (oldRows > rows) {
      state.grid.length = rows;
    } else {
      for (let i = oldRows; i < rows; i++) {
        state.grid.push(getDefaultNodes(i, 0, oldCols));
      }
    }
  },
  [GRID_MUTATIONS.UPDATE_COLS](state, cols: number) {
    const oldCols = state.grid[0].length;

    if (oldCols > cols) {
      state.grid.forEach(row => (row.length = cols));
    } else {
      state.grid.forEach((row, i) => {
        state.grid[i] = [
          ...row,
          ...getDefaultNodes(row[0].position.row, oldCols, cols - oldCols)
        ];
      });
    }
  },
  [GRID_MUTATIONS.RESET_GRID_CLASSNAMES](state) {
    const grid = state.grid.map(row =>
      row.map<GridNode>(node => ({
        type: node.type,
        position: { ...node.position },
        className: '',
        weight: node.weight
      }))
    );

    state.grid = grid;
  },
  [GRID_MUTATIONS.RESET_GRID_WEIGHTS](state) {
    const grid = state.grid.map(row =>
      row.map<GridNode>(node => ({
        type: node.type,
        position: { ...node.position },
        className: node.className,
        weight: 1
      }))
    );

    state.grid = grid;
  },
  [GRID_MUTATIONS.RESET_GRID_ALL](state) {
    const grid = state.grid.map(row =>
      row.map<GridNode>(node => ({
        type:
          node.type === 'start'
            ? 'start'
            : node.type === 'finish'
            ? 'finish'
            : 'default',
        position: { ...node.position },
        className: '',
        weight: 1
      }))
    );

    state.grid = grid;
  }
};

export const weightMutations: GridModule['mutations'] = {
  [WEIGHT_MUTATIONS.TOGGLE_WEIGHT_ACTIVE](state) {
    state.weight.active = !state.weight.active;
  },
  [WEIGHT_MUTATIONS.TOGGLE_WEIGHT_HIDDEN](state) {
    state.weight.hidden = !state.weight.hidden;
  },
  [WEIGHT_MUTATIONS.UPDATE_WEIGHT](state, weight: number) {
    state.weight.value = weight;
  }
};

export const helperMutations: GridModule['mutations'] = {
  [HELPER_MUTATIONS.UPDATE_LAST_CLICKED](state, { row, col }: Position) {
    state.lastClicked = {
      type: state.grid[row][col].type,
      position: { row, col }
    };
  },
  [HELPER_MUTATIONS.UPDATE_LAST_ENTERED_START](state, { row, col }: Position) {
    const type = state.grid[row][col].type;

    state.lastEnteredStart = {
      type: type === 'start' || type === 'finish' ? 'default' : type,
      position: { row, col }
    };
  },
  [HELPER_MUTATIONS.UPDATE_LAST_ENTERED_FINISH](state, { row, col }: Position) {
    const type = state.grid[row][col].type;

    state.lastEnteredFinish = {
      type: type === 'start' || type === 'finish' ? 'default' : type,
      position: { row, col }
    };
  }
};

export const nodeMutations: GridModule['mutations'] = {
  [NODE_MUTATIONS.SET_WEIGHT](state, { row, col }: Position) {
    if (state.grid[row][col].type === 'default') {
      state.grid[row][col].weight = state.weight.value;
    }
  },
  [NODE_MUTATIONS.SET_WALL](state, { row, col }: Position) {
    const type = state.grid[row][col].type;

    if (type === 'wall') {
      state.grid[row][col].type = 'default';
    } else if (type === 'default') {
      state.grid[row][col].type = 'wall';
    }
  },
  [NODE_MUTATIONS.SET_START](state, { row, col }: Position) {
    state.startPosition = { row, col };
    state.grid[row][col].type = 'start';
  },
  [NODE_MUTATIONS.SET_FINISH](state, { row, col }: Position) {
    state.finishPosition = { row, col };
    state.grid[row][col].type = 'finish';
  },
  [NODE_MUTATIONS.SET_DEFAULT](state, { row, col }: Position) {
    state.grid[row][col].type = 'default';
  },
  [NODE_MUTATIONS.SET_TYPE](
    state,
    {
      type,
      position: { row, col }
    }: { type: GridNode['type']; position: Position }
  ) {
    state.grid[row][col].type = type;
  },
  [NODE_MUTATIONS.SET_CLASS_VISITED](state, { row, col }: Position) {
    state.grid[row][col].className = 'visited';
  },
  [NODE_MUTATIONS.SET_CLASS_PATH](state, { row, col }: Position) {
    state.grid[row][col].className = 'path';
  }
};
