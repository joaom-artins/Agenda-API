using Service.Data.Context;
using Service.Data.Repositories.Generic;
using Service.Data.Repositories.Interfaces.v1;
using Service.Domain.Models.v1;

namespace Service.Data.Repositories.v1;

public class AvailabilitySlotRepository(
    AppDbContext context
) : GenericRepository<AvailabilitySlotModel>(context),
    IAvailabilitySlotRepository
{
}
