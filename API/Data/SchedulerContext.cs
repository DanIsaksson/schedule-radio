// [Db.Schema.1] Database Context
// This class is the bridge between our code and the SQLite database.
// It defines which tables exist and handles the connection.
//
// -> See Lesson: Interactive-Lesson/02-EFCore-DbContext-Entities.md

using Microsoft.EntityFrameworkCore; // EF Core
using API.Models; // EventEntity lives here

namespace API.Data
{
    public class SchedulerContext : DbContext
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
    }
}
