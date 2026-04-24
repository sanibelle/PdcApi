using FluentAssertions;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Application.Validators;
using Pdc.Domain.DTOS.Common;
using Pdc.Tests.E2E;
using System.Net;
using System.Net.Http.Json;
using TestDataSeeder;
using TestDataSeeder.Builders.DTOS;

namespace Pdc.E2ETests;

[TestFixture]
public class CompetencyApiTests : ApiTestBase
{

    [Test]
    public async Task GivenExistingProgram_WhenCreatingCompetency_ThenShouldCompetencyUtils()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        CompetencyDTO competencyDTO = CompetencyUtils.CreateCompetency();

        // Act - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdCompetency = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{createdCompetency!.Code}");
        getResponse.EnsureSuccessStatusCode();
        var fetched = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        fetched.Should().NotBeNull();
        CompetencyUtils.AssertDraftCompetencyBasedOnResponse(competencyDTO, fetched!);
    }


    [Test]
    public async Task GivenExistingCompetency_WhenCreatingCompetency_ThenShouldReturnADuplicateError()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        CompetencyDTO competencyDTO = CompetencyUtils.CreateCompetency();
        CompetencyValidation validation = new CompetencyValidation();
        competencyDTO.Code = DataSeeder.CompetencyEntity.Code;
        validation.Validate(competencyDTO).IsValid.Should().BeTrue();
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyDTO);
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        createResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GivenExistingV1DraftCompetency_WhenUpdatingCompetency_ThenShouldUpdateCompetencyWithNoChangeDetails()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Act - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        // Update the competency
        competencyToUpdateDTO.StatementOfCompetency = "Updated competency statement";
        competencyToUpdateDTO.IsOptional = true;

        competencyToUpdateDTO.RealisationContexts.First().Value = "Updated realisation context of the existing element";
        competencyToUpdateDTO.RealisationContexts.Add(realisationContext =new ChangeableDTOBuilder()
            .WithValue("New realisation Context")
            .Build());

        competencyToUpdateDTO.CompetencyElements.Add(competencyElement = new CompetencyElementDTOBuilder()
            .WithValue("New competency element")
            .WithPosition(competencyToUpdateDTO.CompetencyElements.Count() + 1)
            .AddPerformanceCriteria(performanceCriteria = new ChangeableDTOBuilder()
                .WithValue("New performance criteria")
                .WithPosition(1)
                .AddComplementaryInformation(performanceCriteriaComplementaryInformation = new ComplementaryInformationDTOBuilder()
                    .WithText("New performance criteria complementary information")
                    .Build())
                .Build())
            .AddComplementaryInformation(competencyElementComplementaryInformation = new ComplementaryInformationDTOBuilder()
                .WithText("New competency element complementary information")
                .Build())
            .BuildCompetencyElement());
        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.EnsureSuccessStatusCode();
        var updatedCompetency = await updateResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        CompetencyUtils.AssertDraftCompetencyBasedOnResponse(competencyToUpdateDTO, updatedCompetency);
    }

    [Test]
    public async Task GivenExistingV1DraftCompetency_WhenDeletingElementsOfTheCompetencyCompetency_ThenShouldDeleteTheElements()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Act - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        // Clear the realisation contexts complementary information
        competencyToUpdateDTO.RealisationContexts.First().ComplementaryInformations.Clear();

        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.EnsureSuccessStatusCode();
        var updatedCompetency = await updateResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        competencyToUpdateDTO.RealisationContexts.First().ComplementaryInformations.Should().BeEmpty();

        // Clear the realisation contexts
        competencyToUpdateDTO.RealisationContexts.Clear();

        updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.EnsureSuccessStatusCode();
        updatedCompetency = await updateResponse.Content.ReadFromJsonAsync<CompetencyDTO>();


        // performance criteria complementary information
        competencyToUpdateDTO.CompetencyElements.First().PerformanceCriterias.First().ComplementaryInformations.Clear();
        updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.EnsureSuccessStatusCode();
        updatedCompetency = await updateResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        updatedCompetency.CompetencyElements.First().PerformanceCriterias.First().ComplementaryInformations.Should().BeEmpty();

        // Adding competency Element and deleting
        var deletedId = competencyToUpdateDTO.CompetencyElements.First().Id;
        competencyToUpdateDTO.CompetencyElements.Clear();
        competencyToUpdateDTO.CompetencyElements.Add(competencyElement = new CompetencyElementDTOBuilder()
            .WithValue("New competency element")
            .WithPosition(1)
            .AddPerformanceCriteria(performanceCriteria = new ChangeableDTOBuilder()
                .WithValue("New performance criteria")
                .WithPosition(1)
                .AddComplementaryInformation(performanceCriteriaComplementaryInformation = new ComplementaryInformationDTOBuilder()
                    .WithText("New performance criteria complementary information")
                    .Build())
                .Build())
            .AddComplementaryInformation(competencyElementComplementaryInformation = new ComplementaryInformationDTOBuilder()
                .WithText("New competency element complementary information")
                .Build())
            .BuildCompetencyElement());
        updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.EnsureSuccessStatusCode();
        updatedCompetency = await updateResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        updatedCompetency.CompetencyElements.Should().HaveCount(1);
        updatedCompetency.CompetencyElements.First().Id.Should().NotBe(deletedId.ToString());
        updatedCompetency.CompetencyElements.First().ComplementaryInformations.First().CreatedBy.Should().NotBe(null);
        updatedCompetency.ChangeRecordId.Should().NotBe(Guid.Empty);
    }

    [Test]
    public async Task GivenExistingV1DraftCompetency_WhenDeletingCompetency_ThenShouldDeleteCompetencyWithNoChangeDetails()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Act - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        // Act - Delete the competency
        var deletedResponse = await _Client.DeleteAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        deletedResponse.EnsureSuccessStatusCode();

        var getResponse = _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        getResponse.Result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GivenExistingV1DraftCompetency_WhenUpdatingTheCode_ThenShouldFailTheUpdate()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ChangeableDTO realisationContext;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Act - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var oldCode = competencyToUpdateDTO.Code;
        // Update the competency
        competencyToUpdateDTO.Code = "BADCOD";
        competencyToUpdateDTO.RealisationContexts.Add(realisationContext =new ChangeableDTOBuilder()
            .WithValue("New realisation Context")
            .Build());

        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{oldCode}", competencyToUpdateDTO);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        var res = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{oldCode}");
        var notUpdatedCompetency = await res.Content.ReadFromJsonAsync<CompetencyDTO>();
        notUpdatedCompetency.Should().NotBeNull();
        notUpdatedCompetency.RealisationContexts.Should().HaveCount(2, "No realisation contexts added.");
    }
}