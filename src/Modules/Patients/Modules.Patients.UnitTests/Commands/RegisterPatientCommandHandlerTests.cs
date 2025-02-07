using Common.Shared.Repositories;
using Common.Shared;
using Modules.Patients.Application.Accesses.Commands.RegisterPatient;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using Moq;
using FluentAssertions;
using Common.Shared.Security;

namespace Modules.Patients.UnitTests.Commands
{
    public class RegisterPatientCommandHandlerTests
    {
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IPatientRepository> _mockPatientRepository;
        private readonly Mock<IPatientsUnitOfWork> _mockUnitOfWork;
        private readonly RegisterPatientCommandHandler _handler;

        public RegisterPatientCommandHandlerTests()
        {
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockPatientRepository = new Mock<IPatientRepository>();
            _mockUnitOfWork = new Mock<IPatientsUnitOfWork>();
            _handler = new RegisterPatientCommandHandler(
                _mockPasswordHasher.Object,
                _mockPatientRepository.Object,
                _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenEmailAlreadyExists()
        {

            var command = new RegisterPatientCommand("name", "45454545", "patient@exemple.com", "password");


            _mockPatientRepository.Setup(repo => repo.ExistsWithEmail(command.Email)).Returns(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Error.Should().BeOfType<Error>();
            var error = result.Error;
            error?.Message.Should().Be("The email is already in use.");
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenCpfAlreadyExists()
        {
            var command = new RegisterPatientCommand("name", "45454545", "patient@exemple.com", "password");

            _mockPatientRepository.Setup(repo => repo.ExistsWithEmail(command.Email)).Returns(false);
            _mockPatientRepository.Setup(repo => repo.ExistsWithCpf(command.Ssn)).Returns(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Error.Should().BeOfType<Error>();
            var error = result.Error;
            error?.Message.Should().Be("The CPF is already in use.");
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenPatientIsRegistered()
        {

            var command = new RegisterPatientCommand("name", "45454545", "patient@exemple.com", "password");

            _mockPatientRepository.Setup(repo => repo.ExistsWithEmail(command.Email)).Returns(false);
            _mockPatientRepository.Setup(repo => repo.ExistsWithCpf(command.Ssn)).Returns(false);
            _mockPasswordHasher.Setup(ph => ph.Hash(command.Password.Trim())).Returns("hashedPassword");

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeOfType<Result>();
            _mockPatientRepository.Verify(repo => repo.Add(It.IsAny<Patient>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
