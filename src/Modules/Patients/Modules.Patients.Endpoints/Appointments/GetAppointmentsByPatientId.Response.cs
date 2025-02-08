using System.Text.Json.Serialization;
using Common.Shared.Contracts;

namespace Modules.Patients.Endpoints.Appointments;

internal sealed class GetAppointmentsByPatientIdResponse
{
    public required Guid Id { get; init; }

    [JsonPropertyName("doctor_shift_id")]
    public required Guid DoctorShiftId { get; init; }

    [JsonPropertyName("create_at")]
    public required DateTime CreatedAt { get; init; }

    public required EnumResponse Status { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("denial_reason")]
    public required string? AnalysisMessage { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("accepted_at")]
    public required DateTime? AcceptedAt { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("appointment_price")]
    public required decimal? AppointmentPrice { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("cancelled_at")]
    public required DateTime? CancelledAt { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("cancellation_motive")]
    public required string? CancellationMotive { get; init; }
}
