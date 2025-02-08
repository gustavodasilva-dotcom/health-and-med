using Common.Shared.Repositories;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Domain.Abstractions;

public interface IPatientAppointmentRepository : IRepository<PatientAppointment>
{
    bool IsPatientAvailable(Guid patientId, DateTime startAt, int appointmentDuration);
    bool IsDoctorAvailable(Guid doctorId, DateTime startAt, int appointmentDuration);
}
