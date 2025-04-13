
// Program.cs
using Pdc.Application;
using Pdc.Infrastructure;
using Pdc.Infrastructure.Identity;
using Pdc.WebAPI.Middlewares;
using Pdc.WebAPI.Services;

// Fix for CS0119: 'UserControllerService' is a type, which is not valid in the given context
// The issue is that the `Main` method is incorrectly trying to accept `UserControllerService` as a parameter.
// The `Main` method should not have additional parameters. Instead, `UserControllerService` should be registered in the DI container.

public class Program
{
    public static void Main(string[] args)
    {
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
        builder.Services.AddAzureAdAuthentication(builder.Configuration);

        // Register UserControllerService in the DI container
        builder.Services.AddScoped<UserControllerService>();

        // Configure CORS TODO spécifier les headers.
        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("DefaultPolicy", policy =>
        //    {
        //        policy.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(',') ?? Array.Empty<string>())
        //              .AllowAnyHeader()
        //              .AllowAnyMethod();
        //    });
        //});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("DefaultPolicy");
        app.UseExceptionHandling();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
