using AutoMapper;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.User;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Identity;

namespace Pdc.Application.UseCases;

public class GetUser : IGetUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public GetUser(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDTO> Execute(Guid userId)
    {
        User user = await _userRepository.FindUserById(userId);
        return _mapper.Map<UserDTO>(user);
    }
}
