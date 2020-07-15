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
            string[][] dataGrid = systemData.CollectSystemData();
            Data.ShowData(systemDataGridView, dataGrid);
        }

        private void ShowMBData()
        {
            Motherboard mb = new Motherboard();
            string[][] dataGrid = mb.CollectMBData();
            Data.ShowData(mbDataGridView, dataGrid);
            ShowRAMData();
        }

        private void ShowRAMData()
        {
            RAMStick RAMData = new RAMStick();
            List<string[][]> dataGridList = RAMData.CollectRAMData();
            mbDataGridView.Rows.Add(new string[] { string.Empty, string.Empty });
            mbDataGridView.Rows.Add(new string[] { "RAM: ", string.Empty });
            Data.ShowData(mbDataGridView, dataGridList);
            mbDataGridView.Rows.RemoveAt(mbDataGridView.Rows.Count - 1);
        }

        private void ShowCPUData()
        {
            CPU CPUData = new CPU();
            List<string[][]> dataGridList = CPUData.CollectCPUData();
            Data.ShowData(cpuDataGridView, dataGridList);
        }

        private void CheckDataGrid(List<DataGridView> dataGridList)
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
    }
}