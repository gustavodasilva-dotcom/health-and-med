using Common.Shared.Repositories;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Persistence.Repositories
{
    internal sealed class PatientAppointmentRepository(PatientsDbContext dbContext) :
        Repository<PatientsDbContext, PatientAppointment>(dbContext),
        IPatientAppointmentRepository
    {
    }
}
