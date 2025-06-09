﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Identity;
using TestDataSeeder.SeedData;

namespace TestDataSeeder;

public class DataSeeder : IDataSeeder
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static ProgramOfStudyEntity ProgramOfStudyEntity { get; set; }
    public static CompetencyEntity CompetencyEntity { get; set; }
    public static IdentityUserEntity User { get; set; }
    public static IdentityUserEntity Admin { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DataSeeder> _logger;
    private readonly UserManager<IdentityUserEntity> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public DataSeeder(
        IServiceProvider serviceProvider,
        ILogger<DataSeeder> logger,
        AppDbContext context,
        UserManager<IdentityUserEntity> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        ProgramOfStudyEntity = await new ProgramOfStudy(_context).SeedAsync();
        CompetencyEntity =  await new Competency(ProgramOfStudyEntity, _context).SeedAsync();

        // Access
        await new Role(_roleManager).SeedAsync();
        Admin = await new User(_userManager).SeedAsync(Roles.Admin);
        User = await new User(_userManager).SeedAsync(Roles.User);
        await _context.SaveChangesAsync();
    }
}
