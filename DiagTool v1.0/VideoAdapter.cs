using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagTool_v1._0
{
    class VideoAdapter : Data
    {
        protected string AdapterCompatibility { get; set; }
        protected string AdapterDACType { get; set; }
        protected string AdapterRAMSize { get; set; }
        protected string Availability { get; set; }
        protected string AdapterName { get; set; }
        protected string ErrorCode { get; set; }
        protected string UserConfig { get; set; }
        protected string DriverDate { get; set; }
        protected string DriverVersion { get; set; }
        protected string InstalledDrivers { get; set; }
        protected string IsMonochrome { get; set; }
        protected string PNPID { get; set; }
        protected string Status { get; set; }
        protected string VideoArchitecture { get; set; }
        protected string VideoMemoryType { get; set; }
        protected string VideoProcessor { get; set; }
    }
}
