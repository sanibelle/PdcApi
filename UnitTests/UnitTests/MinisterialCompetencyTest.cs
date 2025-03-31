using AutoMapper;
using FluentValidation;
using Moq;
using Pdc.Application.DTOS;
using Pdc.Application.Mappings;
using Pdc.Application.UseCase;
using Pdc.Application.Validators;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Tests.Builders.DTOS;

namespace Pdc.Tests.UnitTests;

public class MinisterialCompetencyTest
{
    //// Repository mocks
    //Mock<IProgramOfStudyRespository> _programOfStudyRepositoryMock;
    Mock<ICompetencyRespository> _competencyRepositoryMock;

    //// Program of study use cases
    ICreateCompetencyUseCase _createCompetencyUseCase;
    //IDeleteProgramOfStudyUseCase _deleteProgramOfStudyUseCase;
    //IGetAllProgramOfStudyUseCase _getAllProgramOfStudyUseCase;
    //IUpdateProgramOfStudyUseCase _updateProgramOfStudyUseCase;

    //// Competency use cases
    //ICreateCompetencyUseCase _createCompetencyUseCase;
    //IGetCompetencyUseCase _getCompetencyUseCase;
    //IUpdateCompetencyUseCase _updateCompetencyUseCase;
    //IDeleteCompetencyUseCase _deleteCompetencyUseCase;

    //// Mapper and validators
    IMapper _mapper;
    //IValidator<CreateProgramOfStudyDTO> _programValidator;
    IValidator<CreateCompetencyDTO> _competencyValidator;

    //// Test data
    string codeOfAFakeProgram = "FakeCode";
    string codeOfAFakeCompetency = "FakeCompCode";

    //private ProgramOfStudy program1 = new ProgramOfStudyBuilder()
    //    .WithCode("420.B0")
    //    .WithName("Techniques de l'informatique")
    //    .WithSanction(SanctionType.DEC)
    //    .WithMonthsDuration(36)
    //    .WithSpecificDurationHours(2010)
    //    .WithTotalDurationHours(5730)
    //    .WithPublishedOn(new DateOnly(2020, 01, 01))
    //    .WithCompetencies(new List<MinisterialCompetency>())
    //    .Build();

    //private ProgramOfStudy program2 = new ProgramOfStudyBuilder()
    //    .WithCode("570.G0")
    //    .WithName("Techniques de design graphique")
    //    .WithSanction(SanctionType.DEC)
    //    .WithMonthsDuration(36)
    //    .WithSpecificDurationHours(1980)
    //    .WithTotalDurationHours(5670)
    //    .WithPublishedOn(new DateOnly(2020, 01, 02))
    //    .WithCompetencies(new List<MinisterialCompetency>())
    //    .Build();

    //private MinisterialCompetency competency1 = new MinisterialCompetencyBuilder()
    //{
    //    Code = "0123",
    //    Name = "Develop web applications",
    //    ProgramOfStudyCode = "420.B0",
    //    CompetencyElements = new List<MinisterialCompetencyElement>
    //    {
    //        new MinisterialCompetencyElement
    //        {
    //            Code = "01",
    //            Description = "Plan the development of a web application",
    //            PerformanceCriterias = new List<PerformanceCriteria>
    //            {
    //                new PerformanceCriteria { Description = "Accurate assessment of client needs" }
    //            }
    //        }
    //    }
    //};

    //private MinisterialCompetency competency2 = new MinisterialCompetency
    //{
    //    Code = "0234",
    //    Name = "Design UI/UX interfaces",
    //    ProgramOfStudyCode = "570.G0",
    //    CompetencyElements = new List<MinisterialCompetencyElement>()
    //};

    [SetUp]
    public void Setup()
    {
        // Initialize repository mocks
        //    _programOfStudyRepositoryMock = new Mock<IProgramOfStudyRespository>();
        _competencyRepositoryMock = new Mock<ICompetencyRespository>();

        // Initialize mapper
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();

        //    // Initialize validators
        //    _programValidator = new UpsertProgramOfStudyValidation();
        _competencyValidator = new CompetencyValidation();

        //    // Initialize program of study use cases
        //    _createProgramOfStudyUseCase = new CreateProgramOfStudy(_programOfStudyRepositoryMock.Object, _mapper, _programValidator);
        //    _deleteProgramOfStudyUseCase = new DeleteProgramOfStudy(_programOfStudyRepositoryMock.Object);
        //    _getAllProgramOfStudyUseCase = new GetAllProgramOfStudy(_programOfStudyRepositoryMock.Object, _mapper);
        //    _updateProgramOfStudyUseCase = new UpdateProgramOfStudy(_programOfStudyRepositoryMock.Object, _mapper, _programValidator);

        //    // Initialize competency use cases
        _createCompetencyUseCase = new CreateCompetency(_competencyRepositoryMock.Object, _mapper, _competencyValidator);
        //    _getCompetencyUseCase = new GetCompetency(_competencyRepositoryMock.Object, _mapper);
        //    _updateCompetencyUseCase = new UpdateCompetency(_competencyRepositoryMock.Object, _mapper, _competencyValidator);
        //    _deleteCompetencyUseCase = new DeleteCompetency(_competencyRepositoryMock.Object);

        //    // Setup Program of Study Repository mock
        //    _programOfStudyRepositoryMock.Setup(repo => repo.Add(It.IsAny<ProgramOfStudy>())).ReturnsAsync(program1);
        //    _programOfStudyRepositoryMock.Setup(repo => repo.Delete(It.IsAny<string>()));
        //    _programOfStudyRepositoryMock.Setup(repo => repo.Update(It.IsAny<ProgramOfStudy>())).ReturnsAsync(program1);
        //    _programOfStudyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ProgramOfStudy> { program1, program2 });
        //    _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(program1.Code))).ReturnsAsync(program1);
        //    _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(program2.Code))).ReturnsAsync(program2);
        //    _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(codeOfAFakeProgram)))
        //        .Throws(new EntityNotFoundException(nameof(ProgramOfStudy), codeOfAFakeProgram));

        //    // Setup Competency Repository mock
        //    _competencyRepositoryMock.Setup(repo => repo.Add(It.IsAny<MinisterialCompetency>())).ReturnsAsync(competency1);
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
        string code = "0123";
        CreateCompetencyDTO createProgramDto = new CreateCompetencyDTOBuilder()
            .WithCode(code)
            .Build();
        // Act
        var result = await _createCompetencyUseCase.Execute(createProgramDto);

        // Assert
        Assert.That(createProgramDto.Code == code, "Code is returned");
    }
    //TODO at least one performance criteria
    //TODO validate position exists? plus dans le E2E


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
    //    CreateCompetencyDTO createCompetencyDto = new CreateCompetencyDTO
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
    //    CreateCompetencyDTO updateCompetencyDto = new CreateCompetencyDTO
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
