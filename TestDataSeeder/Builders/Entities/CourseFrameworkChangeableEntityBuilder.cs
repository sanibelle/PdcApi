using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Version;

namespace TestDataSeeder.Builders.Entities;

public class CourseFrameworkChangeableEntityBuilder
{
    private Guid? _id;
    private string _value = "Default value";
    private CourseFrameworkEntity _courseFramework = null;
    private List<ComplementaryInformationEntity> _complementaryInformations = new List<ComplementaryInformationEntity>();

    public CourseFrameworkChangeableEntityBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public CourseFrameworkChangeableEntityBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public CourseFrameworkChangeableEntityBuilder WithComplementaryInformations(List<ComplementaryInformationEntity> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public CourseFrameworkChangeableEntityBuilder WithCourseFramework(CourseFrameworkEntity courseFramework)
    {
        _courseFramework = courseFramework;
        return this;
    }

    public CourseFrameworkChangeableEntity Build()
    {
        return new CourseFrameworkChangeableEntity
        {
            Id = _id,
            Value = _value,
            ComplementaryInformations = _complementaryInformations,
            CourseFramework = _courseFramework
        };
    }


}
