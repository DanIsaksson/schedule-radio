**Table of Contents**

[📘 Building a Secure Contributor Management System	2](#📘-building-a-secure-contributor-management-system)  
[1\. Metadata & Setup	2](#1.-metadata-&-setup)  
[🛠 Prerequisites	2](#🛠-prerequisites)  
[📂 Project Tree	2](#📂-project-tree)  
[📑 Table of Contents	2](#📑-table-of-contents)  
[2\. The Lesson Content	3](#2.-the-lesson-content)  
[\#\# Section 1: The Identity Architecture	3](###-section-1:-the-identity-architecture)  
[The Concept (Enriched)	3](#the-concept-\(enriched\))  
[The Syntax (Official)	3](#the-syntax-\(official\))  
[The Implementation (Windsurf)	3](#the-implementation-\(windsurf\))  
[🧪 Try This	5](#🧪-try-this)  
[\#\# Section 2: The Financial Engine	5](###-section-2:-the-financial-engine)  
[The Concept (Enriched)	5](#the-concept-\(enriched\)-1)  
[The Syntax (Official)	5](#the-syntax-\(official\)-1)  
[The Implementation (Windsurf)	5](#the-implementation-\(windsurf\)-1)  
[🧪 Try This	7](#🧪-try-this-1)  
[\#\# Section 3: Resource Scheduling	8](###-section-3:-resource-scheduling)  
[The Concept (Enriched)	8](#the-concept-\(enriched\)-2)  
[The Syntax (Official)	8](#the-syntax-\(official\)-2)  
[The Implementation (Windsurf)	8](#the-implementation-\(windsurf\)-2)  
[🧪 Try This	11](#🧪-try-this-2)  
[\#\# Section 4: Security & Provisioning	11](###-section-4:-security-&-provisioning)  
[The Concept (Enriched)	11](#the-concept-\(enriched\)-3)  
[The Syntax (Official)	11](#the-syntax-\(official\)-3)  
[The Implementation (Windsurf)	11](#the-implementation-\(windsurf\)-3)  
[🧪 Try This	14](#🧪-try-this-3)  
[3\. Common Pitfalls & Best Practices	14](#3.-common-pitfalls-&-best-practices)  
[⚠️ Pitfall 1: Service Lifetimes ("Captive Dependencies")	14](#⚠️-pitfall-1:-service-lifetimes-\("captive-dependencies"\))  
[⚠️ Pitfall 2: Silent Failures in Identity	14](#⚠️-pitfall-2:-silent-failures-in-identity)  
[⚠️ Pitfall 3: The "Fat Controller"	14](#⚠️-pitfall-3:-the-"fat-controller")  
[🏁 Conclusion	14](#🏁-conclusion)

Here is Part 1 of the Interactive Book.

# **📘 Building a Secure Contributor Management System** {#📘-building-a-secure-contributor-management-system}

**A Code-First Guide to ASP.NET Core Identity, Financial Precision, and EF Core Modeling.**

## **1\. Metadata & Setup** {#1.-metadata-&-setup}

### **🛠 Prerequisites** {#🛠-prerequisites}

Before beginning, ensure your environment is ready:

* **.NET 8.0 SDK** (or .NET 9.0)

* **Windsurf IDE** (or VS Code with C\# Dev Kit)

* **SQL Server Express** (or LocalDB)

* **EF Core Tools:** Run dotnet tool install \--global dotnet-ef

### **📂 Project Tree** {#📂-project-tree}

This is the target structure for the files we will create. Use this as a map.

code Text  
downloadcontent\_copy  
expand\_less  
    ContributorPlatform/  
├── Data/  
│   ├── ApplicationDbContext.cs    // Modified Identity Context  
│   └── ApplicationUser.cs         // Extended User Entity  
├── Models/  
│   ├── Project.cs                 // Resource Entity  
│   └── ProjectAssignment.cs       // Join Entity (Payload)  
├── Services/  
│   ├── IPaymentService.cs         // Business Logic Interface  
│   └── PaymentService.cs          // Financial Math Implementation  
└── Controllers/  
    └── AdminController.cs         // User Provisioning


### **📑 Table of Contents** {#📑-table-of-contents}

1. **The Identity Architecture:** Extending the User Schema.

2. **The Financial Engine:** Precision Math & Service Layers.

3. **Resource Scheduling:** Advanced Many-to-Many Relationships.

4. **Security & Provisioning:** Admin-Only User Creation.

5. **Common Pitfalls:** Debugging the "Gotchas."

## **2\. The Lesson Content** {#2.-the-lesson-content}

### **\#\# Section 1: The Identity Architecture** {###-section-1:-the-identity-architecture}

We are moving from a standard "Public Provisioning" model (users sign themselves up) to an "Admin-Only" model where we manage freelancers (Contributors) with specific business data.

#### **The Concept (Enriched)** {#the-concept-(enriched)}

Standard ASP.NET Core Identity provides a basic IdentityUser. However, real-world apps need more than just an email and password hash. We need to store **Hourly Rates**, **Addresses**, and **Biographies**.

**Deep Dive:** *Inheritance vs. Composition*  
When extending user data, we have two choices. **Composition** involves creating a separate UserProfile table linked to the user. **Inheritance** involves subclassing IdentityUser to add columns directly to the AspNetUsers table.

For this project, we use **Inheritance**. It simplifies querying (no JOINs needed to get the hourly rate during login) and integrates seamlessly with the UserManager.  
*Source: [Identity model customization](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Fsecurity%2Fauthentication%2Fcustomize-identity-model)*

#### **The Syntax (Official)** {#the-syntax-(official)}

To extend the system, we inherit from the base class and use Data Annotations to define schema constraints.

code C\#  
downloadcontent\_copy  
expand\_less  
    public class MyUser : IdentityUser { ... }


#### **The Implementation (Windsurf)** {#the-implementation-(windsurf)}

We will create the user entity and, **crucially**, update the Database Context to recognize it.

**Step 1: Define the User**

code C\#  
downloadcontent\_copy  
expand\_less  
    // Data/ApplicationUser.cs  
using Microsoft.AspNetCore.Identity;  
using System.ComponentModel.DataAnnotations;

namespace ContributorPlatform.Data  
{  
    public class ApplicationUser : IdentityUser  
    {  
        \[MaxLength(200)\]  
        public string Address { get; set; } \= string.Empty;

        public string Bio { get; set; } \= string.Empty;

        // CRITICAL: Use decimal for money. Never double.  
        // This maps to SQL decimal(18, 2\)  
        public decimal HourlyRate { get; set; }  
    }  
}


**Step 2: Update the Context (The Critical Step)**  
If you miss this step, EF Core will ignore your new properties.

code C\#  
downloadcontent\_copy  
expand\_less  
    // Data/ApplicationDbContext.cs  
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore;

namespace ContributorPlatform.Data  
{  
    // NOTICE: We inherit from IdentityDbContext\<ApplicationUser\>, NOT IdentityDbContext.  
    // This tells EF Core to swap the default user for our custom one.  
    public class ApplicationDbContext : IdentityDbContext\<ApplicationUser\>  
    {  
        public ApplicationDbContext(DbContextOptions\<ApplicationDbContext\> options)  
            : base(options)  
        {  
        }  
    }  
}


**Step 3: Register in Program.cs**

code C\#  
downloadcontent\_copy  
expand\_less  
    // Program.cs  
using ContributorPlatform.Data;  
using Microsoft.AspNetCore.Identity;

// ... builder setup

// Configure Dependency Injection to use ApplicationUser  
builder.Services.AddIdentity\<ApplicationUser, IdentityRole\>(options \=\>   
    {  
        options.SignIn.RequireConfirmedAccount \= true;  
        options.Password.RequireDigit \= true;  
    })  
    .AddEntityFrameworkStores\<ApplicationDbContext\>()  
    .AddDefaultTokenProviders();


#### **🧪 Try This** {#🧪-try-this}

In Data/ApplicationUser.cs:

1. Add a new property: public string LinkedInProfile { get; set; }.

2. Run dotnet ef migrations add AddLinkedIn in your terminal.

3. Check the generated migration file. **Observation:** Does the migration create a new column in AspNetUsers? If yes, your inheritance is working. If the migration is empty, check ApplicationDbContext.cs—did you forget the \<ApplicationUser\> generic argument?

### **\#\# Section 2: The Financial Engine** {###-section-2:-the-financial-engine}

Our platform acts as a financial arbiter. We must calculate fees based on time worked, apply a base fee, and add VAT.

#### **The Concept (Enriched)** {#the-concept-(enriched)-1}

Financial calculations require absolute precision. Standard floating-point types (double, float) use base-2 arithmetic, which cannot accurately represent simple base-10 decimals (like 0.1).

**Deep Dive:** *The Penny Drift*  
"In a payment system processing thousands of line items, micro-errors from binary floating points compound... If a Contributor is owed 750.00 SEK, paying them 749.999999 SEK is technically an underpayment that can cause audit failures."  
*Source: [Floating-point numeric types](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcsharp%2Flanguage-reference%2Fbuiltin-types%2Ffloating-point-numeric-types)*

#### **The Syntax (Official)** {#the-syntax-(official)-1}

We use the decimal type and the m suffix for literals.

code C\#  
downloadcontent\_copy  
expand\_less  
    decimal price \= 10.50m; // The 'm' is mandatory for decimals


#### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-1}

We will implement the **Service Layer Pattern** to decouple this math from our Controllers/UI.

**Step 1: The Contract**

code C\#  
downloadcontent\_copy  
expand\_less  
    // Services/IPaymentService.cs  
namespace ContributorPlatform.Services  
{  
    public interface IPaymentService  
    {  
        decimal CalculateEarnings(decimal hourlyRate, double hoursWorked);  
    }  
}


**Step 2: The Logic**

code C\#  
downloadcontent\_copy  
expand\_less  
    // Services/PaymentService.cs  
using System;

namespace ContributorPlatform.Services  
{  
    public class PaymentService : IPaymentService  
    {  
        public decimal CalculateEarnings(decimal hourlyRate, double hoursWorked)  
        {  
            // Business Rule: Base fee of 300 SEK  
            const decimal BASE\_FEE \= 300m;   
            const decimal VAT\_RATE \= 0.25m; // 25%

            // Business Rule: Any started hour counts as a full hour.  
            // CAUTION: Cast to decimal BEFORE ceiling to avoid double precision artifacts.  
            decimal billableHours \= Math.Ceiling((decimal)hoursWorked);

            decimal workCost \= billableHours \* hourlyRate;  
            decimal netTotal \= BASE\_FEE \+ workCost;

            // Calculate Gross (with VAT) and round to 2 decimals (Banker's Rounding)  
            return Math.Round(netTotal \* (1 \+ VAT\_RATE), 2);  
        }  
    }  
}


**Step 3: Register the Service**

code C\#  
downloadcontent\_copy  
expand\_less  
    // Program.cs  
// Add this before builder.Build()  
builder.Services.AddScoped\<IPaymentService, PaymentService\>();


#### **🧪 Try This** {#🧪-try-this-1}

In Services/PaymentService.cs:

1. Change const decimal VAT\_RATE \= 0.25m; to const double VAT\_RATE \= 0.25;.

2. Hover over the calculation line. **Observation:** The compiler throws an error (CS0019). C\# protects you from implicitly mixing decimal (money) and double (scientific math). You must explicitly cast, which forces you to acknowledge the potential loss of precision.

Here is Part 2 of the Interactive Book.

### **\#\# Section 3: Resource Scheduling** {###-section-3:-resource-scheduling}

We need to assign Contributors (Users) to Projects (Resources). This is a **Many-to-Many** relationship. However, a simple link isn't enough; we need to know *when* they were assigned and *what role* they play.

#### **The Concept (Enriched)** {#the-concept-(enriched)-2}

In simple scenarios, EF Core handles Many-to-Many links invisibly ("Skip Navigation"). But when the relationship itself has data (the "Payload"), we must explicitly model the **Join Entity**.

**Deep Dive:** *The Join Entity*  
A Join Entity (or Associative Entity) decomposes a Many-to-Many relationship into two One-to-Many relationships. It sits in the middle, holding Foreign Keys to both sides plus the payload data (e.g., RoleInProject).  
*Source: [Many-to-many relationships in EF Core](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fef%2Fcore%2Fmodeling%2Frelationships%2Fmany-to-many)*

#### **The Syntax (Official)** {#the-syntax-(official)-2}

We use the Fluent API in OnModelCreating to define the composite key and relationships.

code C\#  
downloadcontent\_copy  
expand\_less  
    modelBuilder.Entity\<JoinClass\>().HasKey(j \=\> new { j.Key1, j.Key2 });


#### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-2}

We will create the Project entity, the Join entity, and configure the mapping.

**Step 1: The Resource & The Join Entity**

code C\#  
downloadcontent\_copy  
expand\_less  
    // Models/Project.cs

using System.Collections.Generic;

namespace ContributorPlatform.Models

{

    public class Project

    {

        public int Id { get; set; }

        public string Title { get; set; } \= string.Empty;

        

        // Navigation to the Join Entity

        public ICollection\<ProjectAssignment\> Assignments { get; set; }

    }

}

// Models/ProjectAssignment.cs

using System;

using ContributorPlatform.Data;

namespace ContributorPlatform.Models

{

    // This is the "Join Entity" with Payload

    public class ProjectAssignment

    {

        // Foreign Key 1 (User)

        public string ContributorId { get; set; }

        public ApplicationUser Contributor { get; set; }

        // Foreign Key 2 (Project)

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        // PAYLOAD: Specific to this link

        public DateTime AssignedDate { get; set; }

        public string RoleInProject { get; set; } \= "Contributor";

    }

}


**Step 2: Update the User (Link the Join)**  
Go back to Data/ApplicationUser.cs and add this property:

code C\#  
downloadcontent\_copy  
expand\_less  
    // Data/ApplicationUser.cs

using ContributorPlatform.Models; // Add namespace

public class ApplicationUser : IdentityUser

{

    // ... previous properties ...

    // Add this navigation property:

    public ICollection\<ProjectAssignment\> Assignments { get; set; }

}


**Step 3: Configure the Relationship (The Glue)**  
We must tell EF Core how these three classes relate.

code C\#  
downloadcontent\_copy  
expand\_less  
    // Data/ApplicationDbContext.cs

using ContributorPlatform.Models;

// Inside the class...

protected override void OnModelCreating(ModelBuilder builder)

{

    base.OnModelCreating(builder); // CRITICAL: Don't remove this\!

    // Define the Composite Primary Key

    builder.Entity\<ProjectAssignment\>()

        .HasKey(pa \=\> new { pa.ProjectId, pa.ContributorId });

    // Define the Relationships

    builder.Entity\<ProjectAssignment\>()

        .HasOne(pa \=\> pa.Project)

        .WithMany(p \=\> p.Assignments)

        .HasForeignKey(pa \=\> pa.ProjectId);

    builder.Entity\<ProjectAssignment\>()

        .HasOne(pa \=\> pa.Contributor)

        .WithMany(c \=\> c.Assignments)

        .HasForeignKey(pa \=\> pa.ContributorId);

}


#### **🧪 Try This** {#🧪-try-this-2}

In Data/ApplicationDbContext.cs:

1. Comment out the line base.OnModelCreating(builder);.

2. Run the application.

3. **Observation:** You will likely crash with an error about Identity keys. base.OnModelCreating is where Identity configures its own tables (AspNetUsers, AspNetRoles). If you override it without calling base, you wipe out the Identity configuration.

### **\#\# Section 4: Security & Provisioning** {###-section-4:-security-&-provisioning}

In this platform, users cannot sign up. An Admin must create them, generate a secure password, and assign their role programmatically.

#### **The Concept (Enriched)** {#the-concept-(enriched)-3}

We are bypassing the standard Razor Pages registration UI. Instead, we use UserManager\<T\> to inject users directly into the database. This requires strict security measures for password generation.

**Deep Dive:** *Programmatic Identity & RNG*  
Never use System.Random for passwords; it is predictable. We use System.Security.Cryptography.RandomNumberGenerator to ensure high entropy. Furthermore, we must manually check IdentityResult because CreateAsync does not throw exceptions on failure—it returns a list of errors.  
*Source: [UserManager.CreateAsync Method](https://www.google.com/url?sa=E&q=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fapi%2Fmicrosoft.aspnetcore.identity.usermanager-1)*

#### **The Syntax (Official)** {#the-syntax-(official)-3}

code C\#  
downloadcontent\_copy  
expand\_less  
    var result \= await \_userManager.CreateAsync(user, password);

if (\!result.Succeeded) { /\* Handle Errors \*/ }


#### **The Implementation (Windsurf)** {#the-implementation-(windsurf)-3}

We will create a controller action that generates a user and a secure temporary password.

**Step 1: The Admin Controller**

code C\#  
downloadcontent\_copy  
expand\_less  
    // Controllers/AdminController.cs

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authorization;

using ContributorPlatform.Data;

using System.Security.Cryptography;

namespace ContributorPlatform.Controllers

{

    // Gatekeeper: Only Admins can enter

    \[Authorize(Roles \= "Admin")\] 

    public class AdminController : Controller

    {

        private readonly UserManager\<ApplicationUser\> \_userManager;

        public AdminController(UserManager\<ApplicationUser\> userManager)

        {

            \_userManager \= userManager;

        }

        // Helper: Generate High-Entropy Password

        private string GenerateSecurePassword()

        {

            // 1\. Get 12 bytes of cryptographic randomness

            byte\[\] randomBytes \= RandomNumberGenerator.GetBytes(12);

            // 2\. Convert to string

            // 3\. Append complex suffix to satisfy Identity Policy (Digit \+ Special \+ Upper)

            return Convert.ToBase64String(randomBytes) \+ "1aA\!"; 

        }

        \[HttpPost\]

        public async Task\<IActionResult\> CreateContributor(string email, decimal hourlyRate)

        {

            var user \= new ApplicationUser 

            { 

                UserName \= email, 

                Email \= email,

                HourlyRate \= hourlyRate,

                EmailConfirmed \= true // Admin trusts this email

            };

            string tempPassword \= GenerateSecurePassword();

            // ATTEMPT to create user

            var result \= await \_userManager.CreateAsync(user, tempPassword);

            if (result.Succeeded)

            {

                await \_userManager.AddToRoleAsync(user, "Contributor");

                

                // IN REALITY: Email this to user. 

                // FOR CLASS: Display it so we can copy it.

                return Content($"User Created\! Temp Password: {tempPassword}");

            }

            // If we are here, creation FAILED.

            // Example errors: "Email already taken", "Password too weak"

            return BadRequest(result.Errors);

        }

    }

}


#### **🧪 Try This** {#🧪-try-this-3}

In GenerateSecurePassword:

1. Remove the \+ "1aA\!" suffix.

2. Run the code and try to create a user.

3. **Observation:** You will likely receive a BadRequest with errors like *"Passwords must have at least one non-alphanumeric character"* or *"Passwords must have at least one digit."* The UserManager enforces strict policies by default.

## **3\. Common Pitfalls & Best Practices** {#3.-common-pitfalls-&-best-practices}

### **⚠️ Pitfall 1: Service Lifetimes ("Captive Dependencies")** {#⚠️-pitfall-1:-service-lifetimes-("captive-dependencies")}

* **The Error:** Registering a service as Singleton when it relies on the Database (Scoped).

* **The Code:** builder.Services.AddSingleton\<IPaymentService, PaymentService\>(); (Incorrect if PaymentService uses DbContext).

* **Why it fails:** The Singleton lives forever. The DbContext lives for one request. The Singleton will hold onto a "dead" DbContext after the first request finishes, causing crashes.

* **The Fix:** Always use AddScoped for services that touch data.

### **⚠️ Pitfall 2: Silent Failures in Identity** {#⚠️-pitfall-2:-silent-failures-in-identity}

* **The Error:** Calling \_userManager.CreateAsync(...) without checking .Succeeded.

* **The Consequence:** The UI might redirect to "Success", but the database remains empty because the password was too weak or the email was duplicate.

* **The Fix:** Always write if (\!result.Succeeded) blocks to log or display result.Errors.

### **⚠️ Pitfall 3: The "Fat Controller"** {#⚠️-pitfall-3:-the-"fat-controller"}

* **The Error:** Putting the VAT math inside AdminController.

* **The Consequence:** You cannot test the math without starting the web server. If you need to calculate VAT in a background job later, you have to copy-paste the code.

* **The Fix:** Keep Controllers as "Traffic Cops." They take input, call the IPaymentService, and return the View.

### **🏁 Conclusion** {#🏁-conclusion}

You have now architected the backend for a secure Contributor Management System. You have:

1. **Extended Identity** using Inheritance (ApplicationUser).

2. **Enforced Precision** using decimal and Service Layers.

3. **Modeled Complexity** using EF Core Join Entities (ProjectAssignment).

4. **Secured Provisioning** using UserManager and Cryptographic RNG.

**Next Steps:**

* Run dotnet ef migrations add InitialCreate to generate your database.

* Run dotnet ef database update to apply it.

* Start building your Views to visualize this powerful backend engine.

