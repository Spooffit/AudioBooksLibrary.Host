using System.Reflection;

namespace AudioBooksLibrary.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
