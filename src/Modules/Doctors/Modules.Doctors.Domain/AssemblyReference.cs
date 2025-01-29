using System.Reflection;

namespace Modules.Doctors.Domain;

public static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;    
}
