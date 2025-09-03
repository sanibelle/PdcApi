using AutoMapper;
using FluentValidation;
using Moq;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Application.Mappings;
using Pdc.Application.UseCase;
using Pdc.Application.Validators;
using Pdc.Domain.Enums;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Exceptions;
using TestDataSeeder.Builders.DTOS;
using TestDataSeeder.Builders.Models;

namespace Pdc.Tests.UnitTests;

public class MinisterialCompetencyTest
{
    //// Repository mocks
    Mock<IProgramOfStudyRespository> _programOfStudyRepositoryMock;
    Mock<ICompetencyRepository> _competencyRepositoryMock;

    //// Competency use cases
    ICreateCompetencyUseCase _createCompetencyUseCase;
    IUpdateDraftV1CompetencyUseCase _updateDraftV1CompetencyUseCase;
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
            .WithIsOptionnal(true)
            .WithStatementOfCompetency("Test Statement")
            .AddRealisationContexts(_realisationContext)
            .AddCompetencyElements(_competencyElement)
            .Build();

        _competency2 = new MinisterialCompetencyBuilder()
            .WithCode(codeOfAFakeCompetency2)
            .WithUnits(new Units(11))
            .WithProgramOfStudyCode("POS2345")
            .WithIsMandatory(true)
            .WithIsOptionnal(false)
            .WithStatementOfCompetency("Test Statement 2")
            .AddRealisationContexts(_realisationContext)
            .AddCompetencyElements(_competencyElement)
            .Build();

        _competencyToUpdateV1Draft = new MinisterialCompetencyBuilder()
            .WithCurrentVersion(
                new ChangeRecordBuilder().WithIsDraft(true)
                .WithVersionNumber(1)
                .Build())
            .WithCode("v1draft")
            .WithProgramOfStudyCode("POS1234")
            .WithStatementOfCompetency("UpdateMe")
            .WithIsMandatory(false)
            .WithIsOptionnal(true)
            .Build();

        _competencyToUpdateV1NotDraft = new MinisterialCompetencyBuilder()
            .WithCurrentVersion(
                new ChangeRecordBuilder().WithIsDraft(false)
                .WithVersionNumber(1)
                .Build())
            .WithCode("v1!Draft")
            .WithProgramOfStudyCode("POS1234")
            .WithStatementOfCompetency("UpdateMe")
            .WithIsMandatory(false)
            .WithIsOptionnal(true)
            .Build();

        _competencyToUpdateV2Draft = new MinisterialCompetencyBuilder()
            .WithCurrentVersion(
                new ChangeRecordBuilder().WithIsDraft(true)
                .WithVersionNumber(2)
                .Build())
            .WithCode("v2Draft")
            .WithProgramOfStudyCode("POS1234")
            .WithStatementOfCompetency("UpdateMe")
            .WithIsMandatory(false)
            .WithIsOptionnal(true)
            .Build();

        _competencyToUpdateV2NotDraft = new MinisterialCompetencyBuilder()
            .WithCurrentVersion(
                new ChangeRecordBuilder().WithIsDraft(false)
                .WithVersionNumber(2)
                .Build())
            .WithCode("v2!Draft")
            .WithProgramOfStudyCode("POS1234")
            .WithStatementOfCompetency("UpdateMe")
            .WithIsMandatory(false)
            .WithIsOptionnal(true)
            .Build();


        // Initialize repository mocks
        //    _programOfStudyRepositoryMock = new Mock<IProgramOfStudyRespository>();
        _competencyRepositoryMock = new Mock<ICompetencyRepository>();
        _programOfStudyRepositoryMock = new Mock<IProgramOfStudyRespository>();

        // Initialize mapper
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();

        //    // Initialize validators
        //    _programValidator = new UpsertProgramOfStudyValidation();
        _competencyValidator = new CompetencyValidation();

        //    // Initialize competency use cases
        _createCompetencyUseCase = new CreateCompetency(_competencyRepositoryMock.Object, _programOfStudyRepositoryMock.Object, _mapper, _competencyValidator);
        _updateDraftV1CompetencyUseCase = new UpdateDraftV1Competency(_competencyRepositoryMock.Object, _mapper, _competencyValidator);
        //    _deleteCompetencyUseCase = new DeleteCompetency(_competencyRepositoryMock.Object);
        //    _getAllCompetencyUseCase = new GetAllCompetency(_competencyRepositoryMock.Object, _mapper);

        // Setup Program of Study Repository mock
        _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(_codeOfAFakeProgram))).ReturnsAsync(_programOfSudy);

        // Setup Competency Repository mock
        _competencyRepositoryMock.Setup(repo => repo.Add(It.IsAny<ProgramOfStudy>(), It.IsAny<MinisterialCompetency>(), It.IsAny<User>())).ReturnsAsync(_competency2);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _codeOfAFakeProgram), It.Is<string>(x => x == _competency1.Code))).ReturnsAsync(_competency1);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _codeOfAFakeProgram), It.Is<string>(x => x != _competency1.Code))).Throws(new EntityNotFoundException(nameof(CompetencyEntity), _competency2.Code));
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _codeOfAFakeProgram), It.Is<string>(x => x == _competencyToUpdateV1Draft.Code))).ReturnsAsync(_competencyToUpdateV1Draft);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _codeOfAFakeProgram), It.Is<string>(x => x == _competencyToUpdateV1NotDraft.Code))).ReturnsAsync(_competencyToUpdateV1NotDraft);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _codeOfAFakeProgram), It.Is<string>(x => x == _competencyToUpdateV2Draft.Code))).ReturnsAsync(_competencyToUpdateV2Draft);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _codeOfAFakeProgram), It.Is<string>(x => x == _competencyToUpdateV2NotDraft.Code))).ReturnsAsync(_competencyToUpdateV2NotDraft);
        _competencyRepositoryMock.Setup(repo => repo.Update(It.IsAny<MinisterialCompetency>())).ReturnsAsync(_competencyToUpdateV1Draft);
        //    _competencyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<MinisterialCompetency> { competency1, competency2 });
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(program1.Code, competency1.Code)).ReturnsAsync(competency1);
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(program2.Code, competency2.Code)).ReturnsAsync(competency2);
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.IsAny<string>(), It.IsIn(codeOfAFakeCompetency)))
        //        .Throws(new EntityNotFoundException(nameof(MinisterialCompetency), codeOfAFakeCompetency));



    }

    [Test]
    public async Task CreateMinisterialCompetency_ShouldReturnMinisterialCompetency()
    {
        var complementaryInformation = new ComplementaryInformationDTOBuilder()
            .WithId(_complementaryInformation.Id)
            .WithVersionNumber(1)
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
        Assert.That(result.RealisationContexts.First()?.ComplementaryInformations?.First().WrittenOnVersion == _changeRecord.VersionNumber, "RealisationContext complementary information version number matches");
        Assert.That(result.CompetencyElements.First().Id == competencyElement.Id, "competencyElement is returned");
        Assert.That(result.CompetencyElements.First().PerformanceCriterias.First().Id == performanceCriteria.Id, "competencyElement is returned");
        Assert.That(result.CompetencyElements.First()?.ComplementaryInformations?.First().Id == complementaryInformation.Id, "ComplementaryInformation is returned");
        Assert.That(result.CompetencyElements.First().ComplementaryInformations.First().ModifiedOn.HasValue, "ComplementaryInformation ModifiedOn is prsent");
        Assert.That(result.CompetencyElements.First().ComplementaryInformations.First().WrittenOnVersion == _changeRecord.VersionNumber, "ComplementaryInformation version is returned");
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
            .WithVersionNumber(1)
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

    [Test]
    public async Task GivenDraftV1MinisterialCompetency_WhenUpdatingDraft_ThenItShouldUpdate()
    {
        // Assert

        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithCode(_competencyToUpdateV1Draft.Code)
            .Build();
        var result = await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO);
    }

    [Test]
    public async Task GivenMinisterialCompetency_WhenUpdatingCompetencyCode_ThenItShouldNotUpdate()
    {
        // Assert

        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithCode(_competencyToUpdateV1Draft.Code)
            .Build();
        var result = await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO);
        Assert.That(result.VersionNumber == 1, "Updating a draft V1 competency should not create a new version");
        _competencyRepositoryMock.Verify(repo => repo.Update(It.IsAny<MinisterialCompetency>()), Times.Once);
    }

    [Test]
    public async Task GivenDraftV1MinisterialCompetency_WhenUpdatingNotDraft_ThenItShouldThrowAnException()
    {
        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithCode(_competencyToUpdateV1NotDraft.Code)
            .Build();

        // Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO));
    }

    [Test]
    public async Task GivenDraftV2MinisterialCompetency_WhenUpdatingDraft_ThenItShouldThrowAnException()
    {
        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithCode(_competencyToUpdateV2Draft.Code)
            .Build();

        // Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO));
    }

    [Test]
    public async Task GivenDraftV2MinisterialCompetency_WhenUpdatingNotDraft_ThenItShouldThrowAnException()
    {
        CompetencyDTO competencyDTO = new CompetencyDTOBuilder()
            .WithCode(_competencyToUpdateV2NotDraft.Code)
            .Build();

        // Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _updateDraftV1CompetencyUseCase.Execute(_codeOfAFakeProgram, competencyDTO.Code, competencyDTO));
    }

    //TODO gestion de la version. Quand on crée un programme, on crée une nouvelle version
    //TODO validate position exists? plus dans le E2E
    //TODO ajouter des change details à une version
    //TODO aller chercher une compétence par version
    // Avoir un concept de brouillon (change pas de version) et de propre (impossible de modifier, on crée une nouvelle version)

    //[Test]
    //public async Task DeleteProgramOfStudy_ShouldCallRepositoryDelete()
    //{
    //    // Act
    //    await _deleteProgramOfStudyUseCase.Execute(program1.Code);

    //    // Assert
    //    _programOfStudyRepositoryMock.Verify(repo => repo.Delete(program1.Code), Times.Once);
    //}

    //[Test]
    //public async Task GetAllProgramOfStudy_ShouldReturnAllPrograms()
    //{
    //    // Act
    //    var result = await _getAllProgramOfStudyUseCase.Execute();

    //    // Assert
    //    Assert.That(result.Count == 2, "Got 2 programs");
    //    Assert.That(program1.Code == result[0].Code, "Both programs are returned");
    //    Assert.That(program2.Code == result[1].Code, "Both programs are returned");
    //}

    //[Test]
    //public async Task UpdateProgramOfStudy_ShouldCallRepositoryUpdate()
    //{
    //    // Arrange
    //    CreateProgramOfStudyDTO updateProgramDto = new CreateProgramOfStudyDTOBuilder()
    //        .WithCode("420.B0")
    //        .WithName("UpdatedName")
    //        .WithProgramType(ProgramType.DEC)
    //        .WithMonthsDuration(36)
    //        .WithSpecificDurationHours(2010)
    //        .WithTotalDurationHours(5730)
    //        .WithPublishedOn(new DateOnly(2020, 01, 01))
    //        .WithOptionalUnits(new Units(16, 2, 3))
    //        .WithSpecificUnits(new Units(26, 2, 3))
    //        .Build();

    //    // Act
    //    var result = await _updateProgramOfStudyUseCase.Execute(program1.Code, updateProgramDto);

    //    // Assert
    //    _programOfStudyRepositoryMock.Verify(repo => repo.Update(It.Is<ProgramOfStudy>(p => p.Code == program1.Code && p.Name == updateProgramDto.Name)), Times.Once);
    //    Assert.That(result.Name == program1.Name, "Program name is updated");
    //}

    //// Competency tests
    //[Test]
    //public async Task CreateCompetency_ShouldReturnCreatedCompetency()
    //{
    //    // Arrange
    //    CompetencyDTO createCompetencyDto = new CompetencyDTO
    //    {
    //        Code = "0123",
    //        Name = "Develop web applications",
    //        CompetencyElements = new List<MinisterialCompetencyElementDTO>
    //        {
    //            new MinisterialCompetencyElementDTO
    //            {
    //                Code = "01",
    //                Description = "Plan the development of a web application",
    //                PerformanceCriterias = new List<PerformanceCriteriaDTO>
    //                {
    //                    new PerformanceCriteriaDTO { Description = "Accurate assessment of client needs" }
    //                }
    //            }
    //        }
    //    };

    //    // Act
    //    var result = await _createCompetencyUseCase.Execute(program1.Code, createCompetencyDto);

    //    // Assert
    //    _competencyRepositoryMock.Verify(repo => repo.Add(It.IsAny<MinisterialCompetency>()), Times.Once);
    //    Assert.That(result.Code, Is.EqualTo(competency1.Code));
    //    Assert.That(result.Name, Is.EqualTo(competency1.Name));
    //}

    //[Test]
    //public async Task GetCompetency_ShouldReturnCompetency()
    //{
    //    // Act
    //    var result = await _getCompetencyUseCase.Execute(program1.Code, competency1.Code);

    //    // Assert
    //    _competencyRepositoryMock.Verify(repo => repo.FindByCode(program1.Code, competency1.Code), Times.Once);
    //    Assert.That(result.Code, Is.EqualTo(competency1.Code));
    //    Assert.That(result.Name, Is.EqualTo(competency1.Name));
    //}

    //[Test]
    //public async Task UpdateCompetency_ShouldCallRepositoryUpdate()
    //{
    //    // Arrange
    //    CompetencyDTO updateCompetencyDto = new CompetencyDTO
    //    {
    //        Code = "0123",
    //        Name = "Updated Competency Name",
    //        CompetencyElements = new List<MinisterialCompetencyElementDTO>()
    //    };

    //    // Act
    //    var result = await _updateCompetencyUseCase.Execute(program1.Code, competency1.Code, updateCompetencyDto);

    //    // Assert
    //    _competencyRepositoryMock.Verify(repo => repo.Update(It.IsAny<MinisterialCompetency>()), Times.Once);
    //    Assert.That(result.Code, Is.EqualTo(competency1.Code));
    //}

    //[Test]
    //public async Task DeleteCompetency_ShouldCallRepositoryDelete()
    //{
    //    // Act
    //    await _deleteCompetencyUseCase.Execute(program1.Code, competency1.Code);

    //    // Assert
    //    _competencyRepositoryMock.Verify(repo => repo.Delete(program1.Code, competency1.Code), Times.Once);
    //}

    //[Test]
    //public void GetCompetency_WithInvalidCode_ShouldThrowException()
    //{
    //    // Act & Assert
    //    Assert.ThrowsAsync<EntityNotFoundException>(async () =>
    //        await _getCompetencyUseCase.Execute(program1.Code, codeOfAFakeCompetency));
    //}

    //[Test]
    //public async Task GetCompetencyElements_ShouldReturnElements()
    //{
    //    // Act
    //    var result = await _getCompetencyUseCase.Execute(program1.Code, competency1.Code);

    //    // Assert
    //    Assert.That(result.CompetencyElements.Count, Is.EqualTo(competency1.CompetencyElements.Count()));
    //    Assert.That(result.CompetencyElements[0].Code, Is.EqualTo("01"));
    //    Assert.That(result.CompetencyElements[0].Description, Is.EqualTo("Plan the development of a web application"));
    //}

    //[Test]
    //public async Task GetCompetencyPerformanceCriteria_ShouldReturnCriteria()
    //{
    //    // Act
    //    var result = await _getCompetencyUseCase.Execute(program1.Code, competency1.Code);

    //    // Assert
    //    var element = result.CompetencyElements[0];
    //    Assert.That(element.PerformanceCriterias.Count, Is.EqualTo(1));
    //    Assert.That(element.PerformanceCriterias[0].Description, Is.EqualTo("Accurate assessment of client needs"));
    //}
}
