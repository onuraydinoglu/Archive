/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Views/**/*.cshtml", // Razor view dosyalarındaki Tailwind sınıflarını tarar
    "./wwwroot/**/*.js", // Statik dosyaları tarar
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require("daisyui"), // DaisyUI eklentisi
  ],
};
