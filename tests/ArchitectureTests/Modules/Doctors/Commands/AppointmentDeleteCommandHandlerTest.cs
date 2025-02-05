using Common.Shared;
using Common.Shared.Constants;
using Modules.Doctors.Application.Appointments.Commands.Delete;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace ArchitectureTests.Modules.Doctors.Commands
{
    public class AppointmentDeleteCommandHandlerTest
    {
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
        private readonly Mock<IAppoinmentUnitOfWork> _mockAppoinmentUnitOfWork;
        private readonly AppointmentDeleteCommandHandler _handler;

        public AppointmentDeleteCommandHandlerTest()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();
            _mockAppoinmentUnitOfWork = new Mock<IAppoinmentUnitOfWork>();

            _handler = new AppointmentDeleteCommandHandler(_mockAppointmentRepository.Object, _mockAppoinmentUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_WhenAppointmentNotFound_ReturnsError()
        {
            var request = new AppointmentDeleteCommand(Guid.NewGuid());

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
        public async Task Handle_WhenAppointmentFound_DeletesAppointmentAndSavesChanges()
        {
            var request = new AppointmentDeleteCommand(Guid.NewGuid());
            var existingAppointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(1));

            _mockAppointmentRepository
                .Setup(repo => repo.GetById(request.Id))
                .Returns(existingAppointment);

            _mockAppoinmentUnitOfWork
                .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsType<Result>(result);
            Assert.True(result.IsSuccess);

            _mockAppointmentRepository.Verify(repo => repo.GetById(request.Id), Times.Once);
            _mockAppoinmentUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
