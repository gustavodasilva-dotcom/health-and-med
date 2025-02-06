using Modules.Doctors.Application.Appointments.Queries.GetAppointmentById;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace Modules.Doctors.UnitTests.Queries;

public class GetAppointmentByIdQueryHandlerTests
{
    private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
    private readonly GetAppointmentByIdQueryHandler _handler;

    public GetAppointmentByIdQueryHandlerTests()
    {
        _mockAppointmentRepository = new Mock<IAppointmentRepository>();
        _handler = new GetAppointmentByIdQueryHandler(_mockAppointmentRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenAppointmentExists_ReturnsAppointment()
    {
        var appointmentId = Guid.NewGuid();
        var expectedAppointment = new Appointment
        {
            IdDoctor = Guid.NewGuid(),
            IdPatient = Guid.NewGuid(),
            DateFrom = DateTime.Now,
            DateUntil = DateTime.Now.AddHours(1)
        };

        _mockAppointmentRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(expectedAppointment);

        var query = new GetAppointmentByIdQuery(appointmentId);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expectedAppointment.Id, result.Id);
        Assert.Equal(expectedAppointment.DateFrom, result.DateFrom);
        Assert.Equal(expectedAppointment.DateUntil, result.DateUntil);
    }

    [Fact]
    public async Task Handle_WhenAppointmentDoesNotExist_ReturnsNull()
    {
        var appointmentId = Guid.NewGuid();

        _mockAppointmentRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns((Appointment?)null);

        var query = new GetAppointmentByIdQuery(appointmentId);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Null(result);
    }
}
