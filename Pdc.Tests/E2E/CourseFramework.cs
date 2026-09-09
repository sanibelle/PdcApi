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
            );
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
        TrackedCourseFrameworkDTO courseFrameworkDTOToUpdate = new TrackedCourseFrameworkDTOBuilder()
            .WithId(createdCourseFramework.Id)
            .WithCode(createdCourseFramework.Code)
            .WithName(createdCourseFramework.Name)
            .WithLaboratoryHours(createdCourseFramework.Weighting.LaboratoryHours)
            .WithTheoryHours(createdCourseFramework.Weighting.TheoryHours)
            .WithPersonnalWorkHours(createdCourseFramework.Weighting.PersonnalWorkHours)
            .WithSemester(createdCourseFramework.Semester)
            .WithCode("YY-UPD-YY")
            .WithName("Updated course framework name")
            .WithLaboratoryHours(6)
            .WithTheoryHours(7)
            .WithPersonnalWorkHours(8)
            .WithSemester(9)
            .WithChangeRecordNumber(1)
            .WithIsDraft(true)
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

    }
}

