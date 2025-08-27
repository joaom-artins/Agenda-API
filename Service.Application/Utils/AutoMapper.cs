using AutoMapper;
using Service.Domain.Dtos.Responses.v1.Users;
using Service.Domain.Models.v1;

namespace Service.Application.Utils;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<UserModel, UserGetAllResponse>();
        CreateMap<UserModel, UserGetProfessionalsResponse>();
    }
}
