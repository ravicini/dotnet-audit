using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetAudit.LockFile;
using DotNetAudit.OssIndex;

namespace DotNetAudit
{
    class Program
    {
        private const string PackagesLock = "packages.lock.json";

        public static async Task Main(string[] args)
        {
            string testLockFile = $"/home/marco/workspace/dotnet-audit/samples/{PackagesLock}";

            var dependencies = await LockFileReader.ReadDependencies(testLockFile);
            foreach (var dependency in dependencies)
            {
                Console.WriteLine($"{dependency.Name} ({dependency.Type.ToString()}) [{dependency.ResolvedVersion}]");
            }
            
            var reports = await OssIndexReader.GetVulnerabilities(dependencies);
            foreach (var report in reports)
            {
                if (report.Vulnerabilities.Any())
                {
                    Console.WriteLine($"{report.Name}");
                    Console.WriteLine($"{report.Description}");
                }
            }
        }
    }
}
