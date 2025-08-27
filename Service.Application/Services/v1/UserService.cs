using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Service.Application.Services.Interfaces.v1;
using Service.Commons.Notification;
using Service.Commons.Notification.Interface;
using Service.Data.Repositories.Interfaces.v1;
using Service.Data.UnitOfWork.Interfaces;
using Service.Domain.Dtos.Request.v1.Users;
using Service.Domain.Dtos.Responses.v1.Users;
using Service.Domain.Models.v1;

namespace Service.Application.Services.v1;

public class UserService(
    IMapper _mapper,
    IUnitOfWork _unitOfWork,
    INotificationContext _notificationContext,
    IUserRepository _userRepository,
    UserManager<UserModel> _userManager
) : IUserService
{
    public async Task<IEnumerable<UserGetAllResponse>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserGetAllResponse>>(users);
    }

    public async Task<IEnumerable<UserGetProfessionalsResponse>> GetProfessionalsAsync()
    {
        var records = await _userRepository.GetProfessionalsAsync();

        return _mapper.Map<IEnumerable<UserGetProfessionalsResponse>>(records);
    }

    public async Task<bool> CreateAsync(UserCreateRequest request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.User.PasswordAreDifferent
            );
            return false;
        }

        await ExistsByEmailOrPhoneNumber(request.Email, request.PhoneNumber);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        _unitOfWork.BeginTransaction();

        var result = await _userManager.CreateAsync(new UserModel
        {
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Name = request.Name,
            Occupation = request.Occupation,
            Type = request.Type,
            UserName = request.Email.ToLower()
        }, request.Password);

        if (!result.Succeeded)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status400BadRequest,
               title: NotificationTitle.BadRequest,
               detail: NotificationMessage.Common.UnexpectedError
            );
            return false;
        }

        var user = await _userManager.FindByEmailAsync(request.Email);

        var role = await _userManager.AddToRoleAsync(user!, "USER");
        if (!role.Succeeded)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status400BadRequest,
               title: NotificationTitle.BadRequest,
               detail: NotificationMessage.Common.UnexpectedError
            );
            return false;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    //TODO : Migrar método para uma business ou userHelper da vida mais para frente
    private async Task<bool> ExistsByEmailOrPhoneNumber(string email, string phoneNumber)
    {
        var userExists = await _userRepository.FindByEmailOrPhoneNumberAsync(email, phoneNumber);
        if (userExists.Any())
        {
            if (userExists.Any(x => x.UserName == phoneNumber))
            {
                _notificationContext.AddNotification("PhoneNumber", NotificationMessage.User.PhoneNumberAlreadyExists);
            }
            if (userExists.Any(x => x.Email == email))
            {
                _notificationContext.AddNotification("Email", NotificationMessage.User.EmailAlreadyExists);
            }

            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status409Conflict,
                title: NotificationTitle.Conflict,
                detail: NotificationMessage.Common.DataExists
            );
            return default!;
        }

        return true;
    }
}
