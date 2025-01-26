using System.Reflection;

namespace Modules.Doctors.Persistence;

internal static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
