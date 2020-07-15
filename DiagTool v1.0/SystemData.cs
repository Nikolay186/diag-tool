using System;
using System.Management;
using System.Globalization;

namespace DiagTool_v1._0
{
    class SystemData : Data
    {
        private string ComputerName { get; set; }

        private string localDT;
        private string LocalDateTime
        {
            get
            {
                return localDT;
            }
            set
            {
                localDT = ParseDate(value);
            }
        }

        private string osVersion;
        private string OSVersion
        {
            get
            {
                return osVersion;
            }
            set
            {
                string[] data = value.Split('|');
                osVersion = data[0] + " " + data[3];
            }
        }

        private string osInstallDate;
        private string OSInstallDate
        {
            get
            {
                return osInstallDate;
            }
            set
            {
                osInstallDate = ParseDate(value);
            }
        }

        private string osLanguage;
        private string OSLanguage
        {
            get
            {
                return osLanguage;
            }
            set
            {
                int v = Convert.ToInt32(value);
                v.ToString("X");
                CultureInfo ci = new CultureInfo(v);
                osLanguage = ci.EnglishName;
            }
        }

        private string OSManufacturer { get; set; }
        private string OSSerialNumber { get; set; }

        public string[][] CollectSystemData()
        {
            var data = ProcessQuery("Win32_OperatingSystem");
            try
            {
                foreach (ManagementObject obj in data)
                {
                    ComputerName = obj["CSName"].ToString();
                    LocalDateTime = obj["LocalDateTime"].ToString().Split('.')[0];
                    OSVersion = obj["Name"].ToString() + "|" + obj["OSArchitecture"].ToString() + " Build: " + obj["BuildNumber"].ToString();
                    OSInstallDate = obj["InstallDate"].ToString().Split('.')[0];
                    OSLanguage = obj["OSLanguage"].ToString();
                    OSManufacturer = obj["Manufacturer"].ToString();
                    OSSerialNumber = obj["SerialNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return GetSystemData();
        }

        private string[][] GetSystemData()
        {
            string[][] dataGrid =
            {
                new string[]{ "Computer name: ", ComputerName},
                new string[]{ "Current local time: ", LocalDateTime},
                new string[]{ "OS version: ", OSVersion},
                new string[]{ "OS installation date: ", OSInstallDate},
                new string[]{ "System language: ", OSLanguage},
                new string[]{ "OS manufacturer: ", OSManufacturer},
                new string[]{ "OS S\\N: ", OSSerialNumber},
            };
            return dataGrid;
        }
    }
}