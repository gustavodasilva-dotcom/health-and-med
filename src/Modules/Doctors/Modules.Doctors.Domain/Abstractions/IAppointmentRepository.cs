using Common.Shared.Repositories;
using Modules.Doctors.Domain.Entities;
using System.Linq.Expressions;

namespace Modules.Doctors.Domain.Abstractions
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool ExistsDateFree(Guid idDoctor,DateTime dateFrom, DateTime dateUntil);
        IEnumerable<Appointment> GetByFilter(Expression<Func<Appointment, bool>> filter);
    }
}
