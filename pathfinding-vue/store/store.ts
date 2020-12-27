import { createLogger, createStore, Module } from 'vuex';
import {
  GridModuleState,
  gridModuleFactory
} from './modules/gridModule/gridModule';

export type RootState = {
  gridModule: GridModuleState;
};

export const store = createStore({
  modules: {
    gridModule: gridModuleFactory(5, 10, { row: 2, col: 2 }, { row: 2, col: 7 })
  },
  strict: true,
  devtools: true,
  plugins: [
    // createLogger({
    //   actionFilter(action) {
    //     if (action.type === 'gridModule/nodeOnMouseEnter') {
    //       return false;
    //     }
    //     return true;
    //   }
    // })
  ]
});

export const storeFactory = (gridModule: Module<GridModuleState, RootState>) =>
  createStore({
    modules: {
      gridModule
    }
  });
