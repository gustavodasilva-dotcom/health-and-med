using System.Reflection;

namespace Modules.Patients.Domain;

public static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;    
}
