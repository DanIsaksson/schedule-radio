**Table of Contents**

[📘 Advanced Architectural Patterns in ASP.NET Core	3](#📘-advanced-architectural-patterns-in-asp.net-core)  
[1\. Metadata & Setup	3](#1.-metadata-&-setup)  
[🛠 Prerequisites	3](#🛠-prerequisites)  
[📂 Project Tree	3](#📂-project-tree)  
[📑 Table of Contents (Part 1\)	3](#📑-table-of-contents-\(part-1\))  
[2\. The Entry Point: Top-Level Statements	4](#2.-the-entry-point:-top-level-statements)  
[The Concept (Enriched)	4](#the-concept-\(enriched\))  
[The Implementation (Windsurf)	4](#the-implementation-\(windsurf\))  
[🧪 Try This	5](#🧪-try-this)  
[3\. Modern Data Structures: Global Usings & Records	5](#3.-modern-data-structures:-global-usings-&-records)  
[The Concept (Enriched)	5](#the-concept-\(enriched\)-1)  
[The Implementation (Windsurf)	5](#the-implementation-\(windsurf\)-1)  
[🧪 Try This	6](#🧪-try-this-1)  
[4\. The Minimal Surface: Routing & TypedResults	6](#4.-the-minimal-surface:-routing-&-typedresults)  
[The Concept (Enriched)	6](#the-concept-\(enriched\)-2)  
[The Syntax (Official)	7](#the-syntax-\(official\))  
[The Implementation (Windsurf)	7](#the-implementation-\(windsurf\)-2)  
[🧪 Try This	7](#🧪-try-this-2)  
[📘 Advanced Architectural Patterns in ASP.NET Core	8](#📘-advanced-architectural-patterns-in-asp.net-core-1)  
[5\. Authentication Mechanics	8](#5.-authentication-mechanics)  
[The Concept (Enriched)	8](#the-concept-\(enriched\)-3)  
[The Implementation (Windsurf)	8](#the-implementation-\(windsurf\)-3)  
[🧪 Try This	9](#🧪-try-this-3)  
[6\. The ClaimsPrincipal Abstraction	9](#6.-the-claimsprincipal-abstraction)  
[The Concept (Enriched)	9](#the-concept-\(enriched\)-4)  
[The Implementation (Windsurf)	10](#the-implementation-\(windsurf\)-4)  
[🧪 Try This	10](#🧪-try-this-4)  
[7\. Identity Architecture: Composition vs. Inheritance	10](#7.-identity-architecture:-composition-vs.-inheritance)  
[The Concept (Enriched)	10](#the-concept-\(enriched\)-5)  
[The Implementation (Windsurf)	11](#the-implementation-\(windsurf\)-5)  
[🧪 Try This	11](#🧪-try-this-5)  
[8\. Common Pitfalls: The "Captive Dependency"	12](#8.-common-pitfalls:-the-"captive-dependency")  
[The Concept (Enriched)	12](#the-concept-\(enriched\)-6)  
[The Implementation (Windsurf)	12](#the-implementation-\(windsurf\)-6)  
[🧪 Try This	12](#🧪-try-this-6)  
[📘 Advanced Architectural Patterns in ASP.NET Core	13](#📘-advanced-architectural-patterns-in-asp.net-core-2)  
[9\. Data Ownership & Multi-Tenancy	13](#9.-data-ownership-&-multi-tenancy)  
[The Concept (Enriched)	13](#the-concept-\(enriched\)-7)  
[The Implementation (Windsurf)	13](#the-implementation-\(windsurf\)-7)  
[🧪 Try This	14](#🧪-try-this-7)  
[10\. Transaction Management: The "AddAsync" Myth	14](#10.-transaction-management:-the-"addasync"-myth)  
[The Concept (Enriched)	14](#the-concept-\(enriched\)-8)  
[The Implementation (Windsurf)	15](#the-implementation-\(windsurf\)-8)  
[🧪 Try This	16](#🧪-try-this-8)  
[11\. Conclusion & Next Steps	16](#11.-conclusion-&-next-steps)  
[Summary of the Architecture	16](#summary-of-the-architecture)  
[🚀 Final Challenge	16](#🚀-final-challenge)

Here is Part 1 of the **Short Interactive Book** based on the Deep Research Report.

# **📘 Advanced Architectural Patterns in ASP.NET Core** {#📘-advanced-architectural-patterns-in-asp.net-core}

**Part 1: Foundations & The Minimal Surface**

This interactive lesson transforms the "Black Box" of ASP.NET Core into a transparent, architecturally sound structure. We will move from basic setup to mastering the Minimal API surface using C\# 10+ features.

## **1\. Metadata & Setup** {#1.-metadata-&-setup}

### **🛠 Prerequisites** {#🛠-prerequisites}

Before writing code, ensure your environment is ready.

* **.NET 8.0 SDK** (Required for C\# 12 and modern Minimal APIs).

* **Windsurf IDE** (or VS Code with C\# Dev Kit).

* **SQLite** (Used for this lesson to minimize infrastructure overhead).

### **📂 Project Tree** {#📂-project-tree}

We will build toward this structure. Create these folders in your workspace now:

code Text  
downloadcontent\_copy  
expand\_less  
    /src  
  ├── /Api  
  │    ├── Program.cs          \# Entry Point  
  │    ├── GlobalUsings.cs     \# Cross-cutting namespaces  
  │    └── /Endpoints          \# API Route Definitions  
  ├── /Domain  
  │    ├── /Entities           \# Business Objects (Customer, Order)  
  │    └── /DTOs               \# Data Transfer Objects (Records)  
  └── /Infrastructure  
       └── /Data               \# EF Core DbContext


### **📑 Table of Contents (Part 1\)** {#📑-table-of-contents-(part-1)}

1. **The Entry Point:** Top-Level Statements & The Compiler.

2. **Modern Data Structures:** Global Usings & Record Types.

3. **The API Surface:** Routing & TypedResults.

## **2\. The Entry Point: Top-Level Statements** {#2.-the-entry-point:-top-level-statements}

### **The Concept (Enriched)** {#the-concept-(enriched)}

Gone are the days of public static void Main(string\[\] args). Modern .NET uses **Top-Level Statements**.

**🧐 Deep Dive: The "Black Box" Effect**  
When you write code at the root of a file in C\# 10+, the compiler *synthetically generates* a Program class and a Main method around your code. While this reduces boilerplate, it introduces a hidden complexity: the generated class is internal by default. This makes **Integration Testing** difficult because your test project cannot see your API's entry point without a specific hack.  
*Source: [Microsoft Learn \- Top-level statements](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcsharp%2Ftutorials%2Ftop-level-statements)*

### **The Implementation (Windsurf)** {#the-implementation-(windsurf)}

Create the entry point that sets up Dependency Injection and the Middleware pipeline.

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Api/Program.cs  
using Microsoft.AspNetCore.Builder;  
using Microsoft.Extensions.Hosting;  
using Microsoft.Extensions.DependencyInjection;

var builder \= WebApplication.CreateBuilder(args);

// 1\. Register Services (Dependency Injection)  
builder.Services.AddEndpointsApiExplorer();  
builder.Services.AddSwaggerGen();

var app \= builder.Build();

// 2\. Configure Middleware Pipeline  
if (app.Environment.IsDevelopment())  
{  
    app.UseSwagger();  
    app.UseSwaggerUI();  
}

app.UseHttpsRedirection();

// 3\. Define a Test Endpoint  
app.MapGet("/", () \=\> "Architecture Initialized");

app.Run();

// CRITICAL: Expose the implicit Program class to Test Projects  
public partial class Program { }


### **🧪 Try This** {#🧪-try-this}

1. **Run the app:** Open your terminal in Windsurf (Ctrl+ ) and run dotnet run \--project src/Api\`.

2. **Break the Tests:** Comment out the line public partial class Program { }.

   * *Observation:* If you were to reference this project in a WebApplicationFactory for testing, it would fail with a "Type not found" error. The partial class makes the compiler-generated internal class accessible.

## **3\. Modern Data Structures: Global Usings & Records** {#3.-modern-data-structures:-global-usings-&-records}

### **The Concept (Enriched)** {#the-concept-(enriched)-1}

We use **Global Usings** to declutter files and **Records** for immutable Data Transfer Objects (DTOs).

**🧐 Deep Dive: Value-Based Equality**  
Traditionally, checking if two objects were equal checked their *memory address* (Referential Equality). **Records** introduce *Value-Based Equality*. Two record instances are equal if their properties are equal. This is perfect for DTOs, where we care about the data, not the object instance.  
*Source: [Microsoft Learn \- Record Types](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcsharp%2Flanguage-reference%2Fbuiltin-types%2Frecord)*

### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-1}

First, clean up your imports.

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Api/GlobalUsings.cs  
// Framework extensions available everywhere  
global using System.Security.Claims;  
global using Microsoft.EntityFrameworkCore;  
global using Microsoft.AspNetCore.Http.HttpResults;

// Project namespaces  
global using MyProject.Domain.DTOs;


Now, define a DTO using the concise record syntax.

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Domain/DTOs/CustomerDto.cs  
namespace MyProject.Domain.DTOs;

// Positional record: Constructor and Properties generated automatically  
public record CustomerResponseDto(  
    Guid Id,   
    string FullName,   
    string Email,   
    List\<string\> Tags  
);


### **🧪 Try This** {#🧪-try-this-1}

Create a temporary scratchpad file to test equality logic.

code C\#  
downloadcontent\_copy  
expand\_less  
    // scratchpad.cs  
var tags \= new List\<string\> { "VIP" };  
var dto1 \= new CustomerResponseDto(Guid.NewGuid(), "John", "j@b.com", tags);  
var dto2 \= new CustomerResponseDto(dto1.Id, "John", "j@b.com", tags);

Console.WriteLine(dto1 \== dto2); // Output: True (Value Equality)

// NOW TRY THIS:  
var list1 \= new List\<string\> { "A" };  
var list2 \= new List\<string\> { "A" };  
var r1 \= new CustomerResponseDto(Guid.NewGuid(), "A", "b", list1);  
var r2 \= new CustomerResponseDto(r1.Id, "A", "b", list2);

Console.WriteLine(r1 \== r2); // Output: False\! Why?


* *Why?* Records compare properties. list1 and list2 are different objects on the heap. Standard collections do not support structural equality. **Best Practice:** Be careful using collections inside Records if you rely on equality checks.

## **4\. The Minimal Surface: Routing & TypedResults** {#4.-the-minimal-surface:-routing-&-typedresults}

### **The Concept (Enriched)** {#the-concept-(enriched)-2}

Minimal APIs strip away Controllers. However, without explicit return types, tools like Swagger cannot generate accurate documentation. We use TypedResults to bridge this gap.

**🧐 Deep Dive: The Metadata Gap**  
If you return Results.Ok(data), the return type is the generic IResult. The compiler (and Swagger) doesn't know *what* is inside the OK. TypedResults.Ok(data) preserves the type information, allowing OpenAPI generators to see the schema of the returned object.  
*Source: [Microsoft Learn \- TypedResults vs Results](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ffundamentals%2Fminimal-apis%2Fresponses%23typedresults-vs-results)*

### **The Syntax (Official)** {#the-syntax-(official)}

code C\#  
downloadcontent\_copy  
expand\_less  
    // Signature for a self-documenting endpoint  
public static Results\<Ok\<T\>, NotFound\> HandleRequest(...)


### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-2}

Add this endpoint to Program.cs (before app.Run()).

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Api/Program.cs

// Explicit return type: Results\<Ok\<CustomerResponseDto\>, NotFound\>  
app.MapGet("/customers/{id}", Results\<Ok\<CustomerResponseDto\>, NotFound\> (Guid id) \=\>   
{  
    // Simulate a database lookup  
    var exists \= true;   
      
    if (\!exists)   
    {  
        return TypedResults.NotFound();  
    }

    var customer \= new CustomerResponseDto(id, "Jane Doe", "jane@example.com", new());  
    return TypedResults.Ok(customer);  
})  
.WithName("GetCustomerById")  
.WithOpenApi();


### **🧪 Try This** {#🧪-try-this-2}

1. **Modify the Return Type:** Change TypedResults.Ok(customer) to Results.Ok(customer).

2. **Check the Hover Text:** Hover over the lambda (Guid id) \=\> ....

   * *Observation:* With Results.Ok, the inferred return type is just IResult. With TypedResults.Ok, it is Ok\<CustomerResponseDto\>. This strong typing is what powers the automatic Swagger documentation generation.

*(Continuing to Part 2: Authentication, Security, and Identity Architecture...)*

Here is Part 2 of the **Short Interactive Book**, focusing on the critical security architecture and domain modeling strategies outlined in the Research Report.

# **📘 Advanced Architectural Patterns in ASP.NET Core** {#📘-advanced-architectural-patterns-in-asp.net-core-1}

**Part 2: Identity, Security & Domain Modeling**

In this section, we move from the API surface to the core security architecture. We will dismantle the "Magic" of ASP.NET Identity and replace the default "Monolithic User" pattern with a clean, composition-based approach.

## **5\. Authentication Mechanics** {#5.-authentication-mechanics}

### **The Concept (Enriched)** {#the-concept-(enriched)-3}

Security in Minimal APIs relies on the .RequireAuthorization() extension. It acts as a gatekeeper, inspecting endpoint metadata before the handler runs.

**🧐 Deep Dive: The "500 Error" Trap**  
A common pitfall occurs when you add .RequireAuthorization() to an endpoint but forget to register the Authentication services.  
If the dependency injection container cannot find a default Authentication Scheme (like Cookies or JWT), the framework throws an exception, resulting in a **500 Internal Server Error** instead of a **401 Unauthorized**. The system crashes because it doesn't know *how* to challenge the user.  
*Source: [GitHub Issue \#49073 \- Minimal API Auth](https://www.google.com/url?sa=E&q=https%3A%2F%2Fgithub.com%2Fdotnet%2Faspnetcore%2Fissues%2F49073)*

### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-3}

Update your Program.cs to explicitly register authentication services before the app is built.

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Api/Program.cs

// ... (Previous Service Registrations)

// 1\. Add Authentication Services  
// We use a simple cookie scheme for this lesson, but this applies to JWTs too.  
builder.Services.AddAuthentication("CookieAuth")  
    .AddCookie("CookieAuth", options \=\>  
    {  
        options.Cookie.Name \= "MySecureSession";  
        options.ExpireTimeSpan \= TimeSpan.FromMinutes(30);  
    });  
builder.Services.AddAuthorization();

var app \= builder.Build();

// ... (Swagger Middleware)

// 2\. Enable the Middleware (Order Matters\!)  
app.UseAuthentication(); // Reconstructs the User from the Cookie  
app.UseAuthorization();  // Checks if the User is allowed

// 3\. Create a Secure Endpoint Group  
var secureGroup \= app.MapGroup("/secure").RequireAuthorization();

secureGroup.MapGet("/me", (ClaimsPrincipal user) \=\>   
{  
    return TypedResults.Ok(new {   
        Name \= user.Identity?.Name,   
        IsAuthenticated \= user.Identity?.IsAuthenticated   
    });  
});

app.Run();


### **🧪 Try This** {#🧪-try-this-3}

1. **Trigger the Crash:** Comment out builder.Services.AddAuthentication(...) and builder.Services.AddAuthorization().

2. **Run the App:** Try to access /secure/me (via Swagger or Browser).

   * *Observation:* You will likely see a 500 error or a verbose exception in the console stating No authenticationScheme was specified. This proves that .RequireAuthorization is not just a flag; it triggers a complex service lookup.

## **6\. The ClaimsPrincipal Abstraction** {#6.-the-claimsprincipal-abstraction}

### **The Concept (Enriched)** {#the-concept-(enriched)-4}

The ClaimsPrincipal injected into your handler is **not** your database user. It is a "Snapshot" of the user at the moment they logged in.

**🧐 Deep Dive: The Reconstruction Flow**  
When a request arrives:

1. The **Authentication Middleware** validates the session cookie.

2. It decrypts the "Authentication Ticket."

3. It "rehydrates" a ClaimsPrincipal object and assigns it to HttpContext.User.

**Critical Disconnect:** If you delete a user from the database, their ClaimsPrincipal (Session) remains valid until the cookie expires. The ClaimsPrincipal knows nothing about the live database state.  
*Source: [Microsoft Learn \- Claims-based authorization](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Fsecurity%2Fauthorization%2Fclaims)*

### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-4}

Let's inspect the "Snapshot" vs. the "Truth".

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Api/Endpoints/UserEndpoints.cs  
// (Add this to your Program.cs or a separate file)

app.MapGet("/debug/claims", (ClaimsPrincipal user) \=\>   
{  
    // This data comes entirely from the decrypted cookie (Memory)  
    // No database call is made here.  
    var claims \= user.Claims.Select(c \=\> new { c.Type, c.Value });  
    return TypedResults.Ok(claims);  
}).RequireAuthorization();


### **🧪 Try This** {#🧪-try-this-4}

*Note: Since we haven't built the Login endpoint yet, we can't fully test this without mocking. However, understand the syntax.*

1. **The Experiment:** Imagine you have a user with Role: Admin in their cookie.

2. **The Change:** You manually change the database record to Role: User.

3. **The Result:** The /debug/claims endpoint will *still* show Role: Admin until the user logs out and logs back in.

   * *Action:* To fix this in real apps, you must use UserManager.GetUserAsync(principal) (which hits the DB) or implement a CookieValidatePrincipal event to check the DB on every request.

## **7\. Identity Architecture: Composition vs. Inheritance** {#7.-identity-architecture:-composition-vs.-inheritance}

### **The Concept (Enriched)** {#the-concept-(enriched)-5}

This is the most critical architectural decision in the report.

* **The Anti-Pattern (Inheritance):** public class ApplicationUser : IdentityUser. This couples your Domain logic (Orders, Customers) to the Microsoft.AspNetCore.Identity framework.

* **The Best Practice (Composition):** Keep IdentityUser (Auth) separate from Customer (Domain). Link them via a string ID.

**🧐 Deep Dive: Clean Architecture**  
By treating IdentityUser as an infrastructure detail, your Core Domain (Customer) remains pure. You can swap ASP.NET Identity for Auth0 or Azure AD B2C later without rewriting your business logic. The Customer entity simply stores a UserIdentityId string as a reference to *some* external authentication provider.  
*Source: [Reddit \- Composition Over Inheritance](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.reddit.com%2Fr%2FSoftwareEngineering%2Fcomments%2F1iba5pg%2Fcomposition_over_inheritance_table_structure%2F)*

### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-5}

Create a clean Domain Entity that does **not** inherit from framework classes.

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Domain/Entities/Customer.cs  
namespace MyProject.Domain.Entities;

public class Customer  
{  
    // Business ID (Internal integer PK)  
    public int Id { get; set; } 

    // Domain Data  
    public string FullName { get; set; } \= string.Empty;  
    public string Email { get; set; } \= string.Empty;  
    public DateTime MemberSince { get; set; }

    // COMPOSITION: Weak Reference to the Identity Infrastructure  
    // This string maps to the AspNetUsers table (Id column),  
    // but we do NOT create a hard foreign key constraint here  
    // if we want to keep the contexts strictly separated.  
    public string UserIdentityId { get; set; } \= string.Empty;  
}


### **🧪 Try This** {#🧪-try-this-5}

**Refactor Mental Check:**  
Look at the Customer class above.

1. Does it have a PasswordHash property? **No.**

2. Does it have AccessFailedCount? **No.**

3. **Why it matters:** If you used Inheritance (class Customer : IdentityUser), your Customer table would have roughly 15 columns related to login logic mixed with your business data. Composition keeps your tables narrow and focused.

## **8\. Common Pitfalls: The "Captive Dependency"** {#8.-common-pitfalls:-the-"captive-dependency"}

### **The Concept (Enriched)** {#the-concept-(enriched)-6}

When wiring up these services, dependency lifetime is crucial.

* **Scoped:** Created once per HTTP Request (e.g., DbContext).

* **Singleton:** Created once per Application Lifetime.

**⚠️ The Trap:** If you inject a **Scoped** service (like DbContext) into a **Singleton** service, the Singleton holds onto that DbContext forever. It never gets disposed, leading to memory leaks and data corruption.

### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-6}

Check your Program.cs for this specific pattern (Standard EF Core registration):

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Api/Program.cs

// Correct Registration  
// AddDbContext defaults to Scoped lifetime.  
builder.Services.AddDbContext\<AppDbContext\>(options \=\> ...);

// Correct Service Registration  
// Since it depends on DbContext, it MUST be Scoped.  
builder.Services.AddScoped\<ICustomerService, CustomerService\>();


### **🧪 Try This** {#🧪-try-this-6}

1. **Break the Code:** Change AddScoped\<ICustomerService...\> to AddSingleton\<ICustomerService...\> in Program.cs.

2. **Run in Development:** ASP.NET Core has a "Scope Validation" check that runs on startup in Development mode. It should throw an InvalidOperationException: *"Cannot consume scoped service 'AppDbContext' from singleton 'ICustomerService'."*

   * *Takeaway:* Never ignore this error. It is saving you from production crashes.

*(Continuing to Part 3: Advanced Data Access, Multi-Tenancy, and Global Filters...)*

Here is Part 3 of the **Short Interactive Book**, concluding with advanced data access patterns, multi-tenancy, and transaction management.

# **📘 Advanced Architectural Patterns in ASP.NET Core** {#📘-advanced-architectural-patterns-in-asp.net-core-2}

**Part 3: Data Ownership, Transactions & Conclusion**

In this final section, we tackle the hardest part of backend development: ensuring users can only see *their* data (Multi-Tenancy) and ensuring data integrity during writes. We will leverage Entity Framework Core's advanced features to enforce security at the database level.

## **9\. Data Ownership & Multi-Tenancy** {#9.-data-ownership-&-multi-tenancy}

### **The Concept (Enriched)** {#the-concept-(enriched)-7}

"Row-Level Security" means that if User A requests /orders, they get *their* orders, not User B's.  
You *could* write .Where(x \=\> x.UserId \== currentId) on every single query, but that is prone to human error. If you forget it once, you have a security breach.  
Instead, we use **Global Query Filters**.

**🧐 Deep Dive: The "Admin Blindness" Trap**  
Global Query Filters apply to *every* query executed against that DbContext.

1. **Safety:** It prevents accidental data leaks.

2. **The Trap:** If you write an Admin Dashboard to "View All Users," the filter will hide everyone else's data from the Admin too\! You must explicitly use .IgnoreQueryFilters() for administrative endpoints.

3. **Performance:** If you filter by UserIdentityId globally, but don't index that column, *every query in your system becomes a table scan*.  
   *Source: [Microsoft Learn \- Global Query Filters](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fquerying%2Ffilters)*

### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-7}

First, we need a way to inject the current user's ID into the DbContext.

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Infrastructure/Data/AppDbContext.cs  
using MyProject.Domain.Entities;

public class AppDbContext : DbContext  
{  
    private readonly IHttpContextAccessor \_httpContextAccessor;

    // Inject HttpContextAccessor to resolve the user dynamically  
    public AppDbContext(DbContextOptions\<AppDbContext\> options, IHttpContextAccessor httpContextAccessor)   
        : base(options)  
    {  
        \_httpContextAccessor \= httpContextAccessor;  
    }

    public DbSet\<Customer\> Customers \=\> Set\<Customer\>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {  
        // 1\. Resolve Current User ID  
        // Note: In production, wrap this logic in a robust "ICurrentUserService"  
        var currentUserId \= \_httpContextAccessor.HttpContext?.User?  
            .FindFirstValue(ClaimTypes.NameIdentifier);

        // 2\. Apply the Global Filter  
        // "For every query on Customer, automatically append WHERE UserIdentityId \== currentUserId"  
        if (\!string.IsNullOrEmpty(currentUserId))  
        {  
            modelBuilder.Entity\<Customer\>()  
                .HasQueryFilter(c \=\> c.UserIdentityId \== currentUserId);  
        }  
    }  
}


### **🧪 Try This** {#🧪-try-this-7}

1. **The Experiment:** Create an endpoint that returns db.Customers.ToList().

2. **The Test:**

   * Log in as User A (mock this by setting a hardcoded string in OnModelCreating if you don't have full auth setup).

   * Insert a record for User B directly into the database (using a SQLite tool or SQL script).

   * Call the endpoint.

   * *Observation:* The list will be empty (or only show User A's data), even though ToList() usually returns everything. The SQL generated (viewable in logs) will show an appended WHERE clause.

## **10\. Transaction Management: The "AddAsync" Myth** {#10.-transaction-management:-the-"addasync"-myth}

### **The Concept (Enriched)** {#the-concept-(enriched)-8}

When saving data, developers often obsess over AddAsync.

* **The Myth:** "Everything must be async for performance."

* **The Reality:** DbContext.Add() is purely an in-memory operation (tracking the entity). It performs **zero I/O** (unless using special ID generators like Hi-Lo). The actual I/O happens at SaveChangesAsync.

* **The Transaction:** SaveChangesAsync is implicitly transactional. If you save 3 entities and the 3rd fails, all 3 are rolled back.

**🧐 Deep Dive: Infrastructure Leakage in Writes**  
Do not calculate the UserIdentityId inside your Domain Entity constructor using HttpContext. Pass it in from the API layer.

* **Bad:** new Customer(httpContext) (Couples Domain to Web).

* **Good:** new Customer(userId) (Pure C\# logic).

### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-8}

Here is a secure, transactional "Create" endpoint.

code C\#  
downloadcontent\_copy  
expand\_less  
    // src/Api/Program.cs (or Endpoint file)

app.MapPost("/customers", async (  
    CustomerResponseDto dto,   
    ClaimsPrincipal user,   
    AppDbContext db) \=\>   
{  
    // 1\. Resolve Infrastructure Concern (Who is this?)  
    var userId \= user.FindFirstValue(ClaimTypes.NameIdentifier);  
      
    if (string.IsNullOrEmpty(userId)) return TypedResults.Unauthorized();

    // 2\. Instantiate Domain Entity (Pure Logic)  
    var newCustomer \= new Customer  
    {  
        FullName \= dto.FullName,  
        Email \= dto.Email,  
        UserIdentityId \= userId, // Explicit Ownership Assignment  
        MemberSince \= DateTime.UtcNow  
    };

    // 3\. Queue the change (Synchronous is fine here\!)  
    db.Customers.Add(newCustomer);

    // 4\. Commit Transaction (The only Async I/O part)  
    await db.SaveChangesAsync();

    return TypedResults.Created($"/customers/{newCustomer.Id}", newCustomer);  
})  
.RequireAuthorization();


### **🧪 Try This** {#🧪-try-this-8}

**Simulate a Rollback:**

1. Modify the code to add *two* customers.

2. After db.Customers.Add(first), throw an exception: throw new Exception("Oops");.

3. Call db.SaveChangesAsync().

4. **Check the DB:** You will see that *neither* customer was created. EF Core ensures atomicity by default.

## **11\. Conclusion & Next Steps** {#11.-conclusion-&-next-steps}

### **Summary of the Architecture** {#summary-of-the-architecture}

We have transformed the default ASP.NET Core template into a robust, enterprise-ready structure:

1. **Entry Point:** We exposed the hidden Program class for testability.

2. **API Surface:** We used TypedResults to ensure our documentation (Swagger) always matches our code.

3. **Identity:** We decoupled "Auth" from "Domain" using **Composition** (User ID string) instead of Inheritance, keeping our business logic clean.

4. **Security:** We moved from manual .Where clauses to **Global Query Filters**, ensuring safety by default.

5. **Data Access:** We clarified that Add is synchronous and SaveChanges is the transactional unit of work.

### **🚀 Final Challenge** {#🚀-final-challenge}

Take this foundation and implement the **"Update Customer"** endpoint.

* **Constraint 1:** It must use TypedResults.

* **Constraint 2:** It must verify the user owns the record (the Global Filter handles reading, but you must handle the logic if the ID provided in the URL doesn't match the retrieved record).

**Happy Coding\!**

