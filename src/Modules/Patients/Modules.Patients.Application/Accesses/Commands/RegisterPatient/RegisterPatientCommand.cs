using Common.Shared;
using MediatR;

namespace Modules.Patients.Application.Accesses.Commands.RegisterPatient;

public sealed record RegisterPatientCommand(string Name, string Ssn, string Email, string Password)
    : IRequest<Result>;
