# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Architecture

This is a .NET 9 Web API project using Clean Architecture with the following structure:

- **Pdc.Domain**: Core domain models and entities
- **Pdc.Application**: Use cases, DTOs, mappings (AutoMapper), and validation (FluentValidation)
- **Pdc.Infrastructure**: Data access (Entity Framework Core), repositories, and external dependencies
- **Pdc.WebAPI**: API controllers and presentation layer
- **Pdc.Tests**: Unit and integration tests using NUnit, FluentAssertions, and Moq
- **TestDataSeeder**: Test data seeding utility
- **Playwright/**: End-to-end tests using Playwright
- **WebAPp/**: Frontend application (Nuxt.js/Vue.js)

## Database Setup

To set up the database:

1. Build the solution: `dotnet build Pdc.sln`
2. Create local tool manifest (once): `dotnet new tool-manifest`
3. Install EF tools locally: `dotnet tool install dotnet-ef`
4. Create migration: `dotnet ef migrations add InitialCreate --startup-project Pdc.WebAPI --project Pdc.Infrastructure`
5. Update database: `dotnet ef database update --startup-project Pdc.WebAPI --project Pdc.Infrastructure`
## Common Commands

### Build and Test
- Build solution: `dotnet build Pdc.sln --configuration Test`
- Run unit tests: `dotnet test Pdc.Tests/Pdc.Tests.csproj --configuration Test`
- Run single test: `dotnet test Pdc.Tests/Pdc.Tests.csproj --filter "FullyQualifiedName~TestMethodName"`
- Run E2E tests: `cd Playwright && npx playwright test`

### Development
- Start API: `dotnet run --project Pdc.WebAPI`
- Start frontend: `cd WebAPp && npm run dev`

## Configuration Notes

- The project uses three build configurations: Debug, Release, and Test
- Test configuration includes the TestDataSeeder project reference
- Entity Framework migrations should be run from the Pdc.Infrastructure directory with Pdc.WebAPI as startup project
- The solution includes comprehensive CI/CD pipeline with automated testing and code coverage reporting