import { Store } from 'vuex';
import { RootState, storeFactory } from '../../store';
import { Point, getDefaultNodes, Node, gridModuleFactory } from '../gridModule';

const nodeFactory = (row: number, col: number, type: Node['type']): Node => ({
  type,
  point: { row, col }
});

const gridFactory = (
  rows: number,
  cols: number,
  start: Point,
  finish: Point
) => {
  const grid = Array.from({ length: rows }, (_, row) =>
    getDefaultNodes(row, 0, cols)
  );
  grid[start.row][start.col].type = 'start';
  grid[finish.row][finish.col].type = 'finish';

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
      nodeFactory(0, 1, ''),
      nodeFactory(0, 2, ''),
      nodeFactory(0, 3, '')
    ]);

    expect(getDefaultNodes(1, 3, 5)).toEqual([
      nodeFactory(1, 3, ''),
      nodeFactory(1, 4, ''),
      nodeFactory(1, 5, ''),
      nodeFactory(1, 6, ''),
      nodeFactory(1, 7, '')
    ]);
  });
});

it('should initialize the correct board', () => {
  expect(store.state.gridModule.grid).toEqual(
    gridFactory(ROWS, COLS, START_POINT, FINISH_POINT)
  );
});

describe('action setRows', () => {
  it('should add rows to board', async () => {
    await store.dispatch('gridModule/setRows', ROWS + 10);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS + 10, COLS, START_POINT, FINISH_POINT)
    );
  });

  it('should remove rows from board', async () => {
    await store.dispatch('gridModule/setRows', ROWS + 10);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS + 10, COLS, START_POINT, FINISH_POINT)
    );

    await store.dispatch('gridModule/setRows', ROWS + 4);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS + 4, COLS, START_POINT, FINISH_POINT)
    );
  });
});

describe('action setCols', () => {
  it('should add columns to board', async () => {
    await store.dispatch('gridModule/setCols', COLS + 10);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS, COLS + 10, START_POINT, FINISH_POINT)
    );
  });

  it('should remove columns from board', async () => {
    await store.dispatch('gridModule/setCols', COLS + 10);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS, COLS + 10, START_POINT, FINISH_POINT)
    );

    await store.dispatch('gridModule/setCols', COLS + 4);

    expect(store.state.gridModule.grid).toEqual(
      gridFactory(ROWS, COLS + 4, START_POINT, FINISH_POINT)
    );
  });
});
