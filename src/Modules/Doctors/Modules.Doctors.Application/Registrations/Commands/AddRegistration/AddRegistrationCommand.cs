using Common.Shared;
using MediatR;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Application.Registrations.Commands.AddRegistration;

public sealed record AddRegistrationCommand(Guid DoctorId, int RegistrationNumber, UFs RegistrationState)
    : IRequest<Result>;
