using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetAudit.LockFile
{
    public static class LockFileReader
    {
        public static async Task<List<Dependency>> ReadDependencies(string file)
        {
            await using FileStream stream = File.OpenRead(file);
            var lockFile = await JsonSerializer.DeserializeAsync<LockFileStructure>(stream);
            return lockFile.dependencies.First().Value.Select(_ =>
            {
                LockFileDependency dependency = _.Value;
                return new Dependency(
                    _.Key,
                    Enum.Parse<DependencyType>(dependency.type),
                    dependency.resolved,
                    dependency.dependencies);
            }).ToList();
        }
    }
}