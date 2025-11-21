import React from 'react' // [Startup.Frontend.1] Imports
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import './styles.css'

/**
 * [Startup.Frontend.2] Router Component
 * This component handles the "Hash Routing" for the app.
 * It watches the URL (e.g., #/admin) and tells App.jsx what to show.
 * 
 * -> See Lesson: Interactive-Lesson/07-Frontend-Bootstrap-and-Routing.md
 */
function Router() {
  // [Startup.Frontend.3] State
  // Stores the current "hash" (route) in React state.
  // When this changes, the app re-renders.
  const [route, setRoute] = React.useState(window.location.hash || '#/')

  // [Startup.Frontend.4] Effect Hook
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
createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Router />
  </React.StrictMode>
)
