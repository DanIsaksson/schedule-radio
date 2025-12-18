## **Table of Contents**

Backend: .NET Web API............................................................................................................................3


Project Setup..........................................................................................................................................3

Create a new Solution/Project..........................................................................................................3
Select the "Web API" template......................................................................................................... 3
Configure the project (ref-backend, .NET 8.0).................................................................................3
API Scaffolding.....................................................................................................................................4

Clean up the default Program.cs template........................................................................................4
Add NuGet packages for Swagger (Swashbuckle.AspNetCore)......................................................5
Configure Swagger services and middleware..................................................................................5
Data Model............................................................................................................................................6

Create a models directory.................................................................................................................6
Create a RefRecord.cs class file.......................................................................................................6
Define the RefRecord class properties.............................................................................................6
Add constructors to the RefRecord class..........................................................................................7
API Endpoint.........................................................................................................................................8

Create a minimal API GET endpoint with app.MapGet()................................................................8
Return data using the RefRecord model...........................................................................................8
Test the endpoint using the Swagger UI...........................................................................................9
Frontend: React + Vite............................................................................................................................... 9

Connecting to the Backend....................................................................................................................9

Fetch data from the backend using axios in a useEffect hook..........................................................9
Store the fetched data in a React state variable..............................................................................10
Handling Cross-Origin Resource Sharing (CORS).............................................................................11

Observe the CORS error in the browser's developer console.........................................................11
Configure a proxy in vite.config.js to forward API requests..........................................................11
Update the axios request URL to use the proxy.............................................................................12
Verify the fix by observing the successful data fetch.....................................................................12
References........................................................................................................................................... 13
Backend: Server-Side CORS Configuration............................................................................................14

Adding the CORS Service...................................................................................................................14

Use builder.Services.AddCors() in Program.cs to register the necessary services.........................14
Define a named policy (e.g., "RefPolicy") to encapsulate your CORS rules.................................14
Configure the policy using methods like AllowAnyOrigin, AllowAnyMethod, and
AllowAnyHeader............................................................................................................................15
Enabling the CORS Middleware.........................................................................................................16

Add app.UseCors() to the HTTP request pipeline, passing your policy name...............................16
Understand the importance of placing app.UseCors() before app.MapGet() and other endpoint
mappings.........................................................................................................................................16
Updating the Frontend.........................................................................................................................17

Revert the axios request URL in your React component to the full, absolute backend address....17
(Optional) Remove the proxy configuration from vite.config.js as it is no longer needed............17
Backend: Publishing the .NET Web API..................................................................................................18

Introduction to Publishing...................................................................................................................18


Explain the purpose of publishing: to create a compiled, runnable version of your application for
a server............................................................................................................................................18
Deployment Modes.............................................................................................................................18

Framework-Dependent................................................................................................................... 18
Self-Contained:...............................................................................................................................19
Creating a Publish Profile....................................................................................................................19

Create a new run configuration in your IDE specifically for publishing.......................................19
Select the ".NET Publish" template................................................................................................19
Name the configuration (e.g., reference-webhotel) and confirm the project and target location...20
Configuring and Running the Publish Profile.....................................................................................20

Set the "Deployment mode" to Self-Contained..............................................................................20
Choose a "Target runtime" that matches the operating system of your deployment server (e.g.,
win-x64 for 64-bit Windows).........................................................................................................20
Run the new publish configuration to build the application...........................................................21
Examining the Published Output.........................................................................................................21

Navigate to the publish directory within your project's bin/Release folder...................................21
Identify the key output files, including your application's .dll, the executable (.exe on Windows),
and all the necessary runtime dependencies...................................................................................21
References........................................................................................................................................... 22


This lesson will guide you through creating a simple .NET Web API backend and connecting it to a
React frontend. We will address common issues like Cross-Origin Resource Sharing (CORS) by setting
up a proxy. The backend will use .NET 8's minimal API syntax, which allows for creating endpoints
with concise code. You can read more about this modern approach to building APIs here:
[https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview.](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ffundamentals%2Fminimal-apis%2Foverview)

# **Backend: .NET Web API**

## **Project Setup**

### **Create a new Solution/Project.**


A Solution in .NET is a container for one or more related projects. Creating a new solution is the first
step in organizing your application's codebase, which in this case will eventually hold both a frontend
and a backend project.


In your IDE, navigate to File > New > Project... or a similar menu option to open the project creation
dialog.


   - _Gotcha: In some IDEs like Rider or Visual Studio, you might create a "Blank Solution" first and_
_then add projects to it. For simplicity here, we'll create the project and solution at the same_

_time._

### **Select the "Web API" template**


ASP.NET Core provides templates to quickly scaffold different types of applications. The "Web API"
template creates a project with the necessary files and configuration for building RESTful HTTP
services. It's a starting point that includes example controllers and setup for features like

Swagger/OpenAPI.


From the project template list, find and select the **Web API** template. It is often located under the
"Web" or "ASP.NET Core" category.


   - _Gotcha: Be careful not to select "Web App (Model-View-Controller)" or "Blazor Web App," as_
_these are designed for serving user interfaces directly, not for creating a standalone data API._

### **Configure the project (ref-backend, .NET 8.0)**


Project configuration involves setting essential metadata like the project name and specifying the target
framework. Using a long-term support (LTS) version like .NET 8.0 is recommended for stability and
extended support from Microsoft.


Set the following options in the dialog:


   - **Project name:** ref-backend


   - **Target framework:** net8.0


   - Ensure you are using C# as the language.


Click "Create" to generate the project files.


   - _Gotcha: If .NET 8.0 is not available in the dropdown, you need to install the .NET 8 SDK. Your_
_IDE should provide a link or prompt to download it._

## **API Scaffolding**

### **Clean up the default Program.cs template**


The default template includes a "WeatherForecast" example. To start fresh, we will remove this
boilerplate code, leaving only the essential application builder and pipeline configuration. This gives us
a clean slate to build our own specific endpoints.


Replace the entire content of Program.cs with the following code:


code C#

```
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())

{

app.UseSwagger();

app.UseSwaggerUI();

}

app.UseHttpsRedirection();

```

```
app.Run();

```

   - _Gotcha: Deleting the WeatherForecast.cs file is also a good practice, but forgetting to do so_
_won't break the application as long as all references to it in Program.cs are removed._

### **Add NuGet packages for Swagger (Swashbuckle.AspNetCore)**


Swagger (OpenAPI) is a tool that generates interactive documentation for your API, allowing you to
visualize and test your endpoints directly from the browser. Swashbuckle.AspNetCore is the library
that integrates Swagger into an ASP.NET Core application.


In your IDE, right-click the project or the "Dependencies" folder and select "Manage NuGet Packages."
Search for and install the latest stable version of Swashbuckle.AspNetCore.


   - _Gotcha: The template usually includes this package by default. If you see it's already installed,_
_you can skip this step. If you get errors related to AddSwaggerGen or UseSwagger, it's a sign_
_the package is missing._

### **Configure Swagger services and middleware**


To make Swagger work, you must perform two steps. First, register the Swagger generator service in
the dependency injection container (builder.Services.AddSwaggerGen()). Second, enable the
middleware to serve the generated documentation UI (app.UseSwagger() and app.UseSwaggerUI()).


This configuration is already present in the cleaned-up code from step 1.2.1. The relevant lines are:


**code C#**

```
// 1. Add the service

builder.Services.AddSwaggerGen();

// ... builder.Build() ...

// 2. Configure the middleware

if (app.Environment.IsDevelopment())

{

  app.UseSwagger();

  app.UseSwaggerUI();

}

```

   - _Gotcha: The UseSwaggerUI() middleware serves the interactive HTML page. The_
_UseSwagger() middleware serves the raw swagger.json file that describes the API. Both are_
_required._

## **Data Model**

### **Create a models directory**


Organizing code into folders based on functionality is a standard practice that improves maintainability.
A models directory is a conventional place to store classes that define the shape of your application's
data (also known as Data Transfer Objects or DTOs).


Right-click on the ref-backend project in the Solution Explorer and select Add > New Directory. Name
the directory models.


   - _Gotcha: While C# doesn't enforce a strict folder structure for compilation, following_
_conventions makes your project easier for other developers (and your future self) to understand._

### **Create a RefRecord.cs class file**


This file will contain the C# class that represents the data structure for a single search result record.
Each file should ideally contain a single class to keep the code organized and easy to find.


Right-click the new models directory and select Add > Class. Name the new file RefRecord.cs.


   - _Gotcha: Ensure the file is created inside the models directory and that the namespace in the_
_new file reflects this, such as ref_backend.models._

### **Define the RefRecord class properties**


Properties define the data fields of a class. For our API, we need fields like Id, Title, and Creator. Using
public properties with { get; set; } creates auto-properties, which are a concise way to allow reading
and writing their values.


Add the following properties inside the RefRecord class:


**code C#**

```
public class RefRecord

{

public int Id { get; set; }

public string Title { get; set; }

```

```
public string Creator { get; set; }

public string Date { get; set; }

public string Publisher { get; set; }

}

```

   - _Gotcha: Property names in C# are case-sensitive and, by convention, use PascalCase (e.g.,_
_Title), whereas JSON objects often use camelCase (e.g., title). ASP.NET Core handles this_
_conversion automatically during serialization._

### **Add constructors to the RefRecord class**


A constructor is a special method that is called when an object of a class is created. It's used to initialize
the object's properties. A parameterless constructor is often required by libraries for serialization, while
a parameterized constructor provides a convenient way to create a fully initialized object in one line.


Add the following two constructors to your RefRecord class:


**code C#**

```
public class RefRecord

{

  // Properties from previous step...

  public int Id { get; set; }

  public string Title { get; set; }

  public string Creator { get; set; }

  public string Date { get; set; }

  public string Publisher { get; set; }

  // Parameterless constructor

  public RefRecord() { }

  // Parameterized constructor

  public RefRecord(int id, string title, string creator, string date, string
publisher)

  {

     Id = id;

     Title = title;

     Creator = creator;

```

```
     Date = date;

     Publisher = publisher;

  }

}

```

   - _Gotcha: If you define any parameterized constructor, the compiler no longer provides a default_
_parameterless one automatically. If a library (like a database mapper) needs a parameterless_
_constructor, you must add one explicitly, as shown above._

## **API Endpoint**

### **Create a minimal API GET endpoint with app.MapGet()**


Minimal APIs use routing methods like MapGet, MapPost, etc., directly on the WebApplication
instance (app) to define endpoints. This is a more direct and less ceremonial way to create APIs
compared to traditional controllers. The first argument is the URL template, and the second is a lambda
function that handles the request.


In Program.cs, add the following code just before app.Run():


code C#

```
app.MapGet("/api/references", () => {

// Handler logic will go here

});

app.Run();

```

   - _Gotcha: The route template is relative to the application's base URL. Prefixing routes with /api_
_is a common convention to distinguish API endpoints from other potential routes, like those for_
_serving web pages._

### **Return data using the RefRecord model**


An API endpoint's purpose is to return data. Here, we will create a new instance of our RefRecord class
with some sample data and return it. ASP.NET Core will automatically serialize this C# object into a
JSON string in the HTTP response.


Update your MapGet call to create and return a new RefRecord:


code C#

```
app.MapGet("/api/references", () =>

{

  return new RefRecord(1, "The Great Gatsby", "Fitzgerald, Scott", "1925",
"HarperCollins") ;

}) ;

```

   - _Gotcha: Forgetting to add using ref_backend.models; at the top of Program.cs will result in a_
_compile error because the compiler won't know where to find the RefRecord class._

### **Test the endpoint using the Swagger UI**


Swagger UI provides a web-based interface to test your API without needing a separate client like
Postman. When you run your application in the Development environment, you can navigate to the
/swagger URL to see all your defined endpoints.


Run your ref-backend project. A browser window should open to the Swagger UI. Find the GET
/api/references endpoint, expand it, click "Try it out," and then "Execute." You should see a 200 OK
response with the sample data in JSON format.


   - _Gotcha: If the browser shows a "page not found" error, check the launchSettings.json file. The_
_launchUrl property should be set to swagger to automatically open the correct page on startup._

# **Frontend: React + Vite**

## **Connecting to the Backend**

### **Fetch data from the backend using axios in a useEffect hook**


The useEffect hook in React is used to perform side effects, such as fetching data, after the component
renders. axios is a popular library for making HTTP requests. We'll use it to call our new backend
endpoint when the component first mounts.


In your frontend's App.jsx file, add a new useEffect hook to fetch data from the backend:


**code JavaScript**
```
import { useEffect } from 'react';

import axios from 'axios';

```

```
// Inside your App component

useEffect(() => {

axios.get("http://localhost:5287/api/references")

  .then(response => {

    // Logic to set state will go here

console.log(response.data);

});

}, []); // Empty dependency array means this runs once on mount

```

   - _Gotcha: The port number (e.g., 5287) must match the one your .NET backend is running on._
_Check your backend's console output or launchSettings.json to find the correct port._

### **Store the fetched data in a React state variable**


To display the data in your component, you must store it in React's state using the useState hook. When
the axios request completes successfully, you call the state's setter function to update it with the data
from the response, which triggers a re-render.


In App.jsx, add a state variable and update the useEffect hook to set it:


**code JavaScript**
```
import { useState, useEffect } from 'react';

import axios from 'axios';

// Inside your App component

const [altBackendData, setAltBackendData] = useState([]);

useEffect(() => {

axios.get("http://localhost:5287/api/references")

  .then(response => {

setAltBackendData([response.data]); // Wrap in an array to match expected

data structure

});

}, []);

```

   - _Gotcha: The initial state of useState should match the data type you expect. If you expect an_
_array of objects, initialize it with an empty array ([]) to prevent errors when trying to map over_
_it before the data has loaded._

## **Handling Cross-Origin Resource Sharing (CORS)**

### **Observe the CORS error in the browser's developer console**


CORS is a security feature enforced by browsers that blocks a frontend at one origin (e.g.,
localhost:5173) from making HTTP requests to a backend at a different origin (e.g., localhost:5287)
unless the backend explicitly allows it. This will cause a "CORS policy" error in the browser's
developer console.


Run both your frontend and backend projects. Open the browser's developer tools (F12 or Ctrl+Shift+I)
and look at the Console tab. You will see an error message similar to: "Access to XMLHttpRequest at
'...' has been blocked by CORS policy."


   - _Gotcha: This is a browser security feature, not a server-side error. The request never reaches_
_your backend code because the browser blocks it first after an initial "preflight" request fails._

### **Configure a proxy in vite.config.js to forward API requests**


A common way to solve CORS issues during development is to use a proxy. The Vite development
server can be configured to forward requests from a specific path (e.g., /api) to your backend server.
This makes the browser think it's requesting data from the same origin, thus avoiding CORS errors.


In your React project's vite.config.js file, add a server.proxy configuration:


**code JavaScript**
```
import { defineConfig } from 'vite'

import react from '@vitejs/plugin-react'

export default defineConfig({

plugins: [react()],

base: "/ref-project/",

server: {

proxy: {

    '/api': {

target: 'http://localhost:5287', // Your backend URL

changeOrigin: true,

```

```
rewrite: (path) => path.replace(/^\/api/, '/api'),

},

},

},

})

```

   - _Gotcha: The rewrite function is crucial. It ensures that when you request /api/references from_
_your frontend, the proxy forwards the request as /api/references to the target, not just_
_/references._

### **Update the axios request URL to use the proxy**


After setting up the proxy, you no longer need to use the full backend URL in your axios calls. Instead,
you use a relative path that starts with the key you defined in the proxy configuration (/api). The Vite
dev server will intercept this request and forward it to the backend.


Change the axios.get URL in App.jsx to a relative path:


code JavaScript
```
// Before

axios. get ("http://localhost:5287/api/references")

// After

axios. get ("/api/references")

```

   - _Gotcha: This proxy setup only works during development with the Vite dev server. For a_
_production build, you will need to configure a similar proxy on your production web server (like_
_Nginx or Apache) or enable CORS on your .NET backend._

### **Verify the fix by observing the successful data fetch**


With the proxy configured and the request URL updated, the CORS error should disappear. The
frontend can now successfully fetch data from the backend, and you should see the data logged to the
console and rendered in your component.


Restart your Vite development server to apply the changes in vite.config.js. Refresh your browser, and
check the developer console. The CORS error should be gone, and you should see the console.log of
your fetched data.


   - _Gotcha: If it still doesn't work, double-check that the target URL in vite.config.js is correct and_
_that your .NET backend is running and accessible at that address._

## **References**


**[Tutorial: Create a web API with ASP.NET Core](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ftutorials%2Ffirst-web-api%3Fview%3Daspnetcore-8.0%26tabs%3Dvisual-studio)**

This official Microsoft tutorial provides a comprehensive walkthrough of creating a .NET Web API
project from scratch, similar to the steps covered in this lesson. It's a great resource for reinforcing the
project setup and controller-based API concepts.


   - **[Minimal APIs overview in ASP.NET Core](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ffundamentals%2Fminimal-apis%2Foverview%3Fview%3Daspnetcore-8.0)**

This document explains the philosophy and benefits of the minimal API syntax used in this
lesson. It details how to build APIs with less boilerplate code compared to the traditional
controller-based approach.


   - **[ASP.NET Core web API documentation with Swagger (OpenAPI)](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ftutorials%2Fweb-api-help-pages-with-swagger%3Fview%3Daspnetcore-8.0)**
Learn more about how to integrate Swagger into your .NET projects. This guide covers the
configuration of Swashbuckle.AspNetCore to generate interactive API documentation.


   - **[Classes (C# programming guide)](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcsharp%2Ffundamentals%2Ftypes%2Fclasses)**
This page provides a fundamental introduction to classes in C#, which are the building blocks
of object-oriented programming. It covers concepts like members, properties, and methods,
which are essential for creating data models like RefRecord.


   - **[Constructors (C# programming guide)](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcsharp%2Fprogramming-guide%2Fclasses-and-structs%2Fconstructors)**
This document explains the role of constructors in initializing class objects. It covers default
constructors, parameterized constructors, and the syntax for defining them.


   - **[Routing in minimal APIs](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ffundamentals%2Fminimal-apis%2Frouting%3Fview%3Daspnetcore-8.0)**
Explore the details of how routing works in minimal APIs. This page covers how methods like
MapGet and MapPost are used to associate URL patterns with handler logic.


   - **[React useState Hook](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Freference%2Freact%2FuseState)**

The official React documentation for the useState hook. It explains how to add state variables to
your functional components to make them dynamic and interactive.


   - **[React useEffect Hook](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Freference%2Freact%2FuseEffect)**

This is the official API reference for the useEffect hook. It explains how to perform side effects,
such as fetching data or subscribing to events, from within your components.


   - **[Fetching data with Effects in React](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsynchronizing-with-effects%23fetching-data)**
This section of the React documentation provides a practical guide on the correct pattern for
fetching data inside a useEffect hook. It covers handling loading states, errors, and avoiding
common pitfalls.


This lesson covers two main topics. First, we will explore an alternative to the frontend proxy for
handling Cross-Origin Resource Sharing (CORS) by configuring a policy directly on the .NET
backend. Second, we will walk through the process of publishing a .NET application, which compiles
your code into a distributable format that can be run on a server. We will focus on the differences
between framework-dependent and self-contained deployment modes. You can read more about .NET
[application publishing here: https://learn.microsoft.com/en-us/dotnet/core/deploying/.](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcore%2Fdeploying%2F)

# **Backend: Server-Side CORS Configuration**

## **Adding the CORS Service**

### **Use builder.Services.AddCors() in Program.cs to register the necessary** **services**


Instead of using a frontend proxy, you can configure your backend to send the correct CORS headers,
telling browsers that it's safe to allow requests from other origins. The first step is to register the CORS
services with the application's dependency injection container.


In Program.cs, add the following line in the "Add services to the container" section:


**code C#**

```
// Add services to the container.

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(); // Add this line

```

   - _Gotcha: Simply adding the service does not enable CORS. It only makes the necessary_
_components available for configuration. You still need to define a policy and apply it to the_
_request pipeline._

### **Define a named policy (e.g., "RefPolicy") to encapsulate your CORS rules**


A CORS policy is a set of rules that defines which origins, methods, and headers are allowed. Creating
a named policy allows you to define these rules once and then apply them selectively where needed.
This is more flexible than a single, global CORS configuration.


Modify the AddCors call to configure a policy named "RefPolicy":


**code C#**

```
builder.Services.AddCors(options =>

{

```

```
  options.AddPolicy("RefPolicy", policy =>

  {

     // Policy rules will go here

  });

});

```

   - _Gotcha: The policy name is just a string and can be anything you choose. However, it must be_
_spelled identically when you later apply it with app.UseCors()._

### **Configure the policy using methods like AllowAnyOrigin,** **AllowAnyMethod, and AllowAnyHeader**


These methods provide a fluent API for building your policy. For development, it's common to create a
very permissive policy that allows requests from any origin, with any HTTP method, and any header. In
a production environment, you would restrict these to specific, known origins for security.


Complete your policy configuration to make it permissive for development:


**code C#**

```
builder.Services.AddCors(options =>

{

options.AddPolicy("RefPolicy", policy =>

{

policy.AllowAnyOrigin()

.AllowAnyMethod()

.AllowAnyHeader();

});

});

```

   - _Gotcha: AllowAnyOrigin() is convenient for development but is a security risk in production._
_For a live application, you should replace it with WithOrigins("https://your-frontend-_
_domain.com") to only allow requests from your specific frontend._


## **Enabling the CORS Middleware**

### **Add app.UseCors() to the HTTP request pipeline, passing your policy** **name**

After defining the policy, you must tell the application to use it. This is done by adding the CORS
middleware to the HTTP request pipeline. The middleware will inspect incoming requests and add the
appropriate CORS response headers based on the policy you defined.


In Program.cs, add the following line in the "Configure the HTTP request pipeline" section:


**code C#**

```
app.UseHttpsRedirection();

app.UseCors("RefPolicy"); // Add this line

app.MapGet("/api/references", () => { /* ... */ });

app.Run();

```

   - _Gotcha: Forgetting to pass the policy name to app.UseCors() will result in an error if you have_
_not configured a default policy. Always specify the name of the policy you intend to use._

### **Understand the importance of placing app.UseCors() before app.MapGet()** **and other endpoint mappings**


Middleware in ASP.NET Core is executed in the order it is added to the pipeline. The CORS
middleware must run before the endpoint-handling logic so it can apply the necessary headers before
the response is sent. If app.UseCors() is placed after app.MapGet(), the endpoint will handle the request
and terminate the pipeline before the CORS middleware gets a chance to run.


Correct Middleware Order:


**code C#**

```
// ...

app.UseCors("RefPolicy"); // Correct: Placed before endpoint mapping

app.MapGet("/api/references", () => { /* ... */ });

// ...

```

   - _Gotcha: Placing app.UseCors() after routing middleware (like app.UseRouting() in non-_
_minimal APIs) or after endpoint mappings is a common mistake that causes CORS to fail_
_silently, as the headers are simply never added._


## **Updating the Frontend**

### **Revert the axios request URL in your React component to the full,** **absolute backend address**

Since the backend now handles CORS, the frontend proxy is no longer necessary. You should update
your data-fetching logic to call the backend's full URL directly. The browser will now allow this crossorigin request because the server will respond with an Access-Control-Allow-Origin: * header.


In your App.jsx file, change the axios call back to the absolute URL:


**code JavaScript**
```
// Inside your useEffect hook

useEffect(() => {

 // The URL must include the protocol and port of your backend server

 axios. get ("http://localhost:5287/api/references")

  .then(response => {

    setAltBackendData([response.data]);

  });

}, []);

```

   - _Gotcha: Make sure the port number (5287 in this example) matches the actual port your .NET_
_backend is running on. Check the backend's console output if you are unsure._

### **(Optional) Remove the proxy configuration from vite.config.js as it is no** **longer needed**


To keep your project clean, you can now remove the proxy configuration from your Vite settings.
While leaving it in won't cause any harm (since your axios call is no longer using a relative path that
would trigger it), removing it makes your configuration reflect the current architecture.


You can remove or comment out the server block in vite.config.js:


**code JavaScript**
```
export default defineConfig({

 plugins: [react()],

 base: "/ref-project/",

 // The 'server' block can now be removed.

```

```
 /*

 server: {

  proxy: { ... },

 },

 */

})

```

   - _Gotcha: If you forget to update the axios URL to the absolute path before removing the proxy,_
_your frontend requests will fail with a 404 Not Found error, as the Vite dev server will no longer_
_know how to handle the /api path._

# **Backend: Publishing the .NET Web API**

## **Introduction to Publishing**

### **Explain the purpose of publishing: to create a compiled, runnable version** **of your application for a server**


Publishing is the process of compiling your C# source code and packaging it with all its dependencies
into a format that can be deployed and executed on a server. This is different from running in
development, which often uses just-in-time compilation and a development server. The published
output is optimized for performance and contains only the files needed to run the application.


   - _Gotcha: The published output is not meant to be human-readable. It consists of compiled_
_assemblies (.dll files) and other assets, not your original .cs source files._

## **Deployment Modes**

### **Framework-Dependent**


Creates a smaller package but requires the target server to have the .NET runtime installed.


This mode assumes that the correct version of the .NET runtime is already installed on the server where
you will deploy the application. Because the runtime is not included in the package, the output is much
smaller and faster to upload. This is a good choice when you control the server environment.


   - _Gotcha: If the server has the wrong version of the .NET runtime (or none at all), a framework-_
_dependent application will fail to start._


### **Self-Contained:**

Bundles the .NET runtime with your application, resulting in a larger package that can run on a target
server without any pre-installed .NET dependencies.


This mode creates a fully self-sufficient package by including the .NET runtime and all necessary
libraries. The resulting folder can be copied to a compatible server and run without any prior setup.
This is ideal for environments where you cannot guarantee the .NET runtime is installed, such as when
distributing to clients or using certain container-based services.


   - _Gotcha: A self-contained publish is specific to a target operating system and architecture (e.g.,_
_Windows x64, Linux ARM64). You must create a separate publish for each platform you want to_

_support._

## **Creating a Publish Profile**

### **Create a new run configuration in your IDE specifically for publishing**


Most .NET IDEs provide a way to manage different configurations for running, debugging, and
publishing your application. Creating a dedicated publish configuration allows you to save your
deployment settings so you can easily re-publish with the same options later.


In your IDE, find the run configuration manager (often near the "Run" button) and choose to add a new
configuration.


   - _Gotcha: In some IDEs like Visual Studio, this is done by right-clicking the project and selecting_
_"Publish...", which creates a .pubxml file. In Rider, it's managed through "Edit_
_Configurations..."._

### **Select the ".NET Publish" template**


Your IDE will present a list of configuration templates for different tasks. The ".NET Publish" template
is specifically designed for compiling and packaging your application for deployment.


From the list of new configuration templates, select **.NET Publish** .


   - _Gotcha: Do not confuse this with a standard ".NET Project" run configuration, which is used_
_for running the application in development mode._


### **Name the configuration (e.g., reference-webhotel) and confirm the project** **and target location**

Give your publish profile a descriptive name. The configuration will automatically select your current
project and default to a standard output location, typically a publish subfolder within your project's bin
directory.


   - **Name:** reference-webhotel


   - **Project:** ref-backend (should be pre-selected)


   - **Target location:** The default path is usually fine.


   - _Gotcha: Storing the configuration as a project file is a good practice, as it allows the settings to_

_be shared with other team members via source control._

## **Configuring and Running the Publish Profile**

### **Set the "Deployment mode" to Self-Contained**


This is the key setting that determines whether the .NET runtime will be bundled with your application.
For this lesson, we want to create a package that can run anywhere, so we will choose the selfcontained option.


In your new publish configuration settings, find the **Deployment mode** dropdown and select **Self-**

**Contained** .


   - _Gotcha: Changing this setting will significantly increase the size of your published output, from_
_a few megabytes to over 100 megabytes, because it now includes the entire .NET runtime._

### **Choose a "Target runtime" that matches the operating system of your** **deployment server (e.g., win-x64 for 64-bit Windows)**


When you create a self-contained deployment, you must specify the exact operating system and CPU
architecture of the target server. This is because the bundled runtime is platform-specific. The identifier
for this is called a Runtime Identifier (RID).


Find the **Target runtime** dropdown and select the appropriate option for your server. For a standard
64-bit Windows server, you would choose win-x64.


   - _Gotcha: If you select the wrong target runtime (e.g., you publish for linux-x64 but try to run the_
_output on a Windows server), the application will not start._


### **Run the new publish configuration to build the application**

Once your profile is configured, you can execute it. The IDE will invoke the dotnet publish command
with the options you selected. This process will restore dependencies, compile your code in "Release"
mode, and copy all the necessary files to the target location.


Save your configuration and run it using the "Run" button in your IDE, ensuring the referencewebhotel profile is selected.


   - _Gotcha: The publish process can take longer than a normal build, especially for a self-_
_contained deployment, as it has to copy many more files._

## **Examining the Published Output**

### **Navigate to the publish directory within your project's bin/Release folder**


After the publish process completes successfully, the output files will be placed in the specified target
location. By default, this is a path similar to ref-backend/bin/Release/net8.0/[Target-Runtime]/publish.


Using your file explorer, navigate to this directory to see the results.


   - _Gotcha: Be sure to look in the Release folder, not Debug. The publish process defaults to the_
_Release configuration, which includes optimizations not present in a debug build._

### **Identify the key output files, including your application's .dll, the** **executable (.exe on Windows), and all the necessary runtime** **dependencies**


Inside the publish folder, you will see many files. The most important ones are:


   - ref-backend.exe (on Windows) or ref-backend (on Linux/macOS): The main executable that
starts your application.


   - ref-backend.dll: The compiled code for your actual project.


   - Numerous other .dll files: These are the .NET runtime libraries and other dependencies required
by your application.


This entire folder is what you would copy to your web server to deploy the application.


   - _Gotcha: You must copy the entire contents of the publish folder to the server. The main_
_executable depends on all the other .dll files in that directory to function correctly._


## **References**

**[Enable Cross-Origin Requests (CORS) in ASP.NET Core](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Fsecurity%2Fcors%3Fview%3Daspnetcore-8.0)**
This is the official documentation for configuring CORS on the server. It provides in-depth details on
setting up policies with AddCors and applying them with UseCors, which is the alternative to a
frontend proxy.


**[.NET application publishing overview](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcore%2Fdeploying%2F)**
This document provides a high-level overview of the concepts behind publishing a .NET application. It
serves as a starting point for understanding how to prepare your code for a production server

environment.


**[Framework-dependent vs. self-contained deployment](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcore%2Fdeploying%2F%23publish-framework-dependent)**
Read this section to get a detailed comparison between the two main deployment modes. It explains the
trade-offs between creating a smaller package that relies on a pre-installed runtime versus a larger, allinclusive package.


**[.NET RID Catalog](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcore%2Frid-catalog)**
This page contains the official catalog of Runtime Identifiers (RIDs). You need a RID to specify the
target platform (like win-x64 or linux-x64) when creating a self-contained deployment.


**[Publish a Web API with Visual Studio](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ftutorials%2Fpublish-to-iis%3Fview%3Daspnetcore-8.0%26tabs%3Dvisual-studio)**

While the user interface may differ from your IDE, this tutorial explains the concepts behind creating
and using a publish profile. These profiles store your deployment settings, making it easy to
consistently publish your application with the same configuration.


