using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Doctors.Commands.LoginDoctor;

public sealed record LoginDoctorCommand(string Email, string Password) : IRequest<Result<string>>;
