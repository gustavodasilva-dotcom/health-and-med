using Common.Shared;
using Common.Shared.Constants;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;

namespace Modules.Doctors.Application.Shifts.Commands.DeleteShift;

internal sealed class DeleteShiftCommandHandler(
    IDoctorShiftRepository doctorShiftRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<DeleteShiftCommand, Result>
{
    private readonly IDoctorShiftRepository _doctorShiftRepository = doctorShiftRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        DeleteShiftCommand request,
        CancellationToken cancellationToken)
    {
        var shifts = _doctorShiftRepository.GetById(request.Id);
        if (shifts is null)
        {
            return new Error(
                SharedErrorConstants.NotFoundTitle,
                ErrorConstants.ShiftNotFoundMessage);
        }

        shifts.Delete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
