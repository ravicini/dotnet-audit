using System.Collections.Generic;

namespace DotNetAudit
{
    public class LockFile
    {
        public int version { get; set; }
        public Dictionary<string, Dictionary<string, Dependency>> dependencies { get; set; }
    }

    public class Dependency
    {
        public string type { get; set; }
        public string requested { get; set; }
        public string resolved { get; set; }
        public string contentHash { get; set; }
        public Dictionary<string, string> dependencies { get; set; }
    }
}