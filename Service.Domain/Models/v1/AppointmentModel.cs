using System.ComponentModel.DataAnnotations;
using Service.Domain.Enums.v1;

namespace Service.Domain.Models.v1;

public class AppointmentModel
{
    public Guid Id { get; set; }
    public Guid AvailabilitySlotId { get; set; }
    public AvailabilitySlotModel AvailabilitySlot { get; set; } = default!;
    public Guid ClientId { get; set; }
    public UserModel Client { get; set; } = default!;
    public AppointmentStatusEnum Status { get; set; } = AppointmentStatusEnum.Scheduled;
    [MaxLength(1024)]
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
