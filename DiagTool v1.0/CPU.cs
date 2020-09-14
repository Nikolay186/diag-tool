using System;
using System.Collections.Generic;
using System.Management;

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

        private int CpuPosNumber { get; set; }

        private string AddressWidth { get; set; }
        private string _architecture;

        private string Architecture
        {
            get => _architecture;
            
            set
            {
                _architecture = ArchitectureDictionary[value];
            }
        }

        private string _availability;
        private string Availability
        {
            get => _availability;
            
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
            get => powermgmnt;
            
            set
            {
                powermgmnt = SupportedStatus[value];
            }
        }

        private string CpuID { get; set; }

        private string cput;
        private string CPUType
        {
            get => cput;
            
            set
            {
                cput = CPUTypeDictionary[value];
            }
        }

        private string secondLvlAddress;
        private string IsSecondLvlAddressTranslationSupported
        {
            get => secondLvlAddress;
            
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

        private string version;
        private string Version
        {
            get => version;

            set
            {
                if (string.IsNullOrEmpty(value))
                    version = "Undefined";
            }
        }

        private string isVEnabled;
        private string IsVirtualizationEnabled
        {
            get => isVEnabled;
            
            set
            {
                isVEnabled = EnabledStaus[value];
            }
        }

        private string vmMMExt;
        private string VmMonitorModeExt
        {
            get => vmMMExt;
            
            set
            {
                vmMMExt = SupportedStatus[value];
            }

        }

        public List<string[][]> CollectCPUData()
        {
            var data = ProcessQuery("Win32_Processor");

            var processors = new List<CPU>();
            for (int i = 1; i <= data.Count; i++)
                processors.Add(new CPU { CpuPosNumber = i });

            int pos = 0;
            try
            {
                foreach (ManagementObject obj in data)
                {
                    processors[pos].AddressWidth = obj["AddressWidth"].ToString();
                    processors[pos].Architecture = obj["Architecture"].ToString();
                    processors[pos].Availability = obj["Availability"].ToString();
                    processors[pos].CoresCount = obj["NumberOfCores"].ToString();
                    processors[pos].CpuID = obj["ProcessorId"].ToString();
                    processors[pos].CpuStatus = obj["CpuStatus"].ToString();
                    processors[pos].CPUType = obj["ProcessorType"].ToString();
                    processors[pos].CurrentClockSpeed = obj["CurrentClockSpeed"].ToString();
                    processors[pos].CurrentVoltage = obj["CurrentVoltage"].ToString();
                    processors[pos].DataWidth = obj["DataWidth"].ToString();
                    processors[pos].DeviceID = obj["DeviceID"].ToString();
                    processors[pos].EnabledCoresCount = obj["NumberOfEnabledCore"].ToString();
                    processors[pos].ExternClockSpeed = obj["ExtClock"].ToString();
                    processors[pos].IsPowerManagementSupported = obj["PowerManagementSupported"].ToString();
                    processors[pos].IsSecondLvlAddressTranslationSupported = obj["SecondLevelAddressTranslationExtensions"].ToString();
                    processors[pos].IsVirtualizationEnabled = obj["VirtualizationFirmwareEnabled"].ToString();
                    processors[pos].L2Cache = obj["L2CacheSize"].ToString();
                    processors[pos].L3Cache = obj["L3CacheSize"].ToString();
                    processors[pos].LoadPercent = obj["LoadPercentage"].ToString();
                    processors[pos].Manufacturer = obj["Manufacturer"].ToString();
                    processors[pos].MaxClockSpeed = obj["MaxClockSpeed"].ToString();
                    processors[pos].Model = obj["Description"].ToString();
                    processors[pos].Name = obj["Name"].ToString();
                    processors[pos].SerialNumber = obj["SerialNumber"].ToString();
                    processors[pos].SocketChipType = obj["SocketDesignation"].ToString();
                    processors[pos].StatusInfo = obj["StatusInfo"].ToString();
                    processors[pos].ThreadsCount = obj["ThreadCount"].ToString();
                    processors[pos].UpgradeMethod = obj["UpgradeMethod"].ToString();
                    processors[pos].Version = obj["Version"].ToString();
                    processors[pos].VmMonitorModeExt = obj["VMMonitorModeExtensions"].ToString();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return GetCPUData(processors);
        }

        private List<string[][]> GetCPUData(List<CPU> list)
        {
            var dataList = new List<string[][]>();

            foreach (CPU cpu in list)
            {
                string[][] dataGrid =
                {
                    new string[] {"CPU: ", string.Empty},
                    new string[] {$"CPU #{cpu.CpuPosNumber}", string.Empty},
                    new string[] {"Manufacturer: ", cpu.Manufacturer},
                    new string[] {"Name: ", cpu.Name},
                    new string[] {"Cores: ", cpu.CoresCount},
                    new string[] {"Enabled cores: ", cpu.EnabledCoresCount},
                    new string[] {"Threads: ", cpu.ThreadsCount},
                    new string[] {"Max core speed (MHz): ", cpu.MaxClockSpeed},
                    new string[] {"Current core speed (MHz): ", cpu.CurrentClockSpeed},
                    new string[] {"Current voltage (V): ", cpu.CurrentVoltage},
                    new string[] {"Architecture: ", cpu.Architecture},
                    new string[] {"L2 cache (KB): ", cpu.L2Cache},
                    new string[] {"L3 cache (KB): ", cpu.L3Cache},
                    new string[] {"Load percentage: ", cpu.LoadPercent + "%"},
                    new string[] {"Virtualization: ", cpu.IsVirtualizationEnabled},
                    new string[] {"VM Monitor extensions", cpu.VmMonitorModeExt},
                    new string[] {"Codename: ", cpu.Model},
                    new string[] {"Data width: ", cpu.DataWidth},
                    new string[] {"Availability: ", cpu.Availability},
                    new string[] {"Part number: ", cpu.CpuID},
                    new string[] {"Status: ", cpu.CpuStatus},
                    new string[] {"Type: ", cpu.CPUType},
                    new string[] {"ID: ", cpu.DeviceID},
                    new string[] {"Bus speed (MHz): ", cpu.ExternClockSpeed},
                    new string[] {"Power management: ", cpu.IsPowerManagementSupported},
                    new string[] {"Second level address translation: ", cpu.IsSecondLvlAddressTranslationSupported},
                    new string[] {"S\\N", cpu.SerialNumber},
                    new string[] {"Socket chip type: ", cpu.SocketChipType},
                    new string[] {"Status: ", cpu.StatusInfo},
                    new string[] {"Upgrade: ", cpu.UpgradeMethod},
                    new string[] {"Version: ", cpu.Version},
                };
                dataList.Add(dataGrid);
            }
            return dataList;
        }
    }
}