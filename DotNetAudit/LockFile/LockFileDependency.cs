using System.Collections.Generic;

namespace DotNetAudit.LockFile
{
    public class LockFileDependency
    {
        public string type { get; set; }
        public string requested { get; set; }
        public string resolved { get; set; }
        public string contentHash { get; set; }
        public Dictionary<string, string> dependencies { get; set; }
    }
}