using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        // common
        CreateMap<Changeable, ChangeableDTO<int>>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => int.Parse(src.Value)))
            .ForMember(dest => dest.Position, opt => opt.Ignore()) // TODO gérer la position
            .ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value.ToString()));

        // ProgramOfStudy  
        CreateMap<ProgramOfStudy, ProgramOfStudyDTO>().ReverseMap();

        // Comptency  
        CreateMap<ChangeableDTO<string>, RealisationContext>().ReverseMap();
        CreateMap<ChangeableDTO<string>, PerformanceCriteria>().ReverseMap();
        CreateMap<ChangeableDTO<string>, Changeable>().ReverseMap();
        CreateMap<CompetencyDTO, MinisterialCompetency>()
            .ForMember(dest => dest.ChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.ProgramOfStudyCode, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.IsDraft, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.IsDraft : default))
            .ForMember(dest => dest.ChangeRecordId, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.Id : default))
            .ForMember(dest => dest.ChangeRecordNumber, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.ChangeRecordNumber : (int?)null));
        CreateMap<CompetencyElementDTO, MinisterialCompetencyElement>().ReverseMap();

        CreateMap<ChangeRecordDTO, ChangeRecord>()
            .ForMember(dest => dest.RootId, opt => opt.Ignore())
            .ForMember(dest => dest.ParentChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.NextChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ValidatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ValidatedOn, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ValidateBy, opt => opt.MapFrom(src => src.ValidatedBy));

        CreateMap<ChangeDetailDTO, ChangeDetail>()
            .ForMember(dest => dest.ChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.Changeable, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ChangeableId, opt => opt.MapFrom(src => src.Changeable.Id != null ? src.Changeable.Id.Value : default));

        CreateMap<UserDTO, User>()
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<ComplementaryInformationDTO, ComplementaryInformation>()
            .ForMember(dest => dest.CreatedBy, opt =>
            {
                opt.Condition(src => !string.IsNullOrEmpty(src.CreatedBy?.UserName)); // Only map if source has CreatedBy
                opt.MapFrom(src => src.CreatedBy);
            })
            .ForMember(dest => dest.WrittenOnChangeRecord, opt => opt.Ignore())
            .PreserveReferences()
            .ReverseMap()
            .ForMember(dest => dest.ChangeRecordNumber, opt => opt.MapFrom(src => src.WrittenOnChangeRecord != null ? src.WrittenOnChangeRecord.ChangeRecordNumber : (int?)default));

        // Course Framework
        CreateMap<Weighting, WeightingDTO>()
            .PreserveReferences()
            .ReverseMap();

        CreateMap<CourseFramework, UnTrackedCourseFrameworkDTO>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.TheoryHours, opt => opt.MapFrom(src => int.Parse(src.Weighting.TheoryHours.Value)))
            .ForMember(dest => dest.LaboratoryHours, opt => opt.MapFrom(src => int.Parse(src.Weighting.LaboratoryHours.Value)))
            .ForMember(dest => dest.PersonnalWorkHours, opt => opt.MapFrom(src => int.Parse(src.Weighting.PersonnalWorkHours.Value)))
            .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => int.Parse(src.Semester.Value)));

        CreateMap<CourseFramework, TrackedCourseFrameworkDTO>()
            .ForMember(dest => dest.ChangeRecordNumber, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.ChangeRecordNumber : (int?)default))
            .ForMember(dest => dest.IsDraft, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.IsDraft : default));

        CreateMap<TrackedCourseFrameworkDTO, CourseFramework>()
            .ForMember(dest => dest.ProgramOfStudyCode, opt => opt.Ignore()) // Not to mistake for for courseFrameworkCode.
            .ForMember(dest => dest.Competencies, opt => opt.Ignore())
            .ForMember(dest => dest.CourseFrameworkPerformanceCriterias, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Units, opt => opt.Ignore())
            .ForMember(dest => dest.FinalCourseObjective, opt => opt.Ignore())
            .ForMember(dest => dest.CourseCharacteristics, opt => opt.Ignore())
            .ForMember(dest => dest.OtherSpecifications, opt => opt.Ignore())
            .ForMember(dest => dest.StatementOfComplexAuthenticTask, opt => opt.Ignore())
            .ForMember(dest => dest.TaskPresentation, opt => opt.Ignore())
            .ForMember(dest => dest.AssedElements, opt => opt.Ignore())
            .ForMember(dest => dest.ChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.Prerequisites, opt => opt.Ignore())
            .PreserveReferences();

        CreateMap<UnTrackedCourseFrameworkDTO, CourseFramework>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => new Changeable(src.Code)))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Changeable(src.Name)))
            .ForMember(dest => dest.Weighting, opt => opt.MapFrom(src => new Weighting(src.TheoryHours, src.LaboratoryHours, src.PersonnalWorkHours)))
            .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => new Changeable(src.Semester)))
            .ForMember(dest => dest.ProgramOfStudyCode, opt => opt.Ignore())
            .ForMember(dest => dest.CourseFrameworkPerformanceCriterias, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Units, opt => opt.Ignore())
            .ForMember(dest => dest.FinalCourseObjective, opt => opt.Ignore())
            .ForMember(dest => dest.CourseCharacteristics, opt => opt.Ignore())
            .ForMember(dest => dest.OtherSpecifications, opt => opt.Ignore())
            .ForMember(dest => dest.StatementOfComplexAuthenticTask, opt => opt.Ignore())
            .ForMember(dest => dest.TaskPresentation, opt => opt.Ignore())
            .ForMember(dest => dest.AssedElements, opt => opt.Ignore())
            .ForMember(dest => dest.ChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.Prerequisites, opt => opt.Ignore()); ;

        CreateMap<CreateCourseFrameworkDTO, CourseFramework>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => new Changeable(src.Code)))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Changeable(src.Name)))
            .ForMember(dest => dest.Weighting, opt => opt.MapFrom(src => new Weighting(src.TheoryHours, src.LaboratoryHours, src.PersonnalWorkHours)))
            .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => new Changeable(src.Semester)))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProgramOfStudyCode, opt => opt.Ignore())
            .ForMember(dest => dest.Competencies, opt => opt.Ignore())
            .ForMember(dest => dest.CourseFrameworkPerformanceCriterias, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Units, opt => opt.Ignore())
            .ForMember(dest => dest.FinalCourseObjective, opt => opt.Ignore())
            .ForMember(dest => dest.CourseCharacteristics, opt => opt.Ignore())
            .ForMember(dest => dest.OtherSpecifications, opt => opt.Ignore())
            .ForMember(dest => dest.StatementOfComplexAuthenticTask, opt => opt.Ignore())
            .ForMember(dest => dest.TaskPresentation, opt => opt.Ignore())
            .ForMember(dest => dest.AssedElements, opt => opt.Ignore())
            .ForMember(dest => dest.ChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.Prerequisites, opt => opt.Ignore());

        CreateMap<UpdateCourseFrameworkDTO, CourseFramework>()
            .ForMember(dest => dest.Competencies, opt => opt.Ignore())
            .ForMember(dest => dest.AssedElements, opt => opt.Ignore())
            .ForMember(dest => dest.ChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.CourseCharacteristics, opt => opt.Ignore())
            .ForMember(dest => dest.CourseFrameworkPerformanceCriterias, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProgramOfStudyCode, opt => opt.Ignore())
            .ForMember(dest => dest.Units, opt => opt.Ignore())
            .ForMember(dest => dest.FinalCourseObjective, opt => opt.Ignore())
            .ForMember(dest => dest.OtherSpecifications, opt => opt.Ignore())
            .ForMember(dest => dest.StatementOfComplexAuthenticTask, opt => opt.Ignore())
            .ForMember(dest => dest.TaskPresentation, opt => opt.Ignore())
            .ForMember(dest => dest.Prerequisites, opt => opt.Ignore());

        CreateMap<CourseFrameworkCompetency, CourseFrameworkCompetencyDTO>()
            .IncludeBase<Competency, CourseFrameworkCompetencyDTO>()
            .PreserveReferences();

        CreateMap<Competency, CourseFrameworkCompetencyDTO>()
            .ForMember(dest => dest.ChangeRecordNumber, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.ChangeRecordNumber : (int?)default))
            .ForMember(dest => dest.IsDraft, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.IsDraft : default))
            .ForMember(dest => dest.ChangeDetails, opt => opt.Ignore())
            .ForMember(dest => dest.CompetencyElements, opt => opt.Ignore())
            .Include<CourseFrameworkCompetency, CourseFrameworkCompetencyDTO>();
    }
}
