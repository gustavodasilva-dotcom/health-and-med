using Mapster;
using Modules.Doctors.Application.Shifts.Commands.UpdateShift;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class UpdateShiftMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid id, UpdateShiftRequest request), UpdateShiftCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest.StartAt, src => src.request.StartAt)
            .Map(dest => dest.EndAt, src => src.request.EndAt);
    }
}
