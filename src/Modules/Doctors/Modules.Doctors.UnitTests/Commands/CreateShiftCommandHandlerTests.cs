using Common.Shared;
using FluentAssertions;
using Modules.Doctors.Application.Shifts.Commands.CreateShift;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.UnitTests.Commands;

public class CreateShiftCommandHandlerTests
{
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;
    private readonly Mock<IDoctorShiftRepository> _mockDoctorShiftRepository;
    private readonly Mock<IDoctorsUnitOfWork> _mockUnitOfWork;
    private readonly CreateShiftCommandHandler _handler;

    public CreateShiftCommandHandlerTests()
    {
        _mockDoctorRepository = new Mock<IDoctorRepository>();
        _mockDoctorShiftRepository = new Mock<IDoctorShiftRepository>();
        _mockUnitOfWork = new Mock<IDoctorsUnitOfWork>();

        _handler = new CreateShiftCommandHandler(
            _mockDoctorRepository.Object,
            _mockDoctorShiftRepository.Object,
            _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_WhenDateIsNotFree_ReturnsError()
    {
        // Arrange
        var doctor = new Doctor
        {
            Name = "doctor",
            Ssn = "123456",
            RegistrationNumber = 123456,
            Specialty = MedicalSpecialties.GeneralPhysician,
            Email = "doctor@example.com",
            Password = "hashedPassword",
        };

        var request = new CreateShiftCommand(
            DoctorId: doctor.Id,
            StartAt: DateTime.UtcNow,
            EndAt: DateTime.UtcNow.AddHours(1));

        _mockDoctorRepository
            .Setup(repo => repo.GetById(request.DoctorId))
            .Returns(doctor);

        _mockDoctorShiftRepository
            .Setup(repo => repo.IsShiftAvailable(request.StartAt, request.EndAt))
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
    public async Task Handle_WhenDateIsFree_CreatesShiftAndSavesIt()
    {
        // Arrange
        var doctor = new Doctor
        {
            Name = "doctor",
            Ssn = "123456",
            RegistrationNumber = 123456,
            Specialty = MedicalSpecialties.GeneralPhysician,
            Email = "doctor@example.com",
            Password = "hashedPassword"
        };

        var request = new CreateShiftCommand(
            DoctorId: doctor.Id,
            StartAt: DateTime.UtcNow,
            EndAt: DateTime.UtcNow.AddHours(1));

        var newShift = new DoctorShift
        {
            StartAt = request.StartAt,
            EndAt = request.EndAt
        };

        _mockDoctorRepository
            .Setup(repo => repo.GetById(request.DoctorId))
            .Returns(doctor);

        _mockDoctorShiftRepository
            .Setup(repo => repo.IsShiftAvailable(request.StartAt, request.EndAt))
            .Returns(true);

        _mockDoctorShiftRepository
            .Setup(repo => repo.Add(It.IsAny<DoctorShift>()))
            .Verifiable();

        _mockUnitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _mockDoctorShiftRepository.Verify(repo => repo.Add(It.IsAny<DoctorShift>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
