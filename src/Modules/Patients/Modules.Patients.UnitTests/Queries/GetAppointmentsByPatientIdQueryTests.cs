using Common.Shared.Repositories;
using Modules.Patients.Application.Appointments.Queries.GetAppointmentsByPatientId;
using Modules.Patients.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace Modules.Patients.UnitTests.Queries;

public class GetAppointmentsByPatientIdQueryTests
{
    private readonly Mock<IRepository<PatientAppointment>> _mockPatientAppointmentRepository;
    private readonly GetAppointmentsByPatientIdQueryHandler _handler;

    public GetAppointmentsByPatientIdQueryTests()
    {
        _mockPatientAppointmentRepository = new Mock<IRepository<PatientAppointment>>();
        _handler = new GetAppointmentsByPatientIdQueryHandler(_mockPatientAppointmentRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenAppointmentsExist_ReturnsAppointments()
    {
        // Arrange
        IEnumerable<PatientAppointment> shifts =
        [
            new()
            {
                DoctorShiftId = Guid.NewGuid()
            },
            new()
            {
                DoctorShiftId = Guid.NewGuid()
            }
        ];

        _mockPatientAppointmentRepository
            .Setup(repo => repo.Get(It.IsAny<Expression<Func<PatientAppointment, bool>>>()))
            .Returns(shifts);

        // Act
        var result = await _handler.Handle(
            new GetAppointmentsByPatientIdQuery(PatientId: Guid.NewGuid()),
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task Handle_WhenAppointmentsDontExist_ReturnsEmptyList()
    {
        // Arrange
        IEnumerable<PatientAppointment> shifts = [];

        _mockPatientAppointmentRepository
            .Setup(repo => repo.Get(It.IsAny<Expression<Func<PatientAppointment, bool>>>()))
            .Returns(shifts);

        // Act
        var result = await _handler.Handle(
            new GetAppointmentsByPatientIdQuery(PatientId: Guid.NewGuid()),
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
