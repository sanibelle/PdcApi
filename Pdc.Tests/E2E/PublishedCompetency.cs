using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.DTOS.Common;
using Pdc.Tests.E2E;
using System.Net.Http.Json;
using TestDataSeeder;
using TestDataSeeder.Builders.DTOS;

namespace Pdc.E2ETests;

[TestFixture]
public class PublishedCompetencyApiTest : ApiTestBase
{

    [Test]
    public async Task GivenExistingV1DraftCompetency_WhenUpdatingCompetency_ThenShouldUpdateCompetencyWithNoChangeDetails()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Prepare - Create and publish the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        // Publish the new version
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var publishResponse = await _Client.PostAsync($"/api/changeRecord/publish/{competencyToUpdateDTO.ChangeRecordId.Value}", null);
        publishResponse.EnsureSuccessStatusCode();

        var getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        getResponse.EnsureSuccessStatusCode();
        competencyToUpdateDTO = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        // Act -  Update the competency
        // XXX Question pour le client : Est-ce qu'on veut tracker l'énoncé de la compétence?
        // XXX Question pour le client : Est-ce qu'on veut tracker qu'une compétence passe d'obligatoire à optionnel? Ça arrive?
        competencyToUpdateDTO.StatementOfCompetency = "Updated competency statement";
        competencyToUpdateDTO.IsOptional = true;

        // removing first ones
        competencyToUpdateDTO.RealisationContexts.Remove(competencyToUpdateDTO.RealisationContexts.First());
        competencyToUpdateDTO.CompetencyElements.Remove(competencyToUpdateDTO.CompetencyElements.First());
        competencyToUpdateDTO.CompetencyElements.First().PerformanceCriterias.Remove(competencyToUpdateDTO.CompetencyElements.First().PerformanceCriterias.First());

        // updating the one left and then adding
        competencyToUpdateDTO.RealisationContexts.First().Value = "Updated realisation context of the existing element";
        competencyToUpdateDTO.RealisationContexts.Add(realisationContext =new ChangeableDTOBuilder()
            .WithValue("New realisation Context")
            .Build());

        competencyToUpdateDTO.CompetencyElements.First().Value = "Updated competency element of the existing element";
        competencyToUpdateDTO.CompetencyElements.First().Position = 1;
        competencyToUpdateDTO.CompetencyElements.First().PerformanceCriterias.Add(new ChangeableDTOBuilder().WithPosition(1).WithValue("Added during update").Build());
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

        CompetencyUtils.AssertDraftCompetencyBasedOnResponse(competencyToUpdateDTO, updatedCompetency, 2);
    }
    // Faker une version 1 draft.
    // Faker une version 300 published.
    // Faker une version 2 draft.
}