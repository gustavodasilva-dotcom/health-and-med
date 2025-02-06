using Common.Shared;
using Modules.Doctors.Application.Appointments.Commands.DeleteAppointment;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace ArchitectureTests.Modules.Doctors.Commands
{
    public class DeleteAppointmentCommandHandlerTests
    {
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
        private readonly Mock<IDoctorsUnitOfWork> _mockDoctorsUnitOfWork;
        private readonly DeleteAppointmentCommandHandler _handler;

        public DeleteAppointmentCommandHandlerTests()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();
            _mockDoctorsUnitOfWork = new Mock<IDoctorsUnitOfWork>();

            _handler = new DeleteAppointmentCommandHandler(_mockAppointmentRepository.Object, _mockDoctorsUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_WhenAppointmentNotFound_ReturnsError()
        {
            var request = new DeleteAppointmentCommand(Guid.NewGuid());

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
            var request = new DeleteAppointmentCommand(Guid.NewGuid());
            var existingAppointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(1));

            _mockAppointmentRepository
                .Setup(repo => repo.GetById(request.Id))
                .Returns(existingAppointment);

            _mockDoctorsUnitOfWork
                .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsType<Result>(result);
            Assert.True(result.IsSuccess);

            _mockAppointmentRepository.Verify(repo => repo.GetById(request.Id), Times.Once);
            _mockDoctorsUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
