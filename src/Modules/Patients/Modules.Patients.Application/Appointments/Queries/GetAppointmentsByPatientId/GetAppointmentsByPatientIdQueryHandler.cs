using Common.Shared.Repositories;
using MediatR;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Application.Appointments.Queries.GetAppointmentsByPatientId;

internal sealed class GetAppointmentsByPatientIdQueryHandler(
    IRepository<PatientAppointment> patientAppointmentRepository) :
    IRequestHandler<GetAppointmentsByPatientIdQuery, IEnumerable<PatientAppointment>>
{
    private readonly IRepository<PatientAppointment> _patientAppointmentRepository
        = patientAppointmentRepository;

    public Task<IEnumerable<PatientAppointment>> Handle(
        GetAppointmentsByPatientIdQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = _patientAppointmentRepository.Get(app => app.PatientId == request.PatientId);
        return Task.FromResult(appointments);
    }
}
