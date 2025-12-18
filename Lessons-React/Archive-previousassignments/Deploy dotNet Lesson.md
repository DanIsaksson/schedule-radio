## **Table of Contents**

Frontend: Proxying API Requests..............................................................................................................2

vite.config.js.......................................................................................................................................... 2
App.jsx..................................................................................................................................................3
Backend: Creating API Endpoints.............................................................................................................4

Program.cs.............................................................................................................................................4

app.MapGet:..................................................................................................................................... 4
app.MapPost:.................................................................................................................................... 5
Backend: Database with Entity Framework Core......................................................................................5

NuGet Package Installation...................................................................................................................6

Microsoft.EntityFrameworkCore.Design:........................................................................................6
Microsoft.EntityFrameworkCore.Sqlite:..........................................................................................6
Database Context (ReferenceDB.cs).....................................................................................................7

Creating the DbContext class:..........................................................................................................7
Defining a DbSet<T>:...................................................................................................................... 7
Configuration (Program.cs)...................................................................................................................8

Registering the DbContext:..............................................................................................................8
Database Migration...............................................................................................................................9

dotnet ef migrations add:.................................................................................................................. 9
References..................................................................................................................................................9


This lesson demonstrates how to connect a React frontend to a .NET backend. We will configure a
development proxy to avoid CORS issues, create API endpoints in the backend, and set up a database
using Entity Framework Core to persist data.

# **Frontend: Proxying API Requests**


In JavaScript development, a proxy server is often used to forward API requests from your frontend
application (e.g., running on localhost:5174) to a backend server (e.g., running on localhost:5287) to
bypass browser security restrictions known as CORS (Cross-Origin Resource Sharing). You can read
[more about this at https://javascript.info/fetch-crossorigin.](https://www.google.com/url?sa=E&q=https%3A%2F%2Fjavascript.info%2Ffetch-crossorigin)

## **vite.config.js**


Configuring server.proxy: This object tells the Vite development server to forward requests that match
a certain pattern to a different address.


The server.proxy option in Vite's configuration file is used during development to forward specific
network requests from the frontend's dev server to another server, such as your backend API. This is a
common technique to work around the browser's Same-Origin Policy, which would otherwise block
requests between different origins (e.g., http://localhost:5174 and http://localhost:5287), causing CORS

errors.


**code JavaScript**
```
// vite.config.js

import { defineConfig } from 'vite'

import react from '@vitejs/plugin-react'

export default defineConfig({

plugins: [react()],

server: {

proxy: {

    // Requests to /api will be forwarded to the .NET backend

    '/api': {

target: 'http://localhost:5287', // The address of your backend server

changeOrigin: true, // Recommended to avoid issues with host headers

},

},

},

})

```

   - _Gotcha: The proxy is a development-only feature. When you deploy your application to_
_production, this proxy will not exist. You must configure your production environment (e.g.,_
_using a reverse proxy like Nginx or enabling CORS on the backend) to handle API requests_
_correctly._

## **App.jsx**


Making a proxied request with axios: The frontend code makes a network request to a relative path,
which the Vite server intercepts and forwards to the backend.


When making an API call from your frontend code, you can now use a relative path that matches the
proxy key you defined in vite.config.js. The Vite development server will automatically intercept this
request and forward it to the backend target. The frontend code itself remains simple and doesn't need

to know the full backend URL.


code JavaScript
```
// App.jsx

import { useEffect, useState } from 'react';

import axios from 'axios';

function App() {

const [data, setData] = useState(null);

useEffect(() => {

  // This request to '/api/references' is caught by the proxy

axios.get('/api/references')

    .then(response => {

setData(response.data);

});

}, []); // Empty dependency array means this runs once on component mount

 return <div>{JSON.stringify(data)}</div>;

}

```

   - _Gotcha: The path in your axios request must start with the key you defined in the proxy_
_configuration. For example, if your proxy is configured for /api, a request to /references will_ _**not**_
_be proxied. The request must go to /api/references to be correctly forwarded._


# **Backend: Creating API Endpoints**

Minimal APIs in .NET provide a streamlined way to build fast HTTP APIs with minimal code. We
define endpoints by mapping HTTP verbs (like GET and POST) to specific URL paths and handler
[logic. You can learn more at https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis.](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ffundamentals%2Fminimal-apis)

## **Program.cs**

### **app.MapGet:**


This method maps an HTTP GET request to a given path and executes a lambda function to handle it,
typically to retrieve data.


The MapGet method in a minimal .NET API creates an endpoint that responds to HTTP GET requests.
It takes a URL path (a "route pattern") and a handler function that executes when a client requests that
URL. This is typically used for retrieving data from the server without making any changes.


**code C#**

```
// Program.cs

// ... (builder setup)

var app = builder.Build();

// Defines a GET endpoint at the path "/api/references"

app.MapGet("/api/references", () => {

  // This example returns a hardcoded list of records

  var records = new List<RefRecord> {

     new RefRecord(1, "The Great Gatsby", "Fitzgerald, Scott", "1925",
"HarperCollins")

  };

  return Results.Ok(records);

});

app.Run();

```

   - _Gotcha: By convention, GET requests should be "safe" and "idempotent," meaning they should_
_not change any data on the server. Using a MapGet endpoint to delete or modify a resource is_


_incorrect and can lead to unpredictable behavior, as browsers and caches may re-issue GET_
_requests automatically._

### **app.MapPost:**


This method maps an HTTP POST request, allowing the client to send data (in the request body) to be
processed or stored on the server.


The MapPost method creates an endpoint that responds to HTTP POST requests. This is used when the
client needs to send data to the server, typically to create a new resource. The data sent in the request
body is automatically mapped to the parameters of the handler function.


**code C#**

```
// Program.cs

// ... (app setup)

// Defines a POST endpoint that accepts a RefRecord in the request body

app.MapPost("/api/references", (RefRecord record) => {

  // In a real app, you would save the 'record' to a database here

  Console.WriteLine($"Received: {record.Title}");

  return Results.Ok("Record created successfully.");

});

app.Run();

// A simple record to define the shape of the data

public record RefRecord(int Id, string Title, string Creator, string Date, string
Publisher);

```

   - _Gotcha: If the JSON structure sent by the client does not exactly match the properties of the C#_
_record or class in the handler (e.g., RefRecord), model binding will fail silently, and your_
_parameter may be null or have default values. Ensure property names and data types match on_

_both the client and server._

# **Backend: Database with Entity Framework Core**


Entity Framework (EF) Core is an object-relational mapper (O/RM) that enables .NET developers to
work with a database using .NET objects, eliminating the need for most of the data-access code they


usually need to write. You can get started at
[https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app.](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fget-started%2Foverview%2Ffirst-app)

## **NuGet Package Installation**

### **Microsoft.EntityFrameworkCore.Design:**


**Contains the EF Core design-time tools for migrations.**


This package provides the command-line tools for Entity Framework Core, such as dotnet ef. These
tools are essential for performing design-time development tasks, most notably creating and managing
database migrations, which allow you to evolve your database schema over time as your models
change.


**code Bash**

```
# Run this command in the terminal in your backend project's directory

dotnet add package Microsoft.EntityFrameworkCore.Design

```

_Gotcha: The dotnet ef commands depend on your project being buildable. If there are any compilation_
_errors in your code, you will not be able to add a migration or update your database until the errors_
_are fixed._

### **Microsoft.EntityFrameworkCore.Sqlite:**


The database provider that allows EF Core to work with a SQLite database.


Entity Framework Core is designed to work with many different databases through a provider model.
This package is the specific database provider that allows EF Core to connect to and interact with a
SQLite database. SQLite is a lightweight, file-based database that is excellent for development and
simple applications.


code Bash

```
# Run this command in the terminal in your backend project's directory

dotnet add package Microsoft.EntityFrameworkCore.Sqlite

```

   - _Gotcha: You must install the correct provider package for your target database. If you_
_configure your application to use SQLite but only install the SQL Server provider_
_(Microsoft.EntityFrameworkCore.SqlServer), your application will fail at runtime with a_
_configuration error._


## **Database Context (ReferenceDB.cs)**

### **Creating the DbContext class:**

This class represents a session with the database and is used to query and save data.


A DbContext class is the heart of EF Core's functionality. It represents a session with the database,
allowing you to query and save data. You define a class that inherits from
Microsoft.EntityFrameworkCore.DbContext and includes properties for each table you want to

manage.


**code C#**

```
// In a new file, e.g., data/ReferenceDB.cs

using Microsoft.EntityFrameworkCore;

public class ReferenceDB : DbContext

{

  public ReferenceDB(DbContextOptions<ReferenceDB> options)

     : base(options) { }

  // DbSet properties will go here

}

```

   - _Gotcha: A common beginner mistake is to create an instance of the context manually (e.g., var_
_db = new ReferenceDB();). You should always get it from the dependency injection container so_
_that it is configured correctly with the right database connection and lifetime._

### **Defining a DbSet<T>:**


This property represents a collection of entities (a table) in the context that can be queried from the

database.


A DbSet<T> property on your DbContext represents a table in the database. The generic type T is your
entity class (e.g., RefRecord). You use this DbSet to perform Create, Read, Update, and Delete
(CRUD) operations on the corresponding table.


**code C#**

```
// In data/ReferenceDB.cs

using Microsoft.EntityFrameworkCore;

public class ReferenceDB : DbContext

{

  public ReferenceDB(DbContextOptions<ReferenceDB> options) : base(options) { }

```

```
  // This property maps to a "RefRecords" table in the database

  public DbSet<RefRecord> RefRecords { get ; set ; }

}

```

   - _Gotcha: By convention, EF Core will name the database table after the DbSet property name_
_(RefRecords in this case). If you want to name the table differently, you must configure it using_
_the Fluent API in an OnModelCreating method override._

## **Configuration (Program.cs)**

### **Registering the DbContext:**


The DbContext must be registered with the dependency injection container so it can be used by other
parts of the application, like API endpoints.


To make your DbContext available throughout your application via dependency injection, you must
register it in Program.cs. This is also where you configure which database provider to use (e.g.,
SQLite) and provide the connection string that tells EF Core where to find the database.


**code C#**

```
// Program.cs

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add the DbContext to the dependency injection container

var connectionString = "Data Source=references.db";

builder.Services.AddDbContext<ReferenceDB>(options =>

  options.UseSqlite(connectionString)

);

var app = builder.Build();

// ...

```

   - _Gotcha: For production applications, never hardcode a connection string directly in your code._
_It is a security risk and makes deployment difficult. Instead, store it in appsettings.json and_
_access it via builder.Configuration.GetConnectionString("DefaultConnection")._


## **Database Migration**

### **dotnet ef migrations add:**

This command scaffolds a migration based on changes to your DbContext model to prepare the

database schema.


After defining your DbContext and models, you need to create a database schema that matches them.
The dotnet ef migrations add command creates a "migration," which is a set of C# files containing the
code needed to create or update your database tables.


code Bash

```
# Run this from the terminal in the main project directory

# The name "InitialCreate" is a convention for the first migration

dotnet ef migrations add InitialCreate --project ref-backend

```

   - _Gotcha: Migrations are sequential and depend on each other. Never delete or edit an already-_
_applied migration file. If you need to make a change, create a_ _**new**_ _migration. If you want to_
_undo a change, use dotnet ef database update <PreviousMigrationName> or dotnet ef_

_migrations remove._

# **References**


**Frontend: Proxying API Requests**


   -   **Cross-origin requests:** [https://javascript.info/fetch](https://www.google.com/url?sa=E&q=https%3A%2F%2Fjavascript.info%2Ffetch-crossorigin) crossorigin
This article explains the browser's Same-Origin Policy and why Cross-Origin Resource Sharing
(CORS) is necessary, providing the context for why a development proxy is a useful tool.


**Backend: Creating API Endpoints**


   - **Minimal APIs in ASP.NET Core:**

                       -                       https://learn.microsoft.com/en [us/aspnet/core/fundamentals/minimal](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ffundamentals%2Fminimal-apis) apis
This is the official introduction to creating minimal APIs in .NET. It covers routing, request
handling, and the basic structure of a minimal API application.


**Backend: Database with Entity Framework Core**


   - **Get Started with EF Core:** [https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fget-started%2Foverview%2Ffirst-app)

[app](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fget-started%2Foverview%2Ffirst-app)
This tutorial walks through the entire process of creating your first EF Core application,
including installing packages, creating a model and context, and performing basic data access.


- **Creating and Configuring a Model:** [https://learn.microsoft.com/en](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fmodeling%2F) us/ef/core/modeling/
This documentation explains how EF Core builds a model of your data based on your entity
classes and DbContext. It covers conventions and data annotations for configuring your schema.


- **DbContext Lifetime and Configuration:** [https://learn.microsoft.com/en-us/ef/core/dbcontext-](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fdbcontext-configuration%2F)
[configuration/](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fdbcontext-configuration%2F)
This page details how to configure your DbContext, including how to register it for dependency
injection in an ASP.NET Core application using AddDbContext.


- **Migrations Overview:**

                    -                     https://learn.microsoft.com/en [us/ef/core/managing](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fmanaging-schemas%2Fmigrations%2F) schemas/migrations/
This guide provides a comprehensive overview of the migrations feature in EF Core. It explains
how to create, apply, and manage migrations to safely evolve your database schema.


