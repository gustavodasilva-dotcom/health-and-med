using Common.Shared.Abstractions;

namespace Modules.Patients.Domain.Entities
{
    public sealed class PatientAppointment : BaseEntity
    {
        private readonly Patient? _patient = null;

        public Guid PatientId { get; private set; }

        public Guid DoctorId { get; private set; }

        public required DateTime StartAt { get; set; }

        public required DateTime EndAt { get; set; }

        public override IEnumerable<object> GetAtomicValues()
            => [PatientId, StartAt, EndAt];

        public Patient Patient
        {
            get
            {
                ArgumentNullException.ThrowIfNull(_patient);
                return _patient;
            }
        }
    }
}
