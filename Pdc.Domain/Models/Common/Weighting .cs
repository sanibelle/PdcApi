using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.Common;

public class Weighting : ICreatedByPropagable, ICreatedOnPropagable
{
    public Changeable? TheoryHours { get; set; }
    public Changeable? LaboratoryHours { get; set; }
    public Changeable? PersonnalWorkHours { get; set; }

    public Weighting() { }
    public Weighting(Changeable theoryHours, Changeable laboratoryHours, Changeable personalWorkHours)
    {
        TheoryHours = theoryHours;
        LaboratoryHours = laboratoryHours;
        PersonnalWorkHours = personalWorkHours;
    }

    public Weighting(int theoryHours, int laboratoryHours, int personalWorkHours)
    {
        TheoryHours = new Changeable(theoryHours);
        LaboratoryHours = new Changeable(laboratoryHours);
        PersonnalWorkHours = new Changeable(personalWorkHours);
    }

    public void SetCreatedByOnUntracked(UserModel createdBy)
    {
        TheoryHours.SetCreatedByOnUntracked(createdBy);
        LaboratoryHours.SetCreatedByOnUntracked(createdBy);
        PersonnalWorkHours.SetCreatedByOnUntracked(createdBy);
    }

    public void SetCreatedOnOnUntracked()
    {
        TheoryHours.SetCreatedOnOnUntracked();
        LaboratoryHours.SetCreatedOnOnUntracked();
        PersonnalWorkHours.SetCreatedOnOnUntracked();
    }

    public int GetTotalHours()
    {
        if (TheoryHours == null || LaboratoryHours == null)
        {
            throw new InvalidOperationException("Weighting properties are null. Impossible to calculate total hours.");
        }
        if (!int.TryParse(TheoryHours.Value, out int theoryHoursValue))
        {
            throw new InvalidOperationException("Weighting properties are not valid integers. Impossible to calculate total hours.");
        }
        if (!int.TryParse(LaboratoryHours.Value, out int laboratoryHoursValue))
        {
            throw new InvalidOperationException("Weighting properties are not valid integers. Impossible to calculate total hours.");
        }

        return theoryHoursValue + laboratoryHoursValue;
    }
}
