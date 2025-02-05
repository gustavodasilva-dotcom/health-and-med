using Common.Shared.Repositories;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using System.Linq.Expressions;

namespace Modules.Doctors.Persistence.Repositories
{
    internal sealed class AppointmentRepository(DoctorsDbContext dbContext) :
        Repository<DoctorsDbContext, Appointment>(dbContext),
        IAppointmentRepository
    {
        public bool ExistsDateFree(Guid idDoctor,DateTime dateFrom, DateTime dateUntil)
            => !DbContext.Appointments
                .Any(app => !app.IsDeleted && app.IdDoctor == idDoctor && dateFrom >= app.DateFrom && dateUntil <= app.DateUntil);

        public IEnumerable<Appointment> GetByFilter(Expression<Func<Appointment, bool>> filter)
        {
            return DbContext.Appointments.Where(filter);
        }
    }
}
