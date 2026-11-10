using FluentAssertions;
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
        List<UnTrackedCourseFrameworkDTO>? courseFrameworks = await response.Content.ReadFromJsonAsync<List<UnTrackedCourseFrameworkDTO>>();

        Assert.That(courseFrameworks, Is.Not.Null);
        Assert.That(courseFrameworks.Count, Is.GreaterThan(0));
        Assert.That(courseFrameworks.Any(p => p.Code == DataSeeder.CourseFrameworkEntity.Code.Value), Is.True);
    }

    [Test]
    public async Task GivenNewCourseFramework_WhenCreateCourseFrameworkOfStudy_ThenShouldAddNewCourseFramework()
    {
        // Arrange
        CreateCourseFrameworkDTO newCourseFramework = new CreateCourseFrameworkDTOBuilder()
            .WithCode("XX-TEST-XX")
            .WithName("Test Course")
            .WithLaboratoryHours(2)
            .WithTheoryHours(3)
            .WithPersonnalWorkHours(4)
            .WithSemester(5)
            .WithCompetencyCodes([DataSeeder.CompetencyEntity.Code])
            .Build();

        // Act
        HttpResponseMessage response = await _Client.PostAsJsonAsync($"/api/programofstudy/{DataSeeder.ProgramOfStudyEntity.Code}/courseFramework", newCourseFramework);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
        TrackedCourseFrameworkDTO? createdCourseFramework = await response.Content.ReadFromJsonAsync<TrackedCourseFrameworkDTO>();
        Assert.That(createdCourseFramework, Is.Not.Null);

        // Verify it was added to the database
        HttpResponseMessage getResponse = await _Client.GetAsync($"/api/courseFramework/{createdCourseFramework.Id}");
        getResponse.EnsureSuccessStatusCode();
        TrackedCourseFrameworkDTO? fetchedCourseFramework = await getResponse.Content.ReadFromJsonAsync<TrackedCourseFrameworkDTO>();
        Assert.That(fetchedCourseFramework, Is.Not.Null);

        createdCourseFramework.Should().BeEquivalentTo(fetchedCourseFramework, options =>
            options.ExcludingMissingMembers()
            .Excluding(x => x!.Id)
            .Excluding(x => x.Competencies)
            );

        createdCourseFramework.Competencies.First().Code.Should().BeEquivalentTo(DataSeeder.CompetencyEntity.Code);

    }

    [Test]
    public async Task GivenExistingCourseFramework_WhenDeleteCourseFrameworkOfStudy_ThenShouldRemoveCourseFramework()
    {
        // Arrange
        CreateCourseFrameworkDTO newCourseFramework = new CreateCourseFrameworkDTOBuilder()
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
        TrackedCourseFrameworkDTO? createdCourseFramework = await response.Content.ReadFromJsonAsync<TrackedCourseFrameworkDTO>();
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
        // Arrange
        CreateCourseFrameworkDTO newCourseFramework = new CreateCourseFrameworkDTOBuilder()
            .WithCode("XX-UPD-XX")
            .WithName("Test Course F update")
            .WithLaboratoryHours(1)
            .WithTheoryHours(2)
            .WithPersonnalWorkHours(3)
            .WithSemester(4)
            .Build();

        // Act
        HttpResponseMessage response = await _Client.PostAsJsonAsync($"/api/programofstudy/{DataSeeder.ProgramOfStudyEntity.Code}/courseFramework", newCourseFramework);
        response.EnsureSuccessStatusCode();
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
        TrackedCourseFrameworkDTO? createdCourseFramework = await response.Content.ReadFromJsonAsync<TrackedCourseFrameworkDTO>();
        Assert.That(createdCourseFramework, Is.Not.Null);

        // Arrange
        UpdateCourseFrameworkDTO courseFrameworkDTOToUpdate = new UpdateCourseFrameworkDTOBuilder(createdCourseFramework)
            .WithCompetencyCodes([DataSeeder.CompetencyEntity.Code])
            .UpdateCode("XX-UPD-01")
            .UpdateName("Test Course F update updated")
            .UpdateLaboratoryHours(5)
            .UpdateTheoryHours(5)
            .UpdatePersonnalWorkHours(5)
            .UpdateSemester(5)
            .Build();

        var updateResponse = await _Client.PutAsJsonAsync($"/api/courseFramework/{createdCourseFramework.Id}", courseFrameworkDTOToUpdate);
        updateResponse.EnsureSuccessStatusCode();
        // Verify it was updated
        var updatedCourseFramework = await updateResponse.Content.ReadFromJsonAsync<TrackedCourseFrameworkDTO>();

        updatedCourseFramework.Code.Value.Should().Be(courseFrameworkDTOToUpdate.Code.Value);
        updatedCourseFramework.Name.Value.Should().Be(courseFrameworkDTOToUpdate.Name.Value);
        updatedCourseFramework.Weighting.TheoryHours.Value.Should().Be(courseFrameworkDTOToUpdate.Weighting.TheoryHours.Value);
        updatedCourseFramework.Weighting.LaboratoryHours.Value.Should().Be(courseFrameworkDTOToUpdate.Weighting.LaboratoryHours.Value);
        updatedCourseFramework.Weighting.PersonnalWorkHours.Value.Should().Be(courseFrameworkDTOToUpdate.Weighting.PersonnalWorkHours.Value);
        updatedCourseFramework.Semester.Value.Should().Be(courseFrameworkDTOToUpdate.Semester.Value);
        updatedCourseFramework.Competencies.Should().HaveCount(1);
        updatedCourseFramework.Competencies.First().Code.Should().BeEquivalentTo(DataSeeder.CompetencyEntity.Code);

        // Then remove the competencyCode
        UpdateCourseFrameworkDTO courseFrameworkDTOToUpdateWithNoCompetencies = new UpdateCourseFrameworkDTOBuilder(createdCourseFramework)
            .WithCompetencyCodes([])
            .Build();

        updateResponse = await _Client.PutAsJsonAsync($"/api/courseFramework/{createdCourseFramework.Id}", courseFrameworkDTOToUpdateWithNoCompetencies);
        updateResponse.EnsureSuccessStatusCode();
        // Verify it was updated
        updatedCourseFramework = await updateResponse.Content.ReadFromJsonAsync<TrackedCourseFrameworkDTO>();
        updatedCourseFramework.Competencies.Should().BeEmpty();
    }
}

