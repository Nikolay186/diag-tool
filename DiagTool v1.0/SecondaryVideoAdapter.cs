using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiagTool_v1._0
{
    class SecondaryVideoAdapter : VideoAdapter
    {
        public List<string[][]> ShowSubVideoData()
        {
            var data = ProcessQuery("Win32_VideoController WHERE DeviceID != \"VideoController1\"");
            List<SecondaryVideoAdapter> adapter = new List<SecondaryVideoAdapter>();

            for (int i = 1; i <= data.Count; i++)
            {
                adapter.Add(new SecondaryVideoAdapter { AdapterID = i.ToString()});
            }
            int pos = 0;
            try
            {
                foreach (ManagementObject obj in data)
                {
                    adapter[pos].AdapterCompatibility = obj["AdapterCompatibility"].ToString();
                    adapter[pos].AdapterDACType = obj["AdapterDACType"].ToString();
                    adapter[pos].AdapterRAMSize = obj["AdapterRAM"].ToString();
                    adapter[pos].Availability = obj["Availability"].ToString();
                    adapter[pos].ErrorCode = obj["ConfigManagerErrorCode"].ToString();
                    adapter[pos].UserConfig = obj["ConfigManagerUserConfig"].ToString();
                    adapter[pos].AdapterName = obj["Name"].ToString();
                    adapter[pos].DriverDate = obj["DriverDate"].ToString().Split('.')[0];
                    adapter[pos].DriverVersion = obj["DriverVersion"].ToString();
                    adapter[pos].InstalledDrivers = obj["InstalledDisplayDrivers"].ToString();
                    adapter[pos].IsMonochrome = obj["Monochrome"].ToString();
                    adapter[pos].PNPID = obj["PNPDeviceID"].ToString().Split('\\')[2];
                    adapter[pos].Status = obj["Status"].ToString();
                    adapter[pos].VideoArchitecture = obj["VideoArchitecture"].ToString();
                    adapter[pos].VideoMemoryType = obj["VideoMemoryType"].ToString();
                    adapter[pos].VideoProcessor = obj["VideoProcessor"].ToString();
                    pos++;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return GetSubVideoData(adapter);
        }

        private List<string[][]> GetSubVideoData(List<SecondaryVideoAdapter> lst)
        {
            var dataLst = new List<string[][]>();

            foreach (SecondaryVideoAdapter ad in lst)
            {
                string[][] dataGrid =
                {
                    new string[] {$"Adapter #{ad.AdapterID}", string.Empty},
                    new string[] {"Name: ", ad.AdapterName},
                    new string[] {"Processor model: ", ad.VideoProcessor},
                    new string[] {"Memory Size: ", ad.AdapterRAMSize},
                    new string[] {"DAC Type: ", ad.AdapterDACType},

                    new string[] {"Additional Info", string.Empty},
                    new string[] {"Status: ", ad.Status},
                    new string[] {"Architecture: ", ad.VideoArchitecture},
                    new string[] {"Memory Type: ", ad.VideoMemoryType},
                    new string[] {"PnP Device ID: ", ad.PNPID},
                    new string[] {"Availability: ", ad.Availability},
                    new string[] {"Error Code: ", ad.ErrorCode},
                    new string[] {"Compatibility: ", ad.AdapterCompatibility},
                    new string[] {"User Configuration: ", ad.UserConfig},
                    new string[] {"Driver Release Date: ", ad.DriverDate},
                    new string[] {"Driver Version: ", ad.DriverVersion},
                    new string[] {"Installed Drivers: ", ad.InstalledDrivers},
                    new string[] {"Monochrome: ", ad.IsMonochrome},
                };
                dataLst.Add(dataGrid);
            }
            return dataLst;
        }
    }
}