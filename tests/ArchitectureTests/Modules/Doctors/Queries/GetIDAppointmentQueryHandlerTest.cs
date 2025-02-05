using Modules.Doctors.Application.Appointments.Queries.GetIDAppointment;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace ArchitectureTests.Modules.Doctors.Queries
{
    public class GetIDAppointmentQueryHandlerTest
    {
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
        private readonly GetIDAppointmentQueryHandler _handler;

        public GetIDAppointmentQueryHandlerTest()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();

            _handler = new GetIDAppointmentQueryHandler(_mockAppointmentRepository.Object);
        }

        [Fact]
        public async Task Handle_WhenAppointmentExists_ReturnsAppointment()
        {
            var appointmentId = Guid.NewGuid();
            var expectedAppointment = new Appointment(
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.Now,
                DateTime.Now.AddHours(1)
            );

            _mockAppointmentRepository
                .Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .Returns(expectedAppointment);

            var query = new GetIDAppointmentQuery(appointmentId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(expectedAppointment.Id, result.Id);
            Assert.Equal(expectedAppointment.DateFrom, result.DateFrom);
            Assert.Equal(expectedAppointment.DateUntil, result.DateUntil);
        }

        [Fact]
        public async Task Handle_WhenAppointmentDoesNotExist_ReturnsNull()
        {
            var appointmentId = Guid.NewGuid();

            _mockAppointmentRepository
                .Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .Returns((Appointment)null);

            var query = new GetIDAppointmentQuery(appointmentId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
