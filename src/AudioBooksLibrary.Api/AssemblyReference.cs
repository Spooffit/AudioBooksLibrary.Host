using System.Reflection;

namespace AudioBooksLibrary.Api;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
