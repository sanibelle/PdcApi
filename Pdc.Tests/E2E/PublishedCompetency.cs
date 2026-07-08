using FluentAssertions;
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
    public async Task GivenPublishedV2Competency_WhenUpdatedCompetency_ThenShouldReturnCompetencyAndChangeDetailsWhenLookingForASpecificVersion()
    {
        // XXX Question pour le client : Est-ce qu'on veut tracker l'énoncé de la compétence?
        // XXX Question pour le client : Est-ce qu'on veut tracker qu'une compétence passe d'obligatoire à optionnel? Ça arrive?
        (string _programCode, CompetencyDTO? competencyToUpdateDTO)=await CreateDraftV2Competency();

        // Get diff
        HttpResponseMessage getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}/v{2}");
        var updatedCompetency = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        updatedCompetency.ChangeDetails.Should().NotBeEmpty();
        var deletedChangeDetails = updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Delete).Select(x => x.ChangeableId).ToList();
        deletedChangeDetails.Should().HaveCount(4);
        updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Add).Should().HaveCount(4);
        updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Update).Should().HaveCount(2);
        updatedCompetency.RealisationContexts.Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
        updatedCompetency.CompetencyElements.Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
        updatedCompetency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
    }

    [Test]
    public async Task GivenPublishedV2Competency_WhenUpdatingCompetencyWithFakeChangeRecord_ThenShouldFail()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Prepare - Create and publish the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        // Publish the new version
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var publishResponse = await _Client.PostAsync($"/api/changeRecord/publish/{competencyToUpdateDTO.ChangeRecordId!.Value}", null);
        publishResponse.EnsureSuccessStatusCode();

        var getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        getResponse.EnsureSuccessStatusCode();
        competencyToUpdateDTO = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        competencyToUpdateDTO.ChangeRecordId = new Guid();

        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task GivenPublishedV2Competency_WhenUpdatingCompetencyWithFakeDraftVersion_ThenShouldFail()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Prepare - Create and publish the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        // Publish the new version
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var publishResponse = await _Client.PostAsync($"/api/changeRecord/publish/{competencyToUpdateDTO.ChangeRecordId!.Value}", null);
        publishResponse.EnsureSuccessStatusCode();

        var getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        getResponse.EnsureSuccessStatusCode();
        competencyToUpdateDTO = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        competencyToUpdateDTO.ChangeRecordNumber = 1;
        competencyToUpdateDTO.IsDraft = true;

        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task GivenPublishedV2Competency_WhenUpdatingCompetencyWithFakeDraft_ThenShouldFail()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Prepare - Create and publish the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        // Publish the new version
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var publishResponse = await _Client.PostAsync($"/api/changeRecord/publish/{competencyToUpdateDTO.ChangeRecordId!.Value}", null);
        publishResponse.EnsureSuccessStatusCode();

        var getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        getResponse.EnsureSuccessStatusCode();
        competencyToUpdateDTO = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        competencyToUpdateDTO.IsDraft = true;

        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task GivenDraftV2Competency_WhenUpdatedCompetency_ThenShouldUpdateCompetencyAndChangeDetails()
    {
        (string _programCode, CompetencyDTO? competencyToUpdateDTO)=await CreateDraftV2Competency();

        // Creating a new draft by updating to the same version.
        HttpResponseMessage getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        var updatedCompetency = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        // Second update
        /// Competency
        /// ├── RealisationContexts
        /// │   ├── "Realisation context #1", v1 [create] → v2 [delete]
        /// │   ├── "Updated realisation context of the existing element", v2 [update] → v2 [update] (should keep update)
        /// │   └── "New realisation Context", v2 [create] → v2 [update] (should keep create)
        /// └── CompetencyElements
        ///     ├── "Competency element #1", v1 [create] → v2 [delete]
        ///     ├── "Updated competency element of the existing element" (Position = 1), v2 [update] → v2 [delete] (should change update to delete)
        ///     │   └── PerformanceCriterias
        ///     │       ├── "Performance criteria #1", v1 [create] → v2 [delete]
        ///     │       ├── "Performance criteria #2", v1 [create]
        ///     │       └── "Added during update" (Position = 1), v2 [create]
        ///     └── "New competency element" (Position = last), v2 [create]
        ///         ├── PerformanceCriterias
        ///         │   └── "New performance criteria" (Position = 1), v2 [create] → v2 [delete] (No change details for this one)
        ///         │       └── ComplementaryInformations
        ///         │           └── "New performance criteria complementary information", v2 [create]
        ///         └── ComplementaryInformations
        ///             └── "New competency element complementary information", v2 [create]
        // Modifying
        var modifiedUpdatedElement = updatedCompetency.RealisationContexts.ElementAt(0);
        var modifiedAddedElement = updatedCompetency.RealisationContexts.ElementAt(1);
        // - An Added element
        modifiedAddedElement.Value = "Modified value of the added element";
        // - A Modified element
        modifiedUpdatedElement.Value = "Modified value of the updated element";
        // Deleting
        var deletedUpdatedElementId = updatedCompetency.CompetencyElements.ElementAt(0).Id;
        var deletedAddedElementId = updatedCompetency.CompetencyElements.ElementAt(1).Id;
        // - An Added element
        updatedCompetency.CompetencyElements.Remove(updatedCompetency.CompetencyElements.ElementAt(0));
        // - A Modified element
        updatedCompetency.CompetencyElements.Remove(updatedCompetency.CompetencyElements.ElementAt(0));

        // Sending the update to the api a second time
        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{updatedCompetency.Code}", updatedCompetency);
        updateResponse.EnsureSuccessStatusCode();
        getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{updatedCompetency.Code}/v{2}");
        updatedCompetency = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();



        updatedCompetency.ChangeDetails.Should().NotBeEmpty();
        var deletedChangeDetails = updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Delete).Select(x => x.ChangeableId).ToList();
        deletedChangeDetails.Should().HaveCount(5);
        updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Add).Should().HaveCount(2);
        updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Update).Should().HaveCount(1);
        updatedCompetency.RealisationContexts.Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
        updatedCompetency.CompetencyElements.Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
        updatedCompetency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
    }


    [Test]
    public async Task GivenPublishedV3Competency_WhenUpdatedCompetency_ThenCompetencyAndChangeDetailsWhenLookingForASpecificVersion()
    {
        (string _programCode, CompetencyDTO? competencyToUpdateDTO)=await CreateDraftV2Competency();
        var getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}/v{2}");
        getResponse.EnsureSuccessStatusCode();
        var updatedCompetency = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        updatedCompetency.ChangeDetails.Should().NotBeEmpty();
        var deletedChangeDetails = updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Delete).Select(x => x.ChangeableId).ToList();
        deletedChangeDetails.Should().HaveCount(4);
        updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Add).Should().HaveCount(4);
        updatedCompetency.ChangeDetails.Where(x => x.ChangeType == Domain.Enums.ChangeType.Update).Should().HaveCount(2);

        updatedCompetency.RealisationContexts.Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
        updatedCompetency.CompetencyElements.Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
        updatedCompetency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).Any(x => deletedChangeDetails.Any(y => y == x.Id)).Should().BeTrue();
    }

    [Test]
    public async Task GivenPublishedV3CompetencyAndGettingV2Changes_WhenUpdatingAnElementOnV1AndV3_ThenValuesOfVOnUpdatedV3Elements()
    {
        string v3UpdatedValue = "UpdatedOnV3";
        (string _programCode, CompetencyDTO? competencyToUpdateDTO)=await CreateDraftV2Competency();

        // Creating a new draft by updating to the same version.
        HttpResponseMessage getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        var updatedCompetency = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var publishResponse = await _Client.PostAsync($"/api/changeRecord/publish/{updatedCompetency.ChangeRecordId!.Value}", null);
        publishResponse.EnsureSuccessStatusCode();

        // Second update
        // Modifying
        getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{updatedCompetency.Code}");
        updatedCompetency = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        updatedCompetency.RealisationContexts.ElementAt(1).Value = v3UpdatedValue;
        updatedCompetency.RealisationContexts.Add(new ChangeableDTOBuilder().WithPosition(2).WithValue("Created on v3").Build());
        updatedCompetency.CompetencyElements.ElementAt(0).Value = v3UpdatedValue;
        updatedCompetency.CompetencyElements.ElementAt(0).PerformanceCriterias.ElementAt(0).Value = v3UpdatedValue;

        HttpResponseMessage updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{updatedCompetency.Code}", updatedCompetency);
        updateResponse.EnsureSuccessStatusCode();
        getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{updatedCompetency.Code}/v{2}");
        updatedCompetency = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        updatedCompetency.RealisationContexts.Should().HaveCount(2); // The added realisation context on v3 should not be showed on the diff from v1 to v2.
        updatedCompetency.RealisationContexts.Select(x => x.Value).Any(x => x == v3UpdatedValue).Should().BeFalse();
        updatedCompetency.CompetencyElements.Select(x => x.Value).Any(x => x == v3UpdatedValue).Should().BeFalse();
        updatedCompetency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).Select(x => x.Value).Any(x => x == v3UpdatedValue).Should().BeFalse();
    }

    /// <summary>
    /// Creates and publishes a competency (V1), then prepares an in-memory draft V2 with this format :
    /// <code>
    /// Competency, v1 [create]
    /// ├── RealisationContexts
    /// │   ├── "Realisation context `#1`", v1 [create] → v2 [delete]
    /// │   ├── "Updated realisation context of the existing element", v1 [create] → v2 [update]
    /// │   └── "New realisation Context", v2 [create]
    /// └── CompetencyElements
    ///     ├── "Competency element `#1`", v1 [create] → v2 [delete]
    ///     ├── "Updated competency element of the existing element" (Position = 1), v1 [create] → v2 [update]
    ///     │   └── PerformanceCriterias
    ///     │       ├── "Performance criteria `#1`", v1 [create] → v2 [delete]
    ///     │       ├── "Performance criteria `#2`", v1 [create]
    ///     │       └── "Added during update" (Position = 1), v2 [create]
    ///     └── "New competency element" (Position = last), v2 [create]
    ///         ├── PerformanceCriterias
    ///         │   └── "New performance criteria" (Position = 1), v2 [create]
    ///         │       └── ComplementaryInformations
    ///         │           └── "New performance criteria complementary information", v2 [create]
    ///         └── ComplementaryInformations
    ///             └── "New competency element complementary information", v2 [create]
    /// </code>
    /// Steps performed:
    /// <list type="number">
    /// <item>Creates the competency (V1) via POST /api/programofstudy/{programCode}/competency.</item>
    /// <item>Publishes V1 via POST /api/changeRecord/publish/{changeRecordId}.</item>
    /// <item>Retrieves the published V1 competency via GET.</item>
    /// <item>Modifies the retrieved DTO in memory ([delete], [update], [create] operations above) to represent the draft V2 payload. The V2 update is sent to the API;</item>
    /// </list>
    /// </summary>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item><c>_programCode</c>: the code of the program of study the competency belongs to.</item>
    /// <item><c>competencyToUpdateDTO</c>: the resulting draft V2 competency DTO, as returned by the API after the update.</item>
    /// </list>
    /// </returns>
    private async Task<(string _programCode, CompetencyDTO? competencyToUpdateDTO)> CreateDraftV2Competency()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyToCreateDTO = CompetencyUtils.CreateCompetency();

        // Prepare - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        // Publish v1
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var publishResponse = await _Client.PostAsync($"/api/changeRecord/publish/{competencyToUpdateDTO.ChangeRecordId!.Value}", null);
        publishResponse.EnsureSuccessStatusCode();

        var getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}");
        getResponse.EnsureSuccessStatusCode();
        competencyToUpdateDTO = await getResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

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
        CompetencyUtils.AssertDraftCompetencyBasedOnResponse(competencyToUpdateDTO, updatedCompetency!, 2);

        return (_programCode, updatedCompetency);
    }
}