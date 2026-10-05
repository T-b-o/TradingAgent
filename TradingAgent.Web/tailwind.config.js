/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Components/**/*.{razor,cs,html}',
    './wwwroot/**/*.html',
  ],
  theme: {
    extend: {
      boxShadow: {
        soft: '0 20px 40px rgba(15, 23, 42, 0.08)',
      },
    },
  },
  plugins: [],
};
