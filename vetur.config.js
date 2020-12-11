module.exports = {
  settings: {
    'vetur.useWorkspaceDependencies': true,
    'vetur.experimental.templateInterpolationService': true
  },
  projects: [
    {
      root: './pathfinding-vue',
      package: './package.json',
      tsconfig: './tsconfig.json'
    }
  ]
};
