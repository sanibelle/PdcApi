
// Program.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Pdc.Application;
using Pdc.Infrastructure;
using Pdc.Infrastructure.Identity;
using Pdc.WebAPI.Middlewares;
using Pdc.WebAPI.Services;
using System.Text.Json;
#if TEST
using TestDataSeeder;
using Pdc.Infrastructure.Identity.TestAuthentication;
#else
using Pdc.Infrastructure.Data;
#endif

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Add Application Layer
        builder.Services.AddApplication();

        // Add Infrastructure Lay
        builder.Services.AddInfrastructure(builder.Configuration);

        Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
        if (builder.Environment.IsProduction())
        {
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownIPNetworks.Clear();
                options.KnownProxies.Clear();
            });
        }
        else
        {
            // Add Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }


#if TEST
        builder.Services.AddScoped<DataSeeder>();
        builder.Services.AddTestAuthentication();
#endif
        if (!string.IsNullOrEmpty(builder.Configuration["AzureAd:Instance"]))
        {
            builder.Services.AddAzureAdAuthentication(builder.Configuration);
        }


        // Security
        builder.Services.AddScoped<UserControllerService>();
        builder.Services.AddSingleton<IAuthorizationHandler, AdminAuthorizationOverrideHandler>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("DefaultPolicy", policy =>
            {
                policy.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(',') ?? Array.Empty<string>())
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });
        builder.Services.AddHealthChecks();

        var app = builder.Build();

        if (builder.Environment.IsProduction()) // because of ngnix
        {
            app.UseForwardedHeaders();
        }
        else if (app.Environment.IsDevelopment()) // Configure the HTTP request pipeline.
        {
            builder.Logging.AddDebug();
            builder.Logging.AddConsole();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
        }

        app.UseCors("DefaultPolicy");
        app.UseExceptionHandling();
        app.UseAuthentication();
        app.UseAuthorization();

#if TEST //adds the /init route
        app.UseMapSeederDataRoute();
#else // Migration applied automatically on startup. Not needed for in memory db.
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
                Console.WriteLine("Database migration completed successfully.");
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
#endif
        app.MapControllers();
        app.MapHealthChecks("/api/health");
        app.MapHealthChecks("/api/ping", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    status = report.Status.ToString(),
                    timestamp = DateTime.UtcNow,
                    duration = report.TotalDuration
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        });
        app.Run();
    }
}
