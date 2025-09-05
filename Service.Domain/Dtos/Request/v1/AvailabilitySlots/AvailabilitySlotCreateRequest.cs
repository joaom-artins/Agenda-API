namespace Service.Domain.Dtos.Request.v1.AvailabilitySlots;

public class AvailabilitySlotCreateRequest
{
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}
