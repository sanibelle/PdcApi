// TODO $$$$ Payer fluent assertion
using FluentAssertions;
using Pdc.Application.DTOS;
using Pdc.Domain.DTOS.Common;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using System.Net.Http.Json;
using TestDataSeeder;
using TestDataSeeder.Builders.DTOS;

namespace Pdc.E2ETests;

[TestFixture]
public class ComplementaryInformation : ApiTestBase
{

    [Test]
    public async Task GivenComplementaryInformation_WhenCreatingComplementaryInformation_ThenShouldReturnCreatedComplementaryInformation()
    {
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text")
            .Build();

        // Act - Create the competencyiojklm
        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/changeable", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();
        createdComplementaryInformation.Should().BeEquivalentTo(complementaryInformationDTO);
        createdComplementaryInformation.CreatedOn.Should().NotBeNull();
        createdComplementaryInformation.ModifiedOn.Should().BeNull();
        createdComplementaryInformation.CreatedBy.Should().NotBeNull();
    }


    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingComplementaryInformation_ThenShouldReturnCreatedComplementaryInformation()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingMissingComplementaryInformation_ThenShouldReturn404()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingComplementaryInformationAsAdmin_ThenShouldSucceed()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingComplementaryInformationAsAuthor_ThenShouldSucceed()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingComplementaryInformationNotAsAuthor_ThenShouldFail()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingComplementaryInformation_ThenShouldReturnCreatedComplementaryInformation()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingMissingComplementaryInformation_ThenShouldReturn404()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingComplementaryInformationAsAdmin_ThenShouldSucceed()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingComplementaryInformationAsAuthor_ThenShouldSucceed()
    {
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingComplementaryInformationNotAsAuthor_ThenShouldFail()
    {
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
            Assert.That(r.Id.HasValue && r.Id.Value != Guid.Empty, "guid is not empty");
            var realisationContext = competencyDTO.RealisationContexts.FirstOrDefault(x => x.Value == r.Value);
            r.Should().BeEquivalentTo(realisationContext, options =>
                options
                .Excluding(x => x.ComplementaryInformations)
                .Excluding(x => x.Id));

            AssertComplementaryInformation(r?.ComplementaryInformations?.FirstOrDefault(), realisationContext?.ComplementaryInformations?.FirstOrDefault());
        }

        // NOTE le foreach a un seul element
        foreach (var c in competencyToCompare.CompetencyElements)
        {
            Assert.That(c.Id.HasValue && c.Id.Value != Guid.Empty, "guid is not empty");
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
                Assert.That(p.Id.HasValue && p.Id.Value != Guid.Empty, "guid is not empty");
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