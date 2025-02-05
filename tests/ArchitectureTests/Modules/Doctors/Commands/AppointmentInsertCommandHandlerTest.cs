using Common.Shared;
using Common.Shared.Constants;
using Modules.Doctors.Application.Appointments.Commands.Insert;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace ArchitectureTests.Modules.Doctors.Commands
{
    public class AppointmentInsertCommandHandlerTests
    {
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
        private readonly Mock<IAppoinmentUnitOfWork> _mockAppoinmentUnitOfWork;
        private readonly AppointmentInsertCommandHandler _handler;

        public AppointmentInsertCommandHandlerTests()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();
            _mockAppoinmentUnitOfWork = new Mock<IAppoinmentUnitOfWork>();

            _handler = new AppointmentInsertCommandHandler(_mockAppointmentRepository.Object, _mockAppoinmentUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_WhenDateIsNotFree_ReturnsError()
        {
            var request = new AppointmentInsertCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

            _mockAppointmentRepository
                .Setup(repo => repo.ExistsDateFree(request.IdDoctor, request.DateFrom, request.DateUntil))
                .Returns(false);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsType<Error>(result.Error);
            var error = result.Error;
            Assert.Equal(ErrorConstants.InvalidOperationTitle, error?.Title);
            Assert.Equal("Busy date for scheduling.", error?.Message);
        }

        [Fact]
        public async Task Handle_WhenDateIsFree_CreatesAppointmentAndSavesIt()
        {
            var request = new AppointmentInsertCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

            var newAppointment = new Appointment(
                request.IdDoctor,
                request.IdPatient,
                request.DateFrom,
                request.DateUntil
            );

            _mockAppointmentRepository
                .Setup(repo => repo.ExistsDateFree(request.IdDoctor, request.DateFrom, request.DateUntil))
                .Returns(true);

            _mockAppointmentRepository
                .Setup(repo => repo.Add(It.IsAny<Appointment>()))
                .Verifiable();

            _mockAppoinmentUnitOfWork
                .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsType<Appointment>(result.Value);
            var appointment = result.Value;
            Assert.Equal(request.IdDoctor, appointment?.IdDoctor);
            Assert.Equal(request.IdPatient, appointment?.IdPatient);
            Assert.Equal(request.DateFrom, appointment?.DateFrom);
            Assert.Equal(request.DateUntil, appointment?.DateUntil);

            _mockAppointmentRepository.Verify(repo => repo.Add(It.IsAny<Appointment>()), Times.Once);
            _mockAppoinmentUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
