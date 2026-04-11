<<<<<<<< HEAD:Pdc.Domain/Interfaces/UseCases/Versioning/IAddComplementaryInformationUseCase.cs
﻿namespace Pdc.Domain.Interfaces.UseCases.Versioning;
========
﻿namespace Pdc.Domain.Interfaces.UseCases.ChangeRecord;
>>>>>>>> 2f52bb1bcb77bc73a5f532a55cf759a5d4c44124:Pdc.Domain/Interfaces/UseCases/ChangeRecord/IAddComplementaryInformationUseCase.cs

using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Models.Security;

public interface IAddComplementaryInformationUseCase
{
    Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformation, Guid changeableId, User currentUser);
}