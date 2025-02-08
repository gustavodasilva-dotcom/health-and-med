using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Commands.AcceptAppointment;

internal sealed class AcceptAppointmentCommandHandler(
    IRepository<DoctorShiftAppointment> doctorShiftAppointmentRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<AcceptAppointmentCommand, Result>
{
    private readonly IRepository<DoctorShiftAppointment> _doctorShiftAppointmentRepository
        = doctorShiftAppointmentRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        AcceptAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var appointment = _doctorShiftAppointmentRepository.GetById(request.AppointmentId);
        if (appointment is null)
        {
            return new Error(
                SharedErrorConstants.NotFoundTitle,
                "Appointment not found.",
                StatusCodes.Status404NotFound);
        }

        var result = appointment.AcceptAppointment();
        if (result.IsFailure)
        {
            return result.Error!;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
