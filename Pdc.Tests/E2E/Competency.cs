// TODO $$$$ Payer fluent assertion
using FluentAssertions;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Application.Validators;
using System.Net.Http.Json;
using TestDataSeeder;
using TestDataSeeder.Builders.DTOS;

namespace Pdc.E2ETests;

[TestFixture]
public class CompetencyApiTests : ApiTestBase
{

    [Test]
    public async Task GivenExistingProgram_WhenCreatingCompetency_ThenShouldCreateCompetency()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        CompetencyDTO competencyDTO = CreateCompetency();

        // Act - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdCompetency = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();
        var getResponse = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{createdCompetency.Code}");
        getResponse.EnsureSuccessStatusCode();
        AssertCompetencyBasedOnResponse(competencyDTO, createdCompetency);
    }


    [Test]
    public async Task GivenExistingCompetency_WhenCreatingCompetency_ThenShouldReturnADuplicateError()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        CompetencyDTO competencyDTO = CreateCompetency();
        CompetencyValidation validation = new CompetencyValidation();
        competencyDTO.Code = DataSeeder.CompetencyEntity.Code;
        validation.Validate(competencyDTO).IsValid.Should().BeTrue();
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyDTO);
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GivenExistingV1DraftCompetency_WhenUpdatingCompetency_ThenShouldUpdateCompetencyWithNoChangeDetails()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyToCreateDTO = CreateCompetency();

        // Act - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        // Update the competency
        competencyToUpdateDTO.StatementOfCompetency = "Updated competency statement";
        competencyToUpdateDTO.IsOptionnal = true;

        competencyToUpdateDTO.RealisationContexts.First().Value = "Updated realisation context of the existing element";
        competencyToUpdateDTO.RealisationContexts.Add(realisationContext =new ChangeableDTOBuilder()
            .WithValue("New realisation Context")
            .Build());

        competencyToUpdateDTO.CompetencyElements.Add(competencyElement = new CompetencyElementDTOBuilder()
            .WithValue("New competency element")
            .WithPosition(2)
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

        AssertCompetencyBasedOnResponse(competencyToUpdateDTO, updatedCompetency);
    }

    [Test]
    public async Task GivenExistingV1DraftCompetency_WhenDeletingElementsOfTheCompetencyCompetency_ThenShouldDeleteTheElements()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyToCreateDTO = CreateCompetency();

        // Act - Create the competency
        var createResponse = await _Client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyToCreateDTO);
        createResponse.EnsureSuccessStatusCode();
        var competencyToUpdateDTO = await createResponse.Content.ReadFromJsonAsync<CompetencyDTO>();

        // Deleting the elements one by one
        // Realisation context
        competencyToUpdateDTO.RealisationContexts.Clear();
        //competencyToUpdateDTO.RealisationContexts.Add(realisationContext =new ChangeableDTOBuilder()
        //    .WithValue("New realisation Context")
        //    .Build());


        var updateResponse = await _Client.PutAsJsonAsync($"/api/programofstudy/{_programCode}/competency/{competencyToUpdateDTO.Code}", competencyToUpdateDTO);
        updateResponse.EnsureSuccessStatusCode();
        var updatedCompetency = await updateResponse.Content.ReadFromJsonAsync<CompetencyDTO>();


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
    }

    [Test]
    public async Task GivenExistingV1DraftCompetency_WhenDeletingCompetency_ThenShouldDeleteCompetencyWithNoChangeDetails()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyToCreateDTO = CreateCompetency();

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
        CompetencyDTO competencyToCreateDTO = CreateCompetency();

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
        updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        var res = await _Client.GetAsync($"/api/programofstudy/{_programCode}/competency/{oldCode}");
        var notUpdatedCompetency = await res.Content.ReadFromJsonAsync<CompetencyDTO>();
        notUpdatedCompetency.Should().NotBeNull();
        notUpdatedCompetency.RealisationContexts.Should().HaveCount(1, "No realisation contexts added.");
    }



    private CompetencyDTO CreateCompetency()
    {
        var realisationContextComplementaryInformation = new ComplementaryInformationDTOBuilder()
            .WithText("realisationContextComplementaryInformation")
            .Build();
        var performanceCriteriaComplementaryInformation = new ComplementaryInformationDTOBuilder()
            .WithText("performanceCriteriaComplementaryInformation ")
            .Build();
        var competencyElementComplementaryInformation = new ComplementaryInformationDTOBuilder()
            .WithText("competencyElementComplementaryInformation")
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
            .AddComplementaryInformation(competencyElementComplementaryInformation)
            .BuildCompetencyElement();
        var competencyDTO = new CompetencyDTOBuilder()
            .AddCompetencyElements(competencyElement)
            .WithRealisationContexts(new List<ChangeableDTO> { realisationContext })
            .Build();

        return competencyDTO;
    }

    private void AssertCompetencyBasedOnResponse(CompetencyDTO competencyDTO, CompetencyDTO competencyToCompare)
    {
        competencyToCompare.Should().BeEquivalentTo(competencyDTO, options =>
                    options
                    .Excluding(x => x.IsDraft)
                    .Excluding(x => x.VersionNumber)
                    .Excluding(x => x.VersionId)
                    .Excluding(x => x.CompetencyElements)
                    .Excluding(x => x.RealisationContexts));

        Assert.That(competencyToCompare.VersionNumber == 1);
        Assert.That(competencyToCompare.IsDraft, Is.True);
        Assert.That(competencyToCompare.VersionId, Is.TypeOf<Guid>());

        foreach (var r in competencyToCompare.RealisationContexts)
        {
            Assert.That(r.Id != Guid.Empty || r.Id != null, "guid is not empty");
            var realisationContext = competencyDTO.RealisationContexts.FirstOrDefault(x => x.Value == r.Value, null);
            r.Should().BeEquivalentTo(realisationContext, options =>
                options
                .Excluding(x => x.ComplementaryInformations)
                .Excluding(x => x.Id));

            AssertComplementaryInformation(r?.ComplementaryInformations?.FirstOrDefault(), realisationContext?.ComplementaryInformations?.FirstOrDefault());
        }

        // NOTE le foreach a un seul element
        foreach (var c in competencyToCompare.CompetencyElements)
        {
            Assert.That(c.Id != Guid.Empty || c.Id != null, "guid is not empty");
            var competencyElement = competencyDTO.CompetencyElements.FirstOrDefault(x => x.Value == c.Value);
            c.Should().BeEquivalentTo(competencyElement, options =>
                options
                .Excluding(x => x.Id)
                .Excluding(x => x.PerformanceCriterias)
                .Excluding(x => x.ComplementaryInformations));

            AssertComplementaryInformation(c?.ComplementaryInformations?.FirstOrDefault(), competencyElement?.ComplementaryInformations?.FirstOrDefault());

            foreach (var p in c?.PerformanceCriterias ?? [])
            {
                var performanceCriteria = competencyElement.PerformanceCriterias.FirstOrDefault(x => x.Value == p.Value);
                Assert.That(p.Id != Guid.Empty || p.Id != null, "guid is not empty");
                p.Should().BeEquivalentTo(performanceCriteria, options =>
                    options
                    .Excluding(x => x.Id)
                    .Excluding(x => x.ComplementaryInformations));

                AssertComplementaryInformation(p?.ComplementaryInformations?.FirstOrDefault(), performanceCriteria?.ComplementaryInformations?.FirstOrDefault());
            }
        }
    }


    private void AssertComplementaryInformation(ComplementaryInformationDTO? originalComplementaryInformation, ComplementaryInformationDTO? complementaryInformation)
    {
        if (originalComplementaryInformation == null && complementaryInformation == null) return;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(originalComplementaryInformation?.Id != null && originalComplementaryInformation?.Id != Guid.Empty, $"guid is not empty");
            Assert.That(originalComplementaryInformation?.WrittenOnVersion != null, $"version is not found");
            originalComplementaryInformation?.WrittenOnVersion.Should().Be(1, "new or update version is always 1");
        }
        originalComplementaryInformation.Should().BeEquivalentTo(complementaryInformation, options =>
           options
           .Excluding(x => x.Id)
           .Excluding(x => x.WrittenOnVersion)
           .Excluding(x => x.CreatedBy));
    }
}