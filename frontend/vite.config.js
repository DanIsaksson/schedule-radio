import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// Vite Configuration
// This file configures the build tool and development server.
// It's like the .csproj or launchSettings.json for the frontend.
export default defineConfig({
  plugins: [react()], // Enable React support
  server: {
    port: 5173, // The port where the frontend runs (http://localhost:5173)
    
    // Proxy Configuration:
    // Since our frontend (port 5173) and backend (port 5219) are on different ports,
    // we need a proxy to avoid Cross-Origin (CORS) issues during development.
    // When the frontend asks for '/db/...', Vite forwards it to 'http://localhost:5219/db/...'.
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
  // Build Output:
  // When we run 'npm run build', the files go here.
  // We could target the ASP.NET wwwroot folder so the backend can serve the frontend.
  // ,build: { outDir: '../API/wwwroot', emptyOutDir: true }
})
