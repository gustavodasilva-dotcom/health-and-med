using Common.Shared;
using Modules.Doctors.Application.Appointments.Commands.DeleteAppointment;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace Modules.Doctors.UnitTests.Commands;

public class AppointmentDeleteCommandHandlerTests
{
    private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
    private readonly Mock<IDoctorsUnitOfWork> _unitOfWork;
    private readonly DeleteAppointmentCommandHandler _handler;

    public AppointmentDeleteCommandHandlerTests()
    {
        _mockAppointmentRepository = new Mock<IAppointmentRepository>();
        _unitOfWork = new Mock<IDoctorsUnitOfWork>();
        _handler = new DeleteAppointmentCommandHandler(_mockAppointmentRepository.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Handle_WhenAppointmentNotFound_ReturnsError()
    {
        var request = new DeleteAppointmentCommand(Guid.NewGuid());

        _mockAppointmentRepository
            .Setup(repo => repo.GetById(request.Id))
            .Returns((Appointment?)null);

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
        var existingAppointment = new Appointment
        {
            IdDoctor = Guid.NewGuid(),
            IdPatient = Guid.NewGuid(),
            DateFrom = DateTime.Now,
            DateUntil = DateTime.Now.AddHours(1)
        };

        _mockAppointmentRepository
            .Setup(repo => repo.GetById(request.Id))
            .Returns(existingAppointment);

        _unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.IsType<Result>(result);
        Assert.True(result.IsSuccess);

        _mockAppointmentRepository.Verify(repo => repo.GetById(request.Id), Times.Once);
        _unitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
