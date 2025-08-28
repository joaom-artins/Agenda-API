namespace Service.Domain.Models.v1;

public class AvailabilitySlotModel
{
    public Guid Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public UserModel Professional { get; set; } = default!;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public bool IsBooked { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
