using AutoMapper;
using Common.Shared.Extensions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class GetShiftByIdMapping : Profile
{
    public GetShiftByIdMapping()
    {
        CreateMap<DoctorShift, GetShiftByIdResponse>();

        CreateMap<Doctor, GetShiftByIdResponseDoctor>()
            .ForMember(
                dest => dest.Specialty,
                opt => opt.MapFrom(src => src.Specialty.ToEnumResponse()));
    }
}
