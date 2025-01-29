using System.Reflection;

namespace Modules.Patients.Persistence;

public static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
