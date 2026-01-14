using AutoMapper;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Application.UseCases;

public class GetUser(IUserRepository userRepository, IMapper mapper) : IGetUserUseCase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDTO> Execute(Guid userId)
    {
        try
        {
            User user = await _userRepository.FindUserById(userId);
            return _mapper.Map<UserDTO>(user);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
    }
}
