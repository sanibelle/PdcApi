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
            throw new InvalidOperationException("Cannot update a non-draft competency with version greater than 1.");
        }
        _mapper.Map(updateCompetencyDto, competencyToUpdate);
        // On prend la version actuelle et on l'assigne à tous les objets qui ont une version.
        competencyToUpdate.SetVersionOnUntracked(competencyToUpdate.CurrentVersion!);
        competencyToUpdate.SetCreatedByOnUntracked(currentUser);
        MinisterialCompetency updatedProgramOfStudy = await _competencyRepository.Update(competencyToUpdate);
        return _mapper.Map<CompetencyDTO>(updatedProgramOfStudy);

        // TODO
        // Si la compétence est en V1 et draft, on update sans tracker
        // Un autre use case pour les changements mineurs en !IsDraft. Normalement, comme un changeable a toujours sa valeur la plus à jour, il ne devrait pas y avoir de problème à faire des updates mineurs.
        // Finalement, si on fait des updates majeurs sur une version 1+, on track les changements de vn à vn+1.
    }
}