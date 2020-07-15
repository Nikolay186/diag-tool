using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace DiagTool_v1._0
{
    class Data
    {
        private readonly string Namespace = @"root/cimv2";
        private readonly string Query = "SELECT * FROM ";
        private ManagementObjectSearcher getData;
        private ManagementObjectCollection data;

        public static void HandleException(Exception ex)
        {
            StringBuilder error = new StringBuilder();
            error.AppendLine(ex.Message);
            error.AppendLine(ex.StackTrace);
            if (ex.InnerException != null)
                error.AppendLine(ex.InnerException.ToString());
            MessageBox.Show(error.ToString());
        }

        protected string ParseDate(string date)
        {
            try
            {
                var parsedDate = DateTime.ParseExact(date, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                string result = parsedDate.Day + "/" + parsedDate.Month + "/" + parsedDate.Year + " " + parsedDate.TimeOfDay;
                return result;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return string.Empty;
            }
        }

        public static void ShowData(DataGridView dataGridView, string[][] dataSet)
        {
            try
            {
                foreach (string[] data in dataSet)
                {
                    dataGridView.Rows.Add(data[0], data[1]);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public static void ShowData(DataGridView dataGridView, List<string[][]> dataList)
        {
            try
            {
                foreach (string[][] dataGrid in dataList)
                {
                    foreach (string[] data in dataGrid)
                    {
                        dataGridView.Rows.Add(data[0], data[1]);
                    }
                }
                dataGridView.Rows.RemoveAt(dataGridView.Rows.Count - 1);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected ManagementObjectCollection ProcessQuery(string scope)
        {
            try
            {
                getData = new ManagementObjectSearcher(Namespace, Query + scope);
                data = getData.Get();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return data;
        }
    }
}