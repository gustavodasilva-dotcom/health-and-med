using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Accesses.Commands.LoginDoctor;

public sealed record LoginDoctorCommand(int RegistrationNumber, string Password)
    : IRequest<Result<string>>;
