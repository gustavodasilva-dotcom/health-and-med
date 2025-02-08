using Common.Shared;
using Common.Shared.Abstractions;
using Common.Shared.Constants;
using Modules.Patients.Domain.DomainEvents;
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

    public Result CancelAppointment(string motive)
    {
        if (Status == AppointmentStatus.Cancelled)
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "Appointment is already cancelled.");
        }

        Status = AppointmentStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        CancellationMotive = motive.Trim();

        RaiseDomainEvent(new AppointmentCancelledDomainEvent(DoctorShiftId));

        return Result.Success();
    }

    public Result DenyBySystem(string message)
    {
        if (Status != AppointmentStatus.Pending)
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "The patient's appointment should be pending.");
        }

        Status = AppointmentStatus.AppointmentDeniedBySystem;
        AnalysisMessage = message.Trim();

        return Result.Success();
    }

    public Result DenyByDoctor()
    {
        if (Status != AppointmentStatus.Pending)
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "The patient's appointment should be pending.");
        }

        Status = AppointmentStatus.AppointmentDeniedByDoctor;

        return Result.Success();
    }

    public Result AcceptByDoctor()
    {
        if (Status != AppointmentStatus.Pending)
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "The patient's appointment should be pending.");
        }

        Status = AppointmentStatus.AcceptedByDoctor;
        AcceptedAt = DateTime.UtcNow;

        return Result.Success();
    }
}
