// TODO $$$$ Payer fluent assertion
using FluentAssertions;
using Pdc.Application.DTOS;
using Pdc.Domain.Models.Common;
using Pdc.Tests.Builders.DTOS;
using System.Net.Http.Json;

namespace Pdc.E2ETests;

[TestFixture]
public class ProgramOfStudyApiTests : ApiTestBase
{
    [Test]
    public async Task GivenSeededPrograms_WhenGetAllProgramsOfStudy_ThenShouldReturnSeededPrograms()
    {
        // Act
        var response = await _client.GetAsync("/api/programofstudy");

        // Assert
        response.EnsureSuccessStatusCode();
        var programs = await response.Content.ReadFromJsonAsync<List<ProgramOfStudyDTO>>();

        Assert.That(programs, Is.Not.Null);
        Assert.That(programs.Count, Is.GreaterThan(0));
        Assert.That(programs.Any(p => p.Code == TestDataSeeder.ProgramOfStudyEntity.Code), Is.True);
    }

    [Test]
    public async Task GivenNewProgram_WhenCreateProgramOfStudy_ThenShouldAddNewProgram()
    {
        // Arrange
        ProgramOfStudyDTO newProgram = new ProgramOfStudyDTOBuilder().Build();

        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/programofstudy", newProgram);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
        ProgramOfStudyDTO? createdProgram = await response.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();
        Assert.That(createdProgram, Is.Not.Null);

        // Verify it was added to the database
        HttpResponseMessage getResponse = await _client.GetAsync($"/api/programofstudy/{createdProgram.Code}");
        getResponse.EnsureSuccessStatusCode();
        ProgramOfStudyDTO? fetchedProgram = await getResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();
        Assert.That(fetchedProgram, Is.Not.Null);

        createdProgram.Should().BeEquivalentTo(fetchedProgram, options =>
            options.ExcludingMissingMembers()
            .Excluding(x => x!.GeneralUnits)
            .Excluding(x => x!.ComplementaryUnits)
            );
    }

    [Test]
    public async Task GivenExistingProgram_WhenDeleteProgramOfStudy_ThenShouldRemoveProgram()
    {
        // Arrange
        ProgramOfStudyDTO newProgram = new ProgramOfStudyDTOBuilder().Build();


        // Act - Create the program
        var createResponse = await _client.PostAsJsonAsync("/api/programofstudy", newProgram);
        createResponse.EnsureSuccessStatusCode();
        var createdProgram = await createResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();

        // Act - Delete the program
        var deleteResponse = await _client.DeleteAsync($"/api/programofstudy/{createdProgram!.Code}");
        deleteResponse.EnsureSuccessStatusCode();

        // Assert - Verify the program was deleted
        var getResponse = await _client.GetAsync($"/api/programofstudy/{createdProgram.Code}");
        Assert.That(getResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GivenExistingProgram_WhenUpdateProgramOfStudy_ThenShouldUpdateProgram()
    {
        // Arrange
        ProgramOfStudyDTO newProgram = new ProgramOfStudyDTOBuilder().Build();

        ProgramOfStudyDTO updatedProgramData = (ProgramOfStudyDTO)new ProgramOfStudyDTOBuilder()
            .WithCode("421.B5")
            .WithName("Techniques de l'informatique 2.0")
            .WithSanction(Pdc.Domain.Enums.SanctionType.AEC)
            .WithMonthsDuration(35)
            .WithSpecificDurationHours(53)
            .WithTotalDurationHours(35)
            .WithPublishedOn(DateOnly.FromDateTime(DateTime.Now))
            .WithOptionnalUnits(new Units(16, 2, 3))
            .WithSpecificUnits(new Units(26, 2, 3))
            .Build();

        // Act - Create the program
        var createResponse = await _client.PostAsJsonAsync("/api/programofstudy", newProgram);
        createResponse.EnsureSuccessStatusCode();
        var createdProgram = await createResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();

        // Act - Uodate the program
        updatedProgramData.Code = createdProgram!.Code;
        var updateResponse = await _client.PutAsJsonAsync($"/api/programofstudy/{updatedProgramData.Code}", updatedProgramData);
        updateResponse.EnsureSuccessStatusCode();
        var updatedProgram = await updateResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();


        updatedProgram.Should().NotBeEquivalentTo(createdProgram, options =>
            options.ExcludingMissingMembers());
    }
}

