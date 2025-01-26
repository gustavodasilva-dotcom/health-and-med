using System.Reflection;

namespace Modules.Doctors.Endpoints;

public static class AssemblyReference
{
    public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
