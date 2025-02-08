using Common.Shared.Extensions;
using Mapster;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class GetShiftsByDoctorMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DoctorShift, GetShiftsByDoctorResponse>();

        config.NewConfig<DoctorShiftAppointment, GetShiftsByDoctorResponseAppointment>()
            .Map(dest => dest.AppointmentId, src => src.Id)
            .Map(dest => dest.Status, src => src.Status.ToEnumResponse());
    }
}
