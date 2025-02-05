using Common.Shared.Abstractions;

namespace Modules.Doctors.Domain.Entities
{
    public class Appointment(Guid idDoctor, Guid idPatient, DateTime dateFrom, DateTime dateUntil) 
        : BaseEntity
    {
        public Guid IdDoctor { get; set; } = idDoctor;
        public Guid IdPatient { get; set; } = idPatient;
        public DateTime DateFrom { get; set; } = dateFrom;
        public DateTime DateUntil { get; set; } = dateUntil;

        public override IEnumerable<object> GetAtomicValues() => default;

        public void Update(Guid idPatient, DateTime dateFrom, DateTime dateUntil)
        {
            IdPatient = idPatient;
            DateFrom = dateFrom;
            DateUntil = dateUntil;
        }
    }
}
