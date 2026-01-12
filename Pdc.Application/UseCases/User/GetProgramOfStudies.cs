using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.User;

namespace Pdc.Application.UseCases;

public class GetUsers : IGetUsersUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsers(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IList<UserDTO>> Execute()
    {
        var users = await _userRepository.GetUsersWithRoles();
        return _mapper.Map<IList<UserDTO>>(users);
    }
}
