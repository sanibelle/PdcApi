// TODO $$$$ Payer fluent assertion
using FluentAssertions;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
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

    [Test]
    public async Task GivenExistingProgram_WhenCreatingCompetency_ThenShouldCreateCompetency()
    {
        string programCode = TestDataSeeder.ProgramOfStudyEntity.Code;
        // Arrange
        var realisationContextComplementaryInformation = new ComplementaryInformationDTOBuilder()
            .Build();
        var performanceCriteriaComplementaryInformation = new ComplementaryInformationDTOBuilder()
            .Build();
        var competencyElementComplementaryInformation = new ComplementaryInformationDTOBuilder()
            .Build();
        var realisationContext = new ChangeableDTOBuilder()
            .AddComplementaryInformation(realisationContextComplementaryInformation)
            .Build();
        var performanceCriteria = new ChangeableDTOBuilder()
            .AddComplementaryInformation(performanceCriteriaComplementaryInformation)
            .WithPosition(1)
            .Build();
        var competencyElement = new CompetencyElementDTOBuilder()
            .AddPerformanceCriteria(performanceCriteria)
            .WithPosition(1)
            .AddComplementaryInformations(competencyElementComplementaryInformation)
            .BuildCompetencyElement();
        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithCode("E2E.TES")
            .AddCompetencyElements(competencyElement)
            .WithRealisationContexts(new List<ChangeableDTO> { realisationContext })
            .Build();

        // Act - Create the program
        var createResponse = await _client.PostAsJsonAsync($"/api/programofstudy/{programCode}/competency", competencyDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdCompetency = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        createdCompetency.Should().BeEquivalentTo(competencyDTO, options =>
            options
            .Excluding(x => x.CompetencyElements)
            .Excluding(x => x.RealisationContexts));

        foreach (var r in createdCompetency.RealisationContexts)
        {

            Assert.That(r.Id != Guid.Empty || r.Id != null, "guid is not empty");
            r.Should().BeEquivalentTo(realisationContext, options =>
                options
                .Excluding(x => x.ComplementaryInformations)
                .Excluding(x => x.Id));

            AssertComplementartInformation(r.ComplementaryInformations.FirstOrDefault(), performanceCriteriaComplementaryInformation);
        }

        // NOTE le foreach a un seul element
        foreach (var c in createdCompetency.CompetencyElements)
        {
            Assert.That(c.Id != Guid.Empty || c.Id != null, "guid is not empty");
            c.Should().BeEquivalentTo(competencyElement, options =>
                options
                .Excluding(x => x.Id)
                .Excluding(x => x.PerformanceCriterias)
                .Excluding(x => x.ComplementaryInformations));

            AssertComplementartInformation(c.ComplementaryInformations.FirstOrDefault(), competencyElementComplementaryInformation);

            foreach (var p in c.PerformanceCriterias)
            {

                Assert.That(p.Id != Guid.Empty || p.Id != null, "guid is not empty");
                p.Should().BeEquivalentTo(performanceCriteria, options =>
                    options
                    .Excluding(x => x.Id)
                    .Excluding(x => x.ComplementaryInformations));

                AssertComplementartInformation(p.ComplementaryInformations.FirstOrDefault(), performanceCriteriaComplementaryInformation);
            }
        }

    }
    private void AssertComplementartInformation(ComplementaryInformationDTO? i, ComplementaryInformationDTO? competencyElementComplementaryInformation)
    {
        //TODO FINIR
        //Assert.That(i.Id != Guid.Empty || i.Id != null, "guid is not empty");
        //Assert.That(i.WrittenOnVersion != null, "version is found");
        //i.Should().BeEquivalentTo(competencyElementComplementaryInformation);
    }
}