using Mapster;
using Modules.Doctors.Application.Appointments.Commands.UpdateAppointment;

namespace Modules.Doctors.Endpoints.Appointments;

public sealed class UpdateAppointmentMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid id, UpdateAppointmentRequest request), UpdateAppointmentCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest.IdDoctor, src => src.request.IdDoctor)
            .Map(dest => dest.IdPatient, src => src.request.IdPatient)
            .Map(dest => dest.DateFrom, src => src.request.DateFrom)
            .Map(dest => dest.DateUntil, src => src.request.DateUntil);
    }
}
