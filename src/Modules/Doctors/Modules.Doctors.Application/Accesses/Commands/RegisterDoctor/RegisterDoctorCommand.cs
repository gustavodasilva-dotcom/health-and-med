using Common.Shared;
using MediatR;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Application.Accesses.Commands.RegisterDoctor;

public sealed record RegisterDoctorCommand(
    string Name,
    string Ssn,
    UFs RegistrationState,
    int RegistrationNumber,
    string Email,
    string Password
) : IRequest<Result>;
