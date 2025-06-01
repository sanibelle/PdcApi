using AutoMapper;
using FluentValidation;
using Moq;
using Pdc.Application.DTOS;
using Pdc.Application.Mappings;
using Pdc.Application.UseCase;
using Pdc.Application.Validators;
using Pdc.Domain.Enums;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Infrastructure.Exceptions;
using Pdc.Tests.Builders.Models;

namespace Pdc.Tests.UnitTests;

public class ProgramOfStudyTest
{
    Mock<IProgramOfStudyRespository> _programOfStudyRepositoryMock;
    ICreateProgramOfStudyUseCase _createProgramOfStudyUseCase;
    IDeleteProgramOfStudyUseCase _deleteProgramOfStudyUseCase;
    IGetAllProgramOfStudyUseCase _getAllProgramOfStudyUseCase;
    IUpdateProgramOfStudyUseCase _updateProgramOfStudyUseCase;
    IMapper _mapper;
    IValidator<ProgramOfStudyDTO> _validator;
    string codeOfAFakeProgram = "fakeCode";


    private ProgramOfStudy throwsNotFoundProgram = new ProgramOfStudyBuilder().Build();
    private ProgramOfStudy existingProgram = new ProgramOfStudyBuilder().Build();

    [SetUp]
    public void Setup()
    {
        _programOfStudyRepositoryMock = new Mock<IProgramOfStudyRespository>();
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        _validator = new ProgramOfStudyValidation();

        _createProgramOfStudyUseCase = new CreateProgramOfStudy(_programOfStudyRepositoryMock.Object, _mapper, _validator);
        _deleteProgramOfStudyUseCase = new DeleteProgramOfStudy(_programOfStudyRepositoryMock.Object);
        _getAllProgramOfStudyUseCase = new GetAllProgramOfStudy(_programOfStudyRepositoryMock.Object, _mapper);
        _updateProgramOfStudyUseCase = new UpdateProgramOfStudy(_programOfStudyRepositoryMock.Object, _mapper, _validator);

        // Arrange
        _programOfStudyRepositoryMock.Setup(repo => repo.Add(It.IsAny<ProgramOfStudy>())).ReturnsAsync(throwsNotFoundProgram);
        _programOfStudyRepositoryMock.Setup(repo => repo.Delete(It.IsAny<string>()));
        _programOfStudyRepositoryMock.Setup(repo => repo.Update(It.IsAny<ProgramOfStudy>())).ReturnsAsync(existingProgram);
        _programOfStudyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ProgramOfStudy> { throwsNotFoundProgram, existingProgram });
        _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(existingProgram.Code))).ReturnsAsync(existingProgram);
        _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsNotIn(existingProgram.Code))).ThrowsAsync(new EntityNotFoundException(nameof(ProgramOfStudy), "12345"));
        _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(codeOfAFakeProgram))).Throws(new EntityNotFoundException(nameof(ProgramOfStudy), codeOfAFakeProgram));
    }

    [Test]
    public async Task CreateProgramOfStudy_ShouldReturnCreatedProgram()
    {
        ProgramOfStudyDTO createProgramDto = new()
        {
            Code = "420.B0",
            Name = "Techniques de l'informatique",
            ProgramType = ProgramType.DEC,
            MonthsDuration = 36,
            SpecificDurationHours = 2010,
            TotalDurationHours = 5730,
            PublishedOn = new DateOnly(2020, 01, 01),
            OptionnalUnits = new Units(16, 2, 3),
            SpecificUnits = new Units(26, 2, 3)
        };
        // Act
        var result = await _createProgramOfStudyUseCase.Execute(createProgramDto);

        // Assert
        Assert.That(throwsNotFoundProgram.Code == result.Code, "Program is returned");
        Assert.That(throwsNotFoundProgram.Name == result.Name, "Same name");
    }

    [Test]
    public async Task CreateDuplicateProgramOfStudy_ShouldReturnDuplicateException()
    {
        ProgramOfStudyDTO createProgramDto = new()
        {
            Code = existingProgram.Code,
            Name = "Techniques de l'informatique",
            ProgramType = ProgramType.DEC,
            MonthsDuration = 36,
            SpecificDurationHours = 2010,
            TotalDurationHours = 5730,
            PublishedOn = new DateOnly(2020, 01, 01),
            OptionnalUnits = new Units(16, 2, 3),
            SpecificUnits = new Units(26, 2, 3)
        };
        // Act
        Assert.CatchAsync<DuplicateException>(async () => await _createProgramOfStudyUseCase.Execute(createProgramDto), "Duplicate program exception is thrown");
    }

    [Test]
    public async Task DeleteProgramOfStudy_ShouldCallRepositoryDelete()
    {
        // Act
        await _deleteProgramOfStudyUseCase.Execute(existingProgram.Code);

        // Assert
        _programOfStudyRepositoryMock.Verify(repo => repo.Delete(existingProgram.Code), Times.Once);
    }

    [Test]
    public async Task GetAllProgramOfStudy_ShouldReturnAllPrograms()
    {
        // Act
        var result = await _getAllProgramOfStudyUseCase.Execute();

        // Assert
        Assert.That(result.Count == 2, "Got 2 programs");
        Assert.That(throwsNotFoundProgram.Code == result[0].Code, "Both programs are returned");
        Assert.That(existingProgram.Code == result[1].Code, "Both programs are returned");
    }

    [Test]
    public async Task UpdateProgramOfStudy_ShouldCallRepositoryUpdate()
    {
        // Arrange
        ProgramOfStudyDTO updateProgramDto = new()
        {
            Code = existingProgram.Code,
            Name = "UpdatedName",
            ProgramType = ProgramType.DEC,
            MonthsDuration = 36,
            SpecificDurationHours = 2010,
            TotalDurationHours = 5730,
            PublishedOn = new DateOnly(2020, 01, 01),
            OptionnalUnits = new Units(16, 2, 3),
            SpecificUnits = new Units(26, 2, 3)
        };

        // Act
        var result = await _updateProgramOfStudyUseCase.Execute(existingProgram.Code, updateProgramDto);

        // Assert
        _programOfStudyRepositoryMock.Verify(repo => repo.Update(It.Is<ProgramOfStudy>(p => p.Code == existingProgram.Code && p.Name == updateProgramDto.Name)), Times.Once);
        Assert.That(result.Name == existingProgram.Name, "Program name is updated");
    }

    [Test]
    public async Task UpdateProgramOfStudyWithAnotherExistingName_ShouldCallDuplicateException()
    {
        // Arrange
        ProgramOfStudyDTO updateProgramDto = new()
        {
            Code = existingProgram.Code,
            Name = "UpdatedName",
            ProgramType = ProgramType.DEC,
            MonthsDuration = 36,
            SpecificDurationHours = 2010,
            TotalDurationHours = 5730,
            PublishedOn = new DateOnly(2020, 01, 01),
            OptionnalUnits = new Units(16, 2, 3),
            SpecificUnits = new Units(26, 2, 3)
        };

        // Act
        // Assert
        Assert.CatchAsync<DuplicateException>(async () => await _updateProgramOfStudyUseCase.Execute(throwsNotFoundProgram.Code, updateProgramDto), "Duplicate program exception is thrown");
    }
}
