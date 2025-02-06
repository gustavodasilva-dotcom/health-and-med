using Common.Shared.Abstractions;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Domain.Entities;

public sealed class DoctorRegistration : BaseEntity
{
    private readonly Doctor? _doctor = null;

    private readonly HashSet<DoctorShift> _shifts = [];

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

    public IReadOnlySet<DoctorShift> Schedules
        => _shifts;

    public void Update(UFs state, int number)
    {
        State = state;
        Number = number;
    }

    public void AddShift(DoctorShift shift)
        => _shifts.Add(shift);
}
