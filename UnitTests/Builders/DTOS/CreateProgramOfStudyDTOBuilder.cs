using Pdc.Application.DTOS;
using Pdc.Domain.Entities.Common;
using Pdc.Domain.Enums;

namespace Pdc.Tests.Builders.DTOS;

public class CreateProgramOfStudyDTOBuilder
{
    protected string _code = "TES" + new Random().Next(1000, 9999);
    protected string _name = "Test Program";
    protected SanctionType _sanction = SanctionType.DEC;
    protected int _monthsDuration = 36;
    protected int _specificDurationHours = 2010;
    protected int _totalDurationHours = 5730;
    protected DateOnly _publishedOn = DateOnly.FromDateTime(DateTime.Now);
    protected Units _specificUnits = new Units(10);
    protected Units _optionnalUnits = new Units(5);

    public CreateProgramOfStudyDTOBuilder()
    {
    }

    public CreateProgramOfStudyDTOBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public CreateProgramOfStudyDTOBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CreateProgramOfStudyDTOBuilder WithSanction(SanctionType sanction)
    {
        _sanction = sanction;
        return this;
    }

    public CreateProgramOfStudyDTOBuilder WithMonthsDuration(int monthsDuration)
    {
        _monthsDuration = monthsDuration;
        return this;
    }

    public CreateProgramOfStudyDTOBuilder WithSpecificDurationHours(int specificDurationHours)
    {
        _specificDurationHours = specificDurationHours;
        return this;
    }

    public CreateProgramOfStudyDTOBuilder WithTotalDurationHours(int totalDurationHours)
    {
        _totalDurationHours = totalDurationHours;
        return this;
    }

    public CreateProgramOfStudyDTOBuilder WithPublishedOn(DateOnly publishedOn)
    {
        _publishedOn = publishedOn;
        return this;
    }

    public CreateProgramOfStudyDTOBuilder WithSpecificUnits(Units specificUnits)
    {
        _specificUnits = specificUnits;
        return this;
    }

    public CreateProgramOfStudyDTOBuilder WithOptionnalUnits(Units optionnalUnits)
    {
        _optionnalUnits = optionnalUnits;
        return this;
    }

    public virtual CreateProgramOfStudyDTO Build()
    {
        return new CreateProgramOfStudyDTO
        {
            Code = _code,
            Name = _name,
            Sanction = _sanction,
            MonthsDuration = _monthsDuration,
            SpecificDurationHours = _specificDurationHours,
            TotalDurationHours = _totalDurationHours,
            PublishedOn = _publishedOn,
            SpecificUnits = _specificUnits,
            OptionnalUnits = _optionnalUnits
        };
    }
}
