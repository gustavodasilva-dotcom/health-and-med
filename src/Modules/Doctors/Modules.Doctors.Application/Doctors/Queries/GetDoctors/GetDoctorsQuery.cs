using MediatR;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Application.Doctors.Queries.GetDoctors;

public sealed record GetDoctorsQuery(string? Name, MedicalSpecialties? Specialty)
    : IRequest<IEnumerable<Doctor>>;
