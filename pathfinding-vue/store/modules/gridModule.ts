import { mapActions, mapState, Module } from 'vuex';
import { RootState } from '../store';

export type Node = {
  type: 'wall' | 'start' | 'finish' | '';
  point: Point;
};

export type GridModuleState = {
  grid: Node[][];
  startPoint: Point;
  finishPoint: Point;
  lastClicked: {
    type: Node['type'];
    point: Point;
  };
  lastEntered: {
    type: Node['type'];
    point: Point;
  };
};

export type Point = { row: number; col: number };

export const getDefaultNodes = (
  row: number,
  startCol: number,
  howMany: number
) =>
  Array.from<undefined, Node>({ length: howMany }, (_, i) => ({
    type: '',
    point: {
      row,
      col: i + startCol
    }
  }));

enum MUTATIONS {
  SET_ROWS = 'setRows',
  SET_COLS = 'setCols',
  SET_DEFAULT = 'setDefault',
  SET_WALL = 'setWall',
  SET_START = 'setStat',
  SET_FINISH = 'setFinish',
  SET_TYPE = 'setType',
  SET_LAST_CLICKED = 'setLastClicked',
  SET_LAST_ENTERED = 'setLastEntered'
}

export const mutations: Module<GridModuleState, RootState>['mutations'] = {
  [MUTATIONS.SET_ROWS](state, rows: number) {
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
  [MUTATIONS.SET_COLS](state, cols: number) {
    const oldCols = state.grid[0].length;

    if (oldCols > cols) {
      state.grid.forEach(row => (row.length = cols));
    } else {
      state.grid.forEach((row, i) => {
        state.grid[i] = [
          ...row,
          ...getDefaultNodes(row[0].point.row, oldCols, cols - oldCols)
        ];
      });
    }
  },
  [MUTATIONS.SET_WALL](state, { row, col }: Point) {
    const type = state.grid[row][col].type;

    if (type === 'wall') {
      state.grid[row][col].type = '';
    } else if (type === '') {
      state.grid[row][col].type = 'wall';
    }
  },
  [MUTATIONS.SET_START](state, { row, col }: Point) {
    state.startPoint = { row, col };
    state.grid[row][col].type = 'start';
  },
  [MUTATIONS.SET_FINISH](state, { row, col }: Point) {
    state.finishPoint = { row, col };
    state.grid[row][col].type = 'finish';
  },
  [MUTATIONS.SET_DEFAULT](state, { row, col }: Point) {
    state.grid[row][col].type = '';
  },
  [MUTATIONS.SET_TYPE](
    state,
    { type, point: { row, col } }: { type: Node['type']; point: Point }
  ) {
    state.grid[row][col].type = type;
  },
  [MUTATIONS.SET_LAST_CLICKED](state, { row, col }: Point) {
    state.lastClicked = {
      type: state.grid[row][col].type,
      point: { row, col }
    };
  },
  [MUTATIONS.SET_LAST_ENTERED](state, { row, col }: Point) {
    state.lastEntered = {
      type:
        state.grid[row][col].type === 'start'
          ? ''
          : state.grid[row][col].type === 'finish'
          ? ''
          : state.grid[row][col].type,
      point: { row, col }
    };
  }
};

export const actions: Module<GridModuleState, RootState>['actions'] = {
  setRows({ commit, state }, rows: number) {
    if (rows >= 5 && rows <= 25) {
      if (state.startPoint.row >= rows) {
        commit(MUTATIONS.SET_START, { row: 0, col: 0 });
      }
      if (state.finishPoint.row >= rows) {
        commit(MUTATIONS.SET_FINISH, { row: 0, col: 1 });
      }
      commit(MUTATIONS.SET_ROWS, rows);
    }
  },
  setCols({ commit, state }, cols: number) {
    if (cols >= 5 && cols <= 50) {
      if (state.startPoint.col >= cols) {
        commit(MUTATIONS.SET_START, { row: 0, col: 0 });
      }
      if (state.finishPoint.col >= cols) {
        commit(MUTATIONS.SET_FINISH, { row: 0, col: 1 });
      }
      commit(MUTATIONS.SET_COLS, cols);
    }
  },
  nodeOnClick({ commit }, point: Point) {
    commit(MUTATIONS.SET_LAST_CLICKED, point);
    commit(MUTATIONS.SET_LAST_ENTERED, point);
    commit(MUTATIONS.SET_WALL, point);
  },
  nodeOnMouseEnter({ commit, state }, point: Point) {
    const type = state.grid[point.row][point.col].type;

    if (type === 'start' || type === 'finish' || !window.mouseIsDown) {
      return;
    }

    switch (state.lastClicked.type) {
      case 'start':
        commit(MUTATIONS.SET_TYPE, state.lastEntered);
        commit(MUTATIONS.SET_LAST_ENTERED, point);
        commit(MUTATIONS.SET_START, point);
        break;
      case 'finish':
        commit(MUTATIONS.SET_TYPE, state.lastEntered);
        commit(MUTATIONS.SET_LAST_ENTERED, point);
        commit(MUTATIONS.SET_FINISH, point);
        break;
      default:
        commit(MUTATIONS.SET_WALL, point);
    }
  }
};

export const getters: Module<GridModuleState, RootState>['getters'] = {
  // Note:
  // rootState, rootGetters are passed as 3rd and 4th argument in getter functions
  // and exposed as properties on the context object in actions.
  // ---> (state, getters, rootState, rootGetters)
  getRows: state => state.grid.length,
  getCols: state => state.grid[0].length
};

export const gridModuleActions = mapActions('gridModule', {
  setRows: (dispatch, rows: number) => dispatch('setRows', rows),
  setCols: (dispatch, cols: number) => dispatch('setCols', cols),
  nodeOnClick: (dispatch, point: Point) => dispatch('nodeOnClick', point),
  nodeOnMouseEnter: (dispatch, point: Point) =>
    dispatch('nodeOnMouseEnter', point)
});

export const gridModuleState = mapState<
  GridModuleState,
  {
    [K in keyof GridModuleState]: (
      state: GridModuleState,
      // Remember to update this type according to the getters in gridModule
      getters: {
        getRows: number;
        getCols: number;
      }
    ) => GridModuleState[K];
  }
>('gridModule', {
  grid: state => state.grid,
  lastClicked: state => state.lastClicked,
  lastEntered: state => state.lastEntered,
  startPoint: state => state.startPoint,
  finishPoint: state => state.finishPoint
});

export function gridModuleFactory(
  INIT_ROWS: number,
  INIT_COLS: number,
  START_POINT: Point,
  FINISH_POINT: Point
) {
  const INIT_GRID = Array.from({ length: INIT_ROWS }, (_, row) =>
    getDefaultNodes(row, 0, INIT_COLS)
  );
  INIT_GRID[START_POINT.row][START_POINT.col].type = 'start';
  INIT_GRID[FINISH_POINT.row][FINISH_POINT.col].type = 'finish';

  return {
    namespaced: true,
    state(): GridModuleState {
      return {
        grid: INIT_GRID,
        lastClicked: INIT_GRID[0][0],
        lastEntered: INIT_GRID[0][0],
        startPoint: START_POINT,
        finishPoint: FINISH_POINT
      };
    },
    mutations,
    actions,
    getters
  } as Module<GridModuleState, RootState>;
}
