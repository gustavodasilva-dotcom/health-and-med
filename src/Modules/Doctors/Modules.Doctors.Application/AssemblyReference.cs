using System.Reflection;

namespace Modules.Doctors.Application;

internal static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
