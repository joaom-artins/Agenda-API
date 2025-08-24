using AutoMapper;
using Service.Data.Repositories.Interfaces.v1.User;
using Service.Domain.Dtos.Responses.v1.Users;

namespace Service.Application.Services.v1;

public class UserService(
    IMapper _mapper,
    IUserRepository _userRepository
)
{
    public async Task<IEnumerable<UserGetAllResponse>> GetAll()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserGetAllResponse>>(users);
    }
}
