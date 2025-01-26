using Common.Shared;
using MediatR;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Application.Doctors.Commands.RegisterDoctor;

public sealed record RegisterDoctorCommand(string Name, string Cpf, UFs CrmUf, int Crm, string Email, string Password)
    : IRequest<Result>;
