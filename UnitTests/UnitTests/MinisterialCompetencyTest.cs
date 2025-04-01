using AutoMapper;
using FluentValidation;
using Moq;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Application.Mappings;
using Pdc.Application.UseCase;
using Pdc.Application.Validators;
using Pdc.Domain.Enums;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Tests.Builders.DTOS;
using Pdc.Tests.Builders.Models;

namespace Pdc.Tests.UnitTests;

public class MinisterialCompetencyTest
{
    //// Repository mocks
    Mock<IProgramOfStudyRespository> _programOfStudyRepositoryMock;
    Mock<ICompetencyRespository> _competencyRepositoryMock;

    //// Competency use cases
    ICreateCompetencyUseCase _createCompetencyUseCase;
    //IGetCompetencyUseCase _getCompetencyUseCase;
    //IUpdateCompetencyUseCase _updateCompetencyUseCase;
    //IDeleteCompetencyUseCase _deleteCompetencyUseCase;

    //// Mapper and validators
    IMapper _mapper;
    IValidator<CompetencyDTO> _competencyValidator;

    //// Test data
    string _codeOfAFakeProgram = "Prog11";
    string codeOfAFakeCompetency1 = "Comp11";
    string codeOfAFakeCompetency2 = "Comp22";

    private ProgramOfStudy _programOfSudy;

    private RealisationContext _realisationContext;

    private MinisterialCompetencyElement _ministerialCompetencyElement;

    private MinisterialCompetency _competency1, _competency2;

    [SetUp]
    public void Setup()
    {
        _programOfSudy = new ProgramOfStudyBuilder()
    .WithCode(_codeOfAFakeProgram)
    .WithName("Techniques de l'informatique")
    .WithSanction(SanctionType.DEC)
    .WithMonthsDuration(36)
    .WithSpecificDurationHours(2010)
    .WithTotalDurationHours(5730)
    .WithPublishedOn(new DateOnly(2020, 01, 01))
    .WithCompetencies(new List<MinisterialCompetency>())
    .Build();

        _realisationContext = new RealisationContextBuilder()
            .Build();

        _realisationContext = new PerformanceCriteriaBuilder()
            .Build();

        _ministerialCompetencyElement = new MinisterialCompetencyElementBuilder()
            .WithPerformanceCriterias(new List<PerformanceCriteria>())
            .Build();

        _competency1 = new MinisterialCompetencyBuilder()
            .WithCode(codeOfAFakeCompetency1)
            .WithVersionNumber(1)
            .WithUnits(new Units(10))
            .WithProgramOfStudyCode("POS1234")
            .WithIsMandatory(false)
            .WithIsOptionnal(true)
            .WithStatementOfCompetency("Test Statement")
            .AddRealisationContexts(_realisationContext)
            .AddCompetencyElements(_ministerialCompetencyElement)
            .Build();

        _competency2 = new MinisterialCompetencyBuilder()
            .WithCode(codeOfAFakeCompetency2)
            .WithVersionNumber(1)
            .WithUnits(new Units(11))
            .WithProgramOfStudyCode("POS2345")
            .WithIsMandatory(true)
            .WithIsOptionnal(false)
            .WithStatementOfCompetency("Test Statement 2")
            .AddRealisationContexts(_realisationContext)
            .AddCompetencyElements(_ministerialCompetencyElement)
            .Build();

        // Initialize repository mocks
        //    _programOfStudyRepositoryMock = new Mock<IProgramOfStudyRespository>();
        _competencyRepositoryMock = new Mock<ICompetencyRespository>();
        _programOfStudyRepositoryMock = new Mock<IProgramOfStudyRespository>();

        // Initialize mapper
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();

        //    // Initialize validators
        //    _programValidator = new UpsertProgramOfStudyValidation();
        _competencyValidator = new CompetencyValidation();

        //    // Initialize competency use cases
        _createCompetencyUseCase = new CreateCompetency(_competencyRepositoryMock.Object, _programOfStudyRepositoryMock.Object, _mapper, _competencyValidator);
        //    _deleteCompetencyUseCase = new DeleteCompetency(_competencyRepositoryMock.Object);
        //    _getAllCompetencyUseCase = new GetAllCompetency(_competencyRepositoryMock.Object, _mapper);
        //    _updateCompetencyUseCase = new UpdateCompetency(_competencyRepositoryMock.Object, _mapper, _competencyValidator);

        // Setup Program of Study Repository mock
        _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(_codeOfAFakeProgram))).ReturnsAsync(_programOfSudy);
        //    _programOfStudyRepositoryMock.Setup(repo => repo.Update(It.IsAny<ProgramOfStudy>())).ReturnsAsync(program1);
        //    _programOfStudyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ProgramOfStudy> { program1, program2 });
        //    _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(program2.Code))).ReturnsAsync(program2);
        //    _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(codeOfAFakeProgram)))
        //        .Throws(new EntityNotFoundException(nameof(ProgramOfStudy), codeOfAFakeProgram));

        // Setup Competency Repository mock
        _competencyRepositoryMock.Setup(repo => repo.Add(It.IsAny<ProgramOfStudy>(), It.IsAny<MinisterialCompetency>())).ReturnsAsync(_competency2);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _codeOfAFakeProgram), It.Is<string>(x => x == _competency1.Code))).ReturnsAsync(_competency1);
        _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.Is<string>(x => x == _codeOfAFakeProgram), It.Is<string>(x => x != _competency1.Code))).Throws(new EntityNotFoundException(nameof(CompetencyEntity), _competency2.Code));
        //    _competencyRepositoryMock.Setup(repo => repo.Update(It.IsAny<MinisterialCompetency>())).ReturnsAsync(competency1);
        //    _competencyRepositoryMock.Setup(repo => repo.Delete(It.IsAny<string>(), It.IsAny<string>()));
        //    _competencyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<MinisterialCompetency> { competency1, competency2 });
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(program1.Code, competency1.Code)).ReturnsAsync(competency1);
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(program2.Code, competency2.Code)).ReturnsAsync(competency2);
        //    _competencyRepositoryMock.Setup(repo => repo.FindByCode(It.IsAny<string>(), It.IsIn(codeOfAFakeCompetency)))
        //        .Throws(new EntityNotFoundException(nameof(MinisterialCompetency), codeOfAFakeCompetency));



    }

    [Test]
    public async Task CreateMinisterialCompetency_ShouldReturnMinisterialCompetency()
    {
        var realisationContext = new ChangeableDTOBuilder()
            .WithId(_realisationContext.Id)
            .Build();
        var performanceCriteria = new ChangeableDTOBuilder()
            .WithId(_performanceCriteria.Id).Build();
        var competencyElement = new CompetencyElementDTOBuilder()
            .WithPerformanceCriterias(new List<ChangeableDTO> { performanceCriteria })
            .BuildCompetencyElement();
        CompetencyDTO createProgramDto = new CompetencyDTOBuilder()

            .WithRealisationContexts(new List<ChangeableDTO> { realisationContext })
            .WithCompetencyElements(new List<CompetencyElementDTO> { competencyElement })
            .WithCode(_competency2.Code)
            .Build();
        // Act
        var result = await _createCompetencyUseCase.Execute(_codeOfAFakeProgram, createProgramDto);

        // Assert
        Assert.That(createProgramDto.Code == _competency2.Code, "Code is returned");
        Assert.That(createProgramDto.RealisationContexts.First().Id == realisationContext.Id, "RealisationContext is returned");
        Assert.That(createProgramDto.CompetencyElements.First().Id == competencyElement.Id, "competencyElement is returned");
        Assert.That(createProgramDto.CompetencyElements.First().PerformanceCriterias.First().Id == competencyElement.Id, "competencyElement is returned");
        Assert.That(createProgramDto.CompetencyElements.First().PerformanceCriterias.First().Id == performanceCriteria.Id, "PerformanceCriteria is returned");
    }
    //TODO at least one competency element
    //TODO at least one performance criteria
    //TODO validate position exists? plus dans le E2E
    //TODO gestion de la version. Quand on crée un programme, on crée une nouvelle version
    //Les compétences sont liées à une version du programme???? Sinon, chaque compétence doit avoir une version
    //Valider la création de la version quand on crée un programme. La version est créée dans un service
    //L'ajout une compétence doit être liée à une version du programme
    // Mettre un todo pour parler du fait que l'ajout d'une version vient modifier le programme
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
    //        .WithSanction(SanctionType.DEC)
    //        .WithMonthsDuration(36)
    //        .WithSpecificDurationHours(2010)
    //        .WithTotalDurationHours(5730)
    //        .WithPublishedOn(new DateOnly(2020, 01, 01))
    //        .WithOptionnalUnits(new Units(16, 2, 3))
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
