using FluentAssertions;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.CourseFramework;
using System.Net.Http.Json;
using TestDataSeeder;
using TestDataSeeder.Builders.DTOS;

namespace Pdc.E2ETests;

[TestFixture]
public class CourseFramework : ApiTestBase
{
    [Test]
    public async Task GivenSeededPrograms_WhenGetCourseFrameworks_ThenShouldReturnCourseFrameworks()
    {
        // Act
        var response = await _Client.GetAsync($"/api/programofstudy/{DataSeeder.ProgramOfStudyEntity.Code}/courseFramework");

        // Assert
        response.EnsureSuccessStatusCode();
        var courseFrameworks = await response.Content.ReadFromJsonAsync<List<CreateCourseFrameworkDTO>>();

        Assert.That(courseFrameworks, Is.Not.Null);
        Assert.That(courseFrameworks.Count, Is.GreaterThan(0));
        Assert.That(courseFrameworks.Any(p => p.Code == DataSeeder.CourseFrameworkEntity.Code.Value), Is.True);
    }

    [Test]
    public async Task GivenNewCourseFramework_WhenCreateCourseFrameworkOfStudy_ThenShouldAddNewCourseFramework()
    {
        // Arrange
        CreateCourseFrameworkDTO newCourseFramework = new CourseFrameworkDTOBuilder()
            .WithCode("XX-TEST-XX")
            .WithName("Test Course")
            .WithLaboratoryHours(2)
            .WithTheoryHours(3)
            .WithPersonnalWorkHours(4)
            .WithSemester(5)
            .Build();

        // Act
        HttpResponseMessage response = await _Client.PostAsJsonAsync($"/api/programofstudy/{DataSeeder.ProgramOfStudyEntity.Code}/courseFramework", newCourseFramework);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
        UntrackedCourseFrameworkDTO? createdCourseFramework = await response.Content.ReadFromJsonAsync<UntrackedCourseFrameworkDTO>();
        Assert.That(createdCourseFramework, Is.Not.Null);

        // Verify it was added to the database
        HttpResponseMessage getResponse = await _Client.GetAsync($"/api/courseFramework/{createdCourseFramework.Code}");
        getResponse.EnsureSuccessStatusCode();
        UntrackedCourseFrameworkDTO? fetchedCourseFramework = await getResponse.Content.ReadFromJsonAsync<UntrackedCourseFrameworkDTO>();
        Assert.That(fetchedCourseFramework, Is.Not.Null);

        createdCourseFramework.Should().BeEquivalentTo(fetchedCourseFramework, options =>
            options.ExcludingMissingMembers()
            .Excluding(x => x!.Id)
            );
    }

    [Test]
    public async Task GivenExistingCourseFramework_WhenDeleteCourseFrameworkOfStudy_ThenShouldRemoveCourseFramework()
    {
        // Arrange
        CreateCourseFrameworkDTO newCourseFramework = new CourseFrameworkDTOBuilder()
            .WithCode("XX-DEL-XX")
            .WithName("Test Course F Del")
            .WithLaboratoryHours(1)
            .WithTheoryHours(2)
            .WithPersonnalWorkHours(3)
            .WithSemester(4)
            .Build();

        // Act
        HttpResponseMessage response = await _Client.PostAsJsonAsync($"/api/programofstudy/{DataSeeder.ProgramOfStudyEntity.Code}/courseFramework", newCourseFramework);
        response.EnsureSuccessStatusCode();
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
        UntrackedCourseFrameworkDTO? createdCourseFramework = await response.Content.ReadFromJsonAsync<UntrackedCourseFrameworkDTO>();
        Assert.That(createdCourseFramework, Is.Not.Null);
        response = await _Client.DeleteAsync($"/api/courseFramework/{createdCourseFramework.Id}");
        response.EnsureSuccessStatusCode();

        // Verify it was delted
        HttpResponseMessage getResponse = await _Client.GetAsync($"/api/courseFramework/{createdCourseFramework.Id}");
        getResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GivenExistingCourseFramework_WhenUpdateCourseFramework_ThenShouldUpdateCourseFramework()
    {
        Assert.Fail();// COMPLETE ME

        // Arrange
        ProgramOfStudyDTO newProgram = new ProgramOfStudyDTOBuilder().Build();


        // Act - Create the program
        var createResponse = await _Client.PostAsJsonAsync("/api/programofstudy", newProgram);
        createResponse.EnsureSuccessStatusCode();
        var createdProgram = await createResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();

        var optionalUnits = createdProgram.OptionalUnits;
        optionalUnits.Denominator = 2;
        optionalUnits.Numerator = 1;
        optionalUnits.WholeUnit = 26;


        ProgramOfStudyDTO updatedProgramData = new ProgramOfStudyDTOBuilder()
            .WithCode("421.B5")
            .WithName("Techniques de l'informatique 2.0")
            .WithProgramType(Domain.Enums.ProgramType.AEC)
            .WithMonthsDuration(35)
            .WithSpecificDurationHours(53)
            .WithTotalDurationHours(35)
            .WithPublishedOn(createdProgram.PublishedOn)
            .WithOptionalUnits(optionalUnits)
            .WithSpecificUnits(createdProgram.SpecificUnits)
            .WithGeneralUnits(createdProgram.GeneralUnits!)
            .WithComplementaryUnits(createdProgram.ComplementaryUnits!)
            .Build();
        // Act - Update the program
        updatedProgramData.Code = createdProgram!.Code;
        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{updatedProgramData.Code}", updatedProgramData);
        updateResponse.EnsureSuccessStatusCode();
        var updatedProgram = await updateResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();


        updatedProgram.Should().NotBeEquivalentTo(createdProgram, options =>
            options.ExcludingMissingMembers());
    }
}

