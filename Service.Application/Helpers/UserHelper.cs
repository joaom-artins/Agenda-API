using Service.Domain.Enums.v1;
using Service.Domain.Models.v1;

namespace Service.Application.Helpers;

public static class UserHelper
{
    public static bool CheckIsProfessional(UserModel user)
    {
        if(user.Type != UserTypeEnum.Professional)
        {
            return false;
        }

        return true;
    }
}
