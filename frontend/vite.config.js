import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      '/schedule': {
        target: 'http://localhost:5219',
        changeOrigin: true
      },
      '/db': {
        target: 'http://localhost:5219',
        changeOrigin: true
      }
    }
  }
  // Optionally output to ASP.NET wwwroot (uncomment if desired):
  // ,build: { outDir: '../API/wwwroot', emptyOutDir: true }
})
