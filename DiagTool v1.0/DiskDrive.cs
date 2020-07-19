using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiagTool_v1._0
{
    class DiskDrive : Data
    {
        private string BytesPerSector { get; set; }
        private string[] CapabilityDescription { get; set; }
        private string ErrorCode { get; set; }
        private string UserConfid { get; set; }
        private string DiskID { get; set; }
        private string FirmwareRev { get; set; }
        private string Index { get; set; }
        private string InterfaceType { get; set; }
        private string IsMediaLoaded { get; set; }
        private string MediaType { get; set; }
        private string Model { get; set; }
        private string PartitionsCount { get; set; }
        private string PNPID { get; set; }
        private string SCSIBus { get; set; }
        private string SCSILogicalUnit { get; set; }
        private string SCSIPort { get; set; }
        private string SCSITargetId { get; set; }
        private string SectorsPerTrack { get; set; }
        private string SerialNumber { get; set; }
        private string Signature { get; set; }
        private string Size { get; set; }
        private string Status { get; set; }
        private string TotalCylinders { get; set; }
        private string TotalHeads { get; set; }
        private string TotalSectors { get; set; }
        private string TotalTracks { get; set; }
        private string TracksPerCylinder { get; set; }

        public List<string[][]> CollectStorageData()
        {
            var data = ProcessQuery("Win32_DiskDrive");

            var dd = new List<DiskDrive>();
            for (int i = 0; i < data.Count; i++)
            {
                dd.Add(new DiskDrive { Index = i.ToString() });
            }

            int pos = 0;
            try
            {
                foreach (ManagementObject obj in data)
                {
                    dd[pos].BytesPerSector = obj["BytesPerSector"].ToString();
                    dd[pos].CapabilityDescription = (string[])obj["CapabilityDescriptions"];
                    dd[pos].DiskID = obj["DeviceID"].ToString().Split('.')[1];
                    dd[pos].ErrorCode = obj["ConfigManagerErrorCode"].ToString();
                    dd[pos].FirmwareRev = obj["FirmwareRevision"].ToString();
                    dd[pos].InterfaceType = obj["InterfaceType"].ToString();
                    dd[pos].IsMediaLoaded = obj["MediaLoaded"].ToString();
                    dd[pos].MediaType = obj["MediaType"].ToString();
                    dd[pos].Model = obj["Model"].ToString();
                    dd[pos].PartitionsCount = obj["Partitions"].ToString();
                    dd[pos].PNPID = obj["PNPDeviceID"].ToString();
                    dd[pos].SCSIBus = obj["SCSIBus"].ToString();
                    dd[pos].SCSILogicalUnit = obj["SCSILogicalUnit"].ToString();
                    dd[pos].SCSIPort = obj["SCSIPort"].ToString();
                    dd[pos].SCSITargetId = obj["SCSITargetId"].ToString();
                    dd[pos].SectorsPerTrack = obj["SectorsPerTrack"].ToString();
                    dd[pos].SerialNumber = obj["SerialNumber"].ToString().Trim();
                    dd[pos].Size = obj["Size"].ToString();
                    dd[pos].Status = obj["Status"].ToString();
                    dd[pos].TotalCylinders = obj["TotalCylinders"].ToString();
                    dd[pos].TotalHeads = obj["TotalHeads"].ToString();
                    dd[pos].TotalSectors = obj["TotalSectors"].ToString();
                    dd[pos].TotalTracks = obj["TotalTracks"].ToString();
                    dd[pos].TracksPerCylinder = obj["TracksPerCylinder"].ToString();
                    pos++;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return GetDiskData(dd);
        }

        private List<string[][]> GetDiskData(List<DiskDrive> lst)
        {
            var dataList = new List<string[][]>();

            try
            {
                foreach (DiskDrive dd in lst)
                {
                    string[][] dataGrid =
                    {
                        new string[] {$"Disk #{dd.Index}", string.Empty},
                        new string[] {"Model: ", dd.Model},
                        new string[] {"Size (GB): ", dd.Size},
                        new string[] {"S\\N: ", dd.SerialNumber},
                        new string[] {"Firmware Version: ", dd.FirmwareRev},
                        new string[] {"Partitions: ", dd.PartitionsCount},
                        new string[] {"PNPID: ", dd.PNPID},
                        new string[] {"Status: ", dd.Status},
                        new string[] {"Interface: ", dd.InterfaceType},
                        new string[] {"SCSI Bus: ", dd.SCSIBus},
                        new string[] {"SCSI Logical Unit: ", dd.SCSILogicalUnit},
                        new string[] {"SCSI Port: ", dd.SCSIPort},
                        new string[] {"SCSI Target ID: ", dd.SCSITargetId},
                        new string[] {"Error Code: ", dd.ErrorCode},
                        new string[] {"Capabilities: ", dd.CapabilityDescription[0] + ", " + dd.CapabilityDescription[1] },
                        new string[] {"Media Loaded: ", dd.IsMediaLoaded},
                        new string[] {"Media Type: ", dd.MediaType},
                        new string[] {"Sectors Per Track: ", dd.SectorsPerTrack},
                        new string[] {"Total Cylinders: ", dd.TotalCylinders},
                        new string[] {"Total Heads: ", dd.TotalHeads},
                        new string[] {"Total Sectors: ", dd.TotalSectors},
                        new string[] {"Total Tracks: ", dd.TotalTracks},
                        new string[] {"Tracks Per Cylinder: ", dd.TracksPerCylinder},
                        new string[] {string.Empty, string.Empty},
                    };
                    dataList.Add(dataGrid);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return dataList;
        }
    }
}