import { createStore, Module } from 'vuex';
import { GridModuleState, gridModuleFactory } from './modules/gridModule';

export type RootState = {
  gridModule: GridModuleState;
};

export const store = createStore({
  modules: {
    gridModule: gridModuleFactory(5, 5, { row: 1, col: 1 }, { row: 3, col: 3 })
  },
  strict: true,
  devtools: true,
  plugins: []
});

export const storeFactory = (gridModule: Module<GridModuleState, RootState>) =>
  createStore({
    modules: {
      gridModule
    }
  });
