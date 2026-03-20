/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./**/*.{razor,html,cshtml}",
        "./wwwroot/**/*.js",
        "./Components/**/*.{razor,cs}",
        "./Pages/**/*.{razor,cs}"
    ],
    theme: {
        extend: {
            colors: {
                primary: 'var(--primary-color)',
                secondary: 'var(--secondary-color)',
                tertiary: 'var(--tertiary-color)',
                text: 'var(--text-color)',
                'light-bg': 'var(--light-bg)',
                white: 'var(--white)',
                gray: 'var(--gray)',
                'border-color': 'var(--border-color)',
                orange: {
                    50: 'var(--orange-50)',
                    100: 'var(--orange-100)',
                    200: 'var(--orange-200)',
                    300: 'var(--orange-300)',
                    400: 'var(--orange-400)',
                    500: 'var(--orange-500)',
                    600: 'var(--orange-600)',
                    700: 'var(--orange-700)',
                    800: 'var(--orange-800)',
                    900: 'var(--orange-900)',
                    950: 'var(--orange-950)',
                },
                blue: {
                    50: 'var(--blue-50)',
                    100: 'var(--blue-100)',
                    200: 'var(--blue-200)',
                    300: 'var(--blue-300)',
                    400: 'var(--blue-400)',
                    500: 'var(--blue-500)',
                    600: 'var(--blue-600)',
                    700: 'var(--blue-700)',
                    800: 'var(--blue-800)',
                    900: 'var(--blue-900)',
                    950: 'var(--blue-950)',
                },
                yellow: {
                    50: 'var(--yellow-50)',
                    100: 'var(--yellow-100)',
                    200: 'var(--yellow-200)',
                    300: 'var(--yellow-300)',
                    400: 'var(--yellow-400)',
                    500: 'var(--yellow-500)',
                    600: 'var(--yellow-600)',
                    700: 'var(--yellow-700)',
                },
            },
            fontFamily: {
                main: 'var(--font-main)',
            },
        },
    },
    plugins: [],
}