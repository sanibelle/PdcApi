using AutoMapper;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Security;

namespace Pdc.Application.UseCases;

public class GetUser(IUserRepository userRepository, IMapper mapper) : IGetUserUseCase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDTO> Execute(Guid userId)
    {
        User user = await _userRepository.FindUserById(userId);
        return _mapper.Map<UserDTO>(user);
    }
}
