using System.Reflection;

namespace Modules.Patients.Endpoints;

public static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
