using FluentAssertions;
using Pdc.Application.DTOS;
using Pdc.Domain.DTOS.Common;
using TestDataSeeder.Builders.DTOS;

namespace Pdc.Tests.E2E;

internal static class CompetencyUtils
{

    public static CompetencyDTO CreateCompetency()
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
            .WithValue("realisationContext")
            .AddComplementaryInformation(realisationContextComplementaryInformation)
            .Build();
        var realisationContext2 = new ChangeableDTOBuilder()
            .WithValue("realisationContext2")
            .Build();
        var performanceCriteria = new ChangeableDTOBuilder()
            .WithValue("performanceCriteria1.1")
            .AddComplementaryInformation(performanceCriteriaComplementaryInformation)
            .WithPosition(1)
            .Build();
        var performanceCriteria2 = new ChangeableDTOBuilder()
            .WithValue("performanceCriteria2.1")
            .WithPosition(1)
            .Build();
        var competencyElement = new CompetencyElementDTOBuilder()
            .WithValue("competencyElement")
            .AddPerformanceCriteria(performanceCriteria)
            .WithPosition(1)
            .AddComplementaryInformation(competencyElementComplementaryInformation)
            .BuildCompetencyElement();
        var competencyElement2 = new CompetencyElementDTOBuilder()
            .WithValue("competencyElement2")
            .AddPerformanceCriteria(performanceCriteria2)
            .WithPosition(2)
            .BuildCompetencyElement();
        var competencyDTO = new CompetencyDTOBuilder()
            .AddCompetencyElements(competencyElement)
            .AddCompetencyElements(competencyElement2)
            .WithRealisationContexts(new List<ChangeableDTO> { realisationContext, realisationContext2 })
            .Build();

        return competencyDTO;
    }

    public static void AssertDraftCompetencyBasedOnResponse(CompetencyDTO originalCompetency, CompetencyDTO competencyToCompare, int targetVersionNumber = 1)
    {
        competencyToCompare.Should().BeEquivalentTo(originalCompetency, options =>
                    options
                    .Excluding(x => x.IsDraft)
                    .Excluding(x => x.ChangeRecordNumber)
                    .Excluding(x => x.ChangeRecordId)
                    .Excluding(x => x.Units)
                    .Excluding(x => x.CompetencyElements)
                    .Excluding(x => x.RealisationContexts));

        Assert.That(competencyToCompare.ChangeRecordNumber == targetVersionNumber);
        Assert.That(competencyToCompare.IsDraft, Is.True);
        Assert.That(competencyToCompare.ChangeRecordId, Is.TypeOf<Guid>());
        Assert.That(competencyToCompare.Units.Id, Is.TypeOf<Guid>());

        foreach (var r in competencyToCompare.RealisationContexts)
        {
            Assert.That(r.Id.HasValue && r.Id.Value != Guid.Empty, "guid is not empty");
            var realisationContext = originalCompetency.RealisationContexts.FirstOrDefault(x => x.Value == r.Value);
            r.Should().BeEquivalentTo(realisationContext, options =>
                options
                .Excluding(x => x.ComplementaryInformations)
                .Excluding(x => x.Id));

            AssertComplementaryInformation(realisationContext?.ComplementaryInformations?.FirstOrDefault(), r?.ComplementaryInformations?.FirstOrDefault(), targetVersionNumber);
        }

        // NOTE le foreach a un seul element
        foreach (var c in competencyToCompare.CompetencyElements)
        {
            Assert.That(c.Id.HasValue && c.Id.Value != Guid.Empty, "guid is not empty");
            var competencyElement = originalCompetency.CompetencyElements.FirstOrDefault(x => x.Value == c.Value);
            c.Should().BeEquivalentTo(competencyElement, options =>
                options
                .Excluding(x => x.Id)
                .Excluding(x => x.PerformanceCriterias)
                .Excluding(x => x.ComplementaryInformations));

            AssertComplementaryInformation(competencyElement?.ComplementaryInformations?.FirstOrDefault(), c?.ComplementaryInformations?.FirstOrDefault(), targetVersionNumber);

            foreach (var p in c?.PerformanceCriterias ?? [])
            {
                var performanceCriteria = competencyElement.PerformanceCriterias.FirstOrDefault(x => x.Value == p.Value);
                Assert.That(p.Id.HasValue && p.Id.Value != Guid.Empty, "guid is not empty");
                p.Should().BeEquivalentTo(performanceCriteria, options =>
                    options
                    .Excluding(x => x.Id)
                    .Excluding(x => x.ComplementaryInformations));

                AssertComplementaryInformation(performanceCriteria?.ComplementaryInformations?.FirstOrDefault(), p?.ComplementaryInformations?.FirstOrDefault(), targetVersionNumber);
            }
        }
    }


    public static void AssertComplementaryInformation(ComplementaryInformationDTO? originalComplementaryInformation, ComplementaryInformationDTO? complementaryInformation, int targetVersion = 1)
    {
        if (originalComplementaryInformation == null && complementaryInformation == null) return;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(complementaryInformation?.Id != null && complementaryInformation?.Id != Guid.Empty, $"guid is not empty");
            Assert.That(complementaryInformation?.ChangeRecordNumber != null, $"changeRecord is not found");
            complementaryInformation?.ChangeRecordNumber.Should().Be(targetVersion, $"changeRecord version should be {targetVersion}");
        }

        complementaryInformation.Should().BeEquivalentTo(originalComplementaryInformation, options =>
           options
           .Excluding(x => x.Id)
           .Excluding(x => x.ChangeRecordNumber)
           .Excluding(x => x.CreatedBy)
           .Excluding(x => x.CreatedOn));
        complementaryInformation.CreatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(30), "CreatedOn should be set to current time");
        complementaryInformation.CreatedBy.Id.Should().NotBeNull();
    }
}
