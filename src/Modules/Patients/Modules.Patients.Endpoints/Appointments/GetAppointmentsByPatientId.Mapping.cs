using AutoMapper;
using Common.Shared.Extensions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Endpoints.Appointments;

public sealed class GetAppointmentsByPatientIdMapping : Profile
{
    public GetAppointmentsByPatientIdMapping()
    {
        CreateMap<PatientAppointment, GetAppointmentsByPatientIdResponse>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToEnumResponse()));
    }
}
