using System.Reflection;

namespace Modules.Patients.Application;

public static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
