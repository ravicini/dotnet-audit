using System.Collections.Generic;

namespace DotNetAudit.LockFile
{
    public class LockFileStructure
    {
        public int version { get; set; }
        public Dictionary<string, Dictionary<string, LockFileDependency>> dependencies { get; set; }
        }
}