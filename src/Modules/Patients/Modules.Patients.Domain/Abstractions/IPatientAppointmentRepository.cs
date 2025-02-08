using Common.Shared.Repositories;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Domain.Abstractions;

public interface IPatientAppointmentRepository : IRepository<PatientAppointment>
{
    bool IsPatientAvailable(Guid patientId, DateTime startAt, DateTime endAt);
    bool IsDoctorAvailable(Guid doctorId, DateTime startAt, DateTime endAt);
}
