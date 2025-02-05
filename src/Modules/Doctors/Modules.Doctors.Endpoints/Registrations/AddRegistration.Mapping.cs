using Mapster;
using Modules.Doctors.Application.Registrations.Commands.AddRegistration;

namespace Modules.Doctors.Endpoints.Registrations;

public sealed class AddRegistrationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid doctorId, AddRegistrationRequest request), AddRegistrationCommand>()
            .Map(dest => dest.DoctorId, src => src.doctorId)
            .Map(dest => dest.RegistrationNumber, src => src.request.RegistrationNumber)
            .Map(dest => dest.RegistrationState, src => src.request.RegistrationState);
    }
}
