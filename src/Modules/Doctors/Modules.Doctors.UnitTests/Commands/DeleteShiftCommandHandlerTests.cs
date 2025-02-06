using Common.Shared;
using Modules.Doctors.Application.Shifts.Commands.DeleteShift;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace Modules.Doctors.UnitTests.Commands;

public class DeleteShiftCommandHandlerTests
{
    private readonly Mock<IDoctorShiftRepository> _mockDoctorShiftRepository;
    private readonly Mock<IDoctorsUnitOfWork> _mockUnitOfWork;
    private readonly DeleteShiftCommandHandler _handler;

    public DeleteShiftCommandHandlerTests()
    {
        _mockDoctorShiftRepository = new Mock<IDoctorShiftRepository>();
        _mockUnitOfWork = new Mock<IDoctorsUnitOfWork>();
        _handler = new DeleteShiftCommandHandler(_mockDoctorShiftRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_WhenShiftNotFound_ReturnsError()
    {
        var request = new DeleteShiftCommand(Guid.NewGuid());

        _mockDoctorShiftRepository
            .Setup(repo => repo.GetById(request.Id))
            .Returns((DoctorShift?)null);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.IsType<Error>(result.Error);
        var error = result.Error;
        Assert.Equal(ErrorConstants.NotFoundTitle, error?.Title);
        Assert.Equal("Shift not found.", error?.Message);
    }

    [Fact]
    public async Task Handle_WhenShiftFound_DeletesShiftAndSavesChanges()
    {
        var request = new DeleteShiftCommand(Guid.NewGuid());
        var existingShift = new DoctorShift
        {
            StartAt = DateTime.Now,
            EndAt = DateTime.Now.AddHours(1)
        };

        _mockDoctorShiftRepository
            .Setup(repo => repo.GetById(request.Id))
            .Returns(existingShift);

        _mockUnitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.IsType<Result>(result);
        Assert.True(result.IsSuccess);

        _mockDoctorShiftRepository.Verify(repo => repo.GetById(request.Id), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
