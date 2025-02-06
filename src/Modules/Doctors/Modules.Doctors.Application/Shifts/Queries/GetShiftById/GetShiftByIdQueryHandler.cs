using MediatR;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Queries.GetShiftById;

internal sealed class GetShiftByIdQueryHandler(IDoctorShiftRepository doctorShiftRepository)
    : IRequestHandler<GetShiftByIdQuery, DoctorShift?>
{
    private readonly IDoctorShiftRepository _doctorShiftRepository = doctorShiftRepository;

    public Task<DoctorShift?> Handle(
        GetShiftByIdQuery request,
        CancellationToken cancellationToken)
        => Task.FromResult(_doctorShiftRepository.GetById(request.Id));
}
