using AutoMapper;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Endpoints.Appointments
{
    public sealed class GetAppointmentsByPatientIdMapping : Profile
    {
        public GetAppointmentsByPatientIdMapping()
        {
            CreateMap<PatientAppointment, GetAppointmentsByPatientIdResponse>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartAt, opts => opts.MapFrom(src => src.StartAt))
                .ForMember(dest => dest.DoctorId, opts => opts.MapFrom(src => src.DoctorId));
        }
    }
}
