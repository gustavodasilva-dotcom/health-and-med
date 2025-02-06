using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Security;
using Microsoft.AspNetCore.Http;
using Modules.Patients.Application.Accesses.Commands.LoginPatient;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using Moq;
using FluentAssertions;

namespace Modules.Patients.UnitTests.Commands;

public class LoginPatientCommandHandlerTests
{
    private readonly Mock<IPasswordHasher> _mockPasswordHasher;
    private readonly Mock<ITokenProvider> _mockTokenProvider;
    private readonly Mock<IPatientRepository> _mockPatientRepository;
    private readonly LoginPatientCommandHandler _handler;

    public LoginPatientCommandHandlerTests()
    {
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockTokenProvider = new Mock<ITokenProvider>();
        _mockPatientRepository = new Mock<IPatientRepository>();

        _handler = new LoginPatientCommandHandler(
            _mockPasswordHasher.Object,
            _mockTokenProvider.Object,
            _mockPatientRepository.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPatientNotFound()
    {
        var command = new LoginPatientCommand("patient@example.com", "password");

        _mockPatientRepository
            .Setup(repo => repo.GetWithEmail(command.Email))
            .Returns((Patient?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Error.Should().BeOfType<Error>();
        var error = result.Error;
        error.Message.Should().Be("No patient was found with the given email.");
        error.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPasswordIsIncorrect()
    {
        var command = new LoginPatientCommand("patient@example.com", "wrongpassword");
        var patient = new Patient
        {
            Cpf = "4545454545",
            Name = "John Doe",
            Email = "patient@example.com",
            Password = "hashedPassword"
        };

        _mockPatientRepository
            .Setup(repo => repo.GetWithEmail(command.Email))
            .Returns(patient);

        _mockPasswordHasher
            .Setup(ph => ph.Verify(command.Password.Trim(), patient.Password))
            .Returns(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Error.Should().BeOfType<Error>();
        var error = result.Error;
        error.Message.Should().Be("The given password is incorrect.");
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
    {
        var command = new LoginPatientCommand("patient@example.com", "correctpassword");
        var patient = new Patient
        {
            Cpf = "4545454545",
            Name = "John Doe",
            Email = "patient@example.com",
            Password = "hashedPassword"
        };
        var expectedToken = "mock-token";

        _mockPatientRepository
            .Setup(repo => repo.GetWithEmail(command.Email))
            .Returns(patient);

        _mockPasswordHasher
            .Setup(ph => ph.Verify(command.Password.Trim(), patient.Password))
            .Returns(true);

        _mockTokenProvider
            .Setup(tp => tp.Create(patient, UserRoles.Patient))
            .Returns(expectedToken);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Value.Should().Be(expectedToken);
    }
}
