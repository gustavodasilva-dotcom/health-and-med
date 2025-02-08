using System.ComponentModel.DataAnnotations;

namespace Modules.Doctors.Domain.Enums;

public enum AppointmentStatus
{
    [Display(Name = "Pending doctor analysis")]
    PendingDoctorAnalysis = 1,

    Accepted = 2,

    Denied = 3,

    Cancelled = 4
}
