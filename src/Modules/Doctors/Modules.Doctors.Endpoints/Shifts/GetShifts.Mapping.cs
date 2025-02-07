using AutoMapper;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class GetShiftsMapping : Profile
{
    public GetShiftsMapping()
    {
        CreateMap<DoctorShift, GetShiftsResponse>()
            .ForMember(
                dest => dest.Doctor,
                opt => opt.MapFrom(src => src.Registration));

        CreateMap<DoctorRegistration, GetShiftsResponseDoctor>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.DoctorId))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Doctor.Name));
    }
}
