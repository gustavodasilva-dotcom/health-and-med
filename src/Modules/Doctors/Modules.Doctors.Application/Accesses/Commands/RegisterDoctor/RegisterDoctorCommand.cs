using Common.Shared;
using MediatR;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Application.Accesses.Commands.RegisterDoctor;

public sealed record RegisterDoctorCommand(
    string Name,
    string Ssn,
    int RegistrationNumber,
    MedicalSpecialties Specialty,
    string Email,
    string Password
) : IRequest<Result<Guid>>;
