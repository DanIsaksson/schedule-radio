// [Db.Schema.1] Database Context
// This class is the bridge between our code and the SQLite database.
// It defines which tables exist and handles the connection.
//
// A.1 Identity + Domain in ONE database (SQLite)
// We intentionally inherit from IdentityDbContext so ASP.NET Core Identity can store:
// - Users (AspNetUsers)
// - Roles (AspNetRoles)
// - User-role links, claims, tokens, etc.
//
// B.1 This matches the project plan: contributors/admins must be able to log in,
//     AND our radio schedule events must still be stored in the same file (scheduler.db).
//
// -> See Lesson: Interactive-Lesson/02-EFCore-DbContext-Entities.md

using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // IdentityDbContext
using Microsoft.EntityFrameworkCore; // EF Core
using API.Models; // EventEntity lives here

namespace API.Data
{
    public class SchedulerContext : IdentityDbContext<ApplicationUser>
    {
        // [Db.Schema.2] Constructor
        // Receives configuration (like the connection string) from Program.cs
        // and passes it to the base DbContext class.
        public SchedulerContext(DbContextOptions<SchedulerContext> options)
            : base(options)
        {
        }

        // [Db.Schema.3] Tables (DbSet)
        // This property tells EF Core: "There is a table called 'Events' that stores 'EventEntity' objects."
        // - One row in the table = One instance of EventEntity.
        public DbSet<EventEntity> Events { get; set; } = default!;

        // B.2 [Db.Schema.Payments] Persisted monthly payment history rows.
        // Why: Read by contributors (GET /api/contributor/payments) and written by the admin payroll batch job.
        public DbSet<ContributorPaymentEntity> ContributorPayments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // B.3 Invariant: one payment row per contributor per month (prevents duplicate ledger entries).
            modelBuilder.Entity<ContributorPaymentEntity>()
                .HasIndex(p => new { p.PeriodYear, p.PeriodMonth, p.UserId })
                .IsUnique();
        }
    }
}
