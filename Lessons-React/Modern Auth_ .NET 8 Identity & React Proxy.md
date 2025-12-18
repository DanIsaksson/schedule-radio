Here is Part 1 of the **Windsurf Interactive Book** for .NET 8 Identity and React.

---

# **📘 Modern Auth: .NET 8 Identity & React Proxy**

**Version:** 1.0 | **Stack:** ASP.NET Core 8, React (Vite), Entity Framework Core

## **1\. Metadata & Setup**

### **Prerequisites**

Before writing code, ensure your Windsurf environment has the following verified:

* **.NET 8 SDK** (Verify with dotnet \--version)  
* **Node.js 18+** (Verify with node \-v)  
* **SQL Server Express** (or a connection string to a running instance)

### **Project Tree**

We will be building a "BFF" (Backend for Frontend) style architecture. Your file explorer should eventually look like this:

code Text  
downloadcontent\_copy  
expand\_less  
   /project-root  
├── /server (ASP.NET Core Web API)  
│   ├── Program.cs             \<-- The Brain (Auth Logic)  
│   ├── Data/  
│   │   └── AppDbContext.cs    \<-- The Store (EF Core)  
│   └── appsettings.json  
├── /client (React \+ Vite)  
│   ├── vite.config.ts         \<-- The Bridge (Proxy)  
│   └── src/  
│       └── utils/  
│           └── axios.ts       \<-- The Messenger (Credentials)  
 

### **Table of Contents**

1. **The Backend Core:** Implementing MapIdentityApi.  
2. **The Bridge:** Configuring the Vite Proxy.  
3. **The Messenger:** Client-side Credential Handling.  
4. **The Pipeline:** CORS, Middleware, and Authorization.  
5. **The Missing Piece:** Implementing Logout.

---

## **2\. The Backend Core: MapIdentityApi**

### **The Concept**

In .NET 8, Microsoft introduced a standardized, "opinionated" set of endpoints for handling authentication, removing the need for complex libraries like IdentityServer for simple SPAs. We use MapIdentityApi\<TUser\>, which automatically generates routes for /register, /login, and /refresh.

**🧐 Deep Dive: The Shift to Minimal APIs**  
Historically, .NET auth required heavy MVC controllers or Razor Pages. The new MapIdentityApi is built on **Minimal APIs**, designed to be lightweight and fast.

**Crucially**, it supports two modes:

1. **Bearer Tokens:** For mobile apps (stateless).  
2. **Cookies:** For SPAs (stateful). We will use **Cookies** (?useCookies=true) because they are HttpOnly and secure against XSS attacks, unlike LocalStorage tokens.  
   *Source: [Microsoft Learn \- Identity API endpoints](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0)*

### **The Syntax**

The official signature requires an EF Core context capable of storing Identity data:

code C\#  
downloadcontent\_copy  
expand\_less  
   // Official Signature  
app.MapIdentityApi\<IdentityUser\>();  
 

### **The Implementation (Windsurf)**

We need to configure Entity Framework and map the endpoints.

**File:** // server/Program.cs

code C\#  
downloadcontent\_copy  
expand\_less  
   using Microsoft.AspNetCore.Identity;  
using Microsoft.EntityFrameworkCore;

var builder \= WebApplication.CreateBuilder(args);

// 1\. Setup the Database Context (EF Core)  
// We use SQL Server, but you can swap this for InMemory for quick testing.  
builder.Services.AddDbContext\<AppDbContext\>(options \=\>  
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2\. Add Identity Services  
// 'AddIdentityApiEndpoints' is the new .NET 8 helper that removes MVC bloat.  
builder.Services.AddIdentityApiEndpoints\<IdentityUser\>()  
    .AddEntityFrameworkStores\<AppDbContext\>();

builder.Services.AddEndpointsApiExplorer();  
builder.Services.AddSwaggerGen();

var app \= builder.Build();

// 3\. The Pipeline  
if (app.Environment.IsDevelopment())  
{  
    app.UseSwagger();  
    app.UseSwaggerUI();  
}

app.UseHttpsRedirection();

// 4\. Map the Identity Endpoints  
// This generates /register, /login, etc.  
app.MapGroup("/api/auth")  
   .MapIdentityApi\<IdentityUser\>();

app.Run();  
 

**File:** // server/Data/AppDbContext.cs

code C\#  
downloadcontent\_copy  
expand\_less  
   using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore;

// Must inherit from IdentityDbContext, not just DbContext  
public class AppDbContext : IdentityDbContext  
{  
    public AppDbContext(DbContextOptions\<AppDbContext\> options) : base(options) { }  
}  
 

### **🧪 Try This: API Exploration**

1. Run the server (dotnet run).  
2. Open the Swagger UI (http://localhost:5000/swagger).  
3. Expand the /api/auth/login endpoint.  
4. **Experiment:** Notice the useCookies boolean parameter.  
   * **Action:** Try executing a login with useCookies: false (default). You will get a JSON accessToken.  
   * **Action:** Try with useCookies: true. You will get a **200 OK** with an empty body, but a Set-Cookie header will be present in the response.

---

## **3\. The Bridge: Vite Proxy**

### **The Concept**

React runs on localhost:5173. Your API runs on localhost:5000. This is a **Cross-Origin** scenario. Browsers block cookies between different ports by default. To fix this during development, we use a **Proxy**. The proxy tricks the browser into thinking the API and the React app are on the same server.

**🧐 Deep Dive: The "Split-Brain" Problem**  
When you deploy, your React app files will likely be served *by* the .NET app (Same Origin). But in dev, they are separate.  
The Vite Proxy mimics the production environment.

* **changeOrigin: true**: Rewrites the "Host" header so Kestrel thinks the request is local.  
* **secure: false**: Critical for .NET because local dev uses self-signed SSL certificates, which Node.js (Vite) rejects by default.  
  *Source: [Vite Docs \- Server Proxy](https://vitejs.dev/config/server-options.html#server-proxy)*

### **The Syntax**

code TypeScript  
downloadcontent\_copy  
expand\_less  
   // vite.config.ts signature  
server: {  
  proxy: {  
    '/api': { target: '...', changeOrigin: true }  
  }  
}  
 

### **The Implementation (Windsurf)**

Configure your Vite settings to tunnel requests.

**File:** // client/vite.config.ts

code TypeScript  
downloadcontent\_copy  
expand\_less  
   import { defineConfig } from 'vite';  
import react from '@vitejs/plugin-react';

export default defineConfig({  
  plugins: \[react()\],  
  server: {  
    proxy: {  
      // Intercept any request starting with /api  
      '/api': {  
        target: 'https://localhost:7158', // CHANGE THIS to your actual .NET HTTPS port  
        changeOrigin: true, // Required: tricks .NET into accepting the host header  
        secure: false,      // Required: allows self-signed dev certs  
        // Optional: Rewrite path if your backend doesn't use /api prefix  
        // rewrite: (path) \=\> path.replace(/^\\/api/, '')   
      }  
    }  
  }  
});  
 

### **🧪 Try This: Breaking the Bridge**

1. Start both the .NET server and the React dev server.  
2. Make a fetch request from React to /api/auth/login.  
3. **Experiment:** In vite.config.ts, set secure: true.  
   * **Result:** The request will likely fail with a 500 or "Depth Zero Self-Signed Certificate" error in your terminal, because Vite refuses to trust the .NET dev certificate.

---

## **4\. The Messenger: Client-Side Axios**

### **The Concept**

Even with the proxy, browsers are conservative. They will **not** send cookies (credentials) with fetch requests unless you explicitly ask them to. If you forget this, you will log in successfully (receive the cookie), but your very next request will be 401 Unauthorized because the browser didn't send the cookie back.

**🧐 Deep Dive: withCredentials**  
The Set-Cookie header from .NET is useless if the client doesn't respect it.

* **Fetch API:** Requires credentials: 'include'.  
* **Axios:** Requires withCredentials: true.  
  This flag tells the browser: "It's okay to send sensitive cookies to this domain."  
  *Source: [MDN Web Docs \- CORS Credentials](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Access-Control-Allow-Credentials)*

### **The Implementation (Windsurf)**

Create a singleton Axios instance. Do not use raw axios calls in your components.

**File:** // client/src/utils/axios.ts

code TypeScript  
downloadcontent\_copy  
expand\_less  
   import axios from 'axios';

const api \= axios.create({  
  // We use a relative path. The Vite Proxy will handle the domain.  
  baseURL: '/api',   
    
  // CRITICAL: This ensures the .AspNetCore.Identity.Application cookie   
  // is sent with every request.  
  withCredentials: true,  
    
  headers: {  
    'Content-Type': 'application/json',  
  },  
});

export default api;  
 

**File:** // client/src/components/LoginForm.tsx (Snippet)

code Tsx  
downloadcontent\_copy  
expand\_less  
   import api from '../utils/axios';

const handleLogin \= async (email, password) \=\> {  
  // Notice we append ?useCookies=true  
  await api.post('/auth/login?useCookies=true', {  
    email,  
    password  
  });  
  // If successful, the browser now holds the HTTP-Only cookie.  
};  
 

### **🧪 Try This: The "Ghost" Cookie**

1. In client/src/utils/axios.ts, comment out withCredentials: true.  
2. Perform a login. It will likely succeed (200 OK).  
3. Immediately try to fetch a protected resource (e.g., /auth/manage/info).  
4. **Result:** You will get a 401 Unauthorized.  
5. **Why?** Check the Network tab. The Login response had a Set-Cookie, but the subsequent request header **did not** have a Cookie field.

---

*(Continuing to Part 2: Middleware Pipeline, CORS, and Authorization Policies...)*

Here is Part 2 of the **Windsurf Interactive Book** for .NET 8 Identity and React.

---

## **5\. The Pipeline: CORS & Middleware Order**

### **The Concept**

In ASP.NET Core, the "Pipeline" is a series of checkpoints every request must pass through. The **order** of these checkpoints is not a suggestion; it is a strict requirement. A common disaster in decoupled auth is placing the "ID Check" (Authentication) *after* the "Security Guard" (Authorization), or placing CORS logic too late to handle the browser's pre-checks.

**🧐 Deep Dive: The "Wildcard Paradox"**  
You might be tempted to use .AllowAnyOrigin() to fix connection errors. **Don't.**  
The W3C Spec forbids using a wildcard \* if you are also using AllowCredentials() (cookies).  
If you try AllowAnyOrigin() \+ AllowCredentials(), the browser will drop the response to protect the user from CSRF attacks. You **must** specify exact origins (e.g., https://localhost:5173).  
*Source: [MDN Web Docs \- CORS Errors](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS/Errors/CORSNotSupportingCredentials)*

### **The Syntax (Official)**

The middleware order must strictly follow this sequence for SPAs:

1. SSL/HTTPS  
2. Routing  
3. **CORS** (Must be before Auth to handle pre-flight OPTIONS requests)  
4. **Authentication** (Who are you?)  
5. **Authorization** (Are you allowed here?)  
6. Endpoints

### **The Implementation (Windsurf)**

Update your Program.cs to enforce strict CORS and correct ordering.

**File:** // server/Program.cs (Update)

code C\#  
downloadcontent\_copy  
expand\_less  
   var builder \= WebApplication.CreateBuilder(args);

// ... (Db and Identity services setup from Section 2\) ...

// 1\. Define the CORS Policy  
// We explicitly list the React Dev Server URL.  
builder.Services.AddCors(options \=\>  
{  
    options.AddPolicy("ReactApp", policy \=\>  
    {  
        policy.WithOrigins("https://localhost:5173") // NO trailing slash\!  
              .AllowAnyHeader()  
              .AllowAnyMethod()  
              .AllowCredentials(); // CRITICAL for Cookies  
    });  
});

var app \= builder.Build();

app.UseHttpsRedirection();

// 2\. The Middleware "Sandwich"  
app.UseRouting();

// CORS must be AFTER Routing but BEFORE Auth  
app.UseCors("ReactApp");

// Authentication decodes the Cookie \-\> User Principal  
app.UseAuthentication();

// Authorization checks User Principal \-\> Policy  
app.UseAuthorization();

// 3\. Map Endpoints  
app.MapGroup("/api/auth").MapIdentityApi\<IdentityUser\>();

// A protected test endpoint  
app.MapGet("/api/protected", () \=\> "You are seeing secret data\!")  
   .RequireAuthorization(); // Requires a valid cookie

app.Run();  
 

### **🧪 Try This: The Preflight Crash**

1. In Program.cs, move app.UseCors("ReactApp") **after** app.UseAuthorization().  
2. Restart the server.  
3. Trigger a login or data fetch from React.  
4. **Result:** The browser console will show a CORS error.  
5. **Why?** The browser sends an OPTIONS request first to check permissions. This request has *no credentials*. If it hits UseAuthorization first, the server asks "Who are you?", sees no cookie, and rejects it with 401\. The CORS middleware never gets a chance to say "It's okay, come in."

---

## **6\. The Missing Piece: Logout**

### **The Concept**

The .NET 8 MapIdentityApi is brilliant, but it has a gaping hole: **It has no /logout endpoint.**  
Since we are using **Cookies**, we cannot just "delete the token" on the client side. The browser will keep sending the cookie until it expires. The server *must* instruct the browser to delete it.

**🧐 Deep Dive: Cookie Invalidation**  
To log out securely, the server must send a Set-Cookie header overwriting the existing cookie with an expiration date in the past. This is handled by SignInManager.SignOutAsync(). Without this, a user is never truly logged out until the session times out.  
*Source: [GitHub Issue \#52834 \- Add logout to Identity API](https://github.com/dotnet/aspnetcore/issues/52834)*

### **The Implementation (Windsurf)**

We must manually map a logout endpoint that invokes the SignInManager.

**File:** // server/Program.cs (Append before app.Run())

code C\#  
downloadcontent\_copy  
expand\_less  
   using Microsoft.AspNetCore.Identity;  
using Microsoft.AspNetCore.Mvc;

// ... existing mappings ...

app.MapPost("/api/auth/logout", async (SignInManager\<IdentityUser\> signInManager) \=\>  
{  
    // This invalidates the cookie and clears the security stamp  
    await signInManager.SignOutAsync();  
    return Results.Ok();  
})  
.RequireAuthorization(); // Only logged-in users can log out  
 

**File:** // client/src/components/LogoutButton.tsx

code Tsx  
downloadcontent\_copy  
expand\_less  
   import api from '../utils/axios';

export const LogoutButton \= () \=\> {  
  const handleLogout \= async () \=\> {  
    try {  
      // POST request triggers the server to clear the cookie  
      await api.post('/auth/logout');  
      window.location.href \= '/login';  
    } catch (error) {  
      console.error("Logout failed", error);  
    }  
  };

  return \<button onClick={handleLogout}\>Log Out\</button\>;  
};  
   
---

## **7\. Common Pitfalls & Best Practices**

### **1\. The "Halfway" Sliding Expiration**

You might set your cookie expiration to 30 minutes and expect it to refresh every time the user clicks a button. **It won't.**

* **The Trap:** ASP.NET Core optimization only refreshes the cookie if **more than 50%** of the window has passed.  
* **The Symptom:** You click around for 10 minutes, go to lunch (25 mins), and come back to find you are logged out, even though you were "active" recently.  
* **The Fix:** Be aware of this when testing. Don't panic if the Set-Cookie header doesn't appear on every response.

### **2\. The "Login Lockout" (Global Authorization)**

If you try to be "Secure by Default" using a global FallbackPolicy:

code C\#  
downloadcontent\_copy  
expand\_less  
   // DANGEROUS CONFIGURATION  
builder.Services.AddAuthorization(options \=\> {  
    options.FallbackPolicy \= new AuthorizationPolicyBuilder()  
        .RequireAuthenticatedUser()  
        .Build();  
});  
 

* **The Trap:** This policy applies to *every* endpoint, including /login and /register.  
* **The Symptom:** You get 401 Unauthorized when trying to Log In.

**The Fix:** If you use a global policy, you must explicitly exclude the Identity group:  
 code C\#  
downloadcontent\_copy  
expand\_less  
    app.MapGroup("/api/auth")  
   .MapIdentityApi\<IdentityUser\>()  
   .AllowAnonymous(); // Override the global lockout

*  

### **3\. Cookie Domains on Localhost**

If your backend code sets a specific domain for the cookie (e.g., .mycompany.com), it will **fail on localhost**.

* **The Fix:** Ensure your appsettings.Development.json or cookie configuration does not enforce a domain when running locally. Browsers will drop cookies if the domain doesn't match the URL bar exactly (or isn't a parent).

---

## **8\. Final Checklist (Definition of Done)**

Before you ship this feature, verify the following:

**Proxy:** vite.config.ts has changeOrigin: true and secure: false.  
**Client:** Axios is configured with withCredentials: true.  
**CORS:** Policy allows specific origin (no wildcards) \+ AllowCredentials().  
**Middleware:** Order is Routing \-\> CORS \-\> AuthN \-\> AuthZ.  
**Logout:** Custom /logout endpoint is implemented and functional.

### **🧪 Final Experiment: The Full Flow**

1. **Register** a new user via the React App.  
2. **Login** (ensure useCookies=true).  
3. **Inspect** the Application/Storage tab in DevTools. Verify the .AspNetCore.Identity.Application cookie exists and is HttpOnly.  
4. **Refresh** the page. The user should remain logged in (cookie persists).  
5. **Click Logout**. Verify the cookie is removed or expired.  
6. **Try to access** /api/protected directly in the browser address bar. You should see the data (because the browser sends the cookie automatically).  
7. **Open an Incognito window** and access /api/protected. You should get a 401 (no cookie).

---

**End of Lesson.**  
You now have a production-ready authentication architecture bridging .NET 8 and React.

