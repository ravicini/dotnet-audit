using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DotNetAudit
{
    class Program
    {
        private const string PackagesLock = "packages.lock.json";

        public static void Main(string[] args)
        {
            string testLockFile = "/home/marco/workspace/hb-therm/e-cockpit/api/ECockpitApi/packages.lock.json";
            
            using FileStream stream = File.OpenRead(testLockFile);
            var lockFile = JsonSerializer.DeserializeAsync<LockFile>(stream).GetAwaiter().GetResult();
            foreach (var dep in lockFile.dependencies.First().Value)
            {
                Console.WriteLine($"{dep.Key} {dep.Value.resolved}");
            }
        }
    }
}
