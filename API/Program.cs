// A.1 [Backend.Startup] Program.cs is the backend "Main Switch".
// What: Orders services (DI), assembles the web app, configures middleware, and maps endpoint "doors".
// Why: Centralizes startup wiring for this ASP.NET Core Minimal API.
// Where: Entry point executed when you run the API project (dotnet run).
//
// Analogy reminders:
// - WebApplication.CreateBuilder(args) = "Ordering a Custom Computer" (blueprint + parts list).
// - app.MapGet/app.MapPost = "Doors" the web app (the "animal") can respond to when a request knocks.
// - CORS = "The Bouncer" deciding which browser origins may talk to the API.
//
// [Startup.Backend.1] Main Switch
// This file is the entry point for the Radio Scheduler backend.
// It wires up the web server, database, and API endpoints.
//
// -> See Lesson: Interactive-Lesson/01-Program-Startup-and-Pipeline.md
//
// Reading order:
// 1. Create Builder (The Order Form)
// 2. Register Services (The Ingredients)
// 3. Build App (The Assembly)
// 4. Configure Pipeline (The Conveyor Belt)
// 5. Map Endpoints (The Doors)

using API.Models; // ScheduleData
using API.Actions; // Booking
using API.Endpoints; // MapScheduleEndpoints, MapEventEndpoints, MapEventDbEndpoints
using Microsoft.EntityFrameworkCore; // AddDbContext, UseSqlite
using Microsoft.AspNetCore.Identity; // Identity (users/roles)
using Microsoft.AspNetCore.Mvc; // [FromBody]
using API.Data; // SchedulerContext
using API.Services;
using Scheduler.Endpoints; // MapSimpleEndpoints

var builder = WebApplication.CreateBuilder(args);

// [Startup.Backend.2] Register Services (Dependency Injection)
// This section configures the "ingredients" our app needs:
// - Database Context (SQLite)
// - CORS Policy (Security)
// - Swagger (Documentation)
// - Legacy Services (In-Memory Data)

// [Startup.Backend.3] CORS Policy ("The Bouncer")
// Allows the React frontend (Vite dev server, usually localhost:5174) to talk to this API (localhost:5219).
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(
                  "http://localhost:5173",
                  "http://127.0.0.1:5173",
                  "http://localhost:5174",
                  "http://127.0.0.1:5174")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

// [Startup.Backend.4] Database Context
// Configures Entity Framework Core to use SQLite.
// The connection string "Scheduler" is defined in appsettings.json.
builder.Services.AddDbContext<SchedulerContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Scheduler"));
});

// [Startup.Backend.4b] Identity + Authorization (login + roles)
// A.1 [Computer Parts] We add the Identity system so our API can:
// - store users/roles in SQLite (AspNetUsers, AspNetRoles, ...)
// - issue auth cookies for the React app (via Identity API endpoints)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});
builder.Services
    .AddIdentityApiEndpoints<ApplicationUser>(options =>
    {
        // B.1 We relax the default password rules so the seeded dev admin password works.
        //     (In a production system you would typically require stronger passwords.)
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SchedulerContext>();

// [Startup.Backend.5] Legacy & Utility Services
// - ScheduleData: Old in-memory storage (kept for experiments).
// - Swagger: Generates the API testing UI.
builder.Services.AddSingleton<ScheduleData>();
builder.Services.AddSingleton<PaymentCalculator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// [Startup.Backend.6] Build the App
// "Seals" the configuration and creates the runnable web application.
var app = builder.Build();

// [Startup.Backend.7] Database Initialization
// Ensures the SQLite file (scheduler.db) exists and has the correct tables.
// A.1 We now use EF Core Migrations instead of EnsureCreated() so the schema can evolve safely.
await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchedulerContext>();
    await db.Database.MigrateAsync();

    // B.1 Seed roles + default admin user (dev/testing).
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = ["Admin", "Contributor"];
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    const string adminEmail = "tobias@test.se";
    const string adminPassword = "supersafe123";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser is null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            MustChangePassword = false,
        };

        var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (!createAdminResult.Succeeded)
        {
            var errors = string.Join(", ", createAdminResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to create seeded admin user: {errors}");
        }
    }

    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    var eventsMissingOwner = await db.Events
        .Where(e => e.ResponsibleUserId == null || e.ResponsibleUserId == string.Empty)
        .ToListAsync();

    if (eventsMissingOwner.Count > 0)
    {
        foreach (var e in eventsMissingOwner)
        {
            e.ResponsibleUserId = adminUser.Id;
        }

        await db.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

// [Startup.Backend.8] Middleware Pipeline ("The Conveyor Belt")
// Every request goes through these steps in order:
// 1. Default Files (index.html)
// 2. Static Files (JS/CSS)
// 3. CORS (Security Check)
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();

// [Startup.Backend.8b] Block public self-registration
// B.1 Security rule: only Admins may create accounts. We disable the public Identity /register door.
app.Use(async (context, next) =>
{
    if (HttpMethods.IsPost(context.Request.Method)
        && context.Request.Path.Equals("/api/auth/register", StringComparison.OrdinalIgnoreCase))
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        return;
    }

    await next();
});

// [Startup.Backend.8c] Authentication + Authorization middleware
// A.1 [Animal waiting for a command] The app can now recognize WHO is calling a door.
app.UseAuthentication();
app.UseAuthorization();

// [Startup.Backend.9] Map Endpoints ("The Doors")
// Connects specific URLs (e.g., /db/schedule) to code handlers.
// We use "Extension Methods" (Map...Endpoints) to keep this file clean.

// Modern DB-backed Endpoints
app.MapScheduleDbEndpoints(); // Read schedule
app.MapEventDbEndpoints();    // Create bookings

app.MapAdminUserEndpoints();
app.MapAdminPaymentsEndpoints();
app.MapContributorEndpoints();

// Auth Endpoints (Identity API)
var authGroup = app.MapGroup("/api/auth");
authGroup.MapIdentityApi<ApplicationUser>();

// B.1 Cookies need a logout "door". Identity API endpoints don't add one for cookie auth.
authGroup.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager, [FromBody] object empty) =>
{
    if (empty is null)
    {
        return Results.Unauthorized();
    }

    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

// B.2 Verification "door": gives 401 if not logged in, 200 if logged in.
authGroup.MapGet("/me", (HttpContext httpContext) =>
{
    return Results.Ok(new
    {
        Name = httpContext.User.Identity?.Name,
    });
}).RequireAuthorization();

authGroup.MapPost("/change-password", async (
    ChangePasswordRequest request,
    HttpContext httpContext,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) =>
{
    if (string.IsNullOrWhiteSpace(request.OldPassword))
    {
        return Results.BadRequest("OldPassword is required.");
    }

    if (string.IsNullOrWhiteSpace(request.NewPassword))
    {
        return Results.BadRequest("NewPassword is required.");
    }

    var user = await userManager.GetUserAsync(httpContext.User);
    if (user is null)
    {
        return Results.Unauthorized();
    }

    var changePasswordResult = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
    if (!changePasswordResult.Succeeded)
    {
        var errors = changePasswordResult.Errors.Select(e => e.Description);
        return Results.BadRequest(new { Errors = errors });
    }

    user.MustChangePassword = false;
    var updateUserResult = await userManager.UpdateAsync(user);
    if (!updateUserResult.Succeeded)
    {
        var errors = updateUserResult.Errors.Select(e => e.Description);
        return Results.BadRequest(new { Errors = errors });
    }

    await signInManager.RefreshSignInAsync(user);
    return Results.Ok();
}).RequireAuthorization();

// Legacy In-Memory Endpoints
app.MapScheduleEndpoints();
app.MapEventEndpoints();

// Development-only Endpoints
if (app.Environment.IsDevelopment())
{
    app.MapSimpleEndpoints();
}

// [Startup.Backend.10] Legacy Demo Endpoint
// A direct example of mapping a POST request to a handler function.
// Kept here to show how MapPost works without hiding it in another file.
app.MapPost("/schedule/book", (DateTime date, int hour, int startMinute, int endMinute, bool isBooked, ScheduleData schedule) =>
{
    bool success = Booking.SendBooking(schedule, date, hour, startMinute, endMinute, isBooked);
    return success ? Results.Ok() : Results.BadRequest("Booking failed (conflict or invalid parameters)");
});

// [Startup.Backend.11] Run
// Starts the server and listens for requests.
app.Run();

public sealed record ChangePasswordRequest(
    string? OldPassword,
    string? NewPassword);