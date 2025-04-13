using AutoMapper;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Mappings;
using Pdc.Tests.Builders.Models;

namespace Pdc.Tests.Mapping;

internal class EntityMappingTests
{

    IMapper _mapper;
    private MinisterialCompetency _ministerialCompetency;
    private User _user;
    [SetUp]
    public void Setup()
    {
        _user = new UserBuilder()
            .Build();

        ComplementaryInformation realisationContextComplementaryInformation = new ComplementaryInformationBuilder()
            .WithCreatedBy(_user)
            .Build();

        ComplementaryInformation performanceCriteriaComplementaryInformation = new ComplementaryInformationBuilder()
            .WithCreatedBy(_user)
            .Build();

        ComplementaryInformation ministerialCompetencyElelementComplementaryInformation = new ComplementaryInformationBuilder()
            .WithCreatedBy(_user)
            .Build();

        RealisationContext realisationContext = new RealisationContextBuilder()
            .AddComplementaryInformations(realisationContextComplementaryInformation)
            .Build();

        PerformanceCriteria performanceCriteria = new PerformanceCriteriaBuilder()
            .AddComplementaryInformations(performanceCriteriaComplementaryInformation)
            .Build();

        MinisterialCompetencyElement competencyElement = new MinisterialCompetencyElementBuilder()
            .AddPerformanceCriteria(performanceCriteria)
            .AddComplementaryInformation(ministerialCompetencyElelementComplementaryInformation)
            .Build();

        _ministerialCompetency = new MinisterialCompetencyBuilder()
            .WithUnits(new Units(10))
            .WithProgramOfStudyCode("POS1234")
            .WithIsMandatory(false)
            .WithIsOptionnal(true)
            .WithStatementOfCompetency("Test Statement")
            .AddRealisationContexts(realisationContext)
            .AddCompetencyElements(competencyElement)
            .Build();

        // Initialize mapper
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
    }

    [Test]
    public async Task MappingMinisterialCompetency_ShouldKeepReferenceChangeRecordOnlyOnce()
    {
        _ministerialCompetency.SetVersion(new ChangeRecord());
        var entity = _mapper.Map<MinisterialCompetency>(_ministerialCompetency);

        // Assert
        Assert.That(entity, Is.Not.Null);
        Assert.That(entity, Is.InstanceOf<MinisterialCompetency>());

        Assert.That(entity.CompetencyElements, Is.Not.Null);
        Assert.That(entity.CompetencyElements.Count, Is.EqualTo(1));
        Assert.That(entity.CompetencyElements[0], Is.InstanceOf<MinisterialCompetencyElement>());
        Assert.That(entity.CompetencyElements[0].ComplementaryInformations, Is.Not.Null);
        Assert.That(entity.CompetencyElements[0].ComplementaryInformations.Count, Is.EqualTo(1));

        // Check that the first element is a ComplementaryInformation
        Assert.That(entity.CompetencyElements[0].ComplementaryInformations[0], Is.InstanceOf<ComplementaryInformation>());
        Assert.That(entity.CompetencyElements[0].ComplementaryInformations[0].WrittenOnVersion, Is.Not.Null);
        Assert.That(entity.CompetencyElements[0].ComplementaryInformations[0].CreatedBy, Is.Not.Null);
        Assert.That(entity.CompetencyElements[0].ComplementaryInformations[0].WrittenOnVersion, Is.InstanceOf<ChangeRecord>());

        Assert.That(entity.RealisationContexts, Is.Not.Null);
        Assert.That(entity.RealisationContexts.Count, Is.EqualTo(1));
        Assert.That(entity.RealisationContexts[0], Is.InstanceOf<RealisationContext>());

        // Check that the first element is a ComplementaryInformation
        Assert.That(entity.RealisationContexts[0].ComplementaryInformations[0], Is.InstanceOf<ComplementaryInformation>());
        Assert.That(entity.RealisationContexts[0].ComplementaryInformations[0].WrittenOnVersion, Is.Not.Null);
        Assert.That(entity.RealisationContexts[0].ComplementaryInformations[0].CreatedBy, Is.Not.Null);
        Assert.That(entity.RealisationContexts[0].ComplementaryInformations[0].WrittenOnVersion, Is.InstanceOf<ChangeRecord>());
        // The version is the same object
        Assert.That(entity.RealisationContexts[0].ComplementaryInformations[0].WrittenOnVersion == entity.CompetencyElements[0].ComplementaryInformations[0].WrittenOnVersion);
    }

    [Test]
    public async Task MappingIndentityUserEntity_ShouldMapUserCorrectly()
    {
        var user = _mapper.Map<User>(_user);

        Assert.That(user.Email == _user.Email);
        // TODO Assert.That(user.DisplayName == _identityUserEntity.Name);
        Assert.That(user.Id == _user.Id);
    }
}
