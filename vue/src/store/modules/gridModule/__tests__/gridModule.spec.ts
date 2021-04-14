import { createStore, Store } from 'vuex';
import { RootState } from '../../../store';
import {
  Position,
  createNodes,
  GridNode,
  createGridModule
} from '../gridModule';

const ROWS = 5;
const COLS = 5;
const START_POSITION = { row: 1, col: 1 };
const FINISH_POSITION = { row: 3, col: 3 };

let store: Store<RootState>;

const createNode = (
  row: number,
  col: number,
  type: GridNode['type']
): GridNode => ({
  type,
  position: { row, col },
  weight: 1,
  className: ''
});

const createGrid = (
  rows: number,
  cols: number,
  startPos: Position,
  finishPos: Position
) => {
  const grid = Array.from({ length: rows }, (_, row) =>
    createNodes(row, 0, cols)
  );
  grid[startPos.row][startPos.col].type = 'start';
  grid[finishPos.row][finishPos.col].type = 'finish';

  return grid;
};

beforeEach(() => {
  store = createStore({
    modules: {
      gridModule: createGridModule({
        ROWS,
        COLS,
        START_POSITION,
        FINISH_POSITION,
        WALL_POSITIONS: []
      })
    }
  });
});

describe('createNodes', () => {
  it('should return the correct nodes', () => {
    expect(createNodes(0, 1, 3)).toEqual([
      createNode(0, 1, 'default'),
      createNode(0, 2, 'default'),
      createNode(0, 3, 'default')
    ]);

    expect(createNodes(1, 3, 5, 'wall')).toEqual([
      createNode(1, 3, 'wall'),
      createNode(1, 4, 'wall'),
      createNode(1, 5, 'wall'),
      createNode(1, 6, 'wall'),
      createNode(1, 7, 'wall')
    ]);
  });
});

it('should initialize the correct board', () => {
  expect(store.state.gridModule.grid).toEqual(
    createGrid(ROWS, COLS, START_POSITION, FINISH_POSITION)
  );
});

describe('action updateRows', () => {
  it('should add rows to board', async () => {
    await store.dispatch('gridModule/updateRows', ROWS + 10);

    expect(store.state.gridModule.grid).toEqual(
      createGrid(ROWS + 10, COLS, START_POSITION, FINISH_POSITION)
    );
  });

  it('should remove rows from board', async () => {
    await store.dispatch('gridModule/updateRows', ROWS + 10);

    expect(store.state.gridModule.grid).toEqual(
      createGrid(ROWS + 10, COLS, START_POSITION, FINISH_POSITION)
    );

    await store.dispatch('gridModule/updateRows', ROWS + 4);

    expect(store.state.gridModule.grid).toEqual(
      createGrid(ROWS + 4, COLS, START_POSITION, FINISH_POSITION)
    );
  });
});

describe('action updateCols', () => {
  it('should add columns to board', async () => {
    await store.dispatch('gridModule/updateCols', COLS + 10);

    expect(store.state.gridModule.grid).toEqual(
      createGrid(ROWS, COLS + 10, START_POSITION, FINISH_POSITION)
    );
  });

  it('should remove columns from board', async () => {
    await store.dispatch('gridModule/updateCols', COLS + 10);

    expect(store.state.gridModule.grid).toEqual(
      createGrid(ROWS, COLS + 10, START_POSITION, FINISH_POSITION)
    );

    await store.dispatch('gridModule/updateCols', COLS + 4);

    expect(store.state.gridModule.grid).toEqual(
      createGrid(ROWS, COLS + 4, START_POSITION, FINISH_POSITION)
    );
  });
});
