using System.Reflection;

namespace Modules.Doctors.Application;

public static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
