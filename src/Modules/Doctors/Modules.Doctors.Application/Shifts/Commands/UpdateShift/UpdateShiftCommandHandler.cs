using Common.Shared;
using Common.Shared.Constants;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Commands.UpdateShift;

internal sealed class UpdateShiftCommandHandler(
    IDoctorShiftRepository doctorShiftRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<UpdateShiftCommand, Result<DoctorShift>>
{
    private readonly IDoctorShiftRepository _doctorShiftRepository = doctorShiftRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DoctorShift>> Handle(
        UpdateShiftCommand request,
        CancellationToken cancellationToken)
    {
        if (!_doctorShiftRepository.IsShiftAvailable(request.StartAt, request.EndAt))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                ErrorConstants.ShiftUnavailableMessage);
        }

        var shift = _doctorShiftRepository.GetById(request.Id);
        if (shift is null)
        {
            return new Error(
                SharedErrorConstants.NotFoundTitle,
                ErrorConstants.ShiftNotFoundMessage);
        }

        shift.Update(request.StartAt, request.EndAt);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return shift;
    }
}
