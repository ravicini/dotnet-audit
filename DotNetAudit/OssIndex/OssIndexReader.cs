using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OssIndexClient;

namespace DotNetAudit.OssIndex
{
    public static class OssIndexReader
    {
        public static async Task<IReadOnlyList<ComponentReport>> GetVulnerabilities(List<Dependency> dependencies)
        { 
            var ossIndexClient = new OssIndexClient.OssIndex();
            // var packages = dependencies.Take(100).Select(_ => new Package(EcoSystem.nuget, _.Name, _.ResolvedVersion));
            var batches = PackageBatches(dependencies).ToList();
            var tasks = batches.Select(_ => Task.Run(
                async () => await ossIndexClient.GetReports(_)));
            var continuation = Task.WhenAll(tasks);
            try
            {
                continuation.Wait();
            }
            catch (AggregateException)
            {
            }
            
            ossIndexClient.Dispose();
            if (continuation.Status == TaskStatus.RanToCompletion)
            {
                return continuation.Result.SelectMany(_ => _).ToList();
            }

            return new List<ComponentReport>();
        }

        private static IEnumerable<IEnumerable<Package>> PackageBatches(IEnumerable<Dependency> dependencies)
        {
            // using var iter = dependencies.GetEnumerator();
            // while (iter.MoveNext())
            // {
            //     var batch = new Package[100];
            //     batch[0] = ToPackage(iter.Current);
            //     for (var i = 1; i < 100 && iter.MoveNext(); i++)
            //     {
            //         batch[i] = ToPackage(iter.Current);
            //     }
            //
            //     yield return batch;
            // }

            var rest = dependencies;
            while (rest.Any())
            {
                yield return rest.Take(100).Select(_ => new Package(EcoSystem.nuget, _.Name, _.ResolvedVersion));
                rest = rest.Skip(100);
            }

            // static Package ToPackage(Dependency dep)
            // {
            //     return new Package(EcoSystem.nuget, dep.Name, dep.ResolvedVersion);
            // }
        }
    }
}