﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalWeightTracker_00176727.UC_Forms
{
    public partial class UC_Charts : UserControl
    {

        //DataBase Connection
        Models.DB_WeightTrackerEntities db = new Models.DB_WeightTrackerEntities();

        public UC_Charts()
        {
            InitializeComponent();
        }

        private void UC_Charts_Load(object sender, EventArgs e)
        {
            var data = db.Animals.ToList();
            comboBox1.DataSource = data;

            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Animal_Id";
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int aniId = Int32.Parse(comboBox1.SelectedValue.ToString());

            if (aniId.ToString() != null)
            {
                var dailyData = db.Daily_Measurement.Select(d => new { d.Daily_Log, d.Daily_Log.Date, d.Weight }).Where(d => d.Daily_Log.Course.Animal.Animal_Id == aniId).ToList();

                chart1.DataSource = dailyData;
                chart1.Series["Series1"].XValueMember = "Date";
                chart1.Series["Series1"].YValueMembers = "Weight";

                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                try
                {
                    var WSum = dailyData.Sum(d => d.Weight);
                    var Count = dailyData.Count();

                    var AVG = WSum / Count;

                    lblAVG.Text = AVG.ToString();

                    var InitialWeight = db.Animals.Where(d => d.Animal_Id == aniId).FirstOrDefault().Weight;

                    var LossGain = InitialWeight - AVG;

                    lblLossGain.Text = LossGain.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Data not found !!!");
                }
            }
        }
    }
}
