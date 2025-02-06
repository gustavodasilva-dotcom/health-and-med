using System.Reflection;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly[] DomainAssemblies =
    [
        //Modules.Doctors.Domain.AssemblyReference.Assembly,
        //Modules.Patients.Domain.AssemblyReference.Assembly
    ];

    protected static readonly Assembly[] PersistenceAssemblies =
    [
       //Modules.Doctors.Persistence.AssemblyReference.Assembly,
       //Modules.Patients.Persistence.AssemblyReference.Assembly
    ];
}
