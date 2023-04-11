/** @type {import('next').NextConfig} */

const nextConfig = {
  reactStrictMode: true,
  env: {
    SECRET_KEY: 'audiIsBetter',
    API_URL:'https://localhost:7236'
  },
}

module.exports = nextConfig
