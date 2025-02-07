using MediatR;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Doctors.Queries.GetDoctors;

internal sealed class GetDoctorsQueryHandler(IDoctorRepository doctorRepository)
    : IRequestHandler<GetDoctorsQuery, IEnumerable<Doctor>>
{
    private readonly IDoctorRepository _doctorRepository = doctorRepository;

    public Task<IEnumerable<Doctor>> Handle(
        GetDoctorsQuery request,
        CancellationToken cancellationToken)
    {
        var doctors = _doctorRepository.Get(_ => true);

        if (!string.IsNullOrEmpty(request.Name))
        {
            doctors = doctors.Where(doctor => doctor.Name
                .Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }

        if (request.Specialty is not null)
        {
            doctors = doctors.Where(doctor => doctor.Specialty == request.Specialty);
        }

        if (request.State is not null)
        {
            doctors = doctors.Where(doctor => doctor
                .Registrations.Any(reg => reg.State == request.State));
        }

        return Task.FromResult(doctors);
    }
}
