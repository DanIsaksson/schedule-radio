import React from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import './styles.css'

// Router: I read window.location.hash (e.g. "#/", "#/schedule", "#/admin")
// - I re-render when the hash changes and pass the route into <App /> as a prop.
function Router() {
  // Keep current route in state; default to home if no hash
  const [route, setRoute] = React.useState(window.location.hash || '#/')
  // Listen for hash changes (user clicks a link like #/schedule)
  React.useEffect(() => {
    const onHashChange = () => setRoute(window.location.hash || '#/')
    window.addEventListener('hashchange', onHashChange)
    return () => window.removeEventListener('hashchange', onHashChange)
  }, [])
  // Pass the current route down; App decides what to render
  return <App route={route} />
}

createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Router />
  </React.StrictMode>
)

// === Experiments: Hash-based routing (main.jsx) ===
// Experiment 1: Add a new route.
//   Step 1: Update App.jsx to handle a new route flag (e.g. #/lab).
//   Step 2: Use links or change window.location.hash to "#/lab" and confirm App renders the new view.
//   Step 3: Keep or remove the new route once you've explored the flow.
// Experiment 2: Hashchange listener.
//   Step 1: Temporarily comment out the addEventListener/removeEventListener in Router.
//   Step 2: Change the URL hash and observe that the UI no longer updates.
//   Step 3: Restore the listener and confirm navigation works again.
