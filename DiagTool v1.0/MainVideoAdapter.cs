using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiagTool_v1._0
{
    class MainVideoAdapter : VideoAdapter
    {
        private string BitsPerPixel { get; set; }
        private string HorizontalRes { get; set; }
        private string RefreshRate { get; set; }
        private string ScanMode { get; set; }
        private string VerticalRes { get; set; }
        private string DitherType { get; set; }
        private string MaxRefreshRate { get; set; }
        private string MinRefreshRate { get; set; }

        public string[][] CollectMainVideoData()
        {
            var data = ProcessQuery("Win32_VideoController WHERE DeviceID = \"VideoController1\"");
            var adapter = new MainVideoAdapter();

            try
            {
                foreach (ManagementObject obj in data)
                {
                    adapter.AdapterCompatibility = obj["AdapterCompatibility"].ToString();
                    adapter.AdapterDACType = obj["AdapterDACType"].ToString();
                    adapter.AdapterRAMSize = obj["AdapterRAM"].ToString();
                    adapter.Availability = obj["Availability"].ToString();
                    adapter.ErrorCode = obj["ConfigManagerErrorCode"].ToString();
                    adapter.DitherType = obj["DitherType"].ToString();
                    adapter.UserConfig = obj["ConfigManagerUserConfig"].ToString();
                    adapter.BitsPerPixel = obj["CurrentBitsPerPixel"].ToString();
                    adapter.HorizontalRes = obj["CurrentHorizontalResolution"].ToString();
                    adapter.VerticalRes = obj["CurrentVerticalResolution"].ToString();
                    adapter.RefreshRate = obj["CurrentRefreshRate"].ToString();
                    adapter.ScanMode = obj["CurrentScanMode"].ToString();
                    adapter.AdapterName = obj["Name"].ToString();
                    adapter.DriverDate = obj["DriverDate"].ToString().Split('.')[0];
                    adapter.DriverVersion = obj["DriverVersion"].ToString();
                    adapter.InstalledDrivers = obj["InstalledDisplayDrivers"].ToString();
                    adapter.MaxRefreshRate = obj["MaxRefreshRate"].ToString();
                    adapter.MinRefreshRate = obj["MinRefreshRate"].ToString();
                    adapter.IsMonochrome = obj["Monochrome"].ToString();
                    adapter.PNPID = obj["PNPDeviceID"].ToString().Split('\\')[2];
                    adapter.Status = obj["Status"].ToString();
                    adapter.VideoArchitecture = obj["VideoArchitecture"].ToString();
                    adapter.VideoMemoryType = obj["VideoMemoryType"].ToString();
                    adapter.VideoProcessor = obj["VideoProcessor"].ToString();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return GetVideoData(adapter);
        }

        private string[][] GetVideoData(MainVideoAdapter ad)
        {
            string[][] dataGrid =
            {
                new string[] {$"Main Adapter", string.Empty},
                new string[] {"Name: ", ad.AdapterName},
                new string[] {"Processor model: ", ad.VideoProcessor},
                new string[] {"Current Display Settings: ", ad.HorizontalRes + " x " + ad.VerticalRes + " " + ad.BitsPerPixel + " Bit"},
                new string[] {"Memory Size: ", ad.AdapterRAMSize},
                new string[] {"DAC Type: ", ad.AdapterDACType},
                new string[] {"Horizontal Resolution: ", ad.HorizontalRes},
                new string[] {"Vertical Resolution: ", ad.VerticalRes},
                new string[] {"Refresh Rate: ", ad.RefreshRate},
                new string[] {"Max. Refresh Rate: ", ad.MaxRefreshRate},
                new string[] {"Min. Refresh Rate: ", ad.MinRefreshRate},

                new string[] {"Additional Info", string.Empty},
                new string[] {"Status: ", ad.Status},
                new string[] {"Scan Mode: ", ad.ScanMode},
                new string[] {"Architecture: ", ad.VideoArchitecture},
                new string[] {"Memory Type: ", ad.VideoMemoryType},
                new string[] {"PnP Device ID: ", ad.PNPID},
                new string[] {"Availability: ", ad.Availability},
                new string[] {"Error Code: ", ad.ErrorCode},
                new string[] {"Compatibility: ", ad.AdapterCompatibility},
                new string[] {"User Configuration: ", ad.UserConfig},
                new string[] {"Driver Release Date: ", ad.DriverDate},
                new string[] {"Driver Version: ", ad.DriverVersion},
                new string[] {"Dither Type: ", ad.DitherType},
                new string[] {"Installed Drivers: ", ad.InstalledDrivers},
                new string[] {"Monochrome: ", ad.IsMonochrome},
            };
            return dataGrid;
        }
    }
}