﻿using Pdc.Application.DTOS.Common;
using Pdc.Domain.Models.Common;

namespace Pdc.Application.DTOS;

public class CompetencyDTO
{
    public required string Code { get; set; }
    public Units? Units { get; set; } = null;
    public bool IsMandatory { get; set; }
    public bool IsOptionnal { get; set; }
    public required string StatementOfCompetency { get; set; }
    public ICollection<ChangeableDTO> RealisationContexts { get; set; } = new List<ChangeableDTO>();
    public ICollection<CompetencyElementDTO> CompetencyElements { get; set; } = new List<CompetencyElementDTO>();
    public int? VersionNumber { get; set; }

    public CompetencyDTO() { }



}
