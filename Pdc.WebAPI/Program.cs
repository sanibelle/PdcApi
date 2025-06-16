
// Program.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Pdc.Application;
using Pdc.Infrastructure;
using Pdc.Infrastructure.Identity;
using Pdc.Infrastructure.Identity.TestAuthentication;
using Pdc.WebAPI.Middlewares;
using Pdc.WebAPI.Services;
using System.Text.Json;
using TestDataSeeder;


public class Program
{
    public static void Main(string[] args)
    {
        Boolean isTest = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test";
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Add Application Layer
        builder.Services.AddApplication();

        // Add Infrastructure Layer
        builder.Services.AddInfrastructure(builder.Configuration);

        // Add Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

        // SECURE: Only add test authentication in Test environment
        if (isTest)
        {
            builder.Services.AddScoped<DataSeeder>();
            builder.Services.AddTestAuthentication();
        }
        else if (!string.IsNullOrEmpty(builder.Configuration["AzureAd:Instance"]))
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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            builder.Logging.AddDebug();
            builder.Logging.AddConsole();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("DefaultPolicy");
        app.UseExceptionHandling();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        if (isTest)
        {
            app.UseMapSeederDataRoute();
        }
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





// WIP en train d'ajouter des roles