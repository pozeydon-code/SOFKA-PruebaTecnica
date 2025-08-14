using System.Reflection;

namespace AccountService.API;
public class PresentationAssemblyReference
{
  internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
}