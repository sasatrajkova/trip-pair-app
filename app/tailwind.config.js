/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}", "./index.html"],
  theme: {
    extend: {
      fontSize: {
        xs: ["0.75rem", { lineHeight: "1rem" }],
        sm: ["0.875rem", { lineHeight: "1.25rem" }],
        base: ["1rem", { lineHeight: "1.5rem" }],
        lg: ["1.125rem", { lineHeight: "1.5rem" }],
        xl: ["1.25rem", { lineHeight: "1.625rem" }], // 20px
        "2xl": ["1.5rem", { lineHeight: "2rem" }], // 24px
        "3xl": ["1.625rem", { lineHeight: "2.125rem" }], // 26px
        "4xl": ["2rem", { lineHeight: "2.625rem" }], // 32px
        // '5xl': ['3rem', { lineHeight: '1' }],
        // '6xl': ['3.75rem', { lineHeight: '1' }],
         '7xl': ['4.5rem', { lineHeight: '1' }],
        // '8xl': ['6rem', { lineHeight: '1' }],
        // '9xl': ['8rem', { lineHeight: '1' }],
      },
    },
  },
  plugins: [],
};
