// --- FILE: Data/SchedulerContext.cs
//
// PURPOSE
// - This class represents our connection to the database.
// - EF Core uses it to know which tables (DbSet<T>) exist and how to talk to the DB.
//
// HOW IT CONNECTS
// - In Program.cs we call AddDbContext<SchedulerContext>(...) and pass UseSqlite(...)
// - The actual connection string is named "Scheduler" in appsettings.json
//
// WHAT TO ADD LATER
// - Add more DbSet<T> properties for new tables (e.g., Hosts, Guests) as your app grows.
// - Optionally override OnModelCreating to add constraints/indexes when you are ready.

using Microsoft.EntityFrameworkCore; // EF Core
using API.Models; // EventEntity lives here

namespace API.Data
{
    public class SchedulerContext : DbContext
    {
        // WHAT THIS CONSTRUCTOR DOES
        // - EF Core (through Dependency Injection) constructs this class and passes configuration
        //   in 'options' (like provider = SQLite, and the connection string).
        // - 'base(options)' hands those settings to the DbContext base class so it can initialize.
        public SchedulerContext(DbContextOptions<SchedulerContext> options)
            : base(options)
        {
        }

        // KEY TYPES YOU'LL SEE (CHEAT SHEET)
        // - DbContext: EF Core's base class that tracks entities and talks to the database.
        // - DbSet<T>: Represents a table for entity type T. Add/Remove/Query against it.
        // - DbContextOptions<T>: Carries provider settings (like SQLite) into this context.

        // HOW DI INJECTS THIS IN ENDPOINTS
        // - We register the context in Program.cs with 'AddDbContext<SchedulerContext>(...)'.
        // - In Minimal APIs, you can ask for 'SchedulerContext db' as a parameter and DI provides it.

        // TABLES
        // - 'Events' will map (by convention) to a table named 'Events'.
        // - Primary key is inferred from 'EventEntity.Id'.
        // - Column names map from property names unless configured otherwise.
        // "Events" table: one row per booking (date+hour+minute range)
        public DbSet<EventEntity> Events { get; set; } = default!;

        // MODEL CONFIGURATION (optional for now)
        // Use this when you need custom keys, indexes, or relationships.
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // EXAMPLE: Add an index to speed up finding bookings by date and hour.
        //     // Why an index? Queries like WHERE Date=... AND Hour=... become faster on large tables.
        //     // modelBuilder.Entity<EventEntity>()
        //     //     .HasIndex(e => new { e.Date, e.Hour });
        //
        //     // EXAMPLE 2 (optional later): enforce unique time ranges per hour
        //     // (you can only do one range per Id, but overlapping checks are in code for now).
        //     // Data annotations or fluent API could enforce more constraints if needed.
        // }
    }
}
