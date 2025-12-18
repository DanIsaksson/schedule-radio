using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace API.Data;

// A.1 Design-time DbContext factory (EF Core tooling)
// EF Core CLI commands ("dotnet ef ...") sometimes try to boot the full web app to find our DbContext.
// In our project, Program.cs runs migrations + seeds users at startup, which would break the *first*
// migration creation (because no migrations exist yet).
//
// This factory is a safe "back door" for the EF tools:
// - It creates SchedulerContext directly
// - Without running the full HTTP pipeline
public class SchedulerContextFactory : IDesignTimeDbContextFactory<SchedulerContext>
{
    public SchedulerContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("Scheduler")
                               ?? "Data Source=scheduler.db";

        var optionsBuilder = new DbContextOptionsBuilder<SchedulerContext>();
        optionsBuilder.UseSqlite(connectionString);

        return new SchedulerContext(optionsBuilder.Options);
    }
}
