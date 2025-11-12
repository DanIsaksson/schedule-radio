// --- FILE: Data/SchedulerContext.cs
// What I use this for (beginner view)
// - EF Core DbContext: my bridge to SQLite.
// - I list my tables with DbSet<T> so EF knows what to create/query.
// Where it plugs in
// - Program.cs calls AddDbContext<SchedulerContext>(UseSqlite(...)). Connection string name: "Scheduler".
// Add later
// - New tables? Add more DbSet<T>. Custom keys/indexes? Override OnModelCreating().
using Microsoft.EntityFrameworkCore; // EF Core
using API.Models; // EventEntity lives here

namespace API.Data
{
    public class SchedulerContext : DbContext
    {
        // DI passes provider settings here (UseSqlite, connection string). 'base(options)' wires DbContext.
        public SchedulerContext(DbContextOptions<SchedulerContext> options)
            : base(options)
        {
        }

        // Cheat sheet: DbContext (tracks entities), DbSet<T> (table), DbContextOptions<T> (provider config).

        // Endpoints: ask for 'SchedulerContext db' and DI gives me a per-request context.

        // Tables
        // - Events => table for bookings. Primary key is EventEntity.Id (by convention). See Models/EventEntity.cs.
        public DbSet<EventEntity> Events { get; set; } = default!;

        // Model configuration (later)
        // - Override OnModelCreating for indexes/constraints when needed.
    }
}
