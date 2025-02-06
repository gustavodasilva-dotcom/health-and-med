using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Common.Shared;
using Moq;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Application.Appointments.Commands.UpdateAppointment;

namespace ArchitectureTests.Modules.Doctors.Commands
{
    public class AppointmentUpdateCommandHandlerTests
    {
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
        private readonly Mock<IDoctorsUnitOfWork> _mockDoctorsUnitOfWork;
        private readonly UpdateAppointmentCommandHandler _handler;

        public AppointmentUpdateCommandHandlerTests()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();
            _mockDoctorsUnitOfWork = new Mock<IDoctorsUnitOfWork>();
            _handler = new UpdateAppointmentCommandHandler(_mockAppointmentRepository.Object, _mockDoctorsUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_WhenDateIsNotFree_ReturnsError()
        {
            var request = new UpdateAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

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
        public async Task Handle_WhenAppointmentNotFound_ReturnsError()
        {
            var request = new UpdateAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(3));

            _mockAppointmentRepository
                    .Setup(repo => repo.ExistsDateFree(request.IdDoctor, request.DateFrom, request.DateUntil))
                    .Returns(true);

            _mockAppointmentRepository
                .Setup(repo => repo.GetById(request.Id))
                    .Returns((Appointment)null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsType<Error>(result.Error);
            var error = result.Error;
            Assert.Equal(ErrorConstants.NotFoundTitle, error?.Title);
            Assert.Equal("Appointment not found.", error?.Message);
        }

        [Fact]
        public async Task Handle_WhenAppointmentUpdatedSuccessfully_ReturnsUpdatedAppointment()
        {

            var request = new UpdateAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

            var existingAppointment = new Appointment(request.IdDoctor, request.IdPatient, DateTime.Now, DateTime.Now.AddHours(1));

            _mockAppointmentRepository
                .Setup(repo => repo.ExistsDateFree(request.IdDoctor, request.DateFrom, request.DateUntil))
                .Returns(true);

            _mockAppointmentRepository
                .Setup(repo => repo.GetById(request.Id))
                .Returns(existingAppointment);

            _mockAppointmentRepository
                .Setup(repo => repo.Update(It.IsAny<Appointment>()))
                .Verifiable();

            _mockDoctorsUnitOfWork
                .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsType<Result>(result);
            Assert.True(result.IsSuccess);

            _mockDoctorsUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
