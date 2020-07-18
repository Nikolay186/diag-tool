using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiagTool_v1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowSystemData();
            ShowMBData();
            ShowCPUData();
            ShowVideoData();
            List<DataGridView> gridViews = new List<DataGridView>()
            {
                systemDataGridView,
                mbDataGridView,
                cpuDataGridView,
                videoDataGridView,
                storageDataGridView,
            };
            CheckDataGrid(gridViews);
        }

        private void ShowSystemData()
        {
            SystemData systemData = new SystemData();
            var dataGrid = systemData.CollectSystemData();
            Data.ShowData(systemDataGridView, dataGrid);
        }

        private void ShowMBData()
        {
            Motherboard mb = new Motherboard();
            var dataGrid = mb.CollectMBData();
            Data.ShowData(mbDataGridView, dataGrid);
            ShowRAMData();
        }

        private void ShowRAMData()
        {
            RAMStick RAMData = new RAMStick();
            var dataGridList = RAMData.CollectRAMData();
            mbDataGridView.Rows.Add(new string[] { string.Empty, string.Empty });
            mbDataGridView.Rows.Add(new string[] { "RAM: ", string.Empty });
            Data.ShowData(mbDataGridView, dataGridList);
            mbDataGridView.Rows.RemoveAt(mbDataGridView.Rows.Count - 1);
        }

        private void ShowCPUData()
        {
            CPU CPUData = new CPU();
            var dataGridList = CPUData.CollectCPUData();
            Data.ShowData(cpuDataGridView, dataGridList);
        }

        private void ShowVideoData()
        {
            MainVideoAdapter adapter = new MainVideoAdapter();
            var dataGridList = adapter.CollectMainVideoData();
            SecondaryVideoAdapter ad = new SecondaryVideoAdapter();
            var secondList = ad.ShowSubVideoData();
            Data.ShowData(videoDataGridView, dataGridList);
            videoDataGridView.Rows.Add(string.Empty, string.Empty);
            Data.ShowData(videoDataGridView, secondList);
        }

        private void CheckDataGrid(List<DataGridView> dataGridList)
        {
            try
            {
                foreach (DataGridView dataGrid in dataGridList)
                    foreach (DataGridViewRow row in dataGrid.Rows)
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value.ToString() == string.Empty)
                            {
                                cell.Style.BackColor = Color.LightGray;
                                cell.Style.SelectionBackColor = Color.Transparent;
                            }
                            else
                            {
                                cell.Style.SelectionBackColor = Color.AntiqueWhite;
                                cell.Style.SelectionForeColor = Color.Black;
                            }
                        }
            }
            catch (Exception ex)
            {
                Data.HandleException(ex);
            }
        }
    }
}