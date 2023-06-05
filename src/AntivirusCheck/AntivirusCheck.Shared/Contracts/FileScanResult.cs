using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntivirusCheck.Shared.Contracts
{
    public class FileScanResult
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public bool IsClean { get; set; }
        public string Message { get; set; }
    }
}
