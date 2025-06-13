using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using TestDataSeeder;

public static class UseSeederDataExtensions
{
    public static WebApplication UseMapSeederDataRoute(this WebApplication app)
    {
        app.MapGet("/init", async (DataSeeder seeder, IHostEnvironment env) =>
        {
            // Only allow seeding in non-production environments
            if (env.IsProduction())
            {
                return Results.Forbid();
            }

            try
            {
                if (DataSeeder.Admin is not null)
                    return Results.Ok(new
                    {
                        message = "Database already seeded successfully",
                        timestamp = DateTime.UtcNow
                    });
                await seeder.SeedAsync();
                return Results.Ok(new
                {
                    message = "Database seeded successfully",
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Error seeding database: {ex.Message}");
            }
        });
        return app;
    }
}