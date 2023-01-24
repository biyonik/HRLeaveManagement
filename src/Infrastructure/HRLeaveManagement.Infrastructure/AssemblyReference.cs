using System.Reflection;

namespace HRLeaveManagement.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}