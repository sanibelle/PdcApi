using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Application.Mappings;
using Pdc.Application.Services.Competency;
using Pdc.Application.UseCases;
using Pdc.Application.UseCases.Competency;
using Pdc.Application.Validators;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Enums;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using TestDataSeeder.Builders.DTOS;
using TestDataSeeder.Builders.Models;

namespace Pdc.Tests.UnitTests;

public class MinisterialCompetencyTest
{
    //// Repository mocks
    Mock<IProgramOfStudyRepository> _programOfStudyRepositoryMock;
    Mock<ICompetencyRepository> _competencyRepositoryMock;
    Mock<CompetencyService> _competencyServiceMock;

    //// Competency use cases
    IAddCompetencyUseCase _createCompetencyUseCase;
    IUpdateDraftV1CompetencyUseCase _updateDraftV1CompetencyUseCase;
    IUpdatePublishedCompetencyUseCase _updateDraftCompetencyUseCase;
    // TODO tester la methode GET
    //IGetCompetencyUseCase _getCompetencyUseCase;
    //IDeleteCompetencyUseCase _deleteCompetencyUseCase;

    //// Mapper and validators
    IMapper _mapper;
    IValidator<CompetencyDTO> _competencyValidator;

    //// Test data
    string _codeOfAFakeProgram = "Prog11";
    string codeOfAFakeCompetency1 = "Comp11";
    string codeOfAFakeCompetency2 = "Comp22";
    private User _user;
    private ProgramOfStudy _programOfSudy;

    private RealisationContext _realisationContext;
    private ChangeRecord _changeRecord;
    private ComplementaryInformation _complementaryInformation;
    private PerformanceCriteria _performanceCriteria;
    private MinisterialCompetencyElement _competencyElement;
    private CompetencyService _competencyService;

    private MinisterialCompetency _competency1, _competency2, _competencyToUpdateV1Draft, _competencyToUpdateV1NotDraft, _competencyToUpdateV2Draft, _competencyToUpdateV2NotDraft;

    [SetUp]
    public void Setup()
    {
        _user = new UserBuilder()
            .Build();

        _programOfSudy = new ProgramOfStudyBuilder()
            .WithCode(_codeOfAFakeProgram)
            .WithName("Techniques de l'informatique")
            .WithProgramType(ProgramType.DEC)
            .WithMonthsDuration(36)
            .WithSpecificDurationHours(2010)
            .WithTotalDurationHours(5730)
            .WithPublishedOn(new DateOnly(2020, 01, 01))
            .WithCompetencies(new List<MinisterialCompetency>())
            .Build();

        _changeRecord = new ChangeRecordBuilder()
            .WithValidatedBy(_user)
            .Build();

        _complementaryInformation = new ComplementaryInformationBuilder()
            .WithChangeRecord(_changeRecord)
            .WithCreatedBy(_user)
            .Build();

        _realisationContext = new RealisationContextBuilder()
            .AddComplementaryInformations(_complementaryInformation)
            .Build();

        _performanceCriteria = new PerformanceCriteriaBuilder()
            .AddComplementaryInformations(_complementaryInformation)
            .Build();

        _competencyElement = new MinisterialCompetencyElementBuilder()
            .AddPerformanceCriteria(_performanceCriteria)
            .AddComplementaryInformation(_complementaryInformation)
            .Build();

        _competency1 = new MinisterialCompetencyBuilder()
            .WithCode(codeOfAFakeCompetency1)
            .WithUnits(new Units(10))
            .WithProgramOfStudyCode("POS1234")
            .WithIsMandatory(false)
            .WithIsOptinoal(true)
            .WithStatementOfCompetency("Test Statement")
            .AddRealisationContexts(_realisationContext)
            .AddCompetencyElements(_competencyElement)
            .Build();

        _competency2 = new MinisterialCompetencyBuilder()
            .WithCode(codeOfAFakeCompetency2)
            .WithUnits(new Units(11))
            .WithProgramOfStudyCode("POS2345")
            .WithIsMandatory(true)
            .WithIsOptinoal(false)
            .WithStatementOfCompetency("Test Statement 2")
            .AddRealisationContexts(_realisationContext)
            .AddCompetencyElements(_competencyElement)
            .Build();

        _competencyToUpdateV1Draft = new MinisterialCompetencyBuilder()
            .WithCurrentChangeREcord(
                new ChangeRecordBuilder().WithIsDraft(true)
                .WithChangeRecordNumber(1)
                .Build())
            .WithCode("v1draft")
            .WithProgramOfStudyCode("POS1234")
            .WithStatementOfCompetency("UpdateMe")
            .WithIsMandatory(false)
            .WithIsOptinoal(true)
            .Build();

        _competencyToUpdateV1NotDraft = new MinisterialCompetencyBuilder()
            .WithCurrentChangeREcord(
                new ChangeRecordBuilder().WithIsDraft(false)
                .WithChangeRecordNumber(1)
                .Build())
            .WithCode("v1!Draft")
            .WithProgramOfStudyCode("POS1234")
            .WithStatementOfCompetency("UpdateMe")
            .WithIsMandatory(false)
            .WithIsOptinoal(true)
            .Build();

        _competencyToUpdateV2Draft = new MinisterialCompetencyBuilder()
            .WithCurrentChangeREcord(
                new ChangeRecordBuilder().WithIsDraft(true)
                .WithChangeRecordNumber(2)
                .Build())
            .WithCode("v2Draft")
            .WithProgramOfStudyCode("POS1234")
            .WithStatementOfCompetency("UpdateMe")
            .WithIsMandatory(false)
            .WithIsOptinoal(true)
            .Build();

        _competencyToUpdateV2NotDraft = new MinisterialCompetencyBuilder()
            .WithCurrentChangeREcord(
                new ChangeRecordBuilder().WithIsDraft(false)
                .WithChangeRecordNumber(2)
                .Build())
            .WithCode("v2!Draft")
            .WithProgramOfStudyCode("POS1234")
            .WithStatementOfCompetency("UpdateMe")
            .WithIsMandatory(false)
            .WithIsOptinoal(true)
            .Build();


        // Initialize repository mocks
        //    _programOfStudyRepositoryMock = new Mock<IProgramOfStudyRepository>();
        _competencyRepositoryMock = new Mock<ICompetencyRepository>();
        _programOfStudyRepositoryMock = new Mock<IProgramOfStudyRepository>();
        _competencyServiceMock = new Mock<CompetencyService>();

        // Initialize mapper
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.LicenseKey = Environment.GetEnvironmentVariable("AutoMapper:LicenseKey");
            cfg.AddProfile<MappingProfile>();
        }, LoggerFactory.Create(_ => { })).CreateMapper();

        //    // Initialize validators
        //    _programValidator = new UpsertProgramOfStudyValidation();
        _competencyValidator = new CompetencyValidation();

        //    // Initialize competency use cases
        _createCompetencyUseCase = new AddCompetency(_competencyRepositoryMock.Object, _programOfStudyRepositoryMock.Object, _mapper, _competencyValidator);
        _updateDraftV1CompetencyUseCase = new UpdateDraftV1Competency(_competencyRepositoryMock.Object, _mapper, _competencyService, _competencyValidator);
        //    _deleteCompetencyUseCase = new DeleteCompetency(_competencyRepositoryMock.Object);
        //    _getAllCompetencyUseCase = new GetAllCompetency(_competencyRepositoryMock.Object, _mapper);

        // Setup Program of Study Repository mock
        _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(_codeOfAFakeProgram))).ReturnsAsync(_programOfSudy);

        // Setup Competency Repository mock
        _competencyRepositoryMock.Setup(repo => repo.Add(It.IsAny<ProgramOfStudy>(), It.IsAny<MinisterialCompetency>())).ReturnsAsync(_competency2);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _competency1.Code))).ReturnsAsync(_competency1);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x != _competency1.Code))).Throws(new NotFoundException(nameof(CompetencyEntity), _competency2.Code));
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _competencyToUpdateV1Draft.Code))).ReturnsAsync(_competencyToUpdateV1Draft);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _competencyToUpdateV1NotDraft.Code))).ReturnsAsync(_competencyToUpdateV1NotDraft);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _competencyToUpdateV2Draft.Code))).ReturnsAsync(_competencyToUpdateV2Draft);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _competencyToUpdateV2NotDraft.Code))).ReturnsAsync(_competencyToUpdateV2NotDraft);
        _competencyRepositoryMock.Setup(repo => repo.Update(It.IsAny<MinisterialCompetency>())).ReturnsAsync(_competencyToUpdateV1Draft);
        //    _competencyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<MinisterialCompetency> { competency1, competency2 });
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(program1.Code, competency1.Code)).ReturnsAsync(competency1);
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(program2.Code, competency2.Code)).ReturnsAsync(competency2);
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.IsAny<string>(), It.IsIn(codeOfAFakeCompetency)))
        //        .Throws(new NotFoundException(nameof(MinisterialCompetency), codeOfAFakeCompetency));



    }

    [Test]
    public async Task CreateMinisterialCompetency_ShouldReturnMinisterialCompetency()
    {
        var complementaryInformation = new ComplementaryInformationDTOBuilder()
            .WithId(_complementaryInformation.Id)
            .WithChangeRecordNumber(1)
            .Build();
        var realisationContext = new ChangeableDTOBuilder()
            .AddComplementaryInformation(complementaryInformation)
            .WithId(_realisationContext.Id)
            .Build();
        var performanceCriteria = new ChangeableDTOBuilder()
            .WithPosition(1)
            .AddComplementaryInformation(complementaryInformation)
            .WithId(_performanceCriteria.Id)
            .Build();
        var competencyElement = new CompetencyElementDTOBuilder()
            .WithPosition(1)
            .AddPerformanceCriteria(performanceCriteria)
            .WithId(_competencyElement.Id)
            .AddComplementaryInformation(complementaryInformation)
            .BuildCompetencyElement();
        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithRealisationContexts(new List<ChangeableDTO> { realisationContext })
            .WithCompetencyElements(new List<CompetencyElementDTO> { competencyElement })
            .WithCode(_competency2.Code)
            .Build();
        // Act
        CompetencyDTO result = await _createCompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO, _user);

        // Assert
        Assert.That(result.Code == _competency2.Code, "Code is returned");
        Assert.That(result.RealisationContexts.First().Id == realisationContext.Id, "RealisationContext is returned");
        Assert.That(result.RealisationContexts.First()?.ComplementaryInformations?.First().ChangeRecordNumber == _changeRecord.ChangeRecordNumber, "RealisationContext complementary information changeRecord number matches");
        Assert.That(result.CompetencyElements.First().Id == competencyElement.Id, "competencyElement is returned");
        Assert.That(result.CompetencyElements.First().PerformanceCriterias.First().Id == performanceCriteria.Id, "competencyElement is returned");
        Assert.That(result.CompetencyElements.First()?.ComplementaryInformations?.First().Id == complementaryInformation.Id, "ComplementaryInformation is returned");
        Assert.That(result.CompetencyElements.First().ComplementaryInformations.First().ModifiedOn.HasValue, "ComplementaryInformation ModifiedOn is prsent");
        Assert.That(result.CompetencyElements.First().ComplementaryInformations.First().ChangeRecordNumber == _changeRecord.ChangeRecordNumber, "ComplementaryInformation changeRecord is returned");
    }

    [Test]
    public void CompetencyElements_ShouldHaveRightPosition()
    {
        var realisationContext = new ChangeableDTOBuilder()
            .Build();
        var performanceCriteria1 = new ChangeableDTOBuilder()
            .Build();
        var performanceCriteria2 = new ChangeableDTOBuilder()
            .Build();
        var competencyElement1 = new CompetencyElementDTOBuilder()
            .WithPerformanceCriterias(new List<ChangeableDTO> { performanceCriteria1, performanceCriteria2 })
            .BuildCompetencyElement();
        var competencyElement2 = new CompetencyElementDTOBuilder()
            .WithPerformanceCriterias(new List<ChangeableDTO> { performanceCriteria1, performanceCriteria2 })
            .BuildCompetencyElement();
        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithRealisationContexts(new List<ChangeableDTO> { realisationContext })
            .WithCompetencyElements(new List<CompetencyElementDTO> { competencyElement1, competencyElement2 })
            .WithCode(_competency1.Code)
            .Build();
        // Act
        var validation = new CompetencyValidation();

        Assert.That(validation.Validate(competencyDTO).Errors.Count == 6, "6 erreurs. Deux par criteres de performances plus deux elements competences");

        //Wrong positions
        performanceCriteria1.Position = 3;
        performanceCriteria2.Position = 1;
        competencyElement2.Position = 1;
        Assert.That(validation.Validate(competencyDTO).Errors.Count == 3, "3 erreurs. Une par criteres de performances plus une pour les competences");

        // RIght positions
        performanceCriteria1.Position = 2;
        performanceCriteria2.Position = 1;
        competencyElement1.Position = 1;
        competencyElement2.Position = 2;
        Assert.That(validation.Validate(competencyDTO).Errors.Count == 0, "Aucune erreur");
    }

    [Test]
    public void CompetencyElements_ShouldHavePosition()
    {
        var realisationContext = new ChangeableDTOBuilder()
            .WithId(_realisationContext.Id)
            .Build();
        var performanceCriteria = new ChangeableDTOBuilder()
            .WithId(_performanceCriteria.Id)
            .WithPosition(1)
            .Build();
        var competencyElement = new CompetencyElementDTOBuilder()
            .AddPerformanceCriteria(performanceCriteria)
            .WithId(_competencyElement.Id)
            .BuildCompetencyElement();
        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithRealisationContexts(new List<ChangeableDTO> { realisationContext })
            .WithCompetencyElements(new List<CompetencyElementDTO> { competencyElement })
            .WithCode(_competency1.Code)
            .Build();

        Assert.ThrowsAsync<ValidationException>(async () =>
                await _createCompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO, _user));
    }

    [Test]
    public void CreateMinisterialCompetency_ShouldHaveAtLeastOnePerformanceCriteria()
    {
        var complementaryInformation = new ComplementaryInformationDTOBuilder()
            .WithId(_complementaryInformation.Id)
            .WithChangeRecordNumber(1)
            .Build();
        var realisationContext = new ChangeableDTOBuilder()
            .AddComplementaryInformation(complementaryInformation)
            .WithId(_realisationContext.Id)
            .WithPosition(1)
            .Build();
        var competencyElement = new CompetencyElementDTOBuilder()
            .WithId(_competencyElement.Id)
            .WithPosition(1)
            .AddComplementaryInformation(complementaryInformation)
            .BuildCompetencyElement();
        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithRealisationContexts([realisationContext])
            .WithCompetencyElements([competencyElement])
            .WithCode(_competency2.Code)
            .Build();

        // Assert
        Assert.ThrowsAsync<ValidationException>(async () =>
                await _createCompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO, _user));
    }

    //[Test]
    //public async Task GivenDraftV1MinisterialCompetency_WhenUpdatingDraft_ThenItShouldUpdate()
    //{
    //    CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
    //        .WithCode(_competencyToUpdateV1Draft.Code)
    //        .Build();
    //    var result = await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO, _user);
    //    Assert.That(result.ChangeRecordNumber == 1);
    //    _competencyRepositoryMock.Verify(repo => repo.Update(It.IsAny<MinisterialCompetency>()), Times.Once);
    //}

    //[Test]
    //public async Task GivenMinisterialCompetency_WhenUpdatingCompetencyCode_ThenItShouldFail()
    //{
    //    // Assert

    //    CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
    //        .WithCode(_competencyToUpdateV1Draft.Code)
    //        .Build();
    //    var result = await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO, _user);
    //    Assert.That(result.ChangeRecordNumber == 1, "Updating a draft V1 competency should not create a new changeRecord");
    //}

    //[Test]
    //public async Task GivenDraftV1MinisterialCompetency_WhenUpdatingNotDraft_ThenItShouldThrowAnException()
    //{
    //    CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
    //        .WithCode(_competencyToUpdateV1NotDraft.Code)
    //        .Build();

    //    // Assert
    //    Assert.ThrowsAsync<InvalidOperationException>(async () =>
    //            await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO, _user));
    //}

    //[Test]
    //public async Task GivenDraftV2MinisterialCompetency_WhenUpdatingDraft_ThenItShouldThrowAnException()
    //{
    //    CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
    //        .WithCode(_competencyToUpdateV2Draft.Code)
    //        .Build();

    //    // Assert
    //    Assert.ThrowsAsync<InvalidOperationException>(async () =>
    //            await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO, _user));
    //}

    //[Test]
    //public async Task GivenDraftV2MinisterialCompetency_WhenUpdatingNotDraft_ThenItShouldThrowAnException()
    //{
    //    CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
    //        .WithCode(_competencyToUpdateV2NotDraft.Code)
    //        .Build();

    //    // Assert
    //    Assert.ThrowsAsync<InvalidOperationException>(async () =>
    //            await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO, _user));
    //}
}
