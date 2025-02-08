using Modules.Patients.Application.Appointments.Queries.GetAppointmentsByPatientId;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace Modules.Patients.UnitTests.Queries
{
    public class GetAppointmentsByPatientIdQueryTests
    {
        private readonly Mock<IPatientAppointmentRepository> _mockPatientAppointmentRepository;
        private readonly GetAppointmentsByPatientIdQueryHandler _handler;

        public GetAppointmentsByPatientIdQueryTests()
        {
            _mockPatientAppointmentRepository = new Mock<IPatientAppointmentRepository>();
            _handler = new GetAppointmentsByPatientIdQueryHandler(_mockPatientAppointmentRepository.Object);
        }

        [Fact]
        public async Task Handle_WhenAppointmentsExistInRange_ReturnsAppointments()
        {
            var patientId = Guid.NewGuid();

            IEnumerable<PatientAppointment> shifts =
            [
                new()
            {
                StartAt = DateTime.Now
            },
            new()
            {
                StartAt = DateTime.Now.AddHours(2)
            }
            ];

            _mockPatientAppointmentRepository
                .Setup(repo => repo.Get(It.IsAny<Expression<Func<PatientAppointment, bool>>>()))
                .Returns(shifts);

            var result = await _handler.Handle(
                new GetAppointmentsByPatientIdQuery(patientId),
                CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Handle_WhenNoShiftsExistInRange_ReturnsEmptyList()
        {
            var patientId = Guid.NewGuid();

            IEnumerable<PatientAppointment> shifts = [];

            _mockPatientAppointmentRepository
                .Setup(repo => repo.Get(It.IsAny<Expression<Func<PatientAppointment, bool>>>()))
                .Returns(shifts);

            var result = await _handler.Handle(
                new GetAppointmentsByPatientIdQuery(patientId),
                CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
