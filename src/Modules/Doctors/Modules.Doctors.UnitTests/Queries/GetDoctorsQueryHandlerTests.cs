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
                Name = StringHelpers.GenerateRandomString(),
                Ssn = StringHelpers.GenerateRandomString(),
                RegistrationNumber = 111111,
                Specialty = MedicalSpecialties.AllergyAndImmunology,
                Email = StringHelpers.GenerateRandomString(),
                Password = StringHelpers.GenerateRandomString()
            },
            new Doctor
            {
                Name = StringHelpers.GenerateRandomString(),
                Ssn = StringHelpers.GenerateRandomString(),
                RegistrationNumber = 111112,
                Specialty = MedicalSpecialties.PhysicalMedicineAndRehabilitation,
                Email = StringHelpers.GenerateRandomString(),
                Password = StringHelpers.GenerateRandomString(),
            },
            new Doctor
            {
                Name = StringHelpers.GenerateRandomString(),
                Ssn = StringHelpers.GenerateRandomString(),
                RegistrationNumber = 111113,
                Specialty = MedicalSpecialties.FamilyMedicine,
                Email = StringHelpers.GenerateRandomString(),
                Password = StringHelpers.GenerateRandomString()
            },
            new Doctor
            {
                Name = StringHelpers.GenerateRandomString(),
                Ssn = StringHelpers.GenerateRandomString(),
                RegistrationNumber = 111114,
                Specialty = MedicalSpecialties.Neurology,
                Email = StringHelpers.GenerateRandomString(),
                Password = StringHelpers.GenerateRandomString(),
            }
        ];

    [Fact]
    public async Task Handle_WhenDoctorsExistsBySpecialty_ReturnsDoctors()
    {
        // Arrange
        var doctors = GetMockDoctors();

        _mockDoctorRepository
            .Setup(repo => repo.Get(_ => true))
            .Returns(doctors);

        var query = new GetDoctorsQuery(Name: null, MedicalSpecialties.Neurology);

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

    [Fact]
    public async Task Handle_WhenDoctorsDontExistBySpecialty_ReturnsEmpty()
    {
        // Arrange
        var doctors = GetMockDoctors();

        _mockDoctorRepository
            .Setup(repo => repo.Get(_ => true))
            .Returns(doctors);

        var query = new GetDoctorsQuery(Name: null, MedicalSpecialties.NuclearMedicine);

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
