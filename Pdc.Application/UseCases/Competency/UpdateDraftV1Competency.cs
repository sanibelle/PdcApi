using AutoMapper;
using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;


namespace Pdc.Application.UseCases;

public class UpdateDraftV1Competency : IUpdateDraftV1CompetencyUseCase
{
    private readonly IValidator<CompetencyDTO> _validator;
    private readonly ICompetencyRepository _competencyRepository;
    private readonly IMapper _mapper;

    public UpdateDraftV1Competency(ICompetencyRepository competencyRepository,
                               IMapper mapper,
                               IValidator<CompetencyDTO> validator)
    {
        _competencyRepository = competencyRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, CompetencyDTO updateCompetencyDto, User currentUser)
    {
        var validationResult = await _validator.ValidateAsync(updateCompetencyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (competencyCode != updateCompetencyDto.Code)
        {
            throw new ValidationException("Competency code in the URL does not match the code in the body. The code should not be changed.");
        }
        MinisterialCompetency competencyToUpdate = await _competencyRepository.FindByCode(updateCompetencyDto.Code);
        if (!competencyToUpdate.IsDraftAndV1OrNull())
        {
            throw new InvalidOperationException("Cannot update a non-draft competency with change record greater than 1.");
        }
        _mapper.Map(updateCompetencyDto, competencyToUpdate);
        // On prend la version actuelle et on l'assigne à tous les objets qui ont une version.
        competencyToUpdate.SetChangeRecordOnUntracked(competencyToUpdate.ChangeRecord!);
        competencyToUpdate.SetCreatedByOnUntracked(currentUser);
        competencyToUpdate.SetCreatedOnOnUntracked();
        MinisterialCompetency updatedProgramOfStudy = await _competencyRepository.Update(competencyToUpdate);
        return _mapper.Map<CompetencyDTO>(updatedProgramOfStudy);
    }
}