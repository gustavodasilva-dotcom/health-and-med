using Common.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Modules.Doctors.Application.Registrations.Commands.AddRegistration;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;
using Moq;

namespace Modules.Doctors.UnitTests.Commands;

public class AddRegistrationCommandHandlerTests
{
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;
    private readonly Mock<IDoctorRegistrationRepository> _mockDoctorRegistrationRepository;
    private readonly Mock<IDoctorsUnitOfWork> _mockUnitOfWork;
    private readonly AddRegistrationCommandHandler _handler;

    public AddRegistrationCommandHandlerTests()
    {
        _mockDoctorRepository = new Mock<IDoctorRepository>();
        _mockDoctorRegistrationRepository = new Mock<IDoctorRegistrationRepository>();
        _mockUnitOfWork = new Mock<IDoctorsUnitOfWork>();

        _handler = new AddRegistrationCommandHandler(
            _mockDoctorRepository.Object,
            _mockDoctorRegistrationRepository.Object,
            _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDoctorNotFound()
    {
        // Arrange
        var command = new AddRegistrationCommand(Guid.NewGuid(), 2222, UFs.SaoPaulo);

        _mockDoctorRepository
            .Setup(repo => repo.GetById(command.DoctorId))
            .Returns((Doctor?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Error.Should().BeOfType<Error>();
        var error = result.Error;
        error.Message.Should().Be("No doctor was found with the given id.");
        error.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenRegistrationNumberAlreadyRegisteredInState()
    {
        // Arrange
        var command = new AddRegistrationCommand(
            DoctorId: Guid.NewGuid(),
            RegistrationNumber: 2222,
            RegistrationState: UFs.SaoPaulo
        );

        var doctor = new Doctor
        {
            Ssn = "12345",
            Name = "Doctor",
            Password = "password",
            Email = "doctor@exemple.com",
            Specialty = MedicalSpecialties.GeneralPhysician
        };

        _mockDoctorRepository
            .Setup(repo => repo.GetById(command.DoctorId))
            .Returns(doctor);

        _mockDoctorRepository
            .Setup(repo => repo.IsRegisteredInState(command.RegistrationNumber, command.RegistrationState))
            .Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Error.Should().BeOfType<Error>();
        var error = result.Error;
        error.Message.Should().Be("The doctor's registration number is already in use in the informed state.");
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRegistrationIsAddedSuccessfully()
    {
        // Arrange
        var command = new AddRegistrationCommand(
            DoctorId: Guid.NewGuid(),
            RegistrationNumber: 2222,
            RegistrationState: UFs.SaoPaulo
        );

        var doctor = new Doctor
        {
            Ssn = "12345",
            Name = "Doctor",
            Password = "password",
            Email = "doctor@exemple.com",
            Specialty = MedicalSpecialties.GeneralPhysician
        };

        _mockDoctorRepository
            .Setup(repo => repo.GetById(command.DoctorId))
            .Returns(doctor);

        _mockDoctorRepository
            .Setup(repo => repo.IsRegisteredInState(command.RegistrationNumber, command.RegistrationState))
            .Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result>();
        result.IsSuccess.Should().BeTrue();
        _mockDoctorRegistrationRepository.Verify(repo => repo.Update(It.IsAny<DoctorRegistration>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
