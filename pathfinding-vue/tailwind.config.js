module.exports = {
  purge: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode: false, // or 'media' or 'class'
  theme: {
    screens: {
      640: '640px',
      768: '768px',
      1024: '1024px',
      1280: '1280px',
      1800: '1800px'
    },
    extend: {}
  },
  variants: {
    extend: {}
  },
  plugins: []
};
