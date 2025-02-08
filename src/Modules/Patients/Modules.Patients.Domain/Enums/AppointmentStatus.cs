using System.ComponentModel.DataAnnotations;

namespace Modules.Patients.Domain.Enums;

public enum AppointmentStatus
{
    Pending = 1,

    [Display(Name = "Appointment denied by the system")]
    AppointmentDeniedBySystem = 2,

    [Display(Name = "Appointment denied by the doctor")]
    AppointmentDeniedByDoctor = 3,

    [Display(Name = "Accepted by doctor")]
    AcceptedByDoctor = 4,

    Cancelled = 5
}
