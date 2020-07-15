using System.Collections.Generic;

namespace DiagTool_v1._0
{
    class CPU : Data
    {
        private readonly Dictionary<string, string> SupportedStatus = new Dictionary<string, string>()
        {
            ["True"] = "Supported",
            ["False"] = "Not supported",
            [""] = "Undefined",
        };
        private readonly Dictionary<string, string> EnabledStaus = new Dictionary<string, string>()
        {
            ["True"] = "Enabled",
            ["False"] = "Disabled",
            [""] = "Undefined",
        };
        private readonly Dictionary<string, string> ArchitectureDictionary = new Dictionary<string, string>()
        {
            ["0"] = "x86",
            ["1"] = "MIPS",
            ["2"] = "Alpha",
            ["3"] = "PowerPC",
            ["4"] = "ARM",
            ["5"] = "la64",
            ["9"] = "x64",
            [""] = "Undefined",
        };
        private readonly Dictionary<string, string> AvailabilityDictionary = new Dictionary<string, string>()
        {
            ["1"] = "Other",
            ["2"] = "Unknown",
            ["3"] = "Running/Full Power",
            ["4"] = "Warning",
            ["5"] = "In Test",
            ["6"] = "Not Applicable",
            ["7"] = "Power Off",
            ["8"] = "Off Line",
            ["9"] = "Off Duty",
            ["10"] = "Degraded",
            ["11"] = "Not Installed",
            ["12"] = "Install Error",
            ["13"] = "Power Save - Unknown",
            ["14"] = "Power Save - Low Power Mode",
            ["15"] = "Power Save - Standby",
            ["16"] = "Power Cycle",
            ["17"] = "Power Save - Warning",
            ["18"] = "Paused",
            ["19"] = "Not ready",
            ["20"] = "Not configured",
            ["21"] = "Quiesced",
        };
        private readonly Dictionary<string, string> CPUTypeDictionary = new Dictionary<string, string>()
        {
            ["1"] = "Other",
            ["2"] = "Unknown",
            ["3"] = "Central Processor",
            ["4"] = "Math Processor",
            ["5"] = "DSP Processor",
            ["6"] = "Video Processor"
        };

        private int cpuPosNumber { get; set; }

        private string AddressWidth { get; set; }
        private string _architecture;

        private string Architecture
        {
            get
            {
                return _architecture;
            }
            set
            {
                _architecture = ArchitectureDictionary[value];
            }
        }

        private string _availability;
        private string Availability
        {
            get
            {
                return _availability;
            }
            set
            {
                _availability = AvailabilityDictionary[value];
            }
        }

        private string Name { get; set; }
        private string CurrentClockSpeed { get; set; }
        private string CurrentVoltage { get; set; }
        private string DataWidth { get; set; }
        private string DeviceID { get; set; }
        private string ExternClockSpeed { get; set; }
        private string L2Cache { get; set; }
        private string L3Cache { get; set; }
        private string LoadPercent { get; set; }
        private string Manufacturer { get; set; }
        private string MaxClockSpeed { get; set; }
        private string Model { get; set; }
        private string CoresCount { get; set; }
        private string EnabledCoresCount { get; set; }
        private string ThreadsCount { get; set; }

        private string powermgmnt;
        private string IsPowerManagementSupported
        {
            get
            {
                return powermgmnt;
            }
            set
            {
                powermgmnt = SupportedStatus[value];
            }
        }

        private string CpuID { get; set; }

        private string cput;
        private string CPUType
        {
            get
            {
                return cput;
            }
            set
            {
                cput = CPUTypeDictionary[value];
            }
        }

        private string secondLvlAddress;
        private string IsSecondLvlAddressTranslationSupported
        {
            get
            {
                return secondLvlAddress;
            }
            set
            {
                secondLvlAddress = SupportedStatus[value];
            }
        }

        private string SerialNumber { get; set; }
        private string SocketChipType { get; set; }
        private string CpuStatus { get; set; }
        private string StatusInfo { get; set; }
        private string UpgradeMethod { get; set; }
        private string Version { get; set; }

        private string isVEnabled;
        private string IsVirtualizationEnabled
        {
            get
            {
                return isVEnabled;
            }
            set
            {
                isVEnabled = EnabledStaus[value];
            }
        }

        private string vmMMExt;
        private string VmMonitorModeExt
        {
            get
            {
                return vmMMExt;
            }
            set
            {
                vmMMExt = SupportedStatus[value];
            }

        }
    }
}