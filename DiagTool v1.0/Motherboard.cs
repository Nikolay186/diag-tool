using System;
using System.Management;

namespace DiagTool_v1._0
{
    class Motherboard : Data
    {
        private string MBModel { get; set; }
        private string MBManufacturer { get; set; }
        private string BIOSVersion { get; set; }
        private string BIOSManufacturer { get; set; }
        private string MachineSN { get; set; }
        private string BIOSReleaseDate { get; set; }

        private string BIOSlang;
        private string BIOSLanguage
        {
            get => BIOSlang;

            set
            {
                string[] lang = value.Split('|');
                BIOSlang = lang[0] + "-" + lang[1];
            }
        }

        public string[][] CollectMBData()
        {
            var data = ProcessQuery("Win32_BIOS");
            try
            {
                foreach (ManagementObject obj in data)
                {
                    BIOSVersion = obj["Name"].ToString();
                    BIOSManufacturer = obj["Manufacturer"].ToString();
                    MachineSN = obj["SerialNumber"].ToString();
                    BIOSReleaseDate = ParseDate(obj["ReleaseDate"].ToString().Split('.')[0]);
                    BIOSLanguage = obj["CurrentLanguage"].ToString();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            data = ProcessQuery("Win32_BaseBoard");
            try
            {
                foreach (ManagementObject obj in data)
                {
                    MBManufacturer = obj["Manufacturer"].ToString();
                    MBModel = obj["Product"].ToString();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return GetMBData();
        }

        private string[][] GetMBData()
        {
            string[][] mbDataGrid =
            {
                new string[] {"Motherboard: ", string.Empty},
                new string[] {"Manufacturer: ", MBManufacturer},
                new string[] {"Model: ", MBModel},
                new string[] {"Machine S\\N: ", MachineSN},
                new string[] {string.Empty, string.Empty},
                new string[] {"BIOS: ", string.Empty},
                new string[] {"Version: ", BIOSVersion},
                new string[] {"Manufacturer: ", BIOSManufacturer},
                new string[] {"Release date: ", BIOSReleaseDate},
                new string[] {"Language: ", BIOSLanguage},
            };
            return mbDataGrid;
        }
    }
}
