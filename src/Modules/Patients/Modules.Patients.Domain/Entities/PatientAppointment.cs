using Common.Shared.Abstractions;
using Modules.Patients.Domain.Enums;

namespace Modules.Patients.Domain.Entities;

public sealed class PatientAppointment : BaseEntity
{
    private readonly Patient? _patient = null;

    public Guid PatientId { get; private set; }

    public required Guid DoctorShiftId { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

    public string? AnalysisMessage { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public decimal? AppointmentPrice { get; set; }

    public DateTime? CancelledAt { get; set; }

    public string? CancellationMotive { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [PatientId, DoctorShiftId];

    public Patient Patient
    {
        get
        {
            ArgumentNullException.ThrowIfNull(_patient);
            return _patient;
        }
    }

    public void SetAnalysisResult(AppointmentStatus status, string message)
    {
        Status = status;
        AnalysisMessage = message.Trim();
    }

    public void DenyByDoctor()
    {
        Status = AppointmentStatus.AppointmentDeniedByDoctor;
    }
}
