using Microsoft.AspNetCore.Http;
using Service.Application.Helpers;
using Service.Commons.LoggedUser.Interfaces;
using Service.Commons.Notification;
using Service.Commons.Notification.Interface;
using Service.Data.Repositories.Interfaces.v1;
using Service.Data.UnitOfWork.Interfaces;
using Service.Domain.Dtos.Request.v1.AvailabilitySlots;
using Service.Domain.Models.v1;

namespace Service.Application.Services.v1;

public class AvailabilitySlotService(
    IUnitOfWork _unitOfWork,
    INotificationContext _notificationContext,
    IUserRepository _userRepository,
    IAvailabilitySlotRepository _availabilitySlotRepository,
    IGetLoggedUser _loggedUser
)
{
    public async Task<bool> CreateAsync(AvailabilitySlotCreateRequest request)
    {
        if(request.EndTime < request.StartTime)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.AvailabilitySlot.EndTimeCannotBeLessThanStart
            );

            return false;
        }

        if (request.EndTime == request.StartTime)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.AvailabilitySlot.TimesCannotBeSame
            );

            return false;
        }

        if(request.EndTime < DateTimeOffset.Now || request.StartTime < DateTimeOffset.Now)
        {

            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.AvailabilitySlot.TimesCannotBeSame
            );

            return false;
        }

        var user = await _userRepository.GetByIdAsync(_loggedUser.GetId());
        if(user is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return false;
        }

        var isProfessional = UserHelper.CheckIsProfessional(user);
        if(!isProfessional)
        {
            if (user is null)
            {
                _notificationContext.SetDetails(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: NotificationTitle.BadRequest,
                    detail: NotificationMessage.AvailabilitySlot.ImpossibleCreate
                );
                return false;
            }
        }

        var record = new AvailabilitySlotModel
        {
            IsBooked = false,
            EndTime = request.EndTime,
            StartTime = request.StartTime,
            ProfessionalId = _loggedUser.GetId(),
        };
        await _availabilitySlotRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
