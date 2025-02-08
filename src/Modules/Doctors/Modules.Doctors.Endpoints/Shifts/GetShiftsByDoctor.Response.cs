using System.Text.Json.Serialization;

namespace Modules.Doctors.Endpoints.Shifts;

internal sealed class GetShiftsByDoctorResponse
{
    public required Guid Id { get; init; }

    [JsonPropertyName("start_at")]
    public required DateTime StartAt { get; init; }

    [JsonPropertyName("end_at")]
    public required DateTime EndAt { get; init; }

    public required GetShiftsByDoctorResponseAppointment Appointment { get; init; }
}
