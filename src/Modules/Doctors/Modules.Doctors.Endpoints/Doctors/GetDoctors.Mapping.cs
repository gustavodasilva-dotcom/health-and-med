using Common.Shared.Extensions;
using Mapster;
using Modules.Doctors.Application.Doctors.Queries.GetDoctors;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Endpoints.Doctors;

public sealed class GetDoctorsMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(string? name, MedicalSpecialties? specialty, UFs? state), GetDoctorsQuery>()
            .Map(dest => dest.Name, src => src.name)
            .Map(dest => dest.Specialty, src => src.specialty)
            .Map(dest => dest.State, src => src.state);

        config.NewConfig<Doctor, GetDoctorsResponse>()
            .Map(dest => dest.Specialty, src => src.Specialty.ToEnumResponse());

        config.NewConfig<DoctorRegistration, GetDoctorsResponseRegistrations>()
            .Map(dest => dest.State, src => src.State.ToEnumResponse());
    }
}
