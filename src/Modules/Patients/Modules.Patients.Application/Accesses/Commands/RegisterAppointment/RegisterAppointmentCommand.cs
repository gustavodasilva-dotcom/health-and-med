using Common.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Patients.Application.Accesses.Commands.RegisterAppointment
{
    public sealed record RegisterAppointmentCommand : IRequest<Result>
    {
    }
}
