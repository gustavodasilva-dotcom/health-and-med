using MediatR;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Queries.GetShiftsByDoctor;

internal sealed class GetShiftsByDoctorQueryHandler(IDoctorShiftRepository doctorShiftRepository)
    : IRequestHandler<GetShiftsByDoctorQuery, IEnumerable<DoctorShift>>
{
    private readonly IDoctorShiftRepository _doctorShiftRepository = doctorShiftRepository;

    public Task<IEnumerable<DoctorShift>> Handle(
        GetShiftsByDoctorQuery request,
        CancellationToken cancellationToken)
        => Task.FromResult(
            _doctorShiftRepository.Get(shift => shift.DoctorId == request.DoctorId));
}
