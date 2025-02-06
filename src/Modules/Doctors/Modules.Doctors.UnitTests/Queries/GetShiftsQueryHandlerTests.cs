using Modules.Doctors.Application.Shifts.Queries.GetShifts;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace Modules.Doctors.UnitTests.Queries;

public class GetShiftsQueryHandlerTests
{
    private readonly Mock<IDoctorShiftRepository> _mockDoctorShiftRepository;
    private readonly GetShiftsQueryHandler _handler;

    public GetShiftsQueryHandlerTests()
    {
        _mockDoctorShiftRepository = new Mock<IDoctorShiftRepository>();
        _handler = new GetShiftsQueryHandler(_mockDoctorShiftRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenShiftsExistInRange_ReturnsShifts()
    {
        var fromDate = DateTime.Now.AddDays(-1);
        var toDate = DateTime.Now.AddDays(1);

        IEnumerable<DoctorShift> shifts =
        [
            new()
            {
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddHours(1)
            },
            new()
            {
                StartAt = DateTime.Now.AddHours(2),
                EndAt = DateTime.Now.AddHours(3)
            }
        ];

        _mockDoctorShiftRepository
            .Setup(repo => repo.Get(It.IsAny<Expression<Func<DoctorShift, bool>>>()))
            .Returns((IQueryable<DoctorShift>)shifts);

        var result = await _handler.Handle(
            new GetShiftsQuery(fromDate, toDate),
            CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task Handle_WhenNoShiftsExistInRange_ReturnsEmptyList()
    {
        var fromDate = DateTime.Now.AddDays(10);
        var toDate = DateTime.Now.AddDays(20);

        IEnumerable<DoctorShift> shifts = [];

        _mockDoctorShiftRepository
            .Setup(repo => repo.Get(It.IsAny<Expression<Func<DoctorShift, bool>>>()))
            .Returns((IQueryable<DoctorShift>)shifts);

        var result = await _handler.Handle(
            new GetShiftsQuery(fromDate, toDate),
            CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
