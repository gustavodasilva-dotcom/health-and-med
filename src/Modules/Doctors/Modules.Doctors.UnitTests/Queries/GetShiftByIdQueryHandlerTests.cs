using Modules.Doctors.Application.Shifts.Queries.GetShiftById;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;

namespace Modules.Doctors.UnitTests.Queries;

public class GetShiftByIdQueryHandlerTests
{
    private readonly Mock<IDoctorShiftRepository> _mockDoctorShiftRepository;
    private readonly GetShiftByIdQueryHandler _handler;

    public GetShiftByIdQueryHandlerTests()
    {
        _mockDoctorShiftRepository = new Mock<IDoctorShiftRepository>();
        _handler = new GetShiftByIdQueryHandler(_mockDoctorShiftRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenShiftExists_ReturnsShift()
    {
        var expectedShift = new DoctorShift
        {
            StartAt = DateTime.Now,
            EndAt = DateTime.Now.AddHours(1)
        };

        _mockDoctorShiftRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(expectedShift);

        var query = new GetShiftByIdQuery(expectedShift.Id);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expectedShift.Id, result.Id);
        Assert.Equal(expectedShift.StartAt, result.StartAt);
        Assert.Equal(expectedShift.EndAt, result.EndAt);
    }

    [Fact]
    public async Task Handle_WhenShiftDoesNotExist_ReturnsNull()
    {
        var shiftId = Guid.NewGuid();

        _mockDoctorShiftRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns((DoctorShift?)null);

        var query = new GetShiftByIdQuery(shiftId);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Null(result);
    }
}
