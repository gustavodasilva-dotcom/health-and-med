using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Helpers;
using Common.Shared.Repositories;
using FluentAssertions;
using Modules.Patients.Application.Appointments.Commands.CreateAppointment;
using Modules.Patients.Application.Constants;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using Moq;

namespace Modules.Patients.UnitTests.Commands;

public class CreateAppointmentCommandHandlerTests
{
    private readonly Mock<IPatientRepository> _mockPatientRepository;
    private readonly Mock<IRepository<PatientAppointment>> _mockPatientAppointmentRepository;
    private readonly Mock<IPatientsUnitOfWork> _mockUnitOfWork;
    private readonly CreateAppointmentCommandHandler _handler;

    public CreateAppointmentCommandHandlerTests()
    {
        _mockPatientRepository = new Mock<IPatientRepository>();
        _mockPatientAppointmentRepository = new Mock<IRepository<PatientAppointment>>();
        _mockUnitOfWork = new Mock<IPatientsUnitOfWork>();

        _handler = new CreateAppointmentCommandHandler(
            _mockPatientRepository.Object,
            _mockPatientAppointmentRepository.Object,
            _mockUnitOfWork.Object
        );
    }

    [Fact]
    public async Task Handle_WhenPatientIsNotFound_ReturnsError()
    {
        // Arrange
        var request = new CreateAppointmentCommand(
            PatientId: Guid.NewGuid(),
            DoctorShiftId: Guid.NewGuid()
        );

        _mockPatientRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns((Patient?)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsType<Error>(result.Error);
        var error = result.Error;
        Assert.Equal(SharedErrorConstants.NotFoundTitle, error?.Title);
        Assert.Equal(ErrorConstants.PatientNotFound, error?.Message);
    }

    [Fact]
    public async Task Handle_WhenDoctorAndPatientAreFree_CreatesAppointmentAndSavesIt()
    {
        // Arrange
        var patient = new Patient
        {
            Name = StringHelpers.GenerateRandomString(),
            Ssn = StringHelpers.GenerateRandomString(11),
            Email = StringHelpers.GenerateRandomString(),
            Password = StringHelpers.GenerateRandomString(),
        };

        var request = new CreateAppointmentCommand(
            PatientId: patient.Id,
            DoctorShiftId: Guid.NewGuid()
        );

        _mockPatientRepository
            .Setup(repo => repo.GetById(request.PatientId))
            .Returns(patient);

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
