using Common.Shared;
using Common.Shared.Constants;
using FluentAssertions;
using Modules.Patients.Application.Appointments.Commands.RegisterAppointment;
using Modules.Patients.Application.Constants;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using Moq;

namespace Modules.Patients.UnitTests.Commands
{
    public class CreateAppointmentCommandHandlerTests
    {
        private readonly Mock<IPatientRepository> _mockPatientRepository;
        private readonly Mock<IPatientAppointmentRepository> _mockPatientAppointmentRepository;
        private readonly Mock<IPatientsUnitOfWork> _mockUnitOfWork;
        private readonly CreateAppointmentCommandHandler _handler;

        public CreateAppointmentCommandHandlerTests()
        {
            _mockPatientRepository = new Mock<IPatientRepository>();
            _mockPatientAppointmentRepository = new Mock<IPatientAppointmentRepository>();
            _mockUnitOfWork = new Mock<IPatientsUnitOfWork>();

            _handler = new CreateAppointmentCommandHandler(
                _mockPatientRepository.Object,
                _mockPatientAppointmentRepository.Object,
                _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_WhenPatientIsNotFree_ReturnsError()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "patient",
                Ssn = "123456",
                Email = "patient@example.com",
                Password = "hashedPassword",
            };

            var request = new CreateAppointmentCommand(
                PatientId: patient.Id,
                DoctorId: Guid.NewGuid(),
                StartAt: DateTime.UtcNow);

            _mockPatientRepository
                .Setup(repo => repo.GetById(request.PatientId))
                .Returns(patient);

            _mockPatientAppointmentRepository
                .Setup(repo => repo.IsPatientAvailable(request.PatientId, request.StartAt, AppointmentContants.APPOINTMENT_DURATION_IN_MINUTES))
                .Returns(false);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Error>(result.Error);
            var error = result.Error;
            Assert.Equal(SharedErrorConstants.InvalidOperationTitle, error?.Title);
            Assert.Equal(ErrorConstants.PatientUnavailableMessage, error?.Message);
        }

        [Fact]
        public async Task Handle_WhenDoctorIsNotFree_ReturnsError()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "patient",
                Ssn = "123456",
                Email = "patient@example.com",
                Password = "hashedPassword",
            };

            var request = new CreateAppointmentCommand(
                PatientId: patient.Id,
                DoctorId: Guid.NewGuid(),
                StartAt: DateTime.UtcNow);

            _mockPatientRepository
                .Setup(repo => repo.GetById(request.PatientId))
                .Returns(patient);

            _mockPatientAppointmentRepository
                .Setup(repo => repo.IsPatientAvailable(request.PatientId, request.StartAt, AppointmentContants.APPOINTMENT_DURATION_IN_MINUTES))
                .Returns(true);

            _mockPatientAppointmentRepository
                .Setup(repo => repo.IsDoctorAvailable(request.DoctorId, request.StartAt, AppointmentContants.APPOINTMENT_DURATION_IN_MINUTES))
                .Returns(false);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Error>(result.Error);
            var error = result.Error;
            Assert.Equal(SharedErrorConstants.InvalidOperationTitle, error?.Title);
            Assert.Equal(ErrorConstants.DoctorUnavailableMessage, error?.Message);
        }

        [Fact]
        public async Task Handle_WhenDoctorAndPatientAreFree_CreatesAppointmentAndSavesIt()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "patient",
                Ssn = "123456",
                Email = "patient@example.com",
                Password = "hashedPassword",
            };

            var request = new CreateAppointmentCommand(
                PatientId: patient.Id,
                DoctorId: Guid.NewGuid(),
                StartAt: DateTime.UtcNow);

            _mockPatientRepository
                .Setup(repo => repo.GetById(request.PatientId))
                .Returns(patient);

            _mockPatientAppointmentRepository
                .Setup(repo => repo.IsPatientAvailable(request.PatientId, request.StartAt, AppointmentContants.APPOINTMENT_DURATION_IN_MINUTES))
                .Returns(true);

            _mockPatientAppointmentRepository
                .Setup(repo => repo.IsDoctorAvailable(request.DoctorId, request.StartAt, AppointmentContants.APPOINTMENT_DURATION_IN_MINUTES))
                .Returns(true);

            _mockPatientAppointmentRepository
                .Setup(repo => repo.Add(It.IsAny<PatientAppointment>()))
                .Verifiable();

            _mockUnitOfWork
                .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _mockPatientAppointmentRepository.Verify(repo => repo.Add(It.IsAny<PatientAppointment>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
