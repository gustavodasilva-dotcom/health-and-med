using Common.Shared;
using Common.Shared.Security;
using FluentAssertions;
using Modules.Doctors.Application.Accesses.Commands.RegisterDoctor;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;
using Moq;

namespace Modules.Doctors.UnitTests.Commands;

public class RegisterDoctorCommandHandlerTests
{
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;
    private readonly Mock<IPasswordHasher> _mockPasswordHasher;
    private readonly Mock<IDoctorsUnitOfWork> _mockUnitOfWork;
    private readonly RegisterDoctorCommandHandler _handler;

    public RegisterDoctorCommandHandlerTests()
    {
        _mockDoctorRepository = new Mock<IDoctorRepository>();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockUnitOfWork = new Mock<IDoctorsUnitOfWork>();

        _handler = new RegisterDoctorCommandHandler(
            _mockPasswordHasher.Object,
            _mockDoctorRepository.Object,
            _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailAlreadyInUse()
    {
        // Arrange
        var command = new RegisterDoctorCommand(
            Name: "Doctor",
            Ssn: "123456789",
            RegistrationState: UFs.SaoPaulo,
            RegistrationNumber: 1234,
            Specialty: MedicalSpecialties.GeneralPhysician,
            Email: "doctor@example.com",
            Password: "password"
        );

        _mockDoctorRepository
            .Setup(repo => repo.ExistsWithEmail(command.Email))
            .Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Error.Should().BeOfType<Error>();
        var error = result.Error;
        error.Message.Should().Be("The informed email is already in use.");
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenSsnAlreadyInUse()
    {
        // Arrange
        var command = new RegisterDoctorCommand(
            Name: "Doctor",
            Ssn: "123456789",
            RegistrationState: UFs.SaoPaulo,
            RegistrationNumber: 1234,
            Specialty: MedicalSpecialties.GeneralPhysician,
            Email: "doctor@example.com",
            Password: "password"
        );

        _mockDoctorRepository
            .Setup(repo => repo.ExistsWithEmail(command.Email))
            .Returns(false);

        _mockDoctorRepository
            .Setup(repo => repo.ExistsWithSsn(command.Ssn))
            .Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Handle
        result.Error.Should().BeOfType<Error>();
        var error = result.Error;
        error.Message.Should().Be("The informed Social Security Number is already in use.");
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenRegistrationNumberIsAlreadyRegistered()
    {
        // Arrange
        var command = new RegisterDoctorCommand(
            Name: "Doctor",
            Ssn: "123456789",
            RegistrationState: UFs.SaoPaulo,
            RegistrationNumber: 1234,
            Specialty: MedicalSpecialties.GeneralPhysician,
            Email: "doctor@example.com",
            Password: "password"
        );

        _mockDoctorRepository
            .Setup(repo => repo.ExistsWithEmail(command.Email))
            .Returns(false);

        _mockDoctorRepository
            .Setup(repo => repo.ExistsWithSsn(command.Ssn))
            .Returns(false);

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
    public async Task Handle_ShouldReturnSuccess_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var command = new RegisterDoctorCommand(
            Name: "Doctor",
            Ssn: "123456789",
            RegistrationState: UFs.SaoPaulo,
            RegistrationNumber: 1234,
            Specialty: MedicalSpecialties.GeneralPhysician,
            Email: "doctor@example.com",
            Password: "password"
        );

        _mockDoctorRepository
            .Setup(repo => repo.ExistsWithEmail(command.Email))
            .Returns(false);

        _mockDoctorRepository
            .Setup(repo => repo.ExistsWithSsn(command.Ssn))
            .Returns(false);

        _mockDoctorRepository
            .Setup(repo => repo.IsRegisteredInState(command.RegistrationNumber, command.RegistrationState))
            .Returns(false);

        _mockPasswordHasher
            .Setup(ph => ph.Hash(command.Password))
            .Returns("hashedPassword");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result>();
        result.IsSuccess.Should().BeTrue();
        _mockDoctorRepository.Verify(repo => repo.Add(It.IsAny<Doctor>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
