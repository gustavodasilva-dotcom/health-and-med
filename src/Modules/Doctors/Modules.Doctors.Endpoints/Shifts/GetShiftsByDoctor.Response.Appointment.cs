using System.Text.Json.Serialization;
using Common.Shared.Contracts;

namespace Modules.Doctors.Endpoints.Shifts;

internal sealed class GetShiftsByDoctorResponseAppointment
{
    [JsonPropertyName("appointment_id")]
    public required Guid AppointmentId { get; init; }

    [JsonPropertyName("patient_id")]
    public required Guid PatientId { get; init; }

    public required EnumResponse Status { get; init; }
}
