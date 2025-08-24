using AutoMapper;
using Service.Domain.Dtos.Responses.v1.Users;
using Service.Domain.Models.v1;

namespace Service.Commons.Utils;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<UserModel, UserGetAllResponse>();
    }
}
