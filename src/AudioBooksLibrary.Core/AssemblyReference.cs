using System.Reflection;

namespace AudioBooksLibrary.Core;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
