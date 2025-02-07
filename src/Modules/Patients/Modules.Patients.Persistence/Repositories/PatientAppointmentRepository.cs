using Common.Shared.Repositories;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Persistence.Repositories
{
    internal sealed class PatientAppointmentRepository(PatientsDbContext dbContext) :
        Repository<PatientsDbContext, PatientAppointment>(dbContext),
        IPatientAppointmentRepository
    {
        public bool IsPatientAvailable(Guid patientId, DateTime startAt, DateTime endAt)
            => !DbContext.PatientAppointments
                .Any(ap => ap.PatientId == patientId && startAt >= ap.StartAt && endAt <= ap.EndAt);

        public bool IsDoctorAvailable(Guid doctorId, DateTime startAt, DateTime endAt)
            => !DbContext.PatientAppointments
                .Any(ap => ap.PatientId == doctorId && startAt >= ap.StartAt && endAt <= ap.EndAt);
    }
}
