using AutoMapper;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class GetShiftByIdMapping : Profile
{
    public GetShiftByIdMapping()
    {
        CreateMap<DoctorShift, GetShiftByIdResponse>()
            .ForMember(
                dest => dest.Doctor,
                opt => opt.MapFrom(src => src.Registration));

        CreateMap<DoctorRegistration, GetShiftByIdResponseDoctor>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.DoctorId))
            .ForMember(
                dest => dest.RegistrationNumber,
                opt => opt.MapFrom(src => src.Number))
            .ForMember(
                dest => dest.RegistrationState,
                opt => opt.MapFrom(src => src.State))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Doctor.Name))
            .ForMember(
                dest => dest.Ssn,
                opt => opt.MapFrom(src => src.Doctor.Ssn));
    }
}
