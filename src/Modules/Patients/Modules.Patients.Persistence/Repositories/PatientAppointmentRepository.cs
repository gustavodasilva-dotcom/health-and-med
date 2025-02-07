using Common.Shared.Repositories;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Patients.Persistence.Repositories
{
    internal sealed class PatientAppointmentRepository(PatientsDbContext dbContext) :
        Repository<PatientsDbContext, PatientAppointment>(dbContext),
        IPatientAppointmentRepository
    {
    }
}
