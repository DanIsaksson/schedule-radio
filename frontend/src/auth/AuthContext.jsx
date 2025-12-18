// A.1 [Auth] Staff portal authentication "memory" (React Context).
// What: Centralizes login state (`user`, `roles`, `mustChangePassword`) + actions (`login`, `logout`, `refreshMe`).
// Why: Staff routes (e.g. #/bookings, #/portal/me) need shared auth state without prop-drilling.
// Where:
// - Provider is mounted in frontend/src/main.jsx (<AuthProvider> wraps <Router>).
// - Consumer is `useAuth()` used inside frontend/src/App.jsx for route guards + profile/payments.
import React, { createContext, useContext, useEffect, useMemo, useState } from 'react'

const AuthContext = createContext(null)

// B.1 [Auth] Normalize the /api/contributor/me response.
// What: Converts PascalCase/camelCase into a consistent camelCase object.
// Why: ASP.NET JSON casing can vary; normalization keeps the UI code simple.
// Where: Used by refreshMe() below; backend "door" is API/Endpoints/ContributorEndpoints.cs (GET /api/contributor/me).
function normalizeMeResponse(data) {
  const roles = data.roles ?? data.Roles ?? []

  return {
    email: data.email ?? data.Email ?? '',
    phone: data.phone ?? data.Phone ?? null,
    address: data.address ?? data.Address ?? null,
    bio: data.bio ?? data.Bio ?? null,
    photoUrl: data.photoUrl ?? data.PhotoUrl ?? null,
    mustChangePassword: Boolean(data.mustChangePassword ?? data.MustChangePassword),
    roles: Array.isArray(roles) ? roles : []
  }
}

// A.2 [Auth] AuthProvider (the Context "battery pack").
// What: Holds auth state in React memory (useState) and exposes it via <AuthContext.Provider>.
// Why: Auth is cross-cutting (many pages need it), so Context keeps it global and consistent.
// Where: main.jsx mounts this provider once at startup; App.jsx reads it via useAuth().
export function AuthProvider({ children }) {
  const [user, setUser] = useState(null)
  const [roles, setRoles] = useState([])
  const [mustChangePassword, setMustChangePassword] = useState(false)
  const [isLoading, setIsLoading] = useState(true)

  // B.2 [Auth] Reset all auth state to "logged out".
  // Why: Used on 401 responses and after logout to avoid stale user data sticking around.
  const clearAuthState = () => {
    setUser(null)
    setRoles([])
    setMustChangePassword(false)
  }

  // B.3 [Auth] Refresh current user info from the backend.
  // What: Calls GET /api/contributor/me using cookies (credentials include).
  // Why: Cookie auth means "login" only sets a cookie; the UI still needs user/role/mustChangePassword data.
  // Where:
  // - Called after login() and at startup (useEffect) to restore sessions on page refresh.
  // - App.jsx uses mustChangePassword for the forced password-change route guard.
  const refreshMe = async () => {
    setIsLoading(true)

    try {
      const res = await fetch('/api/contributor/me', {
        credentials: 'include'
      })

      if (res.status === 401) {
        clearAuthState()
        return null
      }

      if (!res.ok) {
        const text = await res.text()
        throw new Error(text || `HTTP ${res.status}`)
      }

      const data = await res.json()
      const me = normalizeMeResponse(data)

      setUser({
        email: me.email,
        phone: me.phone,
        address: me.address,
        bio: me.bio,
        photoUrl: me.photoUrl
      })
      setRoles(me.roles)
      setMustChangePassword(me.mustChangePassword)

      return me
    } finally {
      setIsLoading(false)
    }
  }

  // B.4 [Auth] Log in via the backend auth "door".
  // What: POST /api/auth/login?useCookies=true with email/password.
  // Why: We use cookie auth, so the browser stores the session cookie and future requests can be authenticated.
  // Where: The portal login form lives in App.jsx (#/portal route).
  const login = async (email, password) => {
    const res = await fetch('/api/auth/login?useCookies=true', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ email, password }),
      credentials: 'include'
    })

    if (!res.ok) {
      const text = await res.text()
      throw new Error(text || `HTTP ${res.status}`)
    }

    return await refreshMe()
  }

  // B.5 [Auth] Log out via the backend auth "door".
  // What: POST /api/auth/logout, then clear local React auth state.
  // Why: This invalidates the cookie session and prevents UI from showing protected data.
  // Where: Triggered by the StaffHeader "Logga ut" link in App.jsx.
  const logout = async () => {
    const res = await fetch('/api/auth/logout', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: '{}',
      credentials: 'include'
    })

    if (!res.ok) {
      const text = await res.text()
      throw new Error(text || `HTTP ${res.status}`)
    }

    clearAuthState()
  }

  // B.6 [Auth] Startup side effect: try to restore an existing cookie session.
  // Why: If the browser already has a valid auth cookie, the user should stay logged in after refresh.
  useEffect(() => {
    refreshMe().catch(() => {
      clearAuthState()
    })
  }, [])

  // C.1 [Auth] Memoize the context value.
  // Why: Prevents unnecessary re-renders of all consumers when unrelated state changes.
  const value = useMemo(
    () => ({
      user,
      roles,
      mustChangePassword,
      isLoading,
      refreshMe,
      login,
      logout
    }),
    [user, roles, mustChangePassword, isLoading]
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

// C.2 [Auth] Convenience hook for consuming the AuthContext.
// Why: Encapsulates the "must be inside provider" guard so consumers fail fast with a clear error.
export function useAuth() {
  const context = useContext(AuthContext)

  if (!context) {
    throw new Error('useAuth must be used inside <AuthProvider>.')
  }

  return context
}
