using Common.Shared.Helpers;
using FluentAssertions;
using Modules.Doctors.Application.Doctors.Queries.GetDoctors;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;
using Moq;

namespace Modules.Doctors.UnitTests.Queries;

public class GetDoctorsQueryHandlerTests
{
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;
    private readonly GetDoctorsQueryHandler _handler;
    
    private readonly int _doctorsInStorage;

    public GetDoctorsQueryHandlerTests()
    {
        _mockDoctorRepository = new Mock<IDoctorRepository>();
        _handler = new GetDoctorsQueryHandler(_mockDoctorRepository.Object);

        _doctorsInStorage = GetMockDoctors().Count();
    }

    private static IEnumerable<Doctor> GetMockDoctors()
        => [
            new Doctor
            {
                Email = StringHelpers.GenerateRandomString(),
                Name = StringHelpers.GenerateRandomString(),
                Ssn = StringHelpers.GenerateRandomString(),
                Password = StringHelpers.GenerateRandomString(),
                Specialty = MedicalSpecialties.AllergyAndImmunology
            },
            new Doctor
            {
                Email = StringHelpers.GenerateRandomString(),
                Name = StringHelpers.GenerateRandomString(),
                Ssn = StringHelpers.GenerateRandomString(),
                Password = StringHelpers.GenerateRandomString(),
                Specialty = MedicalSpecialties.PhysicalMedicineAndRehabilitation
            },
            new Doctor
            {
                Email = StringHelpers.GenerateRandomString(),
                Name = StringHelpers.GenerateRandomString(),
                Ssn = StringHelpers.GenerateRandomString(),
                Password = StringHelpers.GenerateRandomString(),
                Specialty = MedicalSpecialties.FamilyMedicine
            },
            new Doctor
            {
                Email = StringHelpers.GenerateRandomString(),
                Name = StringHelpers.GenerateRandomString(),
                Ssn = StringHelpers.GenerateRandomString(),
                Password = StringHelpers.GenerateRandomString(),
                Specialty = MedicalSpecialties.Neurology
            }
        ];

    public async Task Handle_WhenDoctorsExistsBySpecialty_ReturnsDoctors()
    {
        // Arrange
        var doctors = GetMockDoctors();

        _mockDoctorRepository
            .Setup(repo => repo.Get(_ => true))
            .Returns(doctors);

        var query = new GetDoctorsQuery(
            Name: null,
            Specialty: MedicalSpecialties.Neurology,
            State: null
        );

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        var resultCount = result.Count();
        resultCount.Should().NotBe(_doctorsInStorage);

        var neurologistCount = result
            .Select(doctor => doctor.Specialty == MedicalSpecialties.Neurology)
            .Count();
        neurologistCount.Should().BeGreaterThan(0);
    }

    public async Task Handle_WhenDoctorsDontExistBySpecialty_ReturnsEmpty()
    {
        // Arrange
        var doctors = GetMockDoctors();

        _mockDoctorRepository
            .Setup(repo => repo.Get(_ => true))
            .Returns(doctors);

        var query = new GetDoctorsQuery(
            Name: null,
            Specialty: MedicalSpecialties.NuclearMedicine,
            State: null
        );

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        var resultCount = result.Count();
        resultCount.Should().NotBe(_doctorsInStorage);

        var neurologistCount = result
            .Select(doctor => doctor.Specialty == MedicalSpecialties.Neurology)
            .Count();
        neurologistCount.Should().Be(0);
    }
}
