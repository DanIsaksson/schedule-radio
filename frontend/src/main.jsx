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
