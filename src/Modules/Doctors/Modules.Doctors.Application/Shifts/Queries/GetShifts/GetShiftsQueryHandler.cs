using MediatR;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Queries.GetShifts;

internal sealed class GetShiftsQueryHandler(IDoctorShiftRepository doctorShiftRepository)
    : IRequestHandler<GetShiftsQuery, IEnumerable<DoctorShift>>
{
    private readonly IDoctorShiftRepository _doctorShiftRepository = doctorShiftRepository;

    public Task<IEnumerable<DoctorShift>> Handle(
        GetShiftsQuery request,
        CancellationToken cancellationToken)
    {
        var shifts = _doctorShiftRepository.Get(sft =>
            sft.StartAt >= request.FromDate &&
            sft.StartAt <= request.ToDate);

        return Task.FromResult(shifts);
    }
}
