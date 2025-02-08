using Common.Shared;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Commands.CreateShift;

internal sealed class CreateShiftCommandHandler(
    IDoctorRepository doctorRepository,
    IDoctorShiftRepository doctorShiftRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<CreateShiftCommand, Result>
{
    private readonly IDoctorRepository _doctorRepository = doctorRepository;
    private readonly IDoctorShiftRepository _doctorShiftRepository = doctorShiftRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        CreateShiftCommand request,
        CancellationToken cancellationToken)
    {
        request.Deconstruct(out Guid doctorId, out DateTime startAt, out DateTime endAt);

        var doctor = _doctorRepository.GetById(doctorId);
        if (doctor is null)
        {
            return new Error(
                SharedErrorConstants.NotFoundTitle,
                "No doctor was found with the given id.",
                StatusCodes.Status404NotFound);
        }

        if (!_doctorShiftRepository.IsShiftAvailableForDoctor(doctorId, startAt, endAt))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                ErrorConstants.ShiftUnavailableMessage,
                StatusCodes.Status409Conflict);
        }

        var shift = new DoctorShift
        {
            StartAt = request.StartAt,
            EndAt = request.EndAt
        };
        doctor.AddShift(shift);

        _doctorShiftRepository.Add(shift);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
