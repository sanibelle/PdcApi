<<<<<<<< HEAD:Pdc.Domain/Interfaces/UseCases/Versioning/IUpdateComplementaryInformationUseCase.cs
﻿namespace Pdc.Domain.Interfaces.UseCases.Versioning;
========
﻿namespace Pdc.Domain.Interfaces.UseCases.ChangeRecord;
>>>>>>>> 2f52bb1bcb77bc73a5f532a55cf759a5d4c44124:Pdc.Domain/Interfaces/UseCases/ChangeRecord/IUpdateComplementaryInformationUseCase.cs

using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Models.Security;
using System;

public interface IUpdateComplementaryInformationUseCase
{
    Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformation, User currentUser, Guid complementaryInformationId);
}