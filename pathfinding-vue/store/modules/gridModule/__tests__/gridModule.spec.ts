import { Store } from 'vuex';
import { RootState, storeFactory } from '../../../store';
import {
  Position,
  getDefaultNodes,
  GridNode,
  gridModuleFactory
} from '../../gridModule/gridModule';

const nodeFactory = (
  row: number,
  col: number,
  type: GridNode['type']
): GridNode => ({
  type,
  position: { row, col },
  weight: 1,
  className: ''
});

const gridFactory = (
  rows: number,
  cols: number,
  startPos: Position,
  finishPos: Position
) => {
  const grid = Array.from({ length: rows }, (_, row) =>
    getDefaultNodes(row, 0, cols)
  );
  grid[startPos.row][startPos.col].type = 'start';
  grid[finishPos.row][finishPos.col].type = 'finish';

  return grid;
};

let store: Store<RootState>;

const ROWS = 5;
const COLS = 5;
const START_POINT = { row: 1, col: 1 };
const FINISH_POINT = { row: 3, col: 3 };

beforeEach(() => {
  store = storeFactory(
    gridModuleFactory(ROWS, COLS, START_POINT, FINISH_POINT)
  );
});

describe('getDefaultNodes', () => {
  it('should return the correct nodes', () => {
    expect(getDefaultNodes(0, 1, 3)).toEqual([
      nodeFactory(0, 1, 'default'),
      nodeFactory(0, 2, 'default'),
      nodeFactory(0, 3, 'default')
    ]);

    expect(getDefaultNodes(1, 3, 5)).toEqual([
      nodeFactory(1, 3, 'default'),
      nodeFactory(1, 4, 'default'),
      nodeFactory(1, 5, 'default'),
      nodeFactory(1, 6, 'default'),
      nodeFactory(1, 7, 'default')
    ]);
  });
});

it('should initialize the correct board', () => {
  expect(store.state.gridModule.grid).toEqual(
    gridFactory(ROWS, COLS, START_POINT, FINISH_POINT)
  );
});

describe('action updateRows', () => {
  it('should add rows to board', async () => {
    await store.dispatch('gridModule/updateRows', ROWS + 10);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS + 10, COLS, START_POINT, FINISH_POINT)
    );
  });

  it('should remove rows from board', async () => {
    await store.dispatch('gridModule/updateRows', ROWS + 10);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS + 10, COLS, START_POINT, FINISH_POINT)
    );

    await store.dispatch('gridModule/updateRows', ROWS + 4);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS + 4, COLS, START_POINT, FINISH_POINT)
    );
  });
});

describe('action updateCols', () => {
  it('should add columns to board', async () => {
    await store.dispatch('gridModule/updateCols', COLS + 10);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS, COLS + 10, START_POINT, FINISH_POINT)
    );
  });

  it('should remove columns from board', async () => {
    await store.dispatch('gridModule/updateCols', COLS + 10);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS, COLS + 10, START_POINT, FINISH_POINT)
    );

    await store.dispatch('gridModule/updateCols', COLS + 4);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS, COLS + 4, START_POINT, FINISH_POINT)
    );
  });
});
