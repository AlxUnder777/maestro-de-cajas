/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
      "./Pages/**/*.{cshtml,html}",
      "./Pages/**/*.cshtml",
      "./wwwroot/**/*.{js,css}"
    ],
    theme: {
      extend: {},
    },
    plugins: [
      require("daisyui")
    ],
    daisyui: {
      themes: ["black", "business", "dracula", "dark"]
    }
  };
  