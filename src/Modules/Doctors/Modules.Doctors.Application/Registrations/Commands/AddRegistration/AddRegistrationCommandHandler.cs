using Common.Shared;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;

namespace Modules.Doctors.Application.Registrations.Commands.AddRegistration;

internal sealed class AddRegistrationCommandHandler(
    IDoctorRepository doctorRepository,
    IDoctorRegistrationRepository doctorRegistrationRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<AddRegistrationCommand, Result>
{
    private readonly IDoctorRepository _doctorRepository = doctorRepository;
    private readonly IDoctorRegistrationRepository _doctorRegistrationRepository = doctorRegistrationRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        AddRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = _doctorRepository.GetById(request.DoctorId);
        if (doctor is null)
        {
            return new Error(
                SharedErrorConstants.NotFoundTitle,
                "No doctor was found with the given id.",
                StatusCodes.Status404NotFound);
        }

        if (_doctorRepository.IsRegisteredInState(request.RegistrationNumber, request.RegistrationState))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                ErrorConstants.RegistrationNumberIsRegisteredInStateMessage);
        }

        var registration = doctor.AddRegistration(request.RegistrationNumber, request.RegistrationState);

        _doctorRegistrationRepository.Update(registration);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
