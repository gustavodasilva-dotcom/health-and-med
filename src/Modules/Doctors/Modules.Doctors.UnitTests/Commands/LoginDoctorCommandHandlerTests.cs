using Common.Shared;
using Common.Shared.Security;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Modules.Doctors.Application.Accesses.Commands.LoginDoctor;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;
using Moq;

namespace Modules.Doctors.UnitTests.Commands;

public class LoginDoctorCommandHandlerTests
{
    private readonly Mock<IPasswordHasher> _mockPasswordHasher;
    private readonly Mock<ITokenProvider> _mockTokenProvider;
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;
    private readonly LoginDoctorCommandHandler _handler;

    public LoginDoctorCommandHandlerTests()
    {
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockTokenProvider = new Mock<ITokenProvider>();
        _mockDoctorRepository = new Mock<IDoctorRepository>();

        _handler = new LoginDoctorCommandHandler(
            _mockPasswordHasher.Object,
            _mockTokenProvider.Object,
            _mockDoctorRepository.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDoctorNotFound()
    {
        // Arrange
        var command = new LoginDoctorCommand("doctor@exemple.com", "password");

        _mockDoctorRepository
            .Setup(repo => repo.GetWithEmail(It.IsAny<string>()))
            .Returns((Doctor?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Error.Should().BeOfType<Error>();
        var error = result.Error;
        error?.Message.Should().Be("No doctor was found with the given email.");
        error?.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPasswordIsIncorrect()
    {
        // Arrange
        var doctor = new Doctor
        {
            Name = "doctor",
            Ssn = "123456",
            Email = "doctor@example.com",
            Password = "hashedPassword",
            Specialty = MedicalSpecialties.GeneralPhysician
        };

        var command = new LoginDoctorCommand("doctor@exemple.com", "password");

        _mockDoctorRepository
            .Setup(repo => repo.GetWithEmail(It.IsAny<string>()))
            .Returns(doctor);

        _mockPasswordHasher
            .Setup(ph => ph.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Error.Should().BeOfType<Error>();
        var error = result.Error;
        error?.Message.Should().Be("The given password is incorrect.");
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
    {
        // Arrange
        var doctor = new Doctor
        {
            Name = "doctor",
            Ssn = "123456",
            Email = "doctor@example.com",
            Password = "hashedPassword",
            Specialty = MedicalSpecialties.GeneralPhysician
        };

        var command = new LoginDoctorCommand(Email: "doctor@exemple.com", Password: "password");

        _mockDoctorRepository
            .Setup(repo => repo.GetWithEmail(It.IsAny<string>()))
            .Returns(doctor);

        _mockPasswordHasher
            .Setup(ph => ph.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        _mockTokenProvider
            .Setup(t => t.Create(It.IsAny<Doctor>(), It.IsAny<string>()))
            .Returns("mock-token");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        result.Value.Should().Be("mock-token");
    }
}
