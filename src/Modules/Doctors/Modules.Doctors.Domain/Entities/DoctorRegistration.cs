using Common.Shared.Abstractions;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Domain.Entities;

public sealed class DoctorRegistration : BaseEntity
{
    private readonly Doctor? _doctor = null;

    public Guid DoctorId { get; private set; }

    public required UFs State { get; set; }

    public required int Number { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [State, Number];

    public Doctor Doctor
    {
        get
        {
            ArgumentNullException.ThrowIfNull(_doctor);
            return _doctor;
        }
    }
}
