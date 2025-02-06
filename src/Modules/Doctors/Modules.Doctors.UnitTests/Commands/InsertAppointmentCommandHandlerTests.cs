using Common.Shared;
using FluentAssertions;
using Modules.Doctors.Application.Appointments.Commands.InsertAppointment;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace Modules.Doctors.UnitTests.Commands;

public class AppointmentInsertCommandHandlerTests
{
    private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
    private readonly Mock<IDoctorsUnitOfWork> _mockAppoinmentUnitOfWork;
    private readonly InsertAppointmentCommandHandler _handler;

    public AppointmentInsertCommandHandlerTests()
    {
        _mockAppointmentRepository = new Mock<IAppointmentRepository>();
        _mockAppoinmentUnitOfWork = new Mock<IDoctorsUnitOfWork>();
        _handler = new InsertAppointmentCommandHandler(_mockAppointmentRepository.Object, _mockAppoinmentUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_WhenDateIsNotFree_ReturnsError()
    {
        var request = new InsertAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

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
        var request = new InsertAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

        var newAppointment = new Appointment
        {
            IdDoctor = request.IdDoctor,
            IdPatient = request.IdPatient,
            DateFrom = request.DateFrom,
            DateUntil = request.DateUntil
        };

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
        result.IsSuccess.Should().BeTrue();

        _mockAppointmentRepository.Verify(repo => repo.Add(It.IsAny<Appointment>()), Times.Once);
        _mockAppoinmentUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
