using System.Collections.Generic;

namespace DotNetAudit
{
    public record Dependency(
        string Name,
        DependencyType Type,
        string ResolvedVersion,
        Dictionary<string, string> Dependencies);

    public enum DependencyType
    {
        Direct,
        Transitive
    }
}