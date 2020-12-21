module.exports = {
  settings: {
    'vetur.useWorkspaceDependencies': true,
    'vetur.experimental.templateInterpolationService': true,
    'vetur.validation.style': false,
  },
  projects: [
    {
      root: './pathfinding-vue',
      package: './package.json',
      tsconfig: './tsconfig.json'
    }
  ]
};
