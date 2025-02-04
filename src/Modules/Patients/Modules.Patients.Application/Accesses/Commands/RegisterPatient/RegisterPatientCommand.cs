using Common.Shared;
using MediatR;

namespace Modules.Patients.Application.Accesses.Commands.RegisterPatient;

public sealed record RegisterPatientCommand(string Name, string Cpf, string Email, string Password)
    : IRequest<Result>;
