module.exports = {
  purge: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode: false,
  theme: {
    extend: {
      screens: {
        '3xl': '1800px'
      },
      cursor: {
        grab: 'grab',
        grabbing: 'grabbing'
      }
    }
  },
  variants: {
    extend: {}
  },
  plugins: []
};
