using Common.Shared;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        request.Deconstruct(out Guid shiftId, out DateTime startAt, out DateTime endAt);

        var shift = _doctorShiftRepository.GetById(shiftId);
        if (shift is null)
        {
            return new Error(
                SharedErrorConstants.NotFoundTitle,
                ErrorConstants.ShiftNotFoundMessage,
                StatusCodes.Status400BadRequest);
        }

        
        if (!_doctorShiftRepository.IsShiftAvailableForDoctor(shift.DoctorId, startAt, endAt))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                ErrorConstants.ShiftUnavailableMessage,
                StatusCodes.Status409Conflict);
        }

        shift.Update(startAt, endAt);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return shift;
    }
}
