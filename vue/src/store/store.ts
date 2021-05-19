import { createStore } from 'vuex';
import {
  GridModuleState,
  createGridModule
} from './modules/gridModule/gridModule';

export type RootState = {
  gridModule: GridModuleState;
};

export const store = createStore({
  modules: {
    gridModule: createGridModule({
      ROWS: 7,
      COLS: 15,
      START_POSITION: { row: 3, col: 1 },
      FINISH_POSITION: { row: 3, col: 13 },
      WALL_POSITIONS: [
        { row: 1, col: 6 },
        { row: 2, col: 3 },
        { row: 3, col: 5 },
        { row: 3, col: 10 },
        { row: 5, col: 3 }
      ]
    })
  },
  strict: true,
  devtools: true,
  plugins: []
});
