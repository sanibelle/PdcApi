﻿using Pdc.Domain.Enums;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Domain.Models.CourseFramework;

public class ProgramOfStudy // toujours issu d'un devis ministeriel
{
    public ICollection<MinisterialCompetency> Competencies { get; set; } = new List<MinisterialCompetency>();
    /// <summary>
    /// Code unique de la formation
    /// </summary>
    public required string Code { get; set; } //420.B0
    /// <summary>
    /// Les unité spécifiques à la formation qui sont obligatoires
    /// </summary>
    public Units? SpecificUnits { get; set; }
    /// <summary>
    /// Les unités des programmes optionnels
    /// </summary>
    public Units? OptionalUnits { get; set; }
    /// <summary>
    /// Les unités des cours généraux
    /// </summary>
    public Units GeneralUnits { get; set; } = new Units(16, 2, 3);
    /// <summary>
    /// Les unités des cours complémentaires
    /// </summary>
    public Units ComplementaryUnits { get; set; } = new Units(4);
    public required string Name { get; set; } //Techniques de l'informatique
    public ProgramType ProgramType { get; set; } //DEC, PRE-U
    public int MonthsDuration { get; set; } // 36 mois
    public int SpecificDurationHours { get; set; } // 2010
    public int TotalDurationHours { get; set; } // 5730
    public DateOnly PublishedOn { get; set; }

    public ProgramOfStudy()
    {
    }
}
