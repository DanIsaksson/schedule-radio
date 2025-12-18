// A.1 [Frontend.Startup] Entry point for the React app (Vite).
// What: Mounts React into index.html and sets up a minimal hash router (#/...) without extra libraries.
// Why: Hash routes work on static hosting (no server-side route rewrites) and keep this school assignment focused.
// Where: frontend/index.html loads this file via <script type="module" src="/src/main.jsx">.
import React from 'react' // [Startup.Frontend.1] Imports
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import { AuthProvider } from './auth/AuthContext.jsx'
import './styles.css'

/**
 * [Startup.Frontend.2] Router Component
 * This component handles the "Hash Routing" for the app.
 * It watches the URL (e.g., #/portal) and tells App.jsx what to show.
 * Why: We keep routing lightweight (no react-router). Hash routing works even if the backend only serves index.html.
 * Where: App.jsx receives `route` and decides which "page" (Home / Schedule / Portal) to render.
 * 
 * -> See Lesson: Interactive-Lesson/07-Frontend-Bootstrap-and-Routing.md
 */
function Router() {
  // [Startup.Frontend.3] State
  // Stores the current "hash" (route) in React state.
  // Analogy: useState is the component's "Memory" — it remembers the route between renders.
  // When this changes, the app re-renders.
  const [route, setRoute] = React.useState(window.location.hash || '#/')

  // [Startup.Frontend.4] Effect Hook
  // Analogy: useEffect is a "Side Effect" — it listens to the outside world (browser events) and updates our Memory.
  // Sets up an event listener to watch for URL changes.
  React.useEffect(() => {
    const onHashChange = () => setRoute(window.location.hash || '#/')
    window.addEventListener('hashchange', onHashChange)
    return () => window.removeEventListener('hashchange', onHashChange)
  }, []) 

  // [Startup.Frontend.5] Render App
  // Passes the current route down to the main App component.
  return <App route={route} />
}

// [Startup.Frontend.6] Entry Point
// 1. Finds the <div id="root"> in index.html.
// 2. Takes control of it with React.
// 3. Renders the <Router /> inside.
// B.1 [Frontend.Startup] We wrap <Router /> in <AuthProvider> so any staff page can read auth state via useAuth().
createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AuthProvider>
      <Router />
    </AuthProvider>
  </React.StrictMode>
)
