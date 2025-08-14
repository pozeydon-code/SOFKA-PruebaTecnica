using System.Reflection;

namespace AccountService.Application;
public class ApplicationAssemblyReference
{
  internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}