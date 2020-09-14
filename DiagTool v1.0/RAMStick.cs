using System;
using System.Collections.Generic;
using System.Management;

namespace DiagTool_v1._0
{
    class RAMStick : Data
    {
        private readonly Dictionary<string, string> RAMType = new Dictionary<string, string>()
        {
            ["0"] = "Unknown",
            ["1"] = "Other",
            ["2"] = "DRAM",
            ["9"] = "RAM",
            ["10"] = "ROM",
            ["20"] = "DDR",
            ["21"] = "DDR2",
            ["22"] = "DDR2 FB-DIMM",
            ["24"] = "DDR3",
            ["26"] = "DDR4",
            ["27"] = "LPDDR",
            ["28"] = "LPDDR2",
            ["29"] = "LPDDR3",
            ["30"] = "LPDDR4",
        };
        private readonly Dictionary<string, string> RAMFormFactor = new Dictionary<string, string>()
        {
            ["0"] = "Unknown",
            ["1"] = "Other",
            ["7"] = "SIMM",
            ["8"] = "DIMM",
            ["12"] = "SO-DIMM",
            ["14"] = "SMD",
        };

        private string bType;
        private string Type
        {
            get => bType;
            

            set
            {
                bType = RAMType[value.ToString()];
            }
        }
        private int Position { get; set; }
        private string bCap;
        private string Capacity
        {
            get => bCap;
            
            set
            {
                long tmp = Convert.ToInt64(value);
                bCap = Convert.ToString(tmp / 1073741824);
            }
        }
        private string ClockSpeed { get; set; }
        private string CurrentVoltage { get; set; }
        private string MinVoltage { get; set; }
        private string MaxVoltage { get; set; }
        private string Channel { get; set; }
        private string DataWidth { get; set; }
        private string bFF;
        private string FormFactor
        {
            get => bFF;
            
            set
            {
                bFF = RAMFormFactor[value];
            }
        }
        private string Manufacturer { get; set; }
        private string SN { get; set; }

        public List<string[][]> CollectRAMData()
        {
            var data = ProcessQuery("Win32_PhysicalMemory");

            var sticks = new List<RAMStick>();
            for (int stickNumber = 1; stickNumber <= data.Count; stickNumber++)
            {
                sticks.Add(new RAMStick { Position = stickNumber });
            }

            int pos = 0;
            try
            {
                foreach (ManagementObject obj in data)
                {
                    sticks[pos].Manufacturer = obj["Manufacturer"].ToString();
                    sticks[pos].Capacity = obj["Capacity"].ToString();
                    sticks[pos].Channel = obj["DeviceLocator"].ToString();
                    sticks[pos].DataWidth = obj["DataWidth"].ToString();
                    sticks[pos].FormFactor = obj["FormFactor"].ToString();
                    sticks[pos].SN = obj["PartNumber"].ToString();
                    sticks[pos].Type = obj["SMBIOSMemoryType"].ToString();
                    sticks[pos].ClockSpeed = obj["ConfiguredClockSpeed"].ToString();
                    sticks[pos].CurrentVoltage = obj["ConfiguredVoltage"].ToString();
                    sticks[pos].MinVoltage = obj["MinVoltage"].ToString();
                    sticks[pos].MaxVoltage = obj["MaxVoltage"].ToString();
                    pos++;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return GetRAMData(sticks);
        }

        private List<string[][]> GetRAMData(List<RAMStick> list)
        {
            var dataList = new List<string[][]>();
            foreach (RAMStick stick in list)
            {
                string[][] dataGrid =
                {                   
                    new string[] {$"Stick #{stick.Position}", string.Empty},
                    new string[] {"Manufacturer: ", stick.Manufacturer},
                    new string[] {"Capacity (GB):", stick.Capacity},
                    new string[] {"Channel: ", stick.Channel},
                    new string[] {"DataWidth (Bit): ", stick.DataWidth},
                    new string[] {"Form Factor: ", stick.FormFactor},
                    new string[] {"Model Number: ", stick.SN},
                    new string[] {"Type: ", stick.Type},
                    new string[] {"Frequency (MHz): ", stick.ClockSpeed},
                    new string[] {"Current Voltage (V): ", stick.CurrentVoltage},
                    new string[] {"Min. Voltage (V): ", stick.MinVoltage},
                    new string[] {"Max. Voltage (V): ", stick.MaxVoltage},
                    new string[] {string.Empty, string.Empty},
                };
                dataList.Add(dataGrid);
            }
            return dataList;
        }
    }
}