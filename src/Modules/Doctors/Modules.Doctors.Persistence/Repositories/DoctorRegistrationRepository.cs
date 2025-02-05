using Common.Shared.Repositories;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Persistence.Repositories;

internal sealed class DoctorRegistrationRepository(DoctorsDbContext dbContext) :
    Repository<DoctorsDbContext, DoctorRegistration>(dbContext),
    IDoctorRegistrationRepository;
