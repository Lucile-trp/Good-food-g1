import type { Config } from 'tailwindcss'

const config: Config = {
  content: [
    './pages/**/*.{js,ts,jsx,tsx,mdx}',
    './components/**/*.{js,ts,jsx,tsx,mdx}',
    './app/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    colors: {
      background: "#EBEBEB",
      black: "#000000",
      dark_gray: "#373737",
      white_smoke: "#F5F5F5",
      white: "#FFFFFF",
      error: "#ff2c2c",
      secondary_green: "#6FBC85",
      secondary_purple: "#AFA5D1",
      secondary_yellow: "#FBE216",
      secondary_rose: "#F0869D",

    },
    extend: {
      backgroundImage: {
        'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
        'gradient-conic':
          'conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))',
      },
    },
  },
  plugins: [],
}
export default config
