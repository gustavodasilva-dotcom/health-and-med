using Common.Shared.Repositories;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Persistence.Repositories
{
    internal sealed class PatientAppointmentRepository(PatientsDbContext dbContext) :
        Repository<PatientsDbContext, PatientAppointment>(dbContext),
        IPatientAppointmentRepository
    {
        public bool IsPatientAvailable(Guid patientId, DateTime startAt, int appointmentDuration)
        {
            return !DbContext.PatientAppointments
                .Any(ap => ap.PatientId == patientId &&
                          ap.StartAt < startAt.AddMinutes(appointmentDuration) && // O agendamento existente começa antes do novo terminar
                          ap.StartAt.AddMinutes(appointmentDuration) > startAt);  // O agendamento existente termina depois do novo começar
        }

        public bool IsDoctorAvailable(Guid doctorId, DateTime startAt, int appointmentDuration)
        {
            return !DbContext.PatientAppointments
                .Any(ap => ap.DoctorId == doctorId &&
                          ap.StartAt < startAt.AddMinutes(appointmentDuration) && // O agendamento existente começa antes do novo terminar
                          ap.StartAt.AddMinutes(appointmentDuration) > startAt); // O agendamento existente termina depois do novo começar
        }
    }
}
