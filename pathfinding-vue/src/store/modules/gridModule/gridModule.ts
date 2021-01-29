import { mapActions, mapState, Module } from 'vuex';
import { RootState } from '../../store';
import { PathfindingResponse } from '../../../services/pathfindingService';
import {
  gridMutations,
  weightMutations,
  helperMutations,
  nodeMutations,
  animateMutations
} from './mutations';
import {
  animateActions,
  gridActions,
  helperActions,
  nodeActions,
  weightActions
} from './actions';
import { getters } from './getters';

export type GridNode = {
  type: 'wall' | 'start' | 'finish' | 'default';
  className: 'visited' | 'path' | '';
  position: Position;
  weight: number;
};

export type Delay = number;

export type GridModuleState = {
  grid: GridNode[][];
  startPosition: Position;
  finishPosition: Position;
  delay: Delay;
  weight: {
    active: boolean;
    hidden: boolean;
    value: number;
  };
  lastClicked: {
    type: GridNode['type'];
    position: Position;
  };
  lastEnteredStart: {
    type: GridNode['type'];
    position: Position;
  };
  lastEnteredFinish: {
    type: GridNode['type'];
    position: Position;
  };
};

export type GridModule = Module<GridModuleState, RootState>;

export type Position = { row: number; col: number };

export const getDefaultNodes = (
  row: number,
  startCol: number,
  howMany: number
) =>
  Array.from<undefined, GridNode>({ length: howMany }, (_, i) => ({
    type: 'default',
    className: '',
    position: {
      row,
      col: i + startCol
    },
    weight: 1
  }));

export function gridModuleFactory({
  INIT_ROWS,
  INIT_COLS,
  START_POSITION,
  FINISH_POSITION
}: {
  INIT_ROWS: number;
  INIT_COLS: number;
  START_POSITION: Position;
  FINISH_POSITION: Position;
}): GridModule {
  const INIT_GRID = Array.from({ length: INIT_ROWS }, (_, row) =>
    getDefaultNodes(row, 0, INIT_COLS)
  );
  INIT_GRID[START_POSITION.row][START_POSITION.col].type = 'start';
  INIT_GRID[FINISH_POSITION.row][FINISH_POSITION.col].type = 'finish';

  return {
    namespaced: true,
    state() {
      return {
        grid: INIT_GRID,
        weight: {
          active: false,
          hidden: true,
          value: 1
        },
        lastClicked: INIT_GRID[0][0],
        lastEnteredStart: { type: 'default', position: START_POSITION },
        lastEnteredFinish: { type: 'default', position: FINISH_POSITION },
        startPosition: START_POSITION,
        finishPosition: FINISH_POSITION,
        delay: 100
      };
    },
    mutations: {
      ...gridMutations,
      ...nodeMutations,
      ...weightMutations,
      ...helperMutations,
      ...animateMutations
    },
    actions: {
      ...gridActions,
      ...nodeActions,
      ...weightActions,
      ...helperActions,
      ...animateActions
    },
    getters
  };
}

export const gridModuleActions = mapActions('gridModule', {
  updateRows: (dispatch, rows: number) => dispatch('updateRows', rows),
  updateCols: (dispatch, cols: number) => dispatch('updateCols', cols),
  randomWeights: dispatch => dispatch('randomWeights'),
  resetGridClassnames: dispatch => dispatch('resetGridClassnames'),
  resetGridWeights: dispatch => dispatch('resetGridWeights'),
  resetGridAll: dispatch => dispatch('resetGridAll'),

  toggleWeightActive: dispatch => dispatch('toggleWeightActive'),
  toggleWeightHidden: dispatch => dispatch('toggleWeightHidden'),
  updateWeight: (dispatch, weight: number) => dispatch('updateWeight', weight),

  nodeOnClick: (dispatch, position: Position) =>
    dispatch('nodeOnClick', position),
  nodeOnMouseEnter: (dispatch, position: Position) =>
    dispatch('nodeOnMouseEnter', position),

  animate: (dispatch, pathfindingResponse: PathfindingResponse) =>
    dispatch('animate', pathfindingResponse),
  updateDelay: (dispatch, delay: Delay) => dispatch('updateDelay', delay)
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
  weight: state => state.weight,
  startPosition: state => state.startPosition,
  finishPosition: state => state.finishPosition,
  lastClicked: state => state.lastClicked,
  lastEnteredStart: state => state.lastEnteredStart,
  lastEnteredFinish: state => state.lastEnteredFinish,
  delay: state => state.delay
});
