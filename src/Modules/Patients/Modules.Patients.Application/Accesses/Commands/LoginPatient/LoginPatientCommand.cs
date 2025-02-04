using Common.Shared;
using MediatR;

namespace Modules.Patients.Application.Accesses.Commands.LoginPatient;

public sealed record LoginPatientCommand(string Email, string Password)
    : IRequest<Result<string>>;
