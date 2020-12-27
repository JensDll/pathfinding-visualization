import { GridModule } from './gridModule';

export const getters: GridModule['getters'] = {
  // Note:
  // rootState, rootGetters are passed as 3rd and 4th argument in getter functions
  // and exposed as properties on the context object in actions.
  // ---> (state, getters, rootState, rootGetters)
  getRows: state => state.grid.length,
  getCols: state => state.grid[0].length
};
