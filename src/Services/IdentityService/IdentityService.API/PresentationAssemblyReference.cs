using System.Reflection;

namespace IdentityService.API;
public class PresentationAssemblyReference
{
  internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
}