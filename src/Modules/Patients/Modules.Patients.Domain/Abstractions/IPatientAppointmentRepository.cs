using Common.Shared.Repositories;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Domain.Abstractions;

public interface IPatientAppointmentRepository : IRepository<PatientAppointment>
{
}
