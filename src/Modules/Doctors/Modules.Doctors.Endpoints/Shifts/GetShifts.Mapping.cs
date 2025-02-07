using AutoMapper;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class GetShiftsMapping : Profile
{
    public GetShiftsMapping()
    {
        CreateMap<DoctorShift, GetShiftsResponse>();

        CreateMap<Doctor, GetShiftsResponseDoctor>();
    }
}
