using Modules.Doctors.Application.Appointments.Queries.GetAppointments;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace ArchitectureTests.Modules.Doctors.Queries
{
    public class GetAppointmentsQueryHandlerTests
    {
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
        private readonly GetAppointmentsQueryHandler _handler;

        public GetAppointmentsQueryHandlerTests()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();

            _handler = new GetAppointmentsQueryHandler(_mockAppointmentRepository.Object);
        }

        [Fact]
        public async Task Handle_WhenAppointmentsExistInRange_ReturnsAppointments()
        {
            var fromDate = DateTime.Now.AddDays(-1);
            var toDate = DateTime.Now.AddDays(1);

            var appointments = new List<Appointment>
            {
                new Appointment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(1)),
                new Appointment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddHours(2), DateTime.Now.AddHours(3))
            };

            _mockAppointmentRepository
                .Setup(repo => repo.GetByFilter(It.IsAny<Expression<Func<Appointment, bool>>>()))
                .Returns(appointments);

            var result = await _handler.Handle(new GetAppointmentsQuery(fromDate, toDate), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Handle_WhenNoAppointmentsExistInRange_ReturnsEmptyList()
        {

            var fromDate = DateTime.Now.AddDays(10);
            var toDate = DateTime.Now.AddDays(20);

            var appointments = new List<Appointment>();

            _mockAppointmentRepository
                .Setup(repo => repo.GetByFilter(It.IsAny<Expression<Func<Appointment, bool>>>()))
                .Returns(appointments);

            var result = await _handler.Handle(new GetAppointmentsQuery(fromDate, toDate), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
