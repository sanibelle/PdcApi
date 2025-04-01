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


    private ProgramOfStudy program1 = new ProgramOfStudyBuilder().Build();
    private ProgramOfStudy program2 = new ProgramOfStudyBuilder().Build();

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
        _programOfStudyRepositoryMock.Setup(repo => repo.Add(It.IsAny<ProgramOfStudy>())).ReturnsAsync(program1);
        _programOfStudyRepositoryMock.Setup(repo => repo.Delete(It.IsAny<string>()));
        _programOfStudyRepositoryMock.Setup(repo => repo.Update(It.IsAny<ProgramOfStudy>())).ReturnsAsync(program1);
        _programOfStudyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ProgramOfStudy> { program1, program2 });
        _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(program1.Code))).ReturnsAsync(program1);
        _programOfStudyRepositoryMock.Setup(repo => repo.FindByCode(It.IsIn(codeOfAFakeProgram))).Throws(new EntityNotFoundException(nameof(ProgramOfStudy), codeOfAFakeProgram));
    }

    [Test]
    public async Task CreateProgramOfStudy_ShouldReturnCreatedProgram()
    {
        ProgramOfStudyDTO createProgramDto = new()
        {
            Code = "420.B0",
            Name = "Techniques de l'informatique",
            Sanction = SanctionType.DEC,
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
        Assert.That(program1.Code == result.Code, "Program is returned");
        Assert.That(program1.Name == result.Name, "Same name");
    }

    [Test]
    public async Task DeleteProgramOfStudy_ShouldCallRepositoryDelete()
    {
        // Act
        await _deleteProgramOfStudyUseCase.Execute(program1.Code);

        // Assert
        _programOfStudyRepositoryMock.Verify(repo => repo.Delete(program1.Code), Times.Once);
    }

    [Test]
    public async Task GetAllProgramOfStudy_ShouldReturnAllPrograms()
    {
        // Act
        var result = await _getAllProgramOfStudyUseCase.Execute();

        // Assert
        Assert.That(result.Count == 2, "Got 2 programs");
        Assert.That(program1.Code == result[0].Code, "Both programs are returned");
        Assert.That(program2.Code == result[1].Code, "Both programs are returned");
    }

    [Test]
    public async Task UpdateProgramOfStudy_ShouldCallRepositoryUpdate()
    {
        // Arrange
        ProgramOfStudyDTO updateProgramDto = new()
        {
            Code = "420.B0",
            Name = "UpdatedName",
            Sanction = SanctionType.DEC,
            MonthsDuration = 36,
            SpecificDurationHours = 2010,
            TotalDurationHours = 5730,
            PublishedOn = new DateOnly(2020, 01, 01),
            OptionnalUnits = new Units(16, 2, 3),
            SpecificUnits = new Units(26, 2, 3)
        };

        // Act
        var result = await _updateProgramOfStudyUseCase.Execute(program1.Code, updateProgramDto);

        // Assert
        _programOfStudyRepositoryMock.Verify(repo => repo.Update(It.Is<ProgramOfStudy>(p => p.Code == program1.Code && p.Name == updateProgramDto.Name)), Times.Once);
        Assert.That(result.Name == program1.Name, "Program name is updated");
    }
}
