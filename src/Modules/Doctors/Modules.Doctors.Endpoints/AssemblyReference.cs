using System.Reflection;

namespace Modules.Doctors.Endpoints;

internal static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
