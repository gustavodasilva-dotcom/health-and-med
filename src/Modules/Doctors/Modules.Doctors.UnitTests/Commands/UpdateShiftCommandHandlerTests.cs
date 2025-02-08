using Common.Shared;
using FluentAssertions;
using Modules.Doctors.Application.Shifts.Commands.UpdateShift;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace Modules.Doctors.UnitTests.Commands;

public class UpdateShiftCommandHandlerTests
{
    private readonly Mock<IDoctorShiftRepository> _mockDoctorShiftRepository;
    private readonly Mock<IDoctorsUnitOfWork> _mockDoctorsUnitOfWork;
    private readonly UpdateShiftCommandHandler _handler;

    public UpdateShiftCommandHandlerTests()
    {
        _mockDoctorShiftRepository = new Mock<IDoctorShiftRepository>();
        _mockDoctorsUnitOfWork = new Mock<IDoctorsUnitOfWork>();
        _handler = new UpdateShiftCommandHandler(_mockDoctorShiftRepository.Object, _mockDoctorsUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_WhenDateIsNotFree_ReturnsError()
    {
        // Arrange
        var existingShift = new DoctorShift
        {
            StartAt = DateTime.UtcNow.AddDays(1),
            EndAt = DateTime.UtcNow.AddDays(1).AddHours(4)
        };

        var request = new UpdateShiftCommand(
            Id: Guid.NewGuid(),
            StartAt: DateTime.UtcNow,
            EndAt: DateTime.UtcNow.AddHours(1)
        );

        _mockDoctorShiftRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(existingShift);

        _mockDoctorShiftRepository
            .Setup(repo => repo.IsShiftAvailableForDoctor(
                It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(false);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsType<Error>(result.Error);
        var error = result.Error;
        Assert.Equal(ErrorConstants.InvalidOperationTitle, error?.Title);
        Assert.Equal(ErrorConstants.ShiftUnavailableMessage, error?.Message);
    }

    [Fact]
    public async Task Handle_WhenShiftNotFound_ReturnsError()
    {
        // Arrange
        var request = new UpdateShiftCommand(
            Id: Guid.NewGuid(),
            StartAt: DateTime.UtcNow,
            EndAt: DateTime.UtcNow.AddHours(1)
        );

        _mockDoctorShiftRepository
            .Setup(repo => repo.IsShiftAvailableForDoctor(
                It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        _mockDoctorShiftRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns((DoctorShift?)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsType<Error>(result.Error);
        var error = result.Error;
        Assert.Equal(ErrorConstants.NotFoundTitle, error?.Title);
        Assert.Equal("Shift not found.", error?.Message);
    }

    [Fact]
    public async Task Handle_WhenShiftUpdatedSuccessfully_ReturnsUpdatedShift()
    {
        // Arrange
        var existingShift = new DoctorShift
        {
            StartAt = DateTime.Now,
            EndAt = DateTime.Now.AddHours(1)
        };

        var request = new UpdateShiftCommand(
            Id: existingShift.Id,
            StartAt: DateTime.UtcNow,
            EndAt: DateTime.UtcNow.AddHours(1)
        );

        _mockDoctorShiftRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(existingShift);

        _mockDoctorShiftRepository
            .Setup(repo => repo.IsShiftAvailableForDoctor(
                It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        _mockDoctorShiftRepository
            .Setup(repo => repo.Update(It.IsAny<DoctorShift>()))
            .Verifiable();

        _mockDoctorsUnitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _mockDoctorsUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
