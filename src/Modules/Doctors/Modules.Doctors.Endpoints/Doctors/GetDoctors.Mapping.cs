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
        config.NewConfig<(string? name, MedicalSpecialties? specialty), GetDoctorsQuery>()
            .Map(dest => dest.Name, src => src.name)
            .Map(dest => dest.Specialty, src => src.specialty);

        config.NewConfig<Doctor, GetDoctorsResponse>()
            .Map(dest => dest.DoctorId, src => src.Id)
            .Map(dest => dest.Specialty, src => src.Specialty.ToEnumResponse());
    }
}
