using System.Reflection;

namespace Catalog.API.Utils;


public class CarterDependencyContextAssemblyCatalogCustom : DependencyContextAssemblyCatalog
{
    public override IReadOnlyCollection<Assembly> GetAssemblies()
    {
        return new List<Assembly> { typeof(Program).Assembly };
    }
}