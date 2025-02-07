using Common.Shared.Contracts;

namespace Modules.Doctors.Endpoints.Doctors;

internal sealed class GetDoctorsResponseRegistrations
{
    public required int Number { get; init; }

    public required EnumResponse State { get; init; }
}
