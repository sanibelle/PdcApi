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
        ComplementaryInformationDTO realisationContextComplementaryInformation, performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyDTO;
        CreateCompetency(out realisationContextComplementaryInformation, out performanceCriteriaComplementaryInformation, out competencyElementComplementaryInformation, out realisationContext, out performanceCriteria, out competencyElement, out competencyDTO);

        // Act - Create the program
        var createResponse = await _client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyDTO);
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

            AssertComplementaryInformation(r?.ComplementaryInformations?.FirstOrDefault(), realisationContextComplementaryInformation);
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

            AssertComplementaryInformation(c?.ComplementaryInformations?.FirstOrDefault(), competencyElementComplementaryInformation);

            foreach (var p in c?.PerformanceCriterias ?? [])
            {

                Assert.That(p.Id != Guid.Empty || p.Id != null, "guid is not empty");
                p.Should().BeEquivalentTo(performanceCriteria, options =>
                    options
                    .Excluding(x => x.Id)
                    .Excluding(x => x.ComplementaryInformations));

                AssertComplementaryInformation(p?.ComplementaryInformations?.FirstOrDefault(), performanceCriteriaComplementaryInformation);
            }
        }
    }

    [Test]
    public async Task GivenExistingCompetency_WhenCreatingCompetency_ThenShouldReturnAnError()
    {
        string _programCode = DataSeeder.ProgramOfStudyEntity.Code;
        ComplementaryInformationDTO realisationContextComplementaryInformation, performanceCriteriaComplementaryInformation, competencyElementComplementaryInformation;
        ChangeableDTO realisationContext, performanceCriteria;
        CompetencyElementDTO competencyElement;
        CompetencyDTO competencyDTO;
        CreateCompetency(out realisationContextComplementaryInformation, out performanceCriteriaComplementaryInformation, out competencyElementComplementaryInformation, out realisationContext, out performanceCriteria, out competencyElement, out competencyDTO);
        CompetencyValidation validation = new CompetencyValidation();
        competencyDTO.Code = DataSeeder.CompetencyEntity.Code;
        validation.Validate(competencyDTO).IsValid.Should().BeTrue();
        var createResponse = await _client.PostAsJsonAsync($"/api/programofstudy/{_programCode}/competency", competencyDTO);
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);
    }

    private void CreateCompetency(out ComplementaryInformationDTO realisationContextComplementaryInformation, out ComplementaryInformationDTO performanceCriteriaComplementaryInformation, out ComplementaryInformationDTO competencyElementComplementaryInformation, out ChangeableDTO realisationContext, out ChangeableDTO performanceCriteria, out CompetencyElementDTO competencyElement, out CompetencyDTO competencyDTO)
    {
        realisationContextComplementaryInformation =new ComplementaryInformationDTOBuilder()
            .Build();
        performanceCriteriaComplementaryInformation =new ComplementaryInformationDTOBuilder()
            .Build();
        competencyElementComplementaryInformation =new ComplementaryInformationDTOBuilder()
            .Build();
        realisationContext =new ChangeableDTOBuilder()
            .AddComplementaryInformation(realisationContextComplementaryInformation)
            .Build();
        performanceCriteria =new ChangeableDTOBuilder()
            .AddComplementaryInformation(performanceCriteriaComplementaryInformation)
            .WithPosition(1)
            .Build();
        competencyElement =new CompetencyElementDTOBuilder()
            .AddPerformanceCriteria(performanceCriteria)
            .WithPosition(1)
            .AddComplementaryInformation(competencyElementComplementaryInformation)
            .BuildCompetencyElement();
        competencyDTO =new CompetencyDTOBuilder()
            .AddCompetencyElements(competencyElement)
            .WithRealisationContexts(new List<ChangeableDTO> { realisationContext })
            .Build();
    }

    private void AssertComplementaryInformation(ComplementaryInformationDTO? i, ComplementaryInformationDTO? competencyElementComplementaryInformation)
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(i?.Id != Guid.Empty || i.Id != null, "guid is not empty");
            Assert.That(i?.WrittenOnVersion != null, "version is found");
            Assert.That(i?.WrittenOnVersion == 1, "new version is always 1");
        }
        i.Should().BeEquivalentTo(competencyElementComplementaryInformation, options =>
           options.Excluding(x => x.WrittenOnVersion)
           .Excluding(x => x.CreatedBy));
    }
}